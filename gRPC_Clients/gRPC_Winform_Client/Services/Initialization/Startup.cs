using System.Threading.Tasks;
using Grpc.Net.Client;
using Server;

namespace gRPC_Winform_Client.Services.Initialization
{
    public class Startup
    {
        public Greeter.GreeterClient Load()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7157");
            var client = new Greeter.GreeterClient(channel);
            return client;
        }
    }
}