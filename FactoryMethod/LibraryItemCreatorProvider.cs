using System;

namespace OnlineLibrary.FactoryMethod
{
     public static class LibraryItemCreatorProvider
     {
          public static LibraryItemCreator GetCreator(string bookType)
          {
               switch (bookType)
               {
                    case "Printed":
                         return new PrintedBookCreator();

                    case "Digital":
                         return new DigitalBookCreator();

                    case "Audio":
                         return new AudioBookCreator();

                    default:
                         throw new ArgumentException("Invalid book type.");
               }
          }
     }
}