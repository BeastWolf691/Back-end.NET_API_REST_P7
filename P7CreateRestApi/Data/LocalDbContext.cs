using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Data
{
    public class LocalDbContext : IdentityDbContext<User>
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options)
            : base(options)
        {
        }

        public new DbSet<User> Users { get; set; } = null!;
        public DbSet<BidList> BidLists { get; set; } = null!;
        public DbSet<CurvePoint> CurvePoints { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<RuleName> RuleNames { get; set; } = null!;
        public DbSet<Trade> Trades { get; set; } = null!;
    }
}