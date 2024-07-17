using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public UserController(IUserRepository userRepository,IAuthenticationRepository authenticationRepository)
        {
            _userRepository = userRepository;
            _authenticationRepository = authenticationRepository;
        }
        
        [HttpGet("GetAllUser")] 
        [Authorize]
        public async Task<object> Get()
        {
            try
            {
                var user = await _userRepository.GetAllUser();
                return Ok(user);
            }catch(Exception ex)
            {
               return  StatusCode(500,ex.Message);
            }
        }
        [HttpPost("CreateUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> CreateDeposite([FromBody]UserDto userDto)
        {
            try
            {
                var users = await _userRepository.CreateUser(userDto);
                return Ok(users);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LogginDto logginDto)
        {
            try
            {
                var token = await _authenticationRepository.CreateAuthentication(logginDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
