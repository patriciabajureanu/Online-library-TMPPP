using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Reviews")]
     public class Review
     {
          [Key]
          public int Id { get; set; }

          public int UserId { get; set; }

          public int BookId { get; set; }

          public int Rating { get; set; }

          public string Comment { get; set; }

          public DateTime CreatedAt { get; set; }
     }
}