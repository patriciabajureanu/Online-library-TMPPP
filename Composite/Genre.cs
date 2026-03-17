using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLibrary.Composite
{
     public class Genre : LibraryComponent
     {
          public string Name { get; }
          private readonly List<LibraryComponent> _children = new List<LibraryComponent>();
          public Genre(string name)
          {
               Name = name;
          }

          public void Add(LibraryComponent component) => _children.Add(component);
          public void Remove(LibraryComponent component) => _children.Remove(component);
          public List<LibraryComponent> GetChildren() => _children;

          public void Display(int depth = 0)
          {
               Console.WriteLine($"{new string(' ', depth * 4)}📚 Genre: {Name} — Total Books: {GetTotalBooks()}");
               foreach (var child in _children)
                    child.Display(depth + 1);
          }

          public int GetTotalBooks() => _children.Sum(c => c.GetTotalBooks());
     }
}