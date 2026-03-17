using OnlineLibrary.Prototype.Models;
using System.Collections.Generic;
using OnlineLibrary.Prototype.Interfaces;

namespace OnlineLibrary.Prototype.Registry
{
     public class ReportTemplate : IPrototype
     {
          public string Title { get; set; }
          public string HeaderColor { get; set; }
          public List<string> Sections { get; set; }

          // Constructor normal
          public ReportTemplate(string title, string headerColor, List<string> sections)
          {
               Title = title;
               HeaderColor = headerColor;
               Sections = sections;
          }

          // Copy constructor
          public ReportTemplate(ReportTemplate prototype)
          {
               Title = prototype.Title;
               HeaderColor = prototype.HeaderColor;
               Sections = new List<string>(prototype.Sections);
          }

          // Clone method
          public virtual IPrototype Clone()
          {
               return new ReportTemplate(this);
          }
     }

}