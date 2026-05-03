using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Authors")]
     public class Author
     {
          public int Id { get; set; }

          [Column("FullName")]
          public string FullName { get; set; }

          public string Biography { get; set; }
     }
}