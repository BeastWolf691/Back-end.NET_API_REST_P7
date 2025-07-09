using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {

        private readonly ICurvePointRepository _curvePointRepository;
        private readonly IMapper _mapper;

        public CurvePointService(ICurvePointRepository curvePointRepository, IMapper mapper)
        {
            _curvePointRepository = curvePointRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurvePointDto>> GetCurves()
        {
            var domains = await _curvePointRepository.GetCurves();
            return _mapper.Map<IEnumerable<CurvePointDto>>(domains);
        }

        public async Task<CurvePointDto?> GetCurve(int id)
        {
            var domain = await _curvePointRepository.GetCurve(id);
            return domain == null ? null : _mapper.Map<CurvePointDto>(domain);
        }


        public async Task<CurvePointDto> AddCurve(CurvePointDto dto)
        {
            var domain = _mapper.Map<CurvePoint>(dto);
            var added = await _curvePointRepository.AddCurve(domain);
            return _mapper.Map<CurvePointDto>(added);
        }

        public async Task<CurvePointDto?> UpdateCurve(int id, CurvePointDto dto)
        {
            var domain = _mapper.Map<CurvePoint>(dto);
            var updated = await _curvePointRepository.UpdateCurve(id, domain);
            return updated == null ? null : _mapper.Map<CurvePointDto>(updated);
        }

        public async Task<bool> DeleteCurve(int id)
        {
            return await _curvePointRepository.DeleteCurve(id);
        }
    }
}
