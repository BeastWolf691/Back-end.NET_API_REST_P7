using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;
using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {

        private readonly IBidService _bidService;
        private readonly IMapper _mapper;
        private readonly ILogger<BidListController> _logger;

        public BidListController(IBidService bidService, IMapper mapper, ILogger<BidListController> logger)
        {
            _bidService = bidService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBids()
        {
            try
            {
                var bids = await _bidService.GetBidLists();
                var bidsDto = _mapper.Map<IEnumerable<BidListDto>>(bids);
                return Ok(bidsDto);
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
                    return NotFound(new { message = "Bid not found." });

                var bidDto = _mapper.Map<BidListDto>(bid);
                return Ok(bidDto);
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
            try
            {
                var bidEntity = _mapper.Map<BidList>(bidDto);
                var createdBid = await _bidService.AddBidList(bidEntity);
                var createdBidDto = _mapper.Map<BidListDto>(createdBid);

                return CreatedAtAction(nameof(GetBidById), new { id = createdBidDto.BidListId }, createdBidDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while adding bid");
                return BadRequest(new { message = ex.Message });
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
            try
            {
                var bidEntity = _mapper.Map<BidList>(bidDto);
                var updatedBid = await _bidService.UpdateBidList(id, bidEntity);

                if (updatedBid == null)
                    return NotFound(new { message = "Bid not found for update." });

                var updatedBidDto = _mapper.Map<BidListDto>(updatedBid);
                return Ok(updatedBidDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while updating bid");
                return BadRequest(new { message = ex.Message });
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
                    return NotFound(new { message = "Bid not found for deletion." });

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