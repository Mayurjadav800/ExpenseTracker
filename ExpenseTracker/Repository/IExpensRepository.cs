using ExpenseTracker.Dto;

namespace ExpenseTracker.Repository
{
    public interface IExpensRepository
    {
        Task<List<ExpensDto>> GetAllExpense(ExpensFilterDto expensFilterDto);
        Task<ExpensDto>GetExpenseById(int id);
        Task<ExpensDto> CreateExpense(ExpensDto expensDto);
        Task<ExpensDto> UpdateExpense(ExpensDto expensDto);
        Task<bool> DeleteExpense(int id);
    }
}
