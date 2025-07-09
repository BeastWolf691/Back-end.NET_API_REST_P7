using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers called");
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(string id)
        {
            _logger.LogInformation("GetUserById called with id={Id}", id);
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning("User not found with id={Id}", id);
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);
        }

        // Création utilisateur - mot de passe obligatoire
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> AddUser([FromBody] UserDto userDto)
        {
            _logger.LogInformation("CreateUser called for UserName={UserName}", userDto.UserName);

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                _logger.LogWarning("Password is required for user creation");
                return BadRequest(new { message = "Password is required." });
            }

            var result = await _userService.AddUser(userDto, userDto.Password);

            if (!result.Succeeded)
            {
                _logger.LogWarning("User creation failed: {Errors}", result.Errors);
                return BadRequest(result.Errors);
            }

            // Récupérer l'utilisateur créé (il faut l'ID)
            var createdUser = await _userService.GetUserById(userDto.UserName);
            var createdUserDto = createdUser; // déjà UserReadDto

            return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.Id }, createdUserDto);
        }

        // Mise à jour utilisateur (sans changer le mot de passe ici)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
        {
            _logger.LogInformation("UpdateUser called for id={Id}", id);

            if (id != userDto.Id)
            {
                return BadRequest(new { message = "User ID mismatch." });
            }

            try
            {
                var updated = await _userService.UpdateUser(userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user id={Id}", id);
                return StatusCode(500, new { message = "Error while updating user." });
            }
        }


        // Suppression utilisateur
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation("DeleteUser called for id={Id}", id);

            try
            {
                var deleted = await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting user id={Id}", id);
                return StatusCode(500, new { message = "Error while deleting user." });
            }
        }
    }
}