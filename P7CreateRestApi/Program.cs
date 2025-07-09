using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Services;

namespace P7CreateRestApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration DB
            builder.Services.AddDbContext<LocalDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Ajout d'Identity (User + Password hashing)
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LocalDbContext>()
                .AddDefaultTokenProviders();

            // Déclaration des services (Dependency Injection)
            builder.Services.AddScoped<IBidRepository, BidRepository>();
            builder.Services.AddScoped<ICurvePointRepository, CurvePointRepository>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRuleRepository, RuleRepository>();
            builder.Services.AddScoped<ITradeRepository, TradeRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IBidService, BidService>();
            builder.Services.AddScoped<ICurvePointService, CurvePointService>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddScoped<IRuleService, RuleService>();
            builder.Services.AddScoped<ITradeService, TradeService>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Authentification & Autorisation
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Controllers, Swagger, etc.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
