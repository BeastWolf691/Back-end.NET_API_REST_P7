using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;


namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _context;

        public CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CurvePoint>> GetCurves()
        {
            return await _context.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint?> GetCurve(int id)
        {
            return await _context.CurvePoints.FindAsync(id);
        }

        public async Task<CurvePoint> AddCurve(CurvePoint curvePoint)
        {
            _context.CurvePoints.Add(curvePoint);
            await _context.SaveChangesAsync();
            return curvePoint;
        }

        public async Task<CurvePoint?> UpdateCurve(int id, CurvePoint curvePoint)
        {
            var existing = await _context.CurvePoints.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(curvePoint);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteCurve(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null) return false;

            _context.CurvePoints.Remove(curvePoint);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
