using System.Text;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Data;
using P7CreateRestApi.MappingProfiles;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Services;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/P7CreateRestApi-.log", rollingInterval: RollingInterval.Month)
    .CreateLogger();

builder.Host.UseSerilog();

// Load User Secrets (for local development)
builder.Configuration.AddUserSecrets<Program>();

// Configuration DB
builder.Services.AddDbContext<LocalDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
})
.AddEntityFrameworkStores<LocalDbContext>()
.AddDefaultTokenProviders();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var issuer = builder.Configuration["Authentication:Schemes:Bearer:ValidIssuer"];
        var audience = builder.Configuration["Authentication:Schemes:Bearer:ValidAudience"];
        var secret = builder.Configuration["Authentication:Schemes:Bearer:Secret"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };
    });

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

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

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddAutoMapper(typeof(BidListProfile));
builder.Services.AddAutoMapper(typeof(CurvePointProfile));
builder.Services.AddAutoMapper(typeof(RatingProfile));
builder.Services.AddAutoMapper(typeof(RuleProfile));
builder.Services.AddAutoMapper(typeof(TradeProfile));

// Controllers, Swagger, etc.
builder.Services.AddControllers();

var app = builder.Build();

// Seed Roles (Admin, User)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();