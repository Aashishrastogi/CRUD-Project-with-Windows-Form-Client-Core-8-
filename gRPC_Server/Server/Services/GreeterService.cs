using System.Diagnostics.CodeAnalysis;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Server.DatabaseContext;

namespace Server.Services;

[SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
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

    [Authorize(Roles = "Admin,User")]
    public override async Task RequestAllData(
        DataRequest request,
        IServerStreamWriter<ResponseData> responseStream,
        ServerCallContext context)
    {
        context.CancellationToken.ThrowIfCancellationRequested();
        var dataReceivedFromDatabase = _databaseContext.Query<ResponseData>("SELECT * FROM Greetings");
        try
        {
            foreach (ResponseData entry in dataReceivedFromDatabase)
            {
                //await Task.Delay(1000);
                if (!context.CancellationToken.IsCancellationRequested)
                {
                   await responseStream.WriteAsync(
                        new ResponseData
                        {
                            Name = $"{entry.Name}",
                            Time = $"{entry.Time}"
                        });
                }
            }
        }


        catch (RpcException responseException)
        {
            _logger.LogError(responseException.Message);
        }
        catch (OperationCanceledException cancellationTokenReceived)
        {
            _logger.LogInformation($"Cancellation token Was thrown By The client " +
                                   $"\n{cancellationTokenReceived.CancellationToken}" +
                                   $"\n{cancellationTokenReceived.Data}");
        }
    }

    [Authorize]
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    [Authorize(Roles = "Admin,User")]
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

    [Authorize(Roles = "Admin,User")]
    public override Task<UpdateResponseStatus>? UpdatingRecords(Record request, ServerCallContext context)
    {
        Task<UpdateResponseStatus>? returningVariables = null;
        var status = _repository.UpdatingRecordsRepository(request, context);

        try
        {
            if (status)
            {
                _logger.LogInformation(
                    $@"Record naming {request.RecordName} Time update by the server  with time - {DateTime.Now:u}");

                returningVariables = Task.FromResult(new UpdateResponseStatus
                {
                    Status = $"Time of Recording Updated for Name {request.RecordName}"
                });
                return returningVariables;
            }
            else
            {
                _logger.LogError("Error in Query Execution");

                returningVariables = Task.FromResult(new UpdateResponseStatus
                {
                    Status = "Unreachable Database or incorrect query execution"
                });
            }
        }

        catch (Exception e)
        {
            _logger.LogError($"error in executing UpdatingRecord RPC + {e.Message}");
        }

        return returningVariables;
    }

    [Authorize(Roles = "Admin")]
    public override Task<DeletionStatus> DeletingRecord(Record_deletion request, ServerCallContext context)
    {
        Task<DeletionStatus>? returningVariables = null;
        var status = _repository.DeletingRecordRepository(request, context);

        try
        {
            if (status)
            {
                _logger.LogInformation(
                    $"Record  naming {request.RecordName} deleted from database ");

                returningVariables = Task.FromResult(
                    new DeletionStatus
                    {
                        DeletionResponseStatus = $"Record naming {request.RecordName} deleted successfully"
                    });
                return returningVariables;
            }
            else
            {
                _logger.LogError("Error in Query Execution");
                returningVariables = Task.FromResult(
                    new DeletionStatus
                    {
                        DeletionResponseStatus = "Unreachable Database or incorrect query execution"
                    });
            }
        }

        catch (Exception e)
        {
            _logger.LogError($"error in executing DeletingRecord RPC +{e.Message}");
        }

        return returningVariables;
    }
}