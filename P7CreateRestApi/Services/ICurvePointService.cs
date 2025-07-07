using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface ICurvePointService
    {
        Task<IEnumerable<CurvePointDto>> GetCurves();
        Task<CurvePointDto?> GetCurve(int id);
        Task<CurvePointDto> AddCurve(CurvePointDto curvePointDto);
        Task<CurvePointDto?> UpdateCurve(int id, CurvePointDto curvePointDto);
        Task<bool> DeleteCurve(int id);
    }
}
