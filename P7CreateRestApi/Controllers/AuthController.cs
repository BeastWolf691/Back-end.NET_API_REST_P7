using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(UserManager<User> userManager, IJwtService jwtService, ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _logger = logger;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Validation manuelle
        if (string.IsNullOrWhiteSpace(model.FullName) || model.FullName.Length > 50)
            return BadRequest(new { message = "Le nom complet est obligatoire et ne doit pas dépasser 50 caractères." });

        if (string.IsNullOrWhiteSpace(model.Email) || !model.Email.Contains("@"))
            return BadRequest(new { message = "Email invalide." });

        if (string.IsNullOrWhiteSpace(model.Password) || !IsValidPassword(model.Password))
            return BadRequest(new { message = "Mot de passe invalide (majuscule, minuscule, chiffre, symbole, 8+ caractères)." });

        var user = new User
        {
            UserName = model.Email,
            FullName = model.FullName,
            Email = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            return Ok(new { message = "User registered successfully" });
        }

        _logger.LogWarning("User registration failed for email: {Email}", model.Email);
        return BadRequest(result.Errors);
    }

    // Fonction de validation mot de passe personnalisée
    private bool IsValidPassword(string password)
    {
        if (password.Length < 8) return false;
        if (!password.Any(char.IsUpper)) return false;
        if (!password.Any(char.IsLower)) return false;
        if (!password.Any(char.IsDigit)) return false;
        if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;
        return true;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Vérification manuelle
        if (string.IsNullOrWhiteSpace(model.Email))
        {
            _logger.LogWarning("Login failed: Email is required.");
            return BadRequest(new { message = "Email is required." });
        }

        if (!model.Email.Contains("@") || !model.Email.Contains("."))
        {
            _logger.LogWarning("Login failed: Invalid email format.");
            return BadRequest(new { message = "Invalid email format." });
        }

        if (string.IsNullOrWhiteSpace(model.Password))
        {
            _logger.LogWarning("Login failed: Password is required.");
            return BadRequest(new { message = "Password is required." });
        }

        // Recherche de l'utilisateur
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _logger.LogWarning("Login failed: user not found for email: {Email}", model.Email);
            return Unauthorized(new { message = "Invalid credentials" });
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!passwordValid)
        {
            _logger.LogWarning("Login failed: invalid password for email: {Email}", model.Email);
            return Unauthorized(new { message = "Invalid credentials" });
        }

        var token = _jwtService.GenerateJwtToken(user);
        _logger.LogInformation("User logged in successfully with email: {Email}", model.Email);
        return Ok(new { message = "Login successful", token });
    }
}