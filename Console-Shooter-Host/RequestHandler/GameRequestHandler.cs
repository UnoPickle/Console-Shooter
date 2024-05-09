namespace Console_Shooter_Host.RequestHandler;

public class GameRequestHandler(Server server, Player player) :  IRequestHandler
{
    public bool IsRequestRelevant(Request request)
    {
        throw new NotImplementedException();
    }

    public void HandleRequest(Client client, Request request)
    {
        throw new NotImplementedException();
    }
}