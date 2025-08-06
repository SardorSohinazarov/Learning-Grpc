using Grpc.Core;
using GrpcServer;

namespace GrpcServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            for (int i = 1; i <= 5; i++)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name}, message {i}"
                });

                await Task.Delay(1000); // har 1 soniyada javob
            }
        }

        public override async Task Chat(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {
            // Chatdagi barcha kelgan xabarlarni o'qib, qaytarib yuboramiz
            await foreach (var message in requestStream.ReadAllAsync())
            {
                Console.WriteLine($"[Server oldi] {message.User}: {message.Text}");

                await responseStream.WriteAsync(new ChatMessage
                {
                    User = "Server",
                    Text = $"Qabul qildim: {message.Text}"
                });
            }
        }

    }
}
