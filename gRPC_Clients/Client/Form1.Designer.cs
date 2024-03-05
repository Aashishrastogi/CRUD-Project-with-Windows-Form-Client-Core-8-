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
            button_insert = new Button();
            button_update = new Button();
            button_delete = new Button();
            button_load = new Button();
            richTextBox_name = new RichTextBox();
            richTextBox_time = new RichTextBox();
            label_name = new Label();
            label_time = new Label();
            label_status = new Label();
            label_errors = new Label();
            button_clearDGV = new Button();
            richTextBox_log = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)dgv_display).BeginInit();
            SuspendLayout();
            // 
            // dgv_display
            // 
            dgv_display.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_display.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgv_display.BackgroundColor = Color.PaleTurquoise;
            dgv_display.ColumnHeadersHeight = 34;
            dgv_display.Location = new Point(2, 6);
            dgv_display.Name = "dgv_display";
            dgv_display.ReadOnly = true;
            dgv_display.RowHeadersWidth = 62;
            dgv_display.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_display.Size = new Size(1057, 572);
            dgv_display.TabIndex = 0;
            dgv_display.CellClick += dgv_display_CellClick;
            // 
            // button_insert
            // 
            button_insert.Location = new Point(12, 829);
            button_insert.Name = "button_insert";
            button_insert.Size = new Size(168, 62);
            button_insert.TabIndex = 2;
            button_insert.Text = "Insert";
            button_insert.UseVisualStyleBackColor = true;
            button_insert.Click += button_insert_Click;
            // 
            // button_update
            // 
            button_update.Location = new Point(419, 829);
            button_update.Name = "button_update";
            button_update.Size = new Size(168, 62);
            button_update.TabIndex = 4;
            button_update.Text = "Update";
            button_update.UseVisualStyleBackColor = true;
            button_update.Click += button_update_Click;
            // 
            // button_delete
            // 
            button_delete.Location = new Point(624, 829);
            button_delete.Name = "button_delete";
            button_delete.Size = new Size(168, 62);
            button_delete.TabIndex = 5;
            button_delete.Text = "Delete";
            button_delete.UseVisualStyleBackColor = true;
            button_delete.Click += button_delete_Click;
            // 
            // button_load
            // 
            button_load.Location = new Point(219, 829);
            button_load.Name = "button_load";
            button_load.Size = new Size(168, 62);
            button_load.TabIndex = 6;
            button_load.Text = "Load Data From Database";
            button_load.UseVisualStyleBackColor = true;
            button_load.Click += button_load_Click;
            // 
            // richTextBox_name
            // 
            richTextBox_name.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox_name.Location = new Point(114, 621);
            richTextBox_name.Name = "richTextBox_name";
            richTextBox_name.Size = new Size(341, 54);
            richTextBox_name.TabIndex = 7;
            richTextBox_name.Text = "";
            // 
            // richTextBox_time
            // 
            richTextBox_time.Enabled = false;
            richTextBox_time.Location = new Point(114, 707);
            richTextBox_time.Name = "richTextBox_time";
            richTextBox_time.ReadOnly = true;
            richTextBox_time.Size = new Size(341, 54);
            richTextBox_time.TabIndex = 8;
            richTextBox_time.Text = "Time will be recorded by the server";
            // 
            // label_name
            // 
            label_name.AutoSize = true;
            label_name.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_name.Location = new Point(30, 631);
            label_name.Name = "label_name";
            label_name.Size = new Size(78, 32);
            label_name.TabIndex = 9;
            label_name.Text = "Name";
            // 
            // label_time
            // 
            label_time.AutoSize = true;
            label_time.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_time.Location = new Point(30, 719);
            label_time.Name = "label_time";
            label_time.Size = new Size(67, 32);
            label_time.TabIndex = 10;
            label_time.Text = "Time";
            // 
            // label_status
            // 
            label_status.AutoSize = true;
            label_status.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_status.Location = new Point(520, 707);
            label_status.Name = "label_status";
            label_status.Size = new Size(78, 32);
            label_status.TabIndex = 11;
            label_status.Text = "Status";
            // 
            // label_errors
            // 
            label_errors.AutoSize = true;
            label_errors.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_errors.Location = new Point(520, 624);
            label_errors.Name = "label_errors";
            label_errors.Size = new Size(144, 32);
            label_errors.TabIndex = 12;
            label_errors.Text = "_____________";
            // 
            // button_clearDGV
            // 
            button_clearDGV.Location = new Point(840, 829);
            button_clearDGV.Name = "button_clearDGV";
            button_clearDGV.Size = new Size(168, 62);
            button_clearDGV.TabIndex = 13;
            button_clearDGV.Text = "Clear DataGridView";
            button_clearDGV.UseVisualStyleBackColor = true;
            button_clearDGV.Click += button_clearDGV_Click;
            // 
            // richTextBox_log
            // 
            richTextBox_log.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox_log.Location = new Point(1065, 6);
            richTextBox_log.Name = "richTextBox_log";
            richTextBox_log.ReadOnly = true;
            richTextBox_log.Size = new Size(556, 912);
            richTextBox_log.TabIndex = 14;
            richTextBox_log.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(1629, 929);
            Controls.Add(richTextBox_log);
            Controls.Add(button_clearDGV);
            Controls.Add(label_errors);
            Controls.Add(label_status);
            Controls.Add(label_time);
            Controls.Add(label_name);
            Controls.Add(richTextBox_time);
            Controls.Add(richTextBox_name);
            Controls.Add(button_load);
            Controls.Add(button_delete);
            Controls.Add(button_update);
            Controls.Add(button_insert);
            Controls.Add(dgv_display);
            Name = "Form1";
            Text = "Client Application";
            TopMost = true;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_display).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgv_display;
        private Button button_insert;
        private Button button_update;
        private Button button_delete;
        private Button button_load;
        private RichTextBox richTextBox_name;
        private RichTextBox richTextBox_time;
        private Label label_name;
        private Label label_time;
        private Label label_status;
        private Label label_errors;
        private Button button_clearDGV;
        private RichTextBox richTextBox_log;
    }
}
