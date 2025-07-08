using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;
        private readonly ILogger _logger;

        public RatingController(RatingService ratingService, ILogger logger)
        {
            _ratingService = ratingService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRatings()
        {
            try
            {
                var ratings = await _ratingService.GetRatings();
                return Ok(ratings);
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
                return Ok(rating);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdRating = await _ratingService.AddRating(ratingDto);
                var resultDto = new RatingDto
                {
                    Id = createdRating.Id,
                    MoodysRating = createdRating.MoodysRating,
                    SandPRating = createdRating.SandPRating,
                    FitchRating = createdRating.FitchRating,
                    OrderNumber = createdRating.OrderNumber,
                };
                return CreatedAtAction(nameof(GetRatingById), new { id = resultDto.Id }, resultDto);
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
                var updateRating = await _ratingService.UpdateRating(id, ratingDto);
                if (updateRating == null)
                {
                    return NotFound(new { message = "Rating not found for update." });
                }
                return Ok(updateRating);
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