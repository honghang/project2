using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestLogin.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Confirm Password.")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
        ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
       

        
    }
}