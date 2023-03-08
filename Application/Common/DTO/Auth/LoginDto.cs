namespace Application.Common.DTO.Auth;

public record LoginDto(
    string Email, 
    string Password);