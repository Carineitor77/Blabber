namespace Application.Common.DTO.Profile;

public record EditProfileDto(
    string Bio,
    string FirstName,
    string SecondName,
    int Age);