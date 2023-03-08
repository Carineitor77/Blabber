namespace Application.Common.DTO.User;

public record UserDto(
    Guid Id,
    string Bio,
    string FirstName,
    string SecondName,
    int Age,
    string UserName);