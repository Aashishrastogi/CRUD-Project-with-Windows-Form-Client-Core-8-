using Grpc.Core;

namespace Server.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly Database_Operations.DatabaseContext _databaseContext;
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger, Database_Operations.DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public override async Task RequestAllData(
        DataRequest request,
        IServerStreamWriter<ResponseData> responseStream,
        ServerCallContext context)
    {
        var dataRecievedFromDatabase = _databaseContext.Query<ResponseData>("SELECT * FROM Greetings");

        foreach (ResponseData entry in dataRecievedFromDatabase)
        {
          //  await Task.Delay(5000);
            await responseStream.WriteAsync(
                new ResponseData
                {
                    Name = $"{entry.Name}",
                    Time = $"{entry.Time}"
                });
        }
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
            $"('{request.Name}','{DateTime.Now:U}')");

        _logger.LogInformation("SayGreetings inserted into database successfully ");
        return Task.FromResult(new HelloReply
        {
            Message = $"Greeting from server {request.Name}!"
        });
    }

    public override Task<UpdateResponseStatus> UpdatingRecords(Record request, ServerCallContext context)
    {
        _databaseContext.Query<UpdateResponseStatus>(
            $"UPDATE Greetings SET time = '{DateTime.Now:u}' WHERE Name = '{request.RecordName}';");


        _logger.LogInformation(
            $@"Record naming {request.RecordName} Time update by the server  with time - {DateTime.Now:u}");


        return Task.FromResult(
            new UpdateResponseStatus
            {
                Status = $"Time of Recording Updated for Name {request.RecordName}"
            });
    }

    public override Task<DeletionStatus> DeletingRecord(Record_deletion request, ServerCallContext context)
    {
        _databaseContext.Query<DeletionStatus>(
            $" DELETE FROM Greetings WHERE Name ='{request.RecordName}';");

        _logger.LogInformation($"Record  naming {request.RecordName} deleted from database ");

        return Task.FromResult(
            new DeletionStatus
            {
                DeletionResponseStatus = $"Record naming {request.RecordName} deleted successfully"
            });
    }
}