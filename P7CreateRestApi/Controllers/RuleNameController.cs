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
        private readonly ILogger _logger;

        public RuleNameController(RuleService ruleService, ILogger logger)
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdRule = await _ruleService.AddRule(ruleDto);
                var resultDto = new RuleDto
                {
                    Id = createdRule.Id,
                    Name = createdRule.Name,
                    Description = createdRule.Description,
                    Json = createdRule.Json,
                    Template = createdRule.Template,
                    SqlStr = createdRule.SqlStr,
                    SqlPart = createdRule.SqlPart
                };

                return CreatedAtAction(nameof(GetRuleById), new { id = resultDto.Id }, resultDto);
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
                {
                    return NotFound(new { message = "Rule not found for update." });
                }
                return Ok(updatedRule);
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