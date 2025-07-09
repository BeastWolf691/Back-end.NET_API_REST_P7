using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Models.Dto;


namespace P7CreateRestApi.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly LocalDbContext _context;

        public RuleRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RuleName>> GetRules()
        {
            return await _context.RuleNames.ToListAsync();
        }

        public async Task<RuleName?> GetRule(int id)
        {
            return await _context.RuleNames.FindAsync(id);
        }

        public async Task<RuleName> AddRule(RuleName rule)
        {
            _context.RuleNames.Add(rule);
            await _context.SaveChangesAsync();
            return rule;
        }

        public async Task<RuleName?> UpdateRule(int id, RuleName ruleName)
        {
            var existing = await _context.RuleNames.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(ruleName);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteRule(int id)
        {
            var rule = await _context.RuleNames.FindAsync(id);
            if (rule == null) return false;

            _context.RuleNames.Remove(rule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
