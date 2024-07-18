using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IMapper _mapper;
        private readonly ExpenseDbContext _expenseDbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(IMapper mapper,ExpenseDbContext expenseDbContext,IConfiguration configuration)
        {
            _mapper = mapper;
            _expenseDbContext = expenseDbContext;
            _configuration = configuration;
        }
        public async Task<string> CreateAuthentication(LogginDto logginDto)
        {
            try
            {
                var expense = _expenseDbContext.User.Where(u => u.Email == logginDto.Email && u.Password == logginDto.Password).Count();

                if (expense == 0)
                {
                    throw new UnauthorizedAccessException("Invalid credentials");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,logginDto.Email),

                };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
 }

