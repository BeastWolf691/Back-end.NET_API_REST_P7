using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;
using AutoMapper;
using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurveController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;
        private readonly ILogger<CurveController> _logger;
        private readonly IMapper _mapper;

        public CurveController(ICurvePointService curvePointService, ILogger<CurveController> logger, IMapper mapper)
        {
            _curvePointService = curvePointService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCurves()
        {
            try
            {
                var curves = await _curvePointService.GetCurves();
                var curvePointDto = _mapper.Map<IEnumerable<CurvePointDto>>(curves);
                return Ok(curvePointDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching curves");
                return StatusCode(500, new { message = "Error while fetching curves." });
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
                    return NotFound(new { message = "Curve not found." });

                var curvePointDto = _mapper.Map<CurvePointDto>(curve);
                return Ok(curvePointDto);
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
            try
            {
                var curveEntity = _mapper.Map<CurvePoint>(curvePointDto);
                var createdCurve = await _curvePointService.AddCurve(curveEntity);
                var createdCurveDto = _mapper.Map<CurvePointDto>(createdCurve);

                return CreatedAtAction(nameof(GetCurveById), new { id = createdCurveDto.Id }, createdCurveDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while adding curve");
                return BadRequest(new { message = ex.Message });
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
                var curveEntity = _mapper.Map<CurvePoint>(curvePointDto);
                var updatedCurve = await _curvePointService.UpdateCurve(id, curveEntity);
                if (updatedCurve == null)
                    return NotFound(new { message = "Curve not found for update." });

                var updatedCurveDto = _mapper.Map<CurvePointDto>(updatedCurve);
                return Ok(updatedCurveDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while updating curve");
                return BadRequest(new { message = ex.Message });
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