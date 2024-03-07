using System.Data;
using Client.Services;
using Grpc.Core;
using Server;

namespace Client
{
    public partial class Form1 : Form
    {
        private Greeter.GreeterClient _client;
        private readonly DataTable _dtResponse = new DataTable("ResponseTable");
        private AuthenticationResponse _jwtResponseToken;
        private Metadata _metadata;
        private CancellationTokenSource _cancellationsource = null;
        
        public Form1()
        {
            InitializeComponent();
            _dtResponse.Columns.Add("Name", typeof(string));
            _dtResponse.Columns.Add("time", typeof(string));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var startup = new Initialization();
            _client = startup.Load(ConnectionStatus, AuthenticationResponseInfo)!;
            dgv_display.DataSource = _dtResponse;
          

    }

        private void AuthenticationResponseInfo(AuthenticationResponse jwtResponseToken)
        {
            _jwtResponseToken = jwtResponseToken;
            richTextBox_jwtTokendescription.AppendText($"Token : {jwtResponseToken.AccessToken} \n ExpiresIn : {jwtResponseToken.ExpiresIn}");

            _metadata = Metadata(jwtResponseToken);
        }

        private Metadata Metadata(AuthenticationResponse response)
        {
            var returningMetadata = new Metadata { { "Authorization", $"Bearer {response.AccessToken}" } };
            return returningMetadata;
        }

        private void ConnectionStatus(string status)
        {
            richTextBox_log.AppendText(status);
        }

        private async void button_load_Click(object sender, EventArgs e)
        {
            await ProcessResponseStream(sender, e);
            label_status.Text = @" Data Successfully loaded from the Database ";
        }

      
         
        private async Task ProcessResponseStream(object sender, EventArgs eventArgs)
        {
            //using var call = _client.RequestAllData(new DataRequest { Request = "RequestingAllData" },_metadata);
            using var call = _client.RequestAllData(new DataRequest { Request = "GetAllData" }, _metadata);
            var responseTask = Task.Run(async () =>
            {
                
                
                    await foreach (var message in call.ResponseStream.ReadAllAsync())
                    {
                        Invoke(new Action(() =>
                        {
                            _dtResponse.Rows.Add(
                                message.Name,
                                message.Time
                            );

                        }));
                    }
                
               /*
                catch (RpcException e)
                {
                    Invoke(() =>
                    {
                        richTextBox_log.Clear();
                        richTextBox_log.AppendText($"User Request Timed out \nFreeing up the Resources \nStatus code :{e.StatusCode} \nSource :{e.Source}");

                    });

                }*/
                

            });
            await responseTask;
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox_name.Text == String.Empty)
                {
                    label_errors.Text = @" Name should not be empty";

                    return;
                }

                Invoke(new Action(() =>
                {
                    var response = _client.SayGreetingsAsync(new HelloRequest { Name = $"{richTextBox_name.Text}" }, _metadata);
                    label_status.Text = response.ResponseAsync.Result.Message;
                }));
            }

            finally
            {

            }
        }

        private void dgv_display_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox_name.Text = dgv_display.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button_clearDGV_Click(object sender, EventArgs e)
        {
            _dtResponse.Rows.Clear();

        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox_name.Text == string.Empty)
                {
                    label_errors.Text = @" Name should not be empty";

                    return;
                }

                else
                {
                    Invoke(new Action((() =>
                    {
                        var updateResponse =
                            _client.UpdatingRecordsAsync(new Record { RecordName = $"{richTextBox_name.Text}" }, _metadata);
                        label_status.Text = updateResponse.ResponseAsync.Result.Status;
                    })));
                }

            }
            finally
            {

            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox_name.Text == string.Empty)
                {
                    label_errors.Text = @" Name should not be empty";

                    return;
                }

                else
                {
                    Invoke(new Action((() =>
                    {
                        var deletionResponse =
                            _client.DeletingRecordAsync(new Record_deletion { RecordName = $"{richTextBox_name.Text}" }, _metadata);
                        label_status.Text = deletionResponse.ResponseAsync.Result.DeletionResponseStatus;
                    })));
                }

            }
            finally
            {

            }
        }
    }
}


/*private void button_send_Click(object sender, EventArgs e)
{
    DataGridViewSelectedRowCollection selectedRowCollection = dgv_source.SelectedRows;

    for (int i = 0; i < selectedRowCollection.Count; i++)
    {
        _dtDestinationTable.Rows
            .Add
            ($"{selectedRowCollection[i].Cells[0].Value}",
                $"{selectedRowCollection[i].Cells[1].Value}");

        DataRow rowToRemove =
            _dtSourceTable.Rows.Find(selectedRowCollection[i].Cells[0].Value);

        _dtSourceTable.Rows.Remove(rowToRemove);
    }
}

private void dgv_source_CellContentClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0 && e.ColumnIndex == dgv_source.Columns["isChecked"].Index)
    {

        var content = dgv_source.Columns["isChecked"].Index;
        if (content != 0)
        {

            dgv_source.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                !(Convert.ToBoolean(dgv_source.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));


        }
    }
}*/