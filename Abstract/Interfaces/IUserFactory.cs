namespace OnlineLibrary.Abstract.Interfaces
{
     public interface IUserFactory
     {
          IUser CreateUser(string name);
          ILoan CreateLoan(string bookTitle);
     }
}