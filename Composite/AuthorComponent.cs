using System.Collections.Generic;
using System.Linq;

namespace OnlineLibrary.Composite
{
     public class AuthorComponent : ILibraryComponent
     {
          public string Name { get; set; }
          private readonly List<ILibraryComponent> _children;

          public AuthorComponent(string name)
          {
               Name = name;
               _children = new List<ILibraryComponent>();
          }

          public void Add(ILibraryComponent component)
          {
               _children.Add(component);
          }

          public void Remove(ILibraryComponent component)
          {
               _children.Remove(component);
          }

          public List<ILibraryComponent> GetChildren()
          {
               return _children;
          }

          public string Display(int depth = 0)
          {
               var result = "<div class='author-node ms-4'>";
               result += "<h5>✍️ Author: " + Name + "</h5>";

               foreach (var child in _children)
               {
                    result += child.Display(depth + 1);
               }

               result += "</div>";

               return result;
          }

          public int GetTotalBooks()
          {
               return _children.Sum(c => c.GetTotalBooks());
          }
     }
}