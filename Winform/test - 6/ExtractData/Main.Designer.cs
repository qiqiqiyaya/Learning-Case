namespace ExtractData
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Pl_Header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Pl_Content = new System.Windows.Forms.Panel();
            this.Btn_SelectFile = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Pause = new System.Windows.Forms.Button();
            this.Txt_OutputMsg = new System.Windows.Forms.TextBox();
            this.Pl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Pl_Content.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pl_Header
            // 
            this.Pl_Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.Pl_Header.Controls.Add(this.label1);
            this.Pl_Header.Controls.Add(this.Btn_Close);
            this.Pl_Header.Controls.Add(this.pictureBox1);
            this.Pl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Pl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pl_Header.Name = "Pl_Header";
            this.Pl_Header.Size = new System.Drawing.Size(1184, 62);
            this.Pl_Header.TabIndex = 0;
            this.Pl_Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pl_Header_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(122, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据提取";
            // 
            // Btn_Close
            // 
            this.Btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Close.Enabled = false;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.Btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Close.Image")));
            this.Btn_Close.Location = new System.Drawing.Point(1120, 0);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(64, 62);
            this.Btn_Close.TabIndex = 1;
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ExtractData.Properties.Resources.program;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Pl_Content
            // 
            this.Pl_Content.Controls.Add(this.Btn_SelectFile);
            this.Pl_Content.Controls.Add(this.Btn_Cancel);
            this.Pl_Content.Controls.Add(this.Btn_Pause);
            this.Pl_Content.Controls.Add(this.Txt_OutputMsg);
            this.Pl_Content.Location = new System.Drawing.Point(0, 62);
            this.Pl_Content.Name = "Pl_Content";
            this.Pl_Content.Size = new System.Drawing.Size(1184, 619);
            this.Pl_Content.TabIndex = 1;
            // 
            // Btn_SelectFile
            // 
            this.Btn_SelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.Btn_SelectFile.Enabled = false;
            this.Btn_SelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_SelectFile.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Btn_SelectFile.ForeColor = System.Drawing.Color.White;
            this.Btn_SelectFile.Location = new System.Drawing.Point(369, 526);
            this.Btn_SelectFile.Name = "Btn_SelectFile";
            this.Btn_SelectFile.Size = new System.Drawing.Size(100, 50);
            this.Btn_SelectFile.TabIndex = 7;
            this.Btn_SelectFile.Text = "选取文件";
            this.Btn_SelectFile.UseVisualStyleBackColor = false;
            this.Btn_SelectFile.Click += new System.EventHandler(this.Btn_SelectFile_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.Btn_Cancel.Enabled = false;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Cancel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.Btn_Cancel.Location = new System.Drawing.Point(692, 526);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 50);
            this.Btn_Cancel.TabIndex = 6;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Pause
            // 
            this.Btn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.Btn_Pause.Enabled = false;
            this.Btn_Pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Pause.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Btn_Pause.ForeColor = System.Drawing.Color.White;
            this.Btn_Pause.Location = new System.Drawing.Point(532, 526);
            this.Btn_Pause.Name = "Btn_Pause";
            this.Btn_Pause.Size = new System.Drawing.Size(100, 50);
            this.Btn_Pause.TabIndex = 5;
            this.Btn_Pause.Text = "暂停";
            this.Btn_Pause.UseVisualStyleBackColor = false;
            this.Btn_Pause.Click += new System.EventHandler(this.Btn_Pause_Click);
            // 
            // Txt_OutputMsg
            // 
            this.Txt_OutputMsg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Txt_OutputMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Txt_OutputMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.Txt_OutputMsg.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txt_OutputMsg.Location = new System.Drawing.Point(0, 0);
            this.Txt_OutputMsg.Multiline = true;
            this.Txt_OutputMsg.Name = "Txt_OutputMsg";
            this.Txt_OutputMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Txt_OutputMsg.Size = new System.Drawing.Size(1184, 483);
            this.Txt_OutputMsg.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.Pl_Content);
            this.Controls.Add(this.Pl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "提取数据";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Pl_Header.ResumeLayout(false);
            this.Pl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Pl_Content.ResumeLayout(false);
            this.Pl_Content.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel Pl_Header;
        private PictureBox pictureBox1;
        private Button Btn_Close;
        private Label label1;
        private Panel Pl_Content;
        public TextBox Txt_OutputMsg;
        private Button Btn_SelectFile;
        private Button Btn_Cancel;
        private Button Btn_Pause;
    }
}