using Microsoft.AspNetCore.SignalR;

namespace SignalR.Testing.WebApi.Hubs;

public class ChatHub : Hub<IChatResponse>, IChatHub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.ReceiveMessage(user, message);
    }
}

public interface IChatResponse
{
    Task ReceiveMessage(string user, string message);
}

public interface IChatHub
{
    Task SendMessage(string user, string message);
}
