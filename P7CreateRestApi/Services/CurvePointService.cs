using Dot.Net.WebApi.Domain;
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

        private void ValidateCurvePoint(CurvePoint curvePoint)
        {
            if (!curvePoint.CurveId.HasValue || curvePoint.CurveId == 0)
                throw new ArgumentException("L'identifiant de la courbe est obligatoire.");

            if (curvePoint.Term.HasValue && curvePoint.Term < 0)
                throw new ArgumentException("Le délai ne peut pas être négatif.");

            if (curvePoint.CurvePointValue.HasValue && curvePoint.CurvePointValue < 0)
                throw new ArgumentException("La valeur ne peut pas être négative.");
        }


        public async Task<IEnumerable<CurvePoint>> GetCurves()
        {
            return await _curvePointRepository.GetCurves();
        }

        public async Task<CurvePoint?> GetCurve(int id)
        {
            return await _curvePointRepository.GetCurve(id);
        }


        public async Task<CurvePoint> AddCurve(CurvePoint curvePoint)
        {
            ValidateCurvePoint(curvePoint);
            return await _curvePointRepository.AddCurve(curvePoint);
        }

        public async Task<CurvePoint?> UpdateCurve(int id, CurvePoint curvePoint)
        {
            var existing = await _curvePointRepository.GetCurve(id);
            if (existing == null) return null;

            existing.Term = (curvePoint.Term.HasValue && curvePoint.Term >= 0) ? curvePoint.Term : existing.Term;
            existing.CurvePointValue = (curvePoint.CurvePointValue.HasValue && curvePoint.CurvePointValue >= 0) ? curvePoint.CurvePointValue : existing.CurvePointValue;
            existing.CurveId = (curvePoint.CurveId.HasValue && curvePoint.CurveId != 0) ? curvePoint.CurveId : existing.CurveId;

            ValidateCurvePoint(existing);

            await _curvePointRepository.UpdateCurve(id, existing);
            return existing;
        }

        public async Task<bool> DeleteCurve(int id)
        {
            return await _curvePointRepository.DeleteCurve(id);
        }
    }
}
