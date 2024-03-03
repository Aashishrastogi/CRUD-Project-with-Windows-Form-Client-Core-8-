namespace gRPC_Winform_Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dgv_Display = new System.Windows.Forms.DataGridView();
            this.button_insert = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_update = new System.Windows.Forms.Button();
            this.button_read = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_edit = new System.Windows.Forms.Button();
            this.button_load = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Display)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Display
            // 
            this.dgv_Display.BackgroundColor = System.Drawing.Color.MintCream;
            this.dgv_Display.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_Display, "dgv_Display");
            this.dgv_Display.Name = "dgv_Display";
            this.dgv_Display.RowTemplate.Height = 28;
            // 
            // button_insert
            // 
            resources.ApplyResources(this.button_insert, "button_insert");
            this.button_insert.Name = "button_insert";
            this.button_insert.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            // 
            // button_update
            // 
            resources.ApplyResources(this.button_update, "button_update");
            this.button_update.Name = "button_update";
            this.button_update.UseVisualStyleBackColor = true;
            // 
            // button_read
            // 
            resources.ApplyResources(this.button_read, "button_read");
            this.button_read.Name = "button_read";
            this.button_read.UseVisualStyleBackColor = true;
            // 
            // button_delete
            // 
            resources.ApplyResources(this.button_delete, "button_delete");
            this.button_delete.Name = "button_delete";
            this.button_delete.UseVisualStyleBackColor = true;
            // 
            // button_edit
            // 
            resources.ApplyResources(this.button_edit, "button_edit");
            this.button_edit.Name = "button_edit";
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // button_load
            // 
            resources.ApplyResources(this.button_load, "button_load");
            this.button_load.Name = "button_load";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.load_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.Controls.Add(this.button_load);
            this.Controls.Add(this.button_edit);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_read);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_insert);
            this.Controls.Add(this.dgv_Display);
            this.Name = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Display)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button button_load;

        private System.Windows.Forms.DataGridView dgv_Display;

        private System.Windows.Forms.Button button_insert;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_read;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_edit;

        #endregion
    }
}