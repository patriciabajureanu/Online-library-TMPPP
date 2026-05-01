using OnlineLibrary.Models;

namespace OnlineLibrary.Repositories
{
     public interface IUserRepository
     {
          void Register(User user);
          User GetByEmail(string email);
          User GetByUsername(string username);
     }
}