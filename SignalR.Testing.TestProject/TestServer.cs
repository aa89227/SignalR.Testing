using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using SignalR.Client.Generator;
using SignalR.Test.Generator;
using SignalR.Testing.WebApi.Hubs;

namespace SignalR.Testing.TestProject;

internal class TestServer : WebApplicationFactory<Program>
{
    public TestHubConnection CreateSignalRTestConnection()
    {
        var url = new UriBuilder(Server.BaseAddress!)
        {
            Path = "/chathub"
        }.Uri;
        var connection = new HubConnectionBuilder()
            .WithUrl(url, options =>
            {
                options.Transports = HttpTransportType.ServerSentEvents;
                options.HttpMessageHandlerFactory = _ => Server.CreateHandler();
            });
        return new TestHubConnection(connection);
    }
    
    public async Task<TypedHubConnection> CreateTypedHubConnection()
    {
        var url = new UriBuilder(Server.BaseAddress!)
        {
            Path = "/chathub"
        }.Uri;
        var connection = new HubConnectionBuilder()
            .WithUrl(url, options =>
            {
                options.Transports = HttpTransportType.ServerSentEvents;
                options.HttpMessageHandlerFactory = _ => Server.CreateHandler();
            }).Build();
        await connection.StartAsync();
        return new TypedHubConnection(connection);
    }
}

[SignalRTest(typeof(ChatHub))]
public partial class TestHubConnection;

[TypedHubClient(typeof(ChatHub))]
public partial class TypedHubConnection;
