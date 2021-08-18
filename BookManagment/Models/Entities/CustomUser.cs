using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.Entities
{
    public class CustomUser:IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DisplayName("your phone number")]
        [StringLength(maximumLength:14,ErrorMessage ="Entre a phone number between {0} and {1} numbers",MinimumLength =10)]
        public int Telephone { get; set; }
    }
}
