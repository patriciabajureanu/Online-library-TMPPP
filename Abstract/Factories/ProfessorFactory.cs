using OnlineLibrary.Abstract.Interfaces;
using OnlineLibrary.Abstract.Products;

namespace OnlineLibrary.Abstract.Factories
{
     public class ProfessorFactory : IUserFactory
     {
          public IUser CreateUser(string name) => new Professor(name);

          public ILoan CreateLoan(string bookTitle) => new ProfessorLoan(bookTitle);
     }
}