﻿using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }
      
        public string Email { get; set; }
      
        public string Password { get; set; }
    }
}
