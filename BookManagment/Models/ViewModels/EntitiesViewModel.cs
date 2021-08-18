using BookManagment.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagment.Models.ViewModels
{
    public class EntitiesViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string BookName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DisplayName("Number Of Page")]
        public int PageNumber { get; set; }
        public Authors Author { get; set; }

        [DisplayName("Cover")]
        public string ImageUrl { get; set; }
        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }
        [DisplayName("Select Author")]
        public int AuthorID { get; set; }
        public List<Authors> AuthList { get; set; }
        public IFormFile File { get; set; }

    }
}
