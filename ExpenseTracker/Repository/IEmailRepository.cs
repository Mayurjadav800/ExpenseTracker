using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    public interface IEmailRepository
    {
        
        Task SendEmailAsync(MailRequest mailRequest);
        //Task SendEmailAsync(string email, string v1, string v2);
    }
}
