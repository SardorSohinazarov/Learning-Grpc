// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

Console.WriteLine("Client ishladi...");

var channel = GrpcChannel.ForAddress("https://localhost:7243");
var client = new Greeter.GreeterClient(channel);

var request = new HelloRequest { Name = "Sardor" };

// Streaming so‘rov
using var call = client.SayHelloStream(request);

await foreach (var reply in call.ResponseStream.ReadAllAsync())
{
    Console.WriteLine("Serverdan: " + reply.Message);
}

Console.WriteLine("Stream tugadi");
