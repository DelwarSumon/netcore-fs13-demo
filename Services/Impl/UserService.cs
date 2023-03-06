using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

namespace NETCoreDemo.Services;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public Task<UserSignInResponseDTO?> SignInAsync(UserSignInDTO request)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> SignUpAsync(UserSignUpDTO request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            Email = request.Email,
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return null;
        }

        // TODO: Put all these into a separate controller/service for roles
        var roles = new[] { "Admin", "Dev" };
        foreach (var role in roles)
        {
            if (await _roleManager.FindByNameAsync(role) is null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = role,
                });
            }
        }

        await _userManager.AddToRolesAsync(user, roles);

        return user;
    }
}