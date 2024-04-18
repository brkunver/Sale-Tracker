namespace SaleTrackerBackend.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Models.Dto;
using SaleTrackerBackend.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TokenService _tokenService;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(input.Username!);

        if (user is null) return Unauthorized("User not found");

        var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password!, false);

        if (!result.Succeeded) return Unauthorized("Invalid credentials");
        var token = _tokenService.GenerateToken(user);

        return Ok(new { success = true, token });
    }

    // [HttpPost("register")]
    // public async Task<ActionResult> Register([FromBody] LoginDto input)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
    //     }
    //     try
    //     {
    //         var appUser = new IdentityUser
    //         {
    //             UserName = input.Username,
    //         };
    //         var result = await _userManager.CreateAsync(appUser, input.Password!);
    //         if (result.Succeeded)
    //         {
    //             return Ok(new ResponseDto<string> { Message = "Registered" });
    //         }
    //         return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(500, new ResponseDto<string> { Success = false, Message = "Internal server error", Data = e.Message });
    //     }
    // }
}