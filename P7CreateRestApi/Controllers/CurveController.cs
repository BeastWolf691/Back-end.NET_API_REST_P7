using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurveController : ControllerBase
    {
        private readonly CurvePointService _curvePointService;
        private readonly ILogger<CurveController> _logger;

        public CurveController(CurvePointService curvePointService, ILogger<CurveController> logger)
        {
            _curvePointService = curvePointService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCurves()
        {
            try
            {
                var curves = await _curvePointService.GetCurves();
                return Ok(curves);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching bids");
                return StatusCode(500, new { message = "Error while fetching bids." });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCurveById(int id)
        {
            try
            {
                var curve = await _curvePointService.GetCurve(id);
                if (curve == null)
                {
                    return NotFound(new { message = "Curve not found." });
                }
                return Ok(curve);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching curve by ID");
                return StatusCode(500, new { message = "Error while fetching curve." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCurvePoint([FromBody]CurvePointDto curvePointDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdCurve = await _curvePointService.AddCurve(curvePointDto);
                var resultDto = new CurvePointDto
                {
                    Id = createdCurve.Id,
                    CurveId = createdCurve.CurveId,
                    Term = createdCurve.Term,
                    CurvePointValue = createdCurve.CurvePointValue,
                    AsOfDate = createdCurve.AsOfDate,
                    CreationDate = createdCurve.CreationDate
                };
                return CreatedAtAction(nameof(GetCurveById), new { id = resultDto.Id }, resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding curve");
                return StatusCode(500, new { message = "Error while adding curve." });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePointDto curvePointDto)
        {
            try
            {
                var updatedCurve = await _curvePointService.UpdateCurve(id, curvePointDto);
                if (updatedCurve == null)
                {
                    return NotFound(new { message = "Curve not found for update." });
                }
                return Ok(updatedCurve);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating curve");
                return StatusCode(500, new { message = "Error while updating curve." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCurve(int id)
        {
            try
            {
                var deleted = await _curvePointService.DeleteCurve(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Curve not found for deletion." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting curve");
                return StatusCode(500, new { message = "Error while deleting curve." });
            }
        }
    }
}