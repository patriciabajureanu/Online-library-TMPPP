
namespace OnlineLibrary.AbstractFactory
{
     public class Professor : IUser
     {
          private readonly string _name;

          public Professor(string name)
          {
               _name = name;
          }

          public string GetName()
          {
               return _name;
          }

          public string GetUserType()
          {
               return "Professor";
          }
     }
}