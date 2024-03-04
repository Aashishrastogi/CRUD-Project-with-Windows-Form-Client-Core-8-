using Google.Protobuf;
using Grpc.Core;
using Server;
using Server.Database_Operations;

namespace Server.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<GreeterService> _logger;

    public override async Task RequestAllData(
        DataRequest request,
        IServerStreamWriter<ResponseData> responseStream,
        ServerCallContext context)
    {
        var datas = _databaseContext.Query<ResponseData>("SELECT * FROM Greetings");

        foreach (ResponseData entry in datas)
        {
            await Task.Delay(500);
            await responseStream.WriteAsync(
                new ResponseData
                {
                    Name = $"{entry.Name}",
                    Time = $"{entry.Time}"
                });
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
            $" ('{request.Name}','{DateTime.Now:U}')");

        _logger.LogInformation("SayGreetings inserted into database successfully ");
        return Task.FromResult(new HelloReply
        {
            Message = $"Greeting from server {request.Name}!"
        });
    }

    /*public override Task<UpdateStatus> UpdateData(
        RequestUpdate request,
        ServerCallContext context
    )
    {
        _databaseContext.Query<UpdateStatus>(
            $" UPDATE Greetings " +
            $"SET time = {DateTime.Now:U} " +
            $"WHERE Name = {request.Name}"
            );

        return Task.FromResult(new UpdateStatus
        {
            Status = $"Time of Recording Updated for Name {request.Name}"
        });
    }*/
    public override Task<UpdateResponseStatus> UpdatingRecords(Record request, ServerCallContext context)
    {
        _databaseContext.Query<UpdateResponseStatus>(
            $"UPDATE Greetings SET time = '{DateTime.Now:u}' WHERE Name = '{request.RecordName}';");


        _logger.LogInformation(
            $"Record naming {request.RecordName} Time update by the server  with time - {DateTime.Now:u}");


        return Task.FromResult(new UpdateResponseStatus
        {
            Status = $"Time of Recording Updated for Name {request.RecordName}"
        });
    }
}