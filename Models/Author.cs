using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Authors")]
     public class Author
     {
          [Key]
          public int Id { get; set; }

          [Required]
          public string Name { get; set; }
     }
}