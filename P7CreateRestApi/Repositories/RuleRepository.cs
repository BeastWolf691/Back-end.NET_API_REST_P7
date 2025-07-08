using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
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

        private static RuleDto ToDto(RuleName rule) => new RuleDto
        {
            Id = rule.Id,
            Name = rule.Name,
            Description = rule.Description,
            Json = rule.Json,
            Template = rule.Template,
            SqlStr = rule.SqlStr,
            SqlPart = rule.SqlPart
        };

        private static RuleName ToEntity(RuleDto ruleDto) => new RuleName
        {
            Name = ruleDto.Name,
            Description = ruleDto.Description,
            Json = ruleDto.Json,
            Template = ruleDto.Template,
            SqlStr = ruleDto.SqlStr,
            SqlPart = ruleDto.SqlPart
        };

        public async Task<IEnumerable<RuleDto>> GetRules()
        {
            return await _context.RuleNames
                .Select(rule => ToDto(rule))
                .ToListAsync();
        }

        public async Task<RuleDto?> GetRule(int id)
        {
            var rule = await _context.RuleNames.FindAsync(id);
            return rule == null ? null : ToDto(rule);
        }

        public async Task<RuleDto> AddRule(RuleDto ruleDto)
        {
            var rule = ToEntity(ruleDto);
            _context.RuleNames.Add(rule);
            await _context.SaveChangesAsync();

            return ToDto(rule);
        }

        public async Task<RuleDto?> UpdateRule(int id, RuleDto ruleDto)
        {
            var rule = await _context.RuleNames.FindAsync(id);
            if(rule == null) return null;

            rule.Name = ruleDto.Name;
            rule.Description = ruleDto.Description;
            rule.Json = ruleDto.Json;
            rule.Template = ruleDto.Template;
            rule.SqlStr = ruleDto.SqlStr;
            rule.SqlPart = ruleDto.SqlPart;

            _context.RuleNames.Update(rule);
            await _context.SaveChangesAsync();
            return ToDto(rule);
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
