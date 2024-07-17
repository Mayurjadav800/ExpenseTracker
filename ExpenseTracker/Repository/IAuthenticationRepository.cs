using ExpenseTracker.Dto;

namespace ExpenseTracker.Repository
{
    public interface IAuthenticationRepository
    {
        Task<string> CreateAuthentication(LogginDto logginDto);
    }
}
