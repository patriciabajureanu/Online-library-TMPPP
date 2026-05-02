using System.Collections.Generic;

namespace OnlineLibrary.Prototype
{
     public class ReportTemplate : IPrototype
     {
          public string Title { get; set; }
          public string HeaderColor { get; set; }
          public List<string> Sections { get; set; }

          public ReportTemplate()
          {
               Sections = new List<string>();
          }

          public ReportTemplate(ReportTemplate prototype)
          {
               Title = prototype.Title;
               HeaderColor = prototype.HeaderColor;

               // clonare separată ca să nu modificăm lista originală
               Sections = new List<string>(prototype.Sections);
          }

          public virtual IPrototype Clone()
          {
               return new ReportTemplate(this);
          }
     }
}