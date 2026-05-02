namespace OnlineLibrary.AbstractFactory
{
     public class ProfessorLoan : ILoan
     {
          private readonly string _bookTitle;

          public ProfessorLoan(string bookTitle)
          {
               _bookTitle = bookTitle;
          }

          public int GetLoanDays()
          {
               return 30;
          }

          public string GetDetails()
          {
               return $"Professor loan for {_bookTitle} - 30 days";
          }
     }
}