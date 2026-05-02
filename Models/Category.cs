using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Categories")]
     public class Category
     {
          public int Id { get; set; }

          public string Name { get; set; }
     }
}