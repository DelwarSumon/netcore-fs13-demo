namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface IUserService
{
    Task<User?> SignUpAsync(UserSignUpDTO request);
    Task<UserSignInResponseDTO?> SignInAsync(UserSignInDTO request);
}