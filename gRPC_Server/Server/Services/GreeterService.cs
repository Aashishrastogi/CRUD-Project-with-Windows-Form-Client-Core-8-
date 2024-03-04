using Grpc.Core;
using Server.DatabaseContext;

namespace Server.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly Database_Operations.DatabaseContext _databaseContext;
    private readonly ILogger<GreeterService> _logger;
    private readonly GreeterDatabaseContext _repository;

    public GreeterService(ILogger<GreeterService> logger, Database_Operations.DatabaseContext databaseContext,
        GreeterDatabaseContext repository)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _repository = repository;
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

    public override Task<HelloReply>? SayGreetings(HelloRequest request, ServerCallContext context)
    {
        Task<HelloReply>? returnVariables = null;
        bool status = _repository.SayGreetingRepository(request, context);

        try
        {
            if (status)
            {
                _logger.LogInformation("SayGreetings inserted into database successfully ");
                returnVariables = Task.FromResult(new HelloReply
                {
                    Message = $"Greeting from server {request.Name}!"
                });
            }
            else
            {
                _logger.LogError("Error in Executing query SayGreetings");
                returnVariables = Task.FromResult(new HelloReply
                {
                    Message = "Error in Pinging/Connecting the Database "
                });
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Sending Back the response for SayGreeting RPC");
        }

        return returnVariables;
    }

    public override Task<UpdateResponseStatus>? UpdatingRecords(Record request, ServerCallContext context)
    {
        Task<UpdateResponseStatus>? returningvariables = null;
        var status = _repository.UpdatingRecordsRepository(request, context);

        try
        {
            if (status)
            {
                _logger.LogInformation(
                    $@"Record naming {request.RecordName} Time update by the server  with time - {DateTime.Now:u}");

                returningvariables = Task.FromResult(new UpdateResponseStatus
                {
                    Status = $"Time of Recording Updated for Name {request.RecordName}"
                });
                return returningvariables;
            }
            else
            {
                _logger.LogError("Error in Query Execution");

                returningvariables = Task.FromResult(new UpdateResponseStatus
                {
                    Status = "Unreachable Database or incorrect query execution"
                });
            }
        }

        catch (Exception e)
        {
            _logger.LogError("error in executing UpdatingRecord RPC");
        }

        return returningvariables;
    }

    public override Task<DeletionStatus> DeletingRecord(Record_deletion request, ServerCallContext context)
    {
        Task<DeletionStatus>? returningvariables = null;
        var status = _repository.DeletingRecordRepository(request, context);

        try
        {
            if (status)
            {
                _logger.LogInformation(
                    $"Record  naming {request.RecordName} deleted from database ");

                returningvariables = Task.FromResult(
                    new DeletionStatus
                    {
                        DeletionResponseStatus = $"Record naming {request.RecordName} deleted successfully"
                    });
                return returningvariables;
            }
            else
            {
                _logger.LogError("Error in Query Execution");
                returningvariables = Task.FromResult(
                    new DeletionStatus
                    {
                        DeletionResponseStatus = "Unreachable Database or incorrect query execution"
                    });
            }
        }

        catch (Exception e)
        {
            _logger.LogError("error in executing DeletingRecord RPC");
        }
        return returningvariables;
    }
}
