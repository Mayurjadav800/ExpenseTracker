using ExpenseTracker.Dto;
using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpensController : ControllerBase
    {
        private readonly IExpensRepository _expensRepository;

        public ExpensController(IExpensRepository expensRepository)
        {
            _expensRepository = expensRepository;
        }
        [HttpPost("CreateExpense")]
        public async Task<ActionResult<ExpensDto>>Create([FromBody] ExpensDto expensDto)
        {
            try
            {
                var expense = await _expensRepository.CreateExpense(expensDto);
                return Ok(expense);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
       
    }
}