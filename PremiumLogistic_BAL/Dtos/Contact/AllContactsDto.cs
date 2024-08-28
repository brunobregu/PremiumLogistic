namespace PremiumLogistic_BAL.Dtos.Contact;

public record AllContactsDto
(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Message
);
