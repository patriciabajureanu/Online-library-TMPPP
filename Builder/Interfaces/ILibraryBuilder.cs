namespace OnlineLibrary.Builder.Interfaces
{
     public interface ILibraryBuilder
     {
          void Reset();
          void BuildTitle(string title);
          void BuildAuthor(string author);
          void BuildSpecificDetail(int value);
     }
}