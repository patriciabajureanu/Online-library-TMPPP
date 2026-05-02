using System;

namespace OnlineLibrary.AbstractFactory
{
     public static class UserFactoryProvider
     {
          public static IUserFactory GetFactory(string userType)
          {
               switch (userType)
               {
                    case "Student":
                         return new StudentFactory();

                    case "Professor":
                         return new ProfessorFactory();

                    default:
                         throw new ArgumentException("Invalid user type.");
               }
          }
     }
}