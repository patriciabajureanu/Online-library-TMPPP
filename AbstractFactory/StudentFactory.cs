namespace OnlineLibrary.AbstractFactory
{
     public class StudentFactory : IUserFactory
     {
          public IUser CreateUser(string name)
          {
               return new Student(name);
          }

          public ILoan CreateLoan(string bookTitle)
          {
               // 🔥 creează loan compatibil cu Student
               return new StudentLoan(bookTitle);
          }
     }
}