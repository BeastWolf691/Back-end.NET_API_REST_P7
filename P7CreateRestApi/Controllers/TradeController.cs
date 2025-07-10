using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {

        private readonly ITradeService _tradeService;
        private readonly ILogger<TradeController> _logger;

        public TradeController(ITradeService tradeService, ILogger<TradeController> logger)
        {
            _tradeService = tradeService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTrades()
        {
            try
            {
                var trades = await _tradeService.GetTrades();
                return Ok(trades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching trades");
                return StatusCode(500, new { message = "Error while fetching trades." });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTradeById(int id)
        {
            try
            {
                var trade = await _tradeService.GetTradeById(id);
                if (trade == null)
                {
                    return NotFound(new { message = "Trade not found." });
                }
                return Ok(trade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching trade by ID");
                return StatusCode(500, new { message = "Error while fetching trade." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTrade([FromBody] TradeDto tradeDto)
        {
            try
            {
                var createdTrade = await _tradeService.AddTrade(tradeDto);
                return CreatedAtAction(nameof(GetTradeById), new { id = createdTrade.TradeId }, createdTrade);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while adding trade");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding trade");
                return StatusCode(500, new { message = "Error while adding trade." });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTrade(int id, [FromBody] TradeDto tradeDto)
        {
            try
            {
                var updateTrade = await _tradeService.UpdateTrade(id, tradeDto);
                if (updateTrade == null)
                {
                    return NotFound(new { message = "Trade not found for update." });
                }
                return Ok(updateTrade);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while updating trade");
                return BadRequest(new { message = ex.Message });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating trade");
                return StatusCode(500, new { message = "Error while updating trade." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            try
            {
                var deleted = await _tradeService.DeleteTrade(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Trade not found for deletion." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting trade");
                return StatusCode(500, new { message = "Error while deleting trade." });
            }
        }

    }
}