using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Books")]
     public class Book
     {
          public int Id { get; set; }

          public string Title { get; set; }

          public string Description { get; set; }

          public string ISBN { get; set; }

          public int PublishedYear { get; set; }

          public string Language { get; set; }

          public int TotalCopies { get; set; }

          public int AvailableCopies { get; set; }

          public string CoverImageUrl { get; set; }

          public string PdfUrl { get; set; }

          public int? CategoryId { get; set; }

          public virtual Category Category { get; set; }

          public int? PublisherId { get; set; }

          public DateTime? CreatedAt { get; set; }
          public string BookType { get; set; }
     }
}