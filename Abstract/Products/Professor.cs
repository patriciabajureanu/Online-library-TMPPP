using OnlineLibrary.Abstract.Interfaces;

namespace OnlineLibrary.Abstract.Products
{
     public class Professor : IUser
     {
          private readonly string _name;

          public Professor(string name)
          {
               _name = name;
          }

          public string GetName() => _name;

          public string GetUserType() => "Professor";
     }
}