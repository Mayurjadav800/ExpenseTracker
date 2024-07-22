namespace ExpenseTracker.Dto
{
    public class ExpensFilterDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public TimeRange? TimeRange { get; set; }
        public bool ShortByOrderDescending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2; 
    }
}   

namespace ExpenseTracker.Dto
{
    public enum TimeRange
    {
        LastWeek,
        LastMonth,
        LastThreeMonths
    }
}