namespace Application.Common.DTO.User;

public record UserDto(
    string Id,
    string Bio,
    string FirstName,
    string SecondName,
    int Age,
    string UserName);