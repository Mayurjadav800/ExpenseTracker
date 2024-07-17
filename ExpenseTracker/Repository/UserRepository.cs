using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ExpenseDbContext _expenseDbContext;

        public UserRepository(IMapper mapper,ExpenseDbContext expenseDbContext)
        {
            _mapper = mapper;
            _expenseDbContext = expenseDbContext;
        }
        public async Task<UserDto> CreateUser(UserDto userDto)
        {
           // var users = await _expenseDbContext.User.FirstOrDefaultAsync(e=>e.Id ==  userDto.Id);
            if(userDto == null)
            {
                throw new Exception(nameof(userDto));
            }
            var user = _mapper.Map<User>(userDto);
            await _expenseDbContext.User.AddAsync(user);
            await _expenseDbContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public  async Task<List<UserDto>> GetAllUser()
        {
            var users = await _expenseDbContext.User.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
            
        }

        public async Task<UserDto> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
