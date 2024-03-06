using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server.Services;
using Serilog;
using Server.Database_Operations;
using Server.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", false);
// Add services to the container.

builder.Services.AddGrpc();

#region Serilog

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json"
        , true
        , true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

#region JWT Authentication and Authorization

builder.Services.AddAuthentication
    (authenticationOptions =>
        {
            authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(jwtBearerOption =>
    {
        jwtBearerOption.RequireHttpsMetadata = true;
        jwtBearerOption.SaveToken = true;
        jwtBearerOption.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes($"{builder.Configuration.GetValue(typeof(string), "JWT_TOKEN_KEY")}")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

#endregion

#region Singleton Dependencies

builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddSingleton<GreeterDatabaseContext>();

#endregion

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

#region gRPC ServicesRegistration

app.MapGrpcService<GreeterService>();
app.MapGrpcService<AuthenticationService>();

#endregion

#region CompileTime Database Operations

app.Services.GetRequiredService<GreeterDatabaseContext>();
var databaseServices = app.Services.GetRequiredService<DatabaseContext>();
var status = databaseServices.Preprocessing_Database(builder.Configuration.GetValue<int>("DatabaseOperations"));
if (status != true) throw new Exception("Database Cleanup not successful");

#endregion
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();