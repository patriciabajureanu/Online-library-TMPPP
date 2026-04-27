using System.Data.Entity;
using OnlineLibrary.Models;

namespace OnlineLibrary.Data
{
     public class OnlineLibraryDbContext : DbContext
     {
          public OnlineLibraryDbContext()
              : base("OnlineLibraryConnection")
          {
               Database.SetInitializer<OnlineLibraryDbContext>(null);
          }

          public DbSet<BookDb> Books { get; set; }
     }
}