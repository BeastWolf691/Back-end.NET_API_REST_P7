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
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;
        private readonly ILogger<RatingController> _logger;
        private readonly IMapper _mapper;

        public RatingController(RatingService ratingService, ILogger<RatingController> logger, IMapper mapper)
        {
            _ratingService = ratingService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRatings()
        {
            try
            {
                var ratings = await _ratingService.GetRatings();
                var ratingDto = _mapper.Map<IEnumerable<RatingDto>>(ratings);
                return Ok(ratingDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ratings");
                return StatusCode(500, new { message = "Error while fetching ratings." });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRatingById(int id)
        {
            try
            {
                var rating = await _ratingService.GetRating(id);
                if (rating == null)
                {
                    return NotFound(new { message = "Rating not found" });
                }
                var ratingDto = _mapper.Map<RatingDto>(rating);
                return Ok(ratingDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching rating by ID");
                return StatusCode(500, new { message = "Error while fetching rating." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRating([FromBody] RatingDto ratingDto)
        {
            try
            {
                var ratingEntity = _mapper.Map<Rating>(ratingDto);
                var createdRating = await _ratingService.AddRating(ratingEntity);
                var createRatingDto = _mapper.Map<RatingDto>(createdRating);
                
                return CreatedAtAction(nameof(GetRatingById), new { id = createRatingDto.Id }, createRatingDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while adding rating");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding rating");
                return StatusCode(500, new { message = "Error while adding rating." });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] RatingDto ratingDto)
        {
            try
            {
                var ratingEntity = _mapper.Map<Rating>(ratingDto);
                var updatedRating = await _ratingService.UpdateRating(id, ratingEntity);
                if (updatedRating == null)
                    return NotFound(new { message = "Rating not found for update." });

                var updatedRatingDto = _mapper.Map<RatingDto>(updatedRating);
                return Ok(updatedRatingDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while updating rating");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating rating");
                return StatusCode(500, new { message = "Error while updating rating." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            try
            {
                var deleted = await _ratingService.DeleteRating(id);
                if(!deleted)
                {
                    return NotFound(new { message = "Rating not found for deletion." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting rating");
                return StatusCode(500, new { message = "Error while deleting rating." });
            }
        }
    }
}