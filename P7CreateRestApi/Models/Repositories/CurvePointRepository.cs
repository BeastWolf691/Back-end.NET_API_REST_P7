using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Models.Dto;
using Dot.Net.WebApi.Data;


namespace P7CreateRestApi.Models.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _context;

        public CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurvePointDto>> GetCurves()
        {
            return await _context.CurvePoints
                .Select(curvePoint => new CurvePointDto
                {
                    Id = curvePoint.Id,
                    CurveId = curvePoint.CurveId,
                    Term = curvePoint.Term,
                    CurvePointValue = curvePoint.CurvePointValue
                })
                .ToListAsync();
        }

        public async Task<CurvePointDto?> GetCurve(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return null;
            }

            return new CurvePointDto
            {
                Id = curvePoint.Id,
                CurveId = curvePoint.CurveId,
                Term = curvePoint.Term,
                CurvePointValue = curvePoint.CurvePointValue
            };
        }

        public async Task<CurvePointDto> AddCurve(CurvePointDto curvePointDto)
        {
            var curvePoint = new CurvePoint
            {
                CurveId = curvePointDto.CurveId,
                Term = curvePointDto.Term,
                CurvePointValue = curvePointDto.CurvePointValue,
                CreationDate = curvePointDto.CreationDate,
                AsOfDate = curvePointDto.AsOfDate
            };
            _context.CurvePoints.Add(curvePoint);
            await _context.SaveChangesAsync();
            return new CurvePointDto
            {
                Id = curvePoint.Id,
                CurveId = curvePoint.CurveId,
                Term = curvePoint.Term,
                CurvePointValue = curvePoint.CurvePointValue,
                CreationDate = curvePointDto.CreationDate,
                AsOfDate= curvePointDto.AsOfDate
            };
        }

        public async Task<CurvePointDto?> UpdateCurve(int id, CurvePointDto curvePointDto)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return null;
            }
            curvePoint.CurveId = curvePointDto.CurveId;
            curvePoint.Term = curvePointDto.Term;
            curvePoint.CurvePointValue = curvePointDto.CurvePointValue;
            curvePoint.CreationDate = curvePointDto.CreationDate;
            curvePoint.AsOfDate = curvePointDto.AsOfDate;

            _context.Set<CurvePoint>().Update(curvePoint);
            await _context.SaveChangesAsync();
            return new CurvePointDto
            {
                CurveId = curvePoint.CurveId,
                Term = curvePoint.Term,
                CurvePointValue = curvePoint.CurvePointValue,
                CreationDate = curvePoint.CreationDate,
                AsOfDate = curvePoint.AsOfDate

            };
        }

        public async Task<bool> DeleteCurve(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return false;
            }
            _context.CurvePoints.Remove(curvePoint);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
