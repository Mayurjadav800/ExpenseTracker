using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    public class ExpensRepository : IExpensRepository
    {
        private readonly IMapper _mapper;
        private readonly ExpenseDbContext _expenseDbContext;

        public ExpensRepository(IMapper mapper,ExpenseDbContext expenseDbContext)
        {
            _mapper = mapper;
            _expenseDbContext = expenseDbContext;
        }
        public async Task<ExpensDto> CreateExpense(ExpensDto expensDto)
        {
            if(expensDto == null)
            {
                throw new ArgumentException(nameof(expensDto));
            }
            var expens = _mapper.Map<Expense>(expensDto);
            await _expenseDbContext.Expense.AddAsync(expens);
            await _expenseDbContext.SaveChangesAsync();
            return _mapper.Map<ExpensDto>(expens);
            
        }

        public Task<List<ExpensDto>> GetAllExpense()
        {
            throw new NotImplementedException();
        }

        public Task<ExpensDto> GetExpenseById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
