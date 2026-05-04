namespace OnlineLibrary.Mediator
{
     public class SearchComponent
     {
          private IMediator _mediator;

          public SearchComponent(IMediator mediator)
          {
               _mediator = mediator;
          }

          public void Search(string text)
          {
               _mediator.Notify(this, "search", text);
          }
     }
}