using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     public class ReadingProgress
     {
          [Key]
          public int Id { get; set; }

          public string UserEmail { get; set; }

          public int BookId { get; set; }

          public int CurrentPage { get; set; }

          public string Theme { get; set; }

          public string FontSize { get; set; }

          public DateTime SavedAt { get; set; }

          [ForeignKey("BookId")]
          public virtual Book Book { get; set; }
          public string SessionKey { get; set; }
          public int PreviousPage { get; set; }
     }
}