using Microsoft.Ajax.Utilities;

namespace OnlineLibrary.Visitor
{
     public interface ILibraryResource
     {
          void Accept(IVisitor visitor);
     }
}