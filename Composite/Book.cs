using System;

namespace OnlineLibrary.Composite
{
     public class Book : LibraryComponent
     {
          public string Title { get; }
          public int Pages { get; }

          public Book(string title, int pages)
          {
               Title = title;
               Pages = pages;
          }

          public void Display(int depth = 0)
          {
               Console.WriteLine($"{new string(' ', depth * 4)}📖 {Title} — {Pages} pages");
          }

          public int GetTotalBooks() => 1;
     }
}