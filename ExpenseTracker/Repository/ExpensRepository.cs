using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> DeleteExpense(int id)
        {
            var expense =  await _expenseDbContext.Expense.FindAsync(id);
            if(expense != null)
            {
                _expenseDbContext.Remove(expense);
                await _expenseDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("Incorrect expense Id passed.");
            }
            
        }
        public async Task<List<ExpensDto>> GetAllExpense(ExpensFilterDto expensFilterDto)
        {
            var expenses = await _expenseDbContext.Expense.ToListAsync();
            expenses = ApplyFilter(expenses, expensFilterDto);
            var expensDto = _mapper.Map<List<ExpensDto>>(expenses);
            return expensDto;
        }
        private List<Expense> ApplyFilter(List<Expense> expenses, ExpensFilterDto expensFilterDto)
        {
            if (expensFilterDto.From.HasValue && expensFilterDto.To.HasValue)
            {
                expenses = expenses.Where(e => e.CreatedAt >= expensFilterDto.From.Value && e.CreatedAt <= expensFilterDto.To.Value).ToList();
            }
            else if (expensFilterDto.TimeRange.HasValue)
            {
                DateTime startDate = GetStartDateForTimeRange(expensFilterDto.TimeRange.Value);
                expenses = expenses.Where(e => e.CreatedAt >= startDate).ToList();
            }
            if (expensFilterDto.ShortByOrderDescending)
            {
                expenses = expenses.OrderByDescending(e => e.ExpenseAmount).ToList();
            }
            expenses =expenses.Skip((expensFilterDto.PageNumber - 1)* expensFilterDto.PageSize)
                .Take(expensFilterDto.PageSize).ToList();
             return expenses;
        }
        private DateTime GetStartDateForTimeRange(TimeRange timeRange)
        {
            DateTime startDate = DateTime.UtcNow;

            switch (timeRange)
            {
                case TimeRange.LastWeek:
                    startDate = DateTime.UtcNow.AddDays(-7);
                    break;
                case TimeRange.LastMonth:
                    startDate = DateTime.UtcNow.AddMonths(-1);
                    break;
                case TimeRange.LastThreeMonths:
                    startDate = DateTime.UtcNow.AddMonths(-3);
                    break;
            }
            return startDate;
        }
        public async Task<ExpensDto> GetExpenseById(int id)
        {
            var expens =  await _expenseDbContext.Expense.FindAsync(id);
            if(expens == null)
            {
                throw new ArgumentException("Expense Id is Not Found");
            }
            //var expens = _expenseDbContext.Expense.Where(e => e.Id == id).FirstOrDefault();
            var expensDto = _mapper.Map<ExpensDto>(expens);
            return _mapper.Map<ExpensDto>(expensDto);
        }
        public async Task<ExpensDto> UpdateExpense(ExpensDto expensDto)
        {
            if (expensDto == null)
            {
                throw new ArgumentException(nameof(expensDto));
            }
            var existingExpens = await _expenseDbContext.Expense.FindAsync(expensDto.Id);
            var updateExpens =_mapper.Map(expensDto, existingExpens);
             _expenseDbContext.Update(updateExpens);
            await _expenseDbContext.SaveChangesAsync();
            return _mapper.Map<ExpensDto>(updateExpens);
        }
    }
}


