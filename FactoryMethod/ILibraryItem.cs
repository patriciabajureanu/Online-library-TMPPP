using OnlineLibrary.Models;

namespace OnlineLibrary.FactoryMethod
{
     public interface ILibraryItem
     {
          string Title { get; }
          string Description { get; }
          string BookType { get; }

          Book ToBook();
     }
}