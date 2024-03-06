using Grpc.Core;

namespace Server.Services;

public class AuthenticationService : Authentication.AuthenticationBase
{
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(ILogger<AuthenticationService> logger)
    {
        _logger = logger;
    }

    public override async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
    {
        var authenticationResponse = JwtAuthenticationManager.Authenticate(request);
        if (authenticationResponse == null)
        {
            throw new RpcException(new Status(statusCode: StatusCode.Unauthenticated, "Invalid user credentials"));
        }

        return authenticationResponse;
    }
}