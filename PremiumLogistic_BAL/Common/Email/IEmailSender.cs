namespace PremiumLogistic_BAL.Common.Email;

public interface IEmailSender
{
    Task SendEmail(Message message);
}
