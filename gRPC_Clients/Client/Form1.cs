using System.Data;
using Client.Services;
using Grpc.Core;
using Server;

namespace Client
{
    public partial class Form1 : Form
    {
        private Greeter.GreeterClient _client;
        private DataTable _dtresponse = new DataTable("ResponseTable");

        public Form1()
        {
            InitializeComponent();
            _dtresponse.Columns.Add("Name", typeof(string));
            _dtresponse.Columns.Add("time", typeof(string));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var startup = new Initialization();
            _client = startup.Load();

        }

        private async void button_load_Click(object sender, EventArgs e)
        {
            await ProcessResponseStream(sender, e);
        }

        private async Task ProcessResponseStream(object sender, EventArgs eventArgs)
        {
            using var call = _client.RequestAllData(new DataRequest{Request = "RequestingAllData"});
            var responseTask = Task.Run(async () =>
            {
                await foreach (var message in call.ResponseStream.ReadAllAsync())
                {

                    // dtresponse.Rows.Add(message);
                    richTextBox_inputbox.Invoke(new Action(() =>
                    {
                        // Append the received message to the RichTextBox
                        richTextBox_inputbox.AppendText(message + Environment.NewLine);
                    }));
                }


            });
            await responseTask;
        }
    }
}
