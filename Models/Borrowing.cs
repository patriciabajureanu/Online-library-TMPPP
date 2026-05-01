using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Borrowings")]
     public class Borrowing
     {
          [Key]
          public int Id { get; set; }

          public int UserId { get; set; }

          public int BookId { get; set; }

          public DateTime BorrowDate { get; set; }

          public DateTime DueDate { get; set; }

          public DateTime? ReturnDate { get; set; }

          public string Status { get; set; }

          public bool IsReturned { get; set; }
     }
}