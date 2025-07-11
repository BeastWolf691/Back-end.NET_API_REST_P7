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
    public class TradeController : ControllerBase
    {

        private readonly ITradeService _tradeService;
        private readonly ILogger<TradeController> _logger;
        private readonly IMapper _mapper;

        public TradeController(ITradeService tradeService, ILogger<TradeController> logger, IMapper mapper)
        {
            _tradeService = tradeService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTrades()
        {
            try
            {
                var trades = await _tradeService.GetTrades();
                var tradeDto = _mapper.Map<IEnumerable<TradeDto>>(trades);
                return Ok(tradeDto);
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
                var tradeDto = _mapper.Map<TradeDto>(trade);
                return Ok(tradeDto);
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
                var tradeEntity = _mapper.Map<Trade>(tradeDto);
                var createdTrade = await _tradeService.AddTrade(tradeEntity);
                var createdTradeDto = _mapper.Map<TradeDto>(createdTrade);

                return CreatedAtAction(nameof(GetTradeById), new { id = createdTradeDto.TradeId }, createdTradeDto);
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
                var tradeEntity = _mapper.Map<Trade>(tradeDto);
                var updateTrade = await _tradeService.UpdateTrade(id, tradeEntity);
                if (updateTrade == null)
                    return NotFound(new { message = "Trade not found for update." });

                var updatedTradeDto = _mapper.Map<TradeDto>(updateTrade);
                return Ok(updatedTradeDto);
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