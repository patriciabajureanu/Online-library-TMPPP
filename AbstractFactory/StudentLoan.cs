namespace OnlineLibrary.AbstractFactory
{
     public class StudentLoan : ILoan
     {
          private readonly string _bookTitle;

          public StudentLoan(string bookTitle)
          {
               _bookTitle = bookTitle;
          }

          public int GetLoanDays()
          {
               return 14;
          }

          public string GetDetails()
          {
               return $"Student loan for {_bookTitle} - 14 days";
          }
     }
}