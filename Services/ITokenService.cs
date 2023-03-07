namespace NETCoreDemo.Services;

using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

public interface ITokenService
{
    Task<UserSignInResponseDTO> GenerateTokenAsync(User user);
}