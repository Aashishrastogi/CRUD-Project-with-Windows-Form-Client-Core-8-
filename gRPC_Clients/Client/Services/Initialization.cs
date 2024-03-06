using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Server;

namespace Client.Services
{
    internal class Initialization
    {
        public Greeter.GreeterClient? Load(Action<string> connectionStatus,Action<AuthenticationResponse> authenticationResponseAction)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7157");

                if (PingCheck(channel) == "OK")
                {
                    var client = new Greeter.GreeterClient(channel);
                    try
                    {

                        var authenticationClient = new Authentication.AuthenticationClient(channel);

                        var authenticationResponse = authenticationClient.Authenticate(new AuthenticationRequest
                        {
                            Username = "admin",
                            Password = "admin"
                        });
                        authenticationResponseAction(authenticationResponse);

                        connectionStatus("Connection Established with the Server with Authenticated User Credentials");

                        return client;

                    }
                    catch (Exception authException)
                    {
                        connectionStatus("User credentials not Valid");
                    }
                }
                else
                {
                    throw new Exception("ServerUnreachable");
                }
            }
            catch (Exception e) when (e.Message == "ServerUnreachable")
            {
                connectionStatus("Server Unreachable");
            }

            return null;
        }

        private static string PingCheck(GrpcChannel channel)
        {
            try
            {
                var pingclient = new Greeter.GreeterClient(channel);

                pingclient.SayHelloAsync(new HelloRequest { Name = "dummy" });

                return "OK";
            }
            catch (RpcException e) when (e.StatusCode == StatusCode.Unavailable)
            {
                throw new Exception("ServerUnreachable");
            }
        }
    }
}