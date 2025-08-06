using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

var channel = GrpcChannel.ForAddress("https://localhost:7243");
var client = new Greeter.GreeterClient(channel);

using var call = client.Chat();

// Javoblarni olish — alohida Task
var readTask = Task.Run(async () =>
{
    await foreach (var serverMessage in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"[Serverdan] {serverMessage.User}: {serverMessage.Text}");
    }
});

// Xabar yuborish
for (int i = 1; i <= 3; i++)
{
    Console.Write("Xabar: ");
    var text = Console.ReadLine();

    await call.RequestStream.WriteAsync(new ChatMessage
    {
        User = "Sardor",
        Text = text
    });
}

await call.RequestStream.CompleteAsync();
await readTask;
