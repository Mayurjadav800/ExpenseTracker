using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Model
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ExpenseAmount { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        //virtual property
        public User User { get; set; }


    }
}
