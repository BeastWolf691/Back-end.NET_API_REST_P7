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

        private void ValidateRuleDto(RuleDto ruleDto)
        {
            if (string.IsNullOrWhiteSpace(ruleDto.Name) || ruleDto.Name.Length > 100)
            {
                ModelState.AddModelError(nameof(ruleDto.Name), "Le nom est obligatoire et ne peut pas excéder 100 caractères.");
            }
            if (string.IsNullOrWhiteSpace(ruleDto.Description) || ruleDto.Description.Length > 500)
            {
                ModelState.AddModelError(nameof(ruleDto.Description), "La description est obligatoire et ne peut pas excéder 500 caractères.");
            }
            if (string.IsNullOrWhiteSpace(ruleDto.Json))
            {
                ModelState.AddModelError(nameof(ruleDto.Json), "Le champ Json est obligatoire.");
            }
            if (string.IsNullOrWhiteSpace(ruleDto.Template))
            {
                ModelState.AddModelError(nameof(ruleDto.Template), "Le champ Template est obligatoire.");
            }
            if (string.IsNullOrWhiteSpace(ruleDto.SqlStr))
            {
                ModelState.AddModelError(nameof(ruleDto.SqlStr), "Le champ SqlStr est obligatoire.");
            }
            if (string.IsNullOrWhiteSpace(ruleDto.SqlPart))
            {
                ModelState.AddModelError(nameof(ruleDto.SqlPart), "Le champ SqlPart est obligatoire.");
            }
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
            ValidateRuleDto(ruleDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdRule = await _ruleService.AddRule(ruleDto);
                return CreatedAtAction(nameof(GetRuleById), new { id = createdRule.Id }, createdRule);
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
            ValidateRuleDto(ruleDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedRule = await _ruleService.UpdateRule(id, ruleDto);
                if (updatedRule == null)
                    return NotFound(new { message = "Rule not found for update." });

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