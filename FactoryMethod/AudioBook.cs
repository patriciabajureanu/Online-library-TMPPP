using OnlineLibrary.Builder;
using OnlineLibrary.Models;

namespace OnlineLibrary.FactoryMethod
{
     public class AudioBook : ILibraryItem
     {
          public string Title { get; }
          public string Author { get; }
          public string Description { get; }
          public string BookType { get; }
          public int Pages { get; }

          public AudioBook(string title, string author, string description, int pages)
          {
               Title = title;
               Author = author;
               Description = description;
               Pages = pages;
               BookType = "Audio";
          }

          public Book ToBook()
          {
               var builder = new BookBuilder();
               var director = new BookBuildDirector(builder);

               return director.BuildBook(Title, Author, Description, BookType, Pages);
          }
     }
}