using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.ViewModels
{
    public class RoleSViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
