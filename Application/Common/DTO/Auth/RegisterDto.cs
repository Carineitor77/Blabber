namespace Application.Common.DTO.Auth;

public record RegisterDto(
    string UserName, 
    string Email, 
    string Password, 
    string FirstName, 
    string SecondName, 
    int Age);