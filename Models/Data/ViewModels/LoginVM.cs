using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangible_Africa.Models.Data.ViewModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
