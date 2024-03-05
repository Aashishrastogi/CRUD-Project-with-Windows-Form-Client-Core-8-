using System.Data;
using System.Net.Sockets;
using Microsoft.Data.SqlClient;

namespace Server.Database_Operations;

using Dapper;

public class DatabaseContext
{
    private readonly IConfiguration _config;
    private readonly ILogger<DatabaseContext> _log;
    
    public DatabaseContext(ILogger<DatabaseContext> log, IConfiguration config)
    {
        _log = log;
        _config = config;
    }

    public IEnumerable<T> Query<T>(string sql, object parameters = null)
    {
        using var connection = CreateConnection();
        _log.LogInformation(" Fresh Database Connection Created");
        return connection.Query<T>(sql, parameters);
    }

    public int Execute(string sql, object parameters = null)
    {
        using var connection = CreateConnection();
        _log.LogInformation("Database Connection Created");
        return connection.Execute(sql, parameters);
    }

    private SqlConnection CreateConnection()
    {
        try
        {

            using (var connection = new SqlConnection(_config.GetValue<string>("connectionstrings")))
            { 
                connection.OpenAsync();
                if (connection.State is ConnectionState.Open or ConnectionState.Connecting)
                {
                  SqlConnection.ClearPool(connection);
                  _log.LogInformation("Database ping Successful.Clearing Ping Resources");
                }
                else
                {
                    throw new SocketException();
                }
             
            }

        }
        catch (SqlException ex)
        {
            _log.LogError("Database unreachable");
            
        }
        return new SqlConnection(_config.GetValue<string>("connectionstrings"));
    }

    public bool Preprocessing_Database(int operations)
    {
        switch (operations)
        {
            case 1:
                _log.LogInformation("Database cleaned as requested by the User");
                return true;
            default:
                return false;
        }
    }
}