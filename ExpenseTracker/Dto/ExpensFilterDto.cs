namespace ExpenseTracker.Dto
{
    public class ExpensFilterDto
    {

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public TimeRange? TimeRange { get; set; }
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