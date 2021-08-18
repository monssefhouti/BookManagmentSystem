using System.ComponentModel;

namespace BookManagment.Models.Entities
{
    public class Authors : EntityBase
    {
        
        public string Name { get; set; }
        [DisplayName("Total number of books")]
        public int BooksNumber { get; set; }
    }
}
