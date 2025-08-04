// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcServer;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("https://localhost:7243");
var client = new Greeter.GreeterClient(channel);

var reply = await client.SayHelloAsync(new HelloRequest { Name = "Sardor" });
Console.WriteLine("Javob: " + reply.Message);
