namespace Application.Common.DTO.Profile;

public record ProfileDto(
    string Id,
    string Bio,
    string FirstName,
    string SecondName,
    int Age,
    string UserName,
    string Email);