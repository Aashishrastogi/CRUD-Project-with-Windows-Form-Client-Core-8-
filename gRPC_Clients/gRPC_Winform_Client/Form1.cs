using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gRPC_Winform_Client.Services.Initialization;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.IdentityModel.Tokens;
using Server;

namespace gRPC_Winform_Client
{
    public partial class Form1 : Form
    {
        private Greeter.GreeterClient _client;


        DataTable _dtSourceTable = new DataTable("Source");


        public Form1()
        {
            InitializeComponent();
            //var response = client.SayGreetings(new HelloRequest { Name = "hello from Winformclient" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Startup startup = new Startup();
            var client = startup.Load();
            _client = client;
        }


        private void button_edit_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void load_Click(object sender, EventArgs e)
        {
           await ProcessResponseStream(sender, e);
        }

        private async Task ProcessResponseStream(object sender, EventArgs eventArgs)
        {
            using (var call = _client.RequestAllData())
            {
                var responseTask = Task.Run(async () =>
                {

                    await foreach
                });
            }
        }
    }
}