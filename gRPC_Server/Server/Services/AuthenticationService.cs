using Grpc.Core;

namespace Server.Services;

public class AuthenticationService : Authentication.AuthenticationBase
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IConfiguration _config;

    public AuthenticationService(ILogger<AuthenticationService> logger, IConfiguration config)
    {
        _config = config;
        _logger = logger;
    }

    public override async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request,
        ServerCallContext context)
    {
        var jwtAuthenticationManager = new JwtAuthenticationManager(_config);
        var authenticationResponse = jwtAuthenticationManager.Authenticate(request);
        if (authenticationResponse == null)
        {
            throw new RpcException(new Status(statusCode: StatusCode.Unauthenticated, "Invalid user credentials"));
        }

        return authenticationResponse;
    }
}