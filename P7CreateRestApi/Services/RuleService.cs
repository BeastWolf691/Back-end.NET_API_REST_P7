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

        public async Task<IEnumerable<RuleDto>> GetRules()
        {
            return await _ruleRepository.GetRules();
        }

        public async Task<RuleDto?> GetRule(int id)
        {
            return await _ruleRepository.GetRule(id);
        }

        public async Task<RuleDto> AddRule(RuleDto rule)
        {
            return await _ruleRepository.AddRule(rule);
        }

        public async Task<RuleDto?> UpdateRule(int id, RuleDto ruleDto)
        {
            return await _ruleRepository.UpdateRule(id, ruleDto);
        }

        public async Task<bool> DeleteRule(int id)
        {
            return await _ruleRepository.DeleteRule(id);
        }
    }
}
