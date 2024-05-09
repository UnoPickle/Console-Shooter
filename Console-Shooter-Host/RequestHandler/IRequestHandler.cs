namespace Console_Shooter_Host.RequestHandler;

public interface IRequestHandler
{
    public bool IsRequestRelevant(Request request);

    public void HandleRequest(Client client, Request request);
}