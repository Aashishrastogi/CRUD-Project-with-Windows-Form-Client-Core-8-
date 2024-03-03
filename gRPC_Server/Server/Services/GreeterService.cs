using Google.Protobuf;
using Grpc.Core;
using Server;
using Server.Database_Operations;

namespace Server.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<GreeterService> _logger;

    public override async Task RequestAllData(DataRequest request, IServerStreamWriter<Data> responseStream,
        ServerCallContext context)
    {
        var datas = _databaseContext.Query<Data>("SELECT * FROM Greetings");
       
        foreach (Data entry in datas)
        {
            await responseStream.WriteAsync(new Data { Name = $"{entry.Name}"});
        }
    }

    public GreeterService(ILogger<GreeterService> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override Task<HelloReply> SayGreetings(HelloRequest request, ServerCallContext context)
    {
        _databaseContext.Query<HelloReply>(
            $"INSERT INTO Greetings" +
            $"(NAME, TIME) " +
            $"VALUES" +
            $" ('{request.Name}','{DateTime.UtcNow.TimeOfDay}')");

        _logger.LogInformation("SayGreetings inserted into database successfully ");
        return Task.FromResult(new HelloReply
        {
            Message = "Greeting from Server " + request.Name
        });
    }
}