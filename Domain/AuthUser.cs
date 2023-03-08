using Microsoft.AspNetCore.Identity;

namespace Domain;

public class AuthUser : IdentityUser
{
    public string? Bio { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public int Age { get; set; }
}