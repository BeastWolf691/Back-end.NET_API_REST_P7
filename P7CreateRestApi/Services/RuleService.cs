using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;

        public RuleService(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        private void ValidateRule(RuleName rule)
        {
            if (string.IsNullOrWhiteSpace(rule.Name) || rule.Name.Length > 100)
                throw new ArgumentException("Le nom est obligatoire et ne peut pas excéder 100 caractères.");
            
            if (string.IsNullOrWhiteSpace(rule.Description) || rule.Description.Length > 500)
                throw new ArgumentException("La description est obligatoire et ne peut pas excéder 500 caractères.");
            
            if (string.IsNullOrWhiteSpace(rule.Json))
                throw new ArgumentException("Le champ Json est obligatoire.");
            
            if (string.IsNullOrWhiteSpace(rule.Template))
                throw new ArgumentException("Le champ Template est obligatoire.");
            
            if (string.IsNullOrWhiteSpace(rule.SqlStr))
                throw new ArgumentException("Le champ SqlStr est obligatoire.");
            
            if (string.IsNullOrWhiteSpace(rule.SqlPart))
                throw new ArgumentException("Le champ SqlPart est obligatoire.");
        }
        public async Task<IEnumerable<RuleName>> GetRules()
        {
            return await _ruleRepository.GetRules();
        }

        public async Task<RuleName?> GetRule(int id)
        {
            return await _ruleRepository.GetRule(id);
        }

        public async Task<RuleName> AddRule(RuleName ruleName)
        {
            ValidateRule(ruleName);
            return await _ruleRepository.AddRule(ruleName);
        }

        public async Task<RuleName?> UpdateRule(int id, RuleName ruleName)
        {
            var existing = await _ruleRepository.GetRule(id);
            if (existing == null) return null;

            existing.Name = !string.IsNullOrWhiteSpace(ruleName.Name) ? ruleName.Name : existing.Name;
            existing.Description = !string.IsNullOrWhiteSpace(ruleName.Description) ? ruleName.Description : existing.Description;
            existing.Json = !string.IsNullOrWhiteSpace(ruleName.Json) ? ruleName.Json : existing.Json;
            existing.Template = !string.IsNullOrWhiteSpace(ruleName.Template) ? ruleName.Template : existing.Template;
            existing.SqlStr = !string.IsNullOrWhiteSpace(ruleName.SqlStr) ? ruleName.SqlStr : existing.SqlStr;
            existing.SqlPart = !string.IsNullOrWhiteSpace(ruleName.SqlPart) ? ruleName.SqlPart : existing.SqlPart;

            ValidateRule(existing);

            await _ruleRepository.UpdateRule(id, existing);
            return existing;
        }

        public async Task<bool> DeleteRule(int id)
        {
            return await _ruleRepository.DeleteRule(id);
        }
    }
}
