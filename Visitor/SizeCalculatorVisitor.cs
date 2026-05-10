using System.Collections.Generic;

namespace OnlineLibrary.Visitor
{
     public class SizeCalculatorVisitor : IVisitor
     {
          public List<string> Results { get; private set; }

          public SizeCalculatorVisitor()
          {
               Results = new List<string>();
          }

          public void VisitEbook(Ebook ebook)
          {
               Results.Add("Ebook \"" + ebook.Title + "\" has file size: " + ebook.FileSize + " MB.");
          }

          public void VisitAudiobook(Audiobook audiobook)
          {
               Results.Add("Audiobook \"" + audiobook.Title + "\" has duration: " + audiobook.Duration + " minutes.");
          }

          public void VisitMagazine(Magazine magazine)
          {
               Results.Add("Magazine \"" + magazine.Title + "\" issue number: " + magazine.IssueNumber + ".");
          }
     }
}