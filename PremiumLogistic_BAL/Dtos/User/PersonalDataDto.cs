namespace PremiumLogistic_BAL.Dtos.User;

public record PersonalDataDto
(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? CreatedOn
);
