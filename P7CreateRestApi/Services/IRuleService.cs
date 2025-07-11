using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface IRuleService
    {
        Task<IEnumerable<RuleName>> GetRules();
        Task<RuleName?> GetRule(int id);
        Task<RuleName> AddRule(RuleName ruleName);
        Task<RuleName?> UpdateRule(int id, RuleName ruleName);
        Task<bool> DeleteRule(int id);
    }
}
