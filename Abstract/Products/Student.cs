using OnlineLibrary.Abstract.Interfaces;

namespace OnlineLibrary.Abstract.Products
{
     public class Student : IUser
     {
          private readonly string _name;

          public Student(string name)
          {
               _name = name;
          }

          public string GetName() => _name;

          public string GetUserType() => "Student";
     }
}