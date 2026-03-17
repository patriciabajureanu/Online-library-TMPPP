using OnlineLibrary.Abstract.Interfaces;

namespace OnlineLibrary.Abstract.Products
{
     public class ProfessorLoan : ILoan
     {
          private readonly string _bookTitle;

          public ProfessorLoan(string bookTitle)
          {
               _bookTitle = bookTitle;
          }

          public string GetDetails() => $"Professor loan for book: {_bookTitle}";
     }
}