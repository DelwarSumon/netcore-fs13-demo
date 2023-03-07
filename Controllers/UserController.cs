namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class UserController : ApiControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(UserSignUpDTO request)
    {
        var user = await _service.SignUpAsync(request);
        if (user is null)
        {
            return BadRequest();
        }
        return Ok(UserSignUpResponseDTO.FromUser(user));
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(UserSignInDTO request)
    {
        var response = await _service.SignInAsync(request);
        if (response is null)
        {
            return Unauthorized();
        }
        return Ok(response);
    }
    // TODO: Implement other endpoints for users
}