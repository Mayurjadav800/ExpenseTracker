using ExpenseTracker.Dto;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ExpenseTracker.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUser();
        //Task<UserDto> GetUserById(int id);
        Task<UserDto> CreateUser(UserDto userDto);
        Task<bool> CreateForgotPassword(ResetpasswordDto resetpasswordDto);

        Task<bool> Resetpassword(ResetpasswordDto resetpasswordDto);
    }
}
