using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {

        private readonly IBidService _bidService;
        private readonly ILogger<BidListController> _logger;

        public BidListController(IBidService bidService, ILogger<BidListController> logger)
        {
            _bidService = bidService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBids()
        {
            try
            {
                var bids = await _bidService.GetBidLists();
                return Ok(bids);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching bids");
                return StatusCode(500, new { message = "Error while fetching bids." });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBidById(int id)
        {
            try
            {
                var bid = await _bidService.GetBidList(id);
                if (bid == null)
                {
                    return NotFound(new { message = "Bid not found." });
                }
                return Ok(bid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching bid by ID");
                return StatusCode(500, new { message = "Error while fetching bid." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBid([FromBody] BidListDto bidDto)
        {
            if (string.IsNullOrWhiteSpace(bidDto.Account))
                ModelState.AddModelError(nameof(bidDto.Account), "Le compte est obligatoire.");
            if (string.IsNullOrWhiteSpace(bidDto.BidType))
                ModelState.AddModelError(nameof(bidDto.BidType), "Le type de l'offre est obligatoire.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdBid = await _bidService.AddBidList(bidDto);
                return CreatedAtAction(nameof(GetBidById), new { id = createdBid.BidListId }, createdBid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding bid");
                return StatusCode(500, new { message = "Error while adding bid." });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListDto bidDto)
        {
            if (bidDto.Account != null && string.IsNullOrWhiteSpace(bidDto.Account))
                ModelState.AddModelError(nameof(bidDto.Account), "Le compte ne peut pas être vide.");
            if (bidDto.BidType != null && string.IsNullOrWhiteSpace(bidDto.BidType))
                ModelState.AddModelError(nameof(bidDto.BidType), "Le type de l'offre ne peut pas être vide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedBid = await _bidService.UpdateBidList(id, bidDto);
                if (updatedBid == null)
                {
                    return NotFound(new { message = "Bid not found for update." });
                }
                return Ok(updatedBid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating bid");
                return StatusCode(500, new { message = "Error while updating bid." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            try
            {
                var deleted = await _bidService.DeleteBidList(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Bid not found for deletion." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting bid");
                return StatusCode(500, new { message = "Error while deleting bid." });
            }
        }
    }
}