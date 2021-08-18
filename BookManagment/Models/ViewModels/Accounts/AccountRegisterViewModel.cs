using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Range(18,30,ErrorMessage ="the age must be between {1} and {2} years old")]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DisplayName("your phone number")]
        
        public int Telephone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(16,MinimumLength =8)]
        public string Password { get; set; }
        [DisplayName("Confirm your password")]
        [Compare("Password",ErrorMessage ="Confirmation Password Does't match with previous password")]
        public string ConfirmPassword { get; set; }

        public List<SelectListItem> Genders { get; } = new List<SelectListItem>
        { 
             new SelectListItem{Value="M",Text="Male"},
              new SelectListItem{Value="F",Text="Female"}
        };
    }
}
