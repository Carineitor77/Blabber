namespace Application.Common.DTO;

public record RegisterDto(
    string UserName, 
    string Email, 
    string Password, 
    string FirstName, 
    string SecondName, 
    int Age);