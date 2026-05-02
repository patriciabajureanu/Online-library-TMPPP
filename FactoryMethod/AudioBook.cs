using System;
using OnlineLibrary.Models;

namespace OnlineLibrary.FactoryMethod
{
     public class AudioBook : ILibraryItem
     {
          public string Title { get; private set; }
          public string Description { get; private set; }
          public string BookType { get; private set; }

          public AudioBook(string title, string description)
          {
               Title = title;
               Description = description;
               BookType = "Audio";
          }

          public Book ToBook()
          {
               return new Book
               {
                    Title = Title,
                    Description = Description,
                    BookType = BookType,
                    Language = "English",
                    PublishedYear = 2024,
                    TotalCopies = 999,
                    AvailableCopies = 999,
                    CreatedAt = DateTime.Now
               };
          }
     }
}