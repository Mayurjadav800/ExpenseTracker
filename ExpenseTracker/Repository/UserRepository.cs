using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using ExpenseTracker.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Encodings.Web;

namespace ExpenseTracker.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ExpenseDbContext _expenseDbContext;
        private readonly IEmailRepository _emailRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(
            IMapper mapper,
            ExpenseDbContext expenseDbContext,
            IEmailRepository emailRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _expenseDbContext = expenseDbContext;
            _emailRepository = emailRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateForgotPassword(ResetpasswordDto resetpasswordDto)
        {
            var user = await _expenseDbContext.User.FirstOrDefaultAsync(e=>e.Email == resetpasswordDto.Email);
            if (user == null)
            {
                return false; // Do not reveal that the user does not exist
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/ResetPassword?token={token}&email={user.Email}";

         
            await _emailRepository.SendEmailAsync(,"Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return true;
        }
        public Task<bool> Resetpassword(ResetpasswordDto resetpasswordDto)
        {
            throw new NotImplementedException();
        }


        public async Task<UserDto> CreateUser(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var user = _mapper.Map<User>(userDto);
            await _expenseDbContext.User.AddAsync(user);
            await _expenseDbContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            var users = await _expenseDbContext.User.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

    
    }
}








//using AutoMapper;
//using ExpenseTracker.Data;
//using ExpenseTracker.Dto;
//using ExpenseTracker.Model;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.EntityFrameworkCore;
//using System.Text.Encodings.Web;

//namespace ExpenseTracker.Repository
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly IMapper _mapper;
//        private readonly ExpenseDbContext _expenseDbContext;
//        private readonly IEmailRepository _emailRepository;
//        private readonly IUserRepository _userRepository;

//        public UserRepository(IMapper mapper,ExpenseDbContext expenseDbContext,IEmailRepository emailRepository,IUserRepository userRepository)
//        {
//            _mapper = mapper;
//            _expenseDbContext = expenseDbContext;
//            _emailRepository = emailRepository;
//            _userRepository = userRepository;
//        }

//        public async Task<bool> CreateForgotPassword(string email)
//        {
//            var forget = await _expenseDbContext.User.SingleOrDefaultAsync(e=>e.Email==email);
//            if(forget == null)
//            {
//                return false;
//            }
//            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//            var callbackUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/ResetPassword?token={token}&email={user.Email}";

//            await _emailRepository.SendEmailAsync(email, "Reset Password",
//                   $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

//            return true;
//        }
//        //public class AuthenticationRepository : IUserRepository
//        //{
//        //    private readonly ExpenseDbContext _expenseDbContext;
//        //    private readonly UserManager<User> _userManager;
//        //    private readonly IEmailSender _emailSender;
//        //    private readonly IHttpContextAccessor _httpContextAccessor;

//        //    public AuthenticationRepository(
//        //        ExpenseDbContext expenseDbContext,
//        //        UserManager<User> userManager,
//        //        IEmailSender emailSender,
//        //        IHttpContextAccessor httpContextAccessor)
//        //    {
//        //        _expenseDbContext = expenseDbContext;
//        //        _userManager = userManager;
//        //        _emailSender = emailSender;
//        //        _httpContextAccessor = httpContextAccessor;
//        //    }

//            // Other methods...

//        //    public async Task<bool> ForgotPassword(string email)
//        //    {
//        //        var user = await _expenseDbContext.User.SingleOrDefaultAsync(u => u.Email == email);
//        //        if (user == null)
//        //        {
//        //            return false; // Do not reveal that the user does not exist
//        //        }

//        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//        //        var callbackUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/ResetPassword?token={token}&email={user.Email}";

//        //        // Send email with this link
//        //        await _emailSender.SendEmailAsync(email, "Reset Password",
//        //            $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

//        //        return true;
//        //    }
//        //}


//        public async Task<UserDto> CreateUser(UserDto userDto)
//        {
//           // var users = await _expenseDbContext.User.FirstOrDefaultAsync(e=>e.Id ==  userDto.Id);
//            if(userDto == null)
//            {
//                throw new Exception(nameof(userDto));
//            }
//            var user = _mapper.Map<User>(userDto);
//            await _expenseDbContext.User.AddAsync(user);
//            await _expenseDbContext.SaveChangesAsync();
//            return _mapper.Map<UserDto>(user);
//        }

//        //public async Task<bool> ForgotPassword(string Email)
//        //{
//        //    var reset = await _expenseDbContext.User.FindAsync(Email);
//        //    if(reset != null)
//        //    {

//        //    }

//        //    throw new NotImplementedException();
//        //}

//        public  async Task<List<UserDto>> GetAllUser()
//        {
//            var users = await _expenseDbContext.User.ToListAsync();
//            return _mapper.Map<List<UserDto>>(users);
//        }

//    }
//}
