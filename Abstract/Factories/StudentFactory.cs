using OnlineLibrary.Abstract.Interfaces;
using OnlineLibrary.Abstract.Products;

namespace OnlineLibrary.Abstract.Factories
{
     public class StudentFactory : IUserFactory
     {
          public IUser CreateUser(string name) => new Student(name);

          public ILoan CreateLoan(string bookTitle) => new StudentLoan(bookTitle);
     }
}