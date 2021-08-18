using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.ViewModels.Accounts
{
    public class AccountLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }
        [DisplayName("Remember Me")]
        public Boolean RememberMe { get; set; }
    }
}
