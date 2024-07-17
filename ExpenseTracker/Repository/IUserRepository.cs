using ExpenseTracker.Dto;

namespace ExpenseTracker.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> CreateUser(UserDto userDto);
    }
}
