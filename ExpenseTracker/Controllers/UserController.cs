using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using ExpenseTracker.Model;
using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace ExpenseTracker.Controllers
{
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository,IAuthenticationRepository authenticationRepository,ILogger<UserController>logger)
        {
            _userRepository = userRepository;
            _authenticationRepository = authenticationRepository;
            _logger = logger;
        }
        
        [HttpGet("GetAllUser")] 
        [Authorize]
        public async Task<object> Get()
        {
            try
            {
                _logger.LogInformation("Get All UserListing API");
                var user = await _userRepository.GetAllUser();
                return Ok(user);
            }catch(Exception ex)
            {
                _logger.LogInformation("faild to GetAll User API");
               return  StatusCode(500,ex.Message);
            }
        }
        [HttpPost("CreateUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> CreateDeposite([FromBody]UserDto userDto)
        {
            try
            {
                _logger.LogInformation("Create User API");
                var users = await _userRepository.CreateUser(userDto);
                return Ok(users);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("failed to CreateUser api");
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LogginDto logginDto)
        {
            try
            {
                _logger.LogInformation("Loggin Succefully");
                var token = await _authenticationRepository.CreateAuthentication(logginDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Failed to loggin..");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("Forgotpassword")]
        public async Task<ActionResult<string>> Forgot(ResetpasswordDto resetpasswordDto)
        {
            try
            {
                var result = await _userRepository.CreateForgotPassword(resetpasswordDto);
                if (!result)
                {
                    return BadRequest("Invalid email address.");
                }
                return Ok("Password reset link has been sent to your email.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Resetpassword")]
        public async Task<ActionResult<string>>Reset(ResetpasswordDto resetpasswordDto)
        {
            try
            {
                var Reset = await _userRepository.CreateForgotPassword(resetpasswordDto);
                return Ok(Reset);

            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
