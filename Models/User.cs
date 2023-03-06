using System.ComponentModel.DataAnnotations;
namespace NETCoreDemo.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    [MaxLength(256)]
    public string FirstName { get; set; } = null!;

    [MaxLength(256)]
    public string LastName { get; set; } = null!;
}