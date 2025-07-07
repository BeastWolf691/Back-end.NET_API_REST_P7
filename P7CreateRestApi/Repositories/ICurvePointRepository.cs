using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePointDto>> GetCurves();
        Task<CurvePointDto?> GetCurve(int id);
        Task<CurvePointDto> AddCurve(CurvePointDto curvePointDto);
        Task<CurvePointDto?> UpdateCurve(int id, CurvePointDto curvePointDto);
        Task<bool> DeleteCurve(int id);
    }
}
