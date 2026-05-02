
namespace OnlineLibrary.AbstractFactory
{
     public class Student : IUser
     {
          private readonly string _name;

          public Student(string name)
          {
               _name = name;
          }

          public string GetName()
          {
               return _name;
          }

          public string GetUserType()
          {
               return "Student";
          }
     }
}