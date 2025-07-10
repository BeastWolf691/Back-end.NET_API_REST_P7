using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RuleService : IRuleService
    {

        private readonly IRuleRepository _repository;
        private readonly IMapper _mapper;

        public RuleService(IRuleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private void ValidateRuleDto(RuleDto ruleDto)
        {
            if (string.IsNullOrWhiteSpace(ruleDto.Name) || ruleDto.Name.Length > 100)
                throw new ArgumentException("Le nom est obligatoire et ne peut pas excéder 100 caractères.");
            
            if (string.IsNullOrWhiteSpace(ruleDto.Description) || ruleDto.Description.Length > 500)
                throw new ArgumentException("La description est obligatoire et ne peut pas excéder 500 caractères.");
            
            if (string.IsNullOrWhiteSpace(ruleDto.Json))
                throw new ArgumentException("Le champ Json est obligatoire.");
            
            if (string.IsNullOrWhiteSpace(ruleDto.Template))
                throw new ArgumentException("Le champ Template est obligatoire.");
            
            if (string.IsNullOrWhiteSpace(ruleDto.SqlStr))
                throw new ArgumentException("Le champ SqlStr est obligatoire.");
            
            if (string.IsNullOrWhiteSpace(ruleDto.SqlPart))
                throw new ArgumentException("Le champ SqlPart est obligatoire.");
        }
        public async Task<IEnumerable<RuleDto>> GetRules()
        {
            var entities = await _repository.GetRules();
            return _mapper.Map<IEnumerable<RuleDto>>(entities);
        }

        public async Task<RuleDto?> GetRule(int id)
        {
            var entity = await _repository.GetRule(id);
            return entity == null ? null : _mapper.Map<RuleDto>(entity);
        }

        public async Task<RuleDto> AddRule(RuleDto ruleDto)
        {
            ValidateRuleDto(ruleDto);
            var entity = _mapper.Map<RuleName>(ruleDto);
            var added = await _repository.AddRule(entity);
            return _mapper.Map<RuleDto>(added);
        }

        public async Task<RuleDto?> UpdateRule(int id, RuleDto ruleDto)
        {
            var rule = await _repository.GetRule(id);
            if (rule == null) return null;

            if(!string.IsNullOrWhiteSpace(ruleDto.Name))
                rule.Name = ruleDto.Name;

            if(!string.IsNullOrWhiteSpace(ruleDto.Description))
                rule.Description = ruleDto.Description;

            if(!string.IsNullOrWhiteSpace(ruleDto.Json))
                rule.Json = ruleDto.Json;

            if(!string.IsNullOrWhiteSpace(ruleDto.Template))
                rule.Template = ruleDto.Template;

            if(!string.IsNullOrWhiteSpace(ruleDto.SqlStr))
                rule.SqlStr = ruleDto.SqlStr;

            if(!string.IsNullOrWhiteSpace(ruleDto.SqlPart))
                rule.SqlPart = ruleDto.SqlPart;

            var dtoToValidate = _mapper.Map<RuleDto>(rule);
            ValidateRuleDto(dtoToValidate);

            await _repository.UpdateRule(id, rule);
            return _mapper.Map<RuleDto>(rule);
        }

        public async Task<bool> DeleteRule(int id)
        {
            return await _repository.DeleteRule(id);
        }
    }
}
