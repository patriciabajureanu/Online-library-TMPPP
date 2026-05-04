namespace OnlineLibrary.Mediator
{
     public class FilterComponent
     {
          private IMediator _mediator;

          public FilterComponent(IMediator mediator)
          {
               _mediator = mediator;
          }

          public void SelectFilter(string filter)
          {
               _mediator.Notify(this, "filter", filter);
          }
     }
}