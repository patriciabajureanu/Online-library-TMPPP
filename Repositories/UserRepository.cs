using System.Linq;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.Repositories
{
     public class UserRepository : IUserRepository
     {
          public void Register(User user)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    db.Users.Add(user);
                    db.SaveChanges();
               }
          }

          public User GetByEmail(string email)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    return db.Users.FirstOrDefault(u => u.Email == email);
               }
          }
          public User GetByUsername(string username)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    return db.Users.FirstOrDefault(u => u.Username == username);
               }
          }
     }
}