using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.ViewModels
{
    public class ProfilViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }
        [DisplayName("Confirm your password")]
        [Compare("Password", ErrorMessage = "Confirmation Password Does't match with previous password")]
        public string ConfirmPassword { get; set; }
        public IFormFile File { get; set; }
    }
}
