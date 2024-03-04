namespace Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgv_display = new DataGridView();
            Column_name = new DataGridViewTextBoxColumn();
            Column_time = new DataGridViewTextBoxColumn();
            richTextBox_inputbox = new RichTextBox();
            button_insert = new Button();
            button_read = new Button();
            button_update = new Button();
            button_delete = new Button();
            button_load = new Button();
            ((System.ComponentModel.ISupportInitialize)dgv_display).BeginInit();
            SuspendLayout();
            // 
            // dgv_display
            // 
            dgv_display.BackgroundColor = Color.PaleTurquoise;
            dgv_display.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_display.Columns.AddRange(new DataGridViewColumn[] { Column_name, Column_time });
            dgv_display.Location = new Point(2, 6);
            dgv_display.Name = "dgv_display";
            dgv_display.RowHeadersWidth = 62;
            dgv_display.Size = new Size(1600, 557);
            dgv_display.TabIndex = 0;
            // 
            // Column_name
            // 
            Column_name.HeaderText = "Name";
            Column_name.MinimumWidth = 8;
            Column_name.Name = "Column_name";
            Column_name.Width = 150;
            // 
            // Column_time
            // 
            Column_time.HeaderText = "Time";
            Column_time.MinimumWidth = 8;
            Column_time.Name = "Column_time";
            Column_time.Width = 150;
            // 
            // richTextBox_inputbox
            // 
            richTextBox_inputbox.Location = new Point(2, 569);
            richTextBox_inputbox.Name = "richTextBox_inputbox";
            richTextBox_inputbox.Size = new Size(715, 234);
            richTextBox_inputbox.TabIndex = 1;
            richTextBox_inputbox.Text = "";
            // 
            // button_insert
            // 
            button_insert.Location = new Point(12, 829);
            button_insert.Name = "button_insert";
            button_insert.Size = new Size(168, 62);
            button_insert.TabIndex = 2;
            button_insert.Text = "Insert";
            button_insert.UseVisualStyleBackColor = true;
            // 
            // button_read
            // 
            button_read.Location = new Point(213, 829);
            button_read.Name = "button_read";
            button_read.Size = new Size(168, 62);
            button_read.TabIndex = 3;
            button_read.Text = "Read";
            button_read.UseVisualStyleBackColor = true;
            // 
            // button_update
            // 
            button_update.Location = new Point(419, 829);
            button_update.Name = "button_update";
            button_update.Size = new Size(168, 62);
            button_update.TabIndex = 4;
            button_update.Text = "Update";
            button_update.UseVisualStyleBackColor = true;
            // 
            // button_delete
            // 
            button_delete.Location = new Point(624, 829);
            button_delete.Name = "button_delete";
            button_delete.Size = new Size(168, 62);
            button_delete.TabIndex = 5;
            button_delete.Text = "Delete";
            button_delete.UseVisualStyleBackColor = true;
            // 
            // button_load
            // 
            button_load.Location = new Point(764, 569);
            button_load.Name = "button_load";
            button_load.Size = new Size(168, 62);
            button_load.TabIndex = 6;
            button_load.Text = "Load";
            button_load.UseVisualStyleBackColor = true;
            button_load.Click += button_load_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(1618, 924);
            Controls.Add(button_load);
            Controls.Add(button_delete);
            Controls.Add(button_update);
            Controls.Add(button_read);
            Controls.Add(button_insert);
            Controls.Add(richTextBox_inputbox);
            Controls.Add(dgv_display);
            Name = "Form1";
            Text = "Client Application";
            TopMost = true;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_display).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgv_display;
        private DataGridViewTextBoxColumn Column_name;
        private DataGridViewTextBoxColumn Column_time;
        private RichTextBox richTextBox_inputbox;
        private Button button_insert;
        private Button button_read;
        private Button button_update;
        private Button button_delete;
        private Button button_load;
    }
}
