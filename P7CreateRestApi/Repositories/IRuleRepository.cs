using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public interface IRuleRepository
    {
        Task<IEnumerable<RuleDto>> GetRules();
        Task<RuleDto?> GetRule(int id);
        Task<RuleDto> AddRule(RuleDto ruleDto);
        Task<RuleDto?> UpdateRule(int id, RuleDto ruleDto);
        Task<bool> DeleteRule(int id);
    }
}
