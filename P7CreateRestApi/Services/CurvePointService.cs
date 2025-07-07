using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {

        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointService(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        public async Task<IEnumerable<CurvePointDto>> GetCurves()
        {
            return await _curvePointRepository.GetCurves();
        }

        public async Task<CurvePointDto?> GetCurve(int id)
        {
            return await _curvePointRepository.GetCurve(id);
        }

        public async Task<CurvePointDto> AddCurve(CurvePointDto curvePointDto)
        {
            return await _curvePointRepository.AddCurve(curvePointDto);
        }

        public async Task<CurvePointDto?> UpdateCurve(int id, CurvePointDto curvePointDto)
        {
            return await _curvePointRepository.UpdateCurve(id, curvePointDto);
        }

        public async Task<bool> DeleteCurve(int id)
        {
            return await _curvePointRepository.DeleteCurve(id);
        }
    }
}
