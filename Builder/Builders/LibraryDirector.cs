using OnlineLibrary.Builder.Interfaces;

namespace OnlineLibrary.Builder.Director
{
     public class LibraryDirector
     {
          private ILibraryBuilder _builder;

          public LibraryDirector(ILibraryBuilder builder)
          {
               _builder = builder;
          }

          public void ChangeBuilder(ILibraryBuilder builder)
          {
               _builder = builder;
          }

          public void Make(string type)
          {
               _builder.Reset();
               switch (type)
               {
                    case "StandardBook":
                         _builder.BuildTitle("Default Book Title");
                         _builder.BuildAuthor("Default Author");
                         _builder.BuildSpecificDetail(200); // pages
                         break;
                    case "GenericMagazine":
                         _builder.BuildTitle("Default Magazine Title");
                         _builder.BuildAuthor("Default Publisher");
                         _builder.BuildSpecificDetail(1); // issue
                         break;
               }
          }
     }
}