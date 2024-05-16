namespace Console_Shooter_Host;

public class ClientManager
{
    private Dictionary<Guid, Client> _connectedClients = new();

    public Guid Add(Client client)
    {
        var clientGuid = Guid.NewGuid();
        _connectedClients.Add(clientGuid, client);

        return clientGuid;
    }

    public void Remove(Guid clientId)
    {
        _connectedClients.Remove(clientId);
    }

    public Client GetClient(Guid clientId)
    {
        return _connectedClients[clientId];
    }

    public void UpdateRequestHandler(Guid clientId, IRequestHandler newHandler)
    {
        var client = _connectedClients[clientId];
        client.RequestHandler = newHandler;

        _connectedClients[clientId] = client;
    }
}