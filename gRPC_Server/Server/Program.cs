using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server.Services;
using Serilog;
using Server.Database_Operations;
using Server.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", false);

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
            /* adding and setting the type of authentication for our server
               It basically describes that the server has to go through these barriers
               This a basic step in order to configure the JWT for the .net Server          */
            authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    // this is adding that the middleware will check for the JWT bearer and on what parameters will be defined inside
    // this is basically setting the field values of the classes which are utilized by the methods and they will act
    // accordingly
    .AddJwtBearer(jwtBearerOption =>
    {
        /*  This step basically tells and Defines how the JWT Barrier will Authenticate the received token
            In this we define on what parameters the middleware will check and verify and how  will it do it */
        
        // This means that it require all the https endpoints requests to have metadata .
        // still http will be accepted but https must have metadata
        jwtBearerOption.RequireHttpsMetadata = true;
        
        // This allows to store the keys in the Authentication properties for further use ..imagine the case this
        // is an authentication server and the only purpose is to forward teh request after the authentication in
        // that case you will have access of the token via Authentication properties with in the class can can forward
        // it ot another server or application
        jwtBearerOption.SaveToken = true;
        
        
        // At This stage we are configuring the checking mechanism of the JWt token which the application is going
        // it be receiving from the clients . this is the main step as this verifies that the token received is in fact
        // the same token which is generated form this very server (using the same Key).ref the JWT Token Structure
        // which make JWT unique And Unbreakable
        // we are configuring with the same parameters as we have done wile generating the token
        // i.e same SymmetricSecurityKey which used to make the token in the first place
        // ref the JwtAuthenticationManager Class in the solution Explorer.   
        jwtBearerOption.TokenValidationParameters = new TokenValidationParameters
            // this is setting the field values in the class
        {
            // this make the middle enable and will check the existence of signing key of every token received
            // from the client initially it will decrypt it the check it with the key which will be provided below
            // and then verify
            ValidateIssuerSigningKey = true,
            //  This define the key which is same key which is used to generate the token for the client upon the
            //  connection request ......ref the JwtAuthenticationManager
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes($"{builder.Configuration.GetValue(typeof(string), "JWT_TOKEN_KEY")}")),
            
            // setting it to true will make the middleware to check that the token is indeed coming from the same
            // client it was issued to not forwarded form a different client . both of these properties are set to
            // false for only development purposes as it throws error when tries on loopback IP
            ValidateIssuer = false,
            ValidateAudience = false,
            
            RequireExpirationTime = false, // to be changed when refresh token are implemented
            
            // check and mandates the lifetime of the JWT token received 
            ValidateLifetime = true
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
Console.WriteLine("Waiting Operation completed");
if (status != true) throw new Exception("Database Cleanup not successful");

#endregion

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();