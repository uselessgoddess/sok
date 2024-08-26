using Identity.Data;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    TokenService tokenService,
    DatabaseCx context)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new AppUser
            { UserName = model.Username, Email = model.Email, CreatedDate = DateTime.UtcNow };
        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
            return Ok(new { Message = "User created successfully" });

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            var accessToken = tokenService.Access(user);
            var refreshToken = tokenService.Refresh(user.Id);
            
            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }

        return Unauthorized();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var storedRefreshToken =
            await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == model.RefreshToken);

        if (storedRefreshToken == null || storedRefreshToken.Expire < DateTime.Now ||
            storedRefreshToken.IsRevoked)
            return Unauthorized();

        var user = await userManager.FindByIdAsync(storedRefreshToken.UserId);
        if (user == null)
            return Unauthorized();

        var newAccessToken = tokenService.Access(user);
        var newRefreshToken = tokenService.Refresh(user.Id);
        
        storedRefreshToken.IsRevoked = true;
        context.RefreshTokens.Update(storedRefreshToken);
        context.RefreshTokens.Add(newRefreshToken);
        await context.SaveChangesAsync();

        return Ok(new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token
        });
    }
}