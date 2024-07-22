using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Dto
{
    public class ResetpasswordDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Newpassword { get; set; }
    }
}
