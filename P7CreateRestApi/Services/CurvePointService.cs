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

        private void ValidateCurvePoint(CurvePointDto curvePointDto)
        {
            if (curvePointDto.Term.HasValue && curvePointDto.Term < 0)
                throw new ArgumentException("Le délai ne peut pas être négatif.");

            if (curvePointDto.CurvePointValue.HasValue && curvePointDto.CurvePointValue < 0)
                throw new ArgumentException("La valeur ne peut pas être négative.");
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
            ValidateCurvePoint(dto);
            var domain = _mapper.Map<CurvePoint>(dto);

            var added = await _curvePointRepository.AddCurve(domain);
            return _mapper.Map<CurvePointDto>(added);
        }

        public async Task<CurvePointDto?> UpdateCurve(int id, CurvePointDto dto)
        {
            var existing = await _curvePointRepository.GetCurve(id);
            if (existing == null) return null;

            if (dto.CurveId != 0)
                existing.CurveId = dto.CurveId;

            if (dto.Term.HasValue)
                existing.Term = dto.Term;

            if (dto.CurvePointValue.HasValue)
                existing.CurvePointValue = dto.CurvePointValue;

            var dtoToValidate = _mapper.Map<CurvePointDto>(existing);
            ValidateCurvePoint(dtoToValidate);

            // Sauvegarde
            await _curvePointRepository.UpdateCurve(id, existing);
            return _mapper.Map<CurvePointDto>(existing);
        }

        public async Task<bool> DeleteCurve(int id)
        {
            return await _curvePointRepository.DeleteCurve(id);
        }
    }
}
