using System;
using OnlineLibrary.Models;

namespace OnlineLibrary.FactoryMethod
{
     public class PrintedBook : ILibraryItem
     {
          public string Title { get; private set; }
          public string Description { get; private set; }
          public string BookType { get; private set; }

          public PrintedBook(string title, string description)
          {
               Title = title;
               Description = description;
               BookType = "Printed";
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
                    TotalCopies = 1,
                    AvailableCopies = 1,
                    CreatedAt = DateTime.Now
               };
          }
     }
}