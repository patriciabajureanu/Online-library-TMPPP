using OnlineLibrary.FactoryMethod;
using OnlineLibrary.Models;

namespace OnlineLibrary.Visitor
{
     public interface IVisitor
     {
          void VisitEbook(Ebook ebook);
          void VisitAudiobook(Audiobook audiobook);
          void VisitMagazine(Magazine magazine);
     }
}