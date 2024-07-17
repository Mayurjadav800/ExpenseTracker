using AutoMapper;
using ExpenseTracker.Dto;
using ExpenseTracker.Model;

namespace ExpenseTracker.Mapper
{
    public class MapperConfiguration:Profile
    {
        public MapperConfiguration()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User,LogginDto>().ReverseMap();
            CreateMap<Expense, ExpensDto>().ReverseMap();
        }
    }
}
