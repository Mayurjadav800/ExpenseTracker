using ExpenseTracker.Dto;

namespace ExpenseTracker.Repository
{
    public interface IExpensRepository
    {
        Task<List<ExpensDto>> GetAllExpense();
        Task<ExpensDto>GetExpenseById(int id);
        Task<ExpensDto> CreateExpense(ExpensDto expensDto);
    }
}
