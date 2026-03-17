using OnlineLibrary.Abstract.Interfaces;

namespace OnlineLibrary.Abstract.Products
{
     public class StudentLoan : ILoan
     {
          private readonly string _bookTitle;

          public StudentLoan(string bookTitle)
          {
               _bookTitle = bookTitle;
          }

          public string GetDetails() => $"Student loan for book: {_bookTitle}";
     }
}