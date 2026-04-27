using System;

namespace OnlineLibrary.Iterator
{
     public class Loan
     {
          public string Id { get; set; }
          public string UserId { get; set; }
          public string BookTitle { get; set; }
          public DateTime BorrowDate { get; set; }
          public DateTime DueDate { get; set; }
     }
}