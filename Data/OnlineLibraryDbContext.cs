using System.Data.Entity;
using OnlineLibrary.Models;

namespace OnlineLibrary.Data
{
     public class OnlineLibraryDbContext : DbContext
     {
          public OnlineLibraryDbContext()
              : base("OnlineLibraryDb")
          {
               Database.SetInitializer<OnlineLibraryDbContext>(null);
          }

          public DbSet<Book> Books { get; set; }
          public DbSet<User> Users { get; set; }
          public DbSet<Borrowing> Borrowings { get; set; }
          public DbSet<Reservation> Reservations { get; set; }
          public DbSet<Author> Authors { get; set; }
          public DbSet<Category> Categories { get; set; }
          public DbSet<Review> Reviews { get; set; }
          public DbSet<Loan> Loans { get; set; }
     }
}