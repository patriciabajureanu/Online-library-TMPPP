namespace OnlineLibrary.AbstractFactory
{
     public class ProfessorFactory : IUserFactory
     {
          public IUser CreateUser(string name)
          {
               return new Professor(name);
          }

          public ILoan CreateLoan(string bookTitle)
          {
               return new ProfessorLoan(bookTitle);
          }
     }
}