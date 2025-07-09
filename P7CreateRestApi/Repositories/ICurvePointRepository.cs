using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePoint>> GetCurves();
        Task<CurvePoint?> GetCurve(int id);
        Task<CurvePoint> AddCurve(CurvePoint curvePoint);
        Task<CurvePoint?> UpdateCurve(int id, CurvePoint curvePoint);
        Task<bool> DeleteCurve(int id);
    }
}
