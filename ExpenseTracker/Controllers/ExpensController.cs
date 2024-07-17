using ExpenseTracker.Dto;
using ExpenseTracker.Repository;
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
      //  [Authorize]
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
        [HttpGet("GetExpense")]
        //[Authorize]
        public async Task<object> Get(ExpensFilterDto expensFilterDto)
        {
            try
            {
                var expense = await _expensRepository.GetAllExpense(expensFilterDto);
                return Ok(expense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }
        [HttpGet("ExpensGetById")]
       // [Authorize]
        public async Task<object>GetById(int id)
        {
            try
            {
                var expens = await _expensRepository.GetExpenseById(id);
                return Ok(expens);

            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateExpens")]
        public async Task<object>Update([FromBody]ExpensDto expensDto)
        {
            try
            {
                var expens = await _expensRepository.UpdateExpense(expensDto);
                return Ok(expens);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteExpenss")]
        public async Task<object>Delete(int id)
        {
            try
            {
                var expens = await _expensRepository.DeleteExpense(id);
                return Ok(expens);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}