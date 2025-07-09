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

        private void ValidateTrade(TradeDto tradeDto)
        {
            if (tradeDto.BuyQuantity < 0)
            {
                ModelState.AddModelError(nameof(tradeDto.BuyQuantity), "La quantité achetée ne peut pas être négative.");
            }
            if (tradeDto.SellQuantity < 0)
            {
                ModelState.AddModelError(nameof(tradeDto.SellQuantity), "La quantité vendue ne peut pas être négative.");
            }
            if (tradeDto.BuyPrice < 0)
            {
                ModelState.AddModelError(nameof(tradeDto.BuyPrice), "Le prix d'achat ne peut pas être négatif.");
            }
            if (tradeDto.SellPrice < 0)
            {
                ModelState.AddModelError(nameof(tradeDto.SellPrice), "Le prix de vente ne peut pas être négatif.");
            }
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
            ValidateTrade(tradeDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdTrade = await _tradeService.AddTrade(tradeDto);
                return CreatedAtAction(nameof(GetTradeById), new { id = createdTrade.TradeId }, createdTrade);
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
            ValidateTrade(tradeDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updateTrade = await _tradeService.UpdateTrade(tradeDto);
                if (updateTrade == null)
                {
                    return NotFound(new { message = "Trade not found for update." });
                }
                return Ok(updateTrade);
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