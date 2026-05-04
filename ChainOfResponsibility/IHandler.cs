namespace OnlineLibrary.ChainOfResponsibility
{
     public interface IHandler
     {
          IHandler SetNext(IHandler handler);
          AccessResult Handle(AccessRequest request);
     }
}