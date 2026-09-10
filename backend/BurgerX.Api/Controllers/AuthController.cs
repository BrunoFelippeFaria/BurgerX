using BurgerX.Application.Auth.Commands.Login;
using BurgerX.Infrastructure.Auth;

using Mediator;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BurgerX.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController (IMediator mediator, IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var token = await _mediator.Send(command);

        Response.Cookies.Append("access-token", token, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(_jwtOptions.ExpirationDays),
            HttpOnly = true,
            SameSite = SameSiteMode.Lax
        });

        return NoContent();
    }
}