using System;
using System.ComponentModel;

namespace BookManagment.Models.Entities
{
    public class Books:EntityBase
    {
        [DisplayName("Name")]
        public string BookName { get; set; }
        public string Description { get; set; }
        [DisplayName("Number Of Page")]
        public int PageNumber { get; set; }
        public Authors Author { get; set; }
        [DisplayName("Cover")]
        public string ImageUrl { get; set; }
        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }
    }
}
