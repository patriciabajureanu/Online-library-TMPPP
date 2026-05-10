using System.Collections.Generic;

namespace OnlineLibrary.Visitor
{
     public class PreviewVisitor : IVisitor
     {
          public List<string> Results { get; private set; }

          public PreviewVisitor()
          {
               Results = new List<string>();
          }

          public void VisitEbook(Ebook ebook)
          {
               Results.Add("Preview for ebook: first pages of \"" + ebook.Title + "\".");
          }

          public void VisitAudiobook(Audiobook audiobook)
          {
               Results.Add("Preview for audiobook: first 2 minutes of \"" + audiobook.Title + "\".");
          }

          public void VisitMagazine(Magazine magazine)
          {
               Results.Add("Preview for magazine: cover and contents of \"" + magazine.Title + "\".");
          }
     }
}