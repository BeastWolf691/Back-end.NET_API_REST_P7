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
        private readonly ILogger<RatingController> _logger;

        public RatingController(RatingService ratingService, ILogger<RatingController> logger)
        {
            _ratingService = ratingService;
            _logger = logger;
        }

        private void ValidateRatingDto(RatingDto ratingDto)
        {
            if (string.IsNullOrWhiteSpace(ratingDto.MoodysRating) || ratingDto.MoodysRating.Length > 50)
            {
                ModelState.AddModelError(nameof(ratingDto.MoodysRating), "La note de solvabilité Moody's est obligatoire et ne doit pas excéder 50 caractères.");
            }
            if (string.IsNullOrWhiteSpace(ratingDto.SandPRating) || ratingDto.SandPRating.Length > 50)
            {
                ModelState.AddModelError(nameof(ratingDto.SandPRating), "La note de solvabilité S&P est obligatoire et ne doit pas excéder 50 caractères.");
            }
            if (string.IsNullOrWhiteSpace(ratingDto.FitchRating) || ratingDto.FitchRating.Length > 50)
            {
                ModelState.AddModelError(nameof(ratingDto.FitchRating), "La note de solvabilité Fitch est obligatoire et ne doit pas excéder 50 caractères.");
            }
            if (ratingDto.OrderNumber.HasValue && ratingDto.OrderNumber < 0)
            {
                ModelState.AddModelError(nameof(ratingDto.OrderNumber), "Le numéro de commande ne peut pas être négatif.");
            }
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
            ValidateRatingDto(ratingDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdRating = await _ratingService.AddRating(ratingDto);
                return CreatedAtAction(nameof(GetRatingById), new { id = createdRating.Id }, createdRating);
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
            ValidateRatingDto(ratingDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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