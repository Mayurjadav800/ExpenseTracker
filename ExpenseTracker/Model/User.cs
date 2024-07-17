using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
