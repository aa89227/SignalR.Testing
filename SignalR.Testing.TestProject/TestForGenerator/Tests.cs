namespace SignalR.Testing.TestProject.TestForGenerator;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task RecieveMessage()
    {
        // Arrange
        var server = new TestServer();
        var connection = server.CreateSignalRTestConnection();
        
        await connection.StartAsync();

        // Act
        await connection.Requests.SendMessage("user", "message");

        // Assert
        connection.Events.Pop().ReceiveMessage((user, message) => user == "user");
    }

    [Test]
    public async Task ReciveMessage2()
    {
        // Arrange
        var server = new TestServer();
        var connection = await server.CreateTypedHubConnection();

        // Act
        string actualUser = "";
        string actualMessage = "";
        connection.ReceiveMessageHandler += (user, message) =>
        {
            actualUser = user;
            actualMessage = message;
        };
        await connection.SendMessage("user", "message");
        
        // Assert
        await Task.Delay(200);
        Assert.That(actualUser, Is.EqualTo("user"));
        Assert.That(actualMessage, Is.EqualTo("message"));
    }
}