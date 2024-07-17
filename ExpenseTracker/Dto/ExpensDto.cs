using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Dto
{
    public class ExpensDto
    {
        public int Id { get; set; }
   
        public int ExpenseAmount { get; set; }
      
        public string Description { get; set; }
       
        public int UserId { get; set; }
   
        public DateTime CreatedAt { get; set; }
    }
}
