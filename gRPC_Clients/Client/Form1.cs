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

            dgv_display.DataSource = _dtresponse;

        }

        private async void button_load_Click(object sender, EventArgs e)
        {
            await ProcessResponseStream(sender, e);
            label_status.Text = @" Data Successfully loaded from the Database ";
        }

        private async Task ProcessResponseStream(object sender, EventArgs eventArgs)
        {
            using var call = _client.RequestAllData(new DataRequest { Request = "RequestingAllData" });
            var responseTask = Task.Run(async () =>
            {
                await foreach (var message in call.ResponseStream.ReadAllAsync())
                {
                    Invoke(new Action(() =>
                    {
                        _dtresponse.Rows.Add(
                        message.Name,
                        message.Time
                        );

                    }));
                }

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
                    var response = _client.SayGreetingsAsync(new HelloRequest { Name = $"{richTextBox_name.Text}" });
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
            _dtresponse.Rows.Clear();

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
                            _client.UpdatingRecordsAsync(new Record { RecordName = $"{richTextBox_name.Text}" });
                        label_status.Text = updateResponse.ResponseAsync.Result.Status;
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