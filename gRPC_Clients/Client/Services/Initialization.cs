using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Client.Services
{
    internal class Initialization
    {

        public Greeter.GreeterClient? Load()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7157");
            var client = new Greeter.GreeterClient(channel);
            return client;
        }
    }
}
