using Grpc.Core;

namespace Server.DatabaseContext;

public class GreeterDatabaseContext
{
    private readonly Database_Operations.DatabaseContext _databaseContext;
    private readonly ILogger<GreeterDatabaseContext> _logger;

    public GreeterDatabaseContext(Database_Operations.DatabaseContext databaseContext,
        ILogger<GreeterDatabaseContext> logger)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

    public bool SayGreetingRepository(HelloRequest request, ServerCallContext context)
    {
        try
        {
            _databaseContext.Query<HelloReply>(
                $"INSERT INTO Greetings" +
                $"(NAME, TIME) " +
                $"VALUES" +
                $"('{request.Name}','{DateTime.Now:U}')");

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Executing query SayGreetings ");
        }

        return false;
    }

    public bool UpdatingRecordsRepository(Record request, ServerCallContext context)
    {
        try
        {
            _databaseContext.Query<UpdateResponseStatus>(
                $"UPDATE Greetings SET time = '{DateTime.Now:u}' WHERE Name = '{request.RecordName}';");

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in executing Query for UpdatingRecordsRepository ");
        }

        return false;
    }

    public bool DeletingRecordRepository(Record_deletion request, ServerCallContext context)
    {
        try
        {
            _databaseContext.Query<DeletionStatus>(
                $" DELETE FROM Greetings WHERE Name ='{request.RecordName}';");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in executing Query for DeletingRecordRepository ");
        }

        return false;
    }
}