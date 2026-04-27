using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Books")]
     public class BookDb
     {
          public int Id { get; set; }
          public string Title { get; set; }
          public string Description { get; set; }
          public int TotalCopies { get; set; }
          public int AvailableCopies { get; set; }
     }
}