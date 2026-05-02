namespace OnlineLibrary.AbstractFactory
{
     public interface IUserFactory
     {
          IUser CreateUser(string name);
          ILoan CreateLoan(string bookTitle);
     }
}