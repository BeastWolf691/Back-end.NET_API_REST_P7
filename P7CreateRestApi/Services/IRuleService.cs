using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface IRuleService
    {
        Task<IEnumerable<RuleDto>> GetRules();
        Task<RuleDto?> GetRule(int id);
        Task<RuleDto> AddRule(RuleDto rule);
        Task<RuleDto?> UpdateRule(int id, RuleDto ruleDto);
        Task<bool> DeleteRule(int id);
    }
}
