using OnlineLibrary.Models;
using System;

namespace OnlineLibrary.Builder
{
     public class BookBuilder : IBookBuilder
     {
          private Book _result = new Book();

          public void Reset()
          {
               _result = new Book();
          }

          public void BuildTitle(string title)
          {
               _result.Title = title;
          }

          public void BuildAuthor(string author)
          {
               _result.Description = "Author: " + author;
          }

          public void BuildType(string bookType) // 🔥 FIX
          {
               _result.BookType = bookType;
          }

          public void BuildSpecificDetail(int pages)
          {
               _result.PublishedYear = 2024;
               _result.Language = "English";
               _result.TotalCopies = 1;
               _result.AvailableCopies = 1;
               _result.CreatedAt = System.DateTime.Now;

               _result.ISBN = "ISBN-" + System.Guid.NewGuid().ToString("N").Substring(0, 10);

               _result.Description += " | Pages: " + pages;
          }
          public void BuildDescription(string description)
          {
               _result.Description += " | " + description;
          }
          public Book GetResult()
          {
               return _result;
          }
     }
}