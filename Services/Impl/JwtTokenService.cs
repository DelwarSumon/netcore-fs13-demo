using System.Linq.Expressions;
using System.Text;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NETCoreDemo.Services;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;

    public JwtTokenService(IConfiguration config, UserManager<User> userManager)
    {
        _config = config;
        _userManager = userManager;
    }

    public async Task<UserSignInResponseDTO> GenerateTokenAsync(User user)
    {
        // Payload
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
        };

        if (user.Email == "daniel.monero@gmail.com")
        {
            claims.Add(new Claim("Course", "FS13"));
        }

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        /*
        var subscriptionLevel = await GetSubscriptionAsync(user);
        claims.Add(new Claim("subscriptionLevel", subscriptionLevel));
        */

        // Secret
        var secret = _config["Jwt:Secret"];
        var signingKey = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            SecurityAlgorithms.HmacSha256
        );

        // Expriation
        var expiration = DateTime.Now.AddHours(1);

        // Token description
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: signingKey
        );
        var tokenWriter = new JwtSecurityTokenHandler();

        return new UserSignInResponseDTO
        {
            Token = tokenWriter.WriteToken(token), // Generate the token
            Expiration = expiration,
        };
    }
}