using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Server;

namespace Client.Services
{
    internal class Initialization
    {

        
        public  Greeter.GreeterClient?Load(Action<string> connectionStatus)
        {
            try
            {


                var channel = GrpcChannel.ForAddress("https://localhost:7157");
                
                if (PingCheck(channel) == "OK")
                {
                    var client = new Greeter.GreeterClient(channel);

                    connectionStatus("Connection Established with the Server");

                    return client;
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
