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

        public async Task<RuleDto> AddRule(RuleDto dto)
        {
            var entity = _mapper.Map<RuleName>(dto);
            var added = await _repository.AddRule(entity);
            return _mapper.Map<RuleDto>(added);
        }

        public async Task<RuleDto?> UpdateRule(int id, RuleDto dto)
        {
            var updatedEntity = _mapper.Map<RuleName>(dto);
            var result = await _repository.UpdateRule(id, updatedEntity);
            return result == null ? null : _mapper.Map<RuleDto>(result);
        }

        public async Task<bool> DeleteRule(int id)
        {
            return await _repository.DeleteRule(id);
        }
    }
}
