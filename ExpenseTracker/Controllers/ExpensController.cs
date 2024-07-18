using ExpenseTracker.Dto;
using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpensController : ControllerBase
    {
        private readonly IExpensRepository _expensRepository;
        private readonly ILogger<ExpensController> _logger;

        public ExpensController(IExpensRepository expensRepository,ILogger<ExpensController>logger)
        {
            _expensRepository = expensRepository;
            _logger = logger;
        }
        [HttpPost("CreateExpense")]
        [Authorize]
        public async Task<ActionResult<ExpensDto>>Create([FromBody] ExpensDto expensDto)
        {
            try
            {
                _logger.LogInformation("api for the create response");
                var expense = await _expensRepository.CreateExpense(expensDto);
                return Ok(expense);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Failed to create the api");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetExpense")]
        [Authorize]
        public async Task<object> Get(ExpensFilterDto expensFilterDto)
        {
            try
            {
                _logger.LogInformation("api for the Getall the response");
                var expense = await _expensRepository.GetAllExpense(expensFilterDto);
                return Ok(expense);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Failed to GetALl response");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("ExpensGetById")]
        [Authorize]
        public async Task<object>GetById(int id)
        {
            try
            {
                _logger.LogInformation("API for the GetexpenssById");
                var expens = await _expensRepository.GetExpenseById(id);
                return Ok(expens);

            }catch(Exception ex)
            {
                _logger.LogInformation("Faild to Expens GetById");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UpdateExpens")]
        [Authorize]
        public async Task<object>Update([FromBody]ExpensDto expensDto)
        {
            try
            {
                _logger.LogInformation("api for Update the expenss");
                var expens = await _expensRepository.UpdateExpense(expensDto);
                return Ok(expens);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Failed to update the expens");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteExpenss")]
        [Authorize]
        public async Task<object>Delete(int id)
        {
            try
            {
                _logger.LogInformation("Delete api for the expenss");
                var expens = await _expensRepository.DeleteExpense(id);
                return Ok(expens);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("failed to delete the expens");
                return StatusCode(500, ex.Message);
            }
        }
    }
}