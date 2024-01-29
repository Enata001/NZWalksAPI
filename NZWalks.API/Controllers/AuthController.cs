using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }

    // GET
    [HttpPost]
    [Route("Register")]
    [ValidateModel]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
       
            var identityUser = new IdentityUser() { Email = registerRequestDto.Email, UserName = registerRequestDto.Email };
            var userResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (!userResult.Succeeded)
            {
                return BadRequest("An error occured. Please try again");
            }

            if (registerRequestDto.Roles.Any())
            {
                userResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                if (!userResult.Succeeded)
                {
                    return BadRequest("Something went wrong");
                }
            }

            return Ok("User was registered! Please login.");
       
    }

    [HttpPost]
    [Route("Login")]
    [ValidateModel]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user is null)
            {
                return BadRequest("Username or Password Incorrect");
            }

            var isCorrect = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isCorrect)
            {
                return BadRequest("Username or Password Incorrect");
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles is null)
            {
                return BadRequest("Incorrect Username or Password");
            }
    
            var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
            var response = new LoginResponseDto() { JwtToken = jwtToken};

            return Ok(response);
       
    }
}