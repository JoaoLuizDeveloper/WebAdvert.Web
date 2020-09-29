using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.Accounts
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Email is Required.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Code is Required.")]
        public string Code { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(6, ErrorMessage = "Password must be at least six characters long!")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[StringLength(6, ErrorMessage = "Password must be at least six characters long!")]
        [Compare("Password", ErrorMessage = "Password confirmation do not match")]
        [Display(Name = "Password confirmation")]
        public string ConfirmPassword { get; set; }

    }
}
