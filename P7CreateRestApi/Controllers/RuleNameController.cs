using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly RuleService _ruleService;
        private readonly ILogger<RuleNameController> _logger;

        public RuleNameController(RuleService ruleService, ILogger<RuleNameController> logger)
        {
            _ruleService = ruleService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRules()
        {
            try
            {
                var rules = await _ruleService.GetRules();
                return Ok(rules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching rules");
                return StatusCode(500, new { message = "Error while fetching rules." });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRuleById(int id)
        {
            try
            {
                var rule = await _ruleService.GetRule(id);
                if (rule == null)
                {
                    return NotFound(new { message = "Rule not found." });
                }
                return Ok(rule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching rule by ID");
                return StatusCode(500, new { message = "Error while fetching rule." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRule([FromBody] RuleDto ruleDto)
        {

            try
            {
                var createdRule = await _ruleService.AddRule(ruleDto);
                return CreatedAtAction(nameof(GetRuleById), new { id = createdRule.Id }, createdRule);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while adding rule");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding rule");
                return StatusCode(500, new { message = "Error while adding rule." });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRule(int id, [FromBody] RuleDto ruleDto)
        {
            try
            {
                var updatedRule = await _ruleService.UpdateRule(id, ruleDto);
                if (updatedRule == null)
                    return NotFound(new { message = "Rule not found for update." });

                return Ok(updatedRule);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while updating rule");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating rule");
                return StatusCode(500, new { message = "Error while updating rule." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            try
            {
                var deleted = await _ruleService.DeleteRule(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Rule not found for deletion." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting rule");
                return StatusCode(500, new { message = "Error while deleting rule." });
            }
        }
    }
}