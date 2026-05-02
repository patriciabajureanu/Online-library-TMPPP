using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Loans")]
     public class Loan
     {
          public int Id { get; set; }

          public int BookId { get; set; }
          public virtual Book Book { get; set; }

          public string UserEmail { get; set; }

          public DateTime BorrowDate { get; set; }

          public DateTime? ReturnDate { get; set; }

          public bool IsReturned { get; set; }
     }
}