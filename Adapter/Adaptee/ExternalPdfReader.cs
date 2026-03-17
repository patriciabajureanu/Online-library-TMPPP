using System;

namespace OnlineLibrary.Adapter.Adaptee
{
     public class ExternalPdfReader
     {
          public void LoadDocument(string path)
          {
               Console.WriteLine($"[PDF SDK] Loading document: {path}");
          }

          public void JumpTo(int pageNumber)
          {
               Console.WriteLine($"[PDF SDK] Jumping to page {pageNumber}");
          }

          public void Exit()
          {
               Console.WriteLine("[PDF SDK] Closing document");
          }
     }
}