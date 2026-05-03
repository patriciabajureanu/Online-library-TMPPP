using OnlineLibrary.Data;
using OnlineLibrary.Models;
using System.Linq;

namespace OnlineLibrary.Facade
{
     public class UserRepository
     {
          public User GetUser(string userId)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    return db.Users.FirstOrDefault(u => u.Email == userId);
               }
          }
     }
}