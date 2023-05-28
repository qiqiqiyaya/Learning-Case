namespace ExtractionData
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
            this.Btn_Pause = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Txt_OutputMsg = new System.Windows.Forms.TextBox();
            this.Pb_Progress = new System.Windows.Forms.ProgressBar();
            this.Btn_SelectFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Pause
            // 
            this.Btn_Pause.Enabled = false;
            this.Btn_Pause.Location = new System.Drawing.Point(265, 373);
            this.Btn_Pause.Name = "Btn_Pause";
            this.Btn_Pause.Size = new System.Drawing.Size(75, 23);
            this.Btn_Pause.TabIndex = 0;
            this.Btn_Pause.Text = "暂停";
            this.Btn_Pause.UseVisualStyleBackColor = true;
            this.Btn_Pause.Click += new System.EventHandler(this.Btn_Pause_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Enabled = false;
            this.Btn_Cancel.Location = new System.Drawing.Point(425, 373);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 1;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Txt_OutputMsg
            // 
            this.Txt_OutputMsg.Location = new System.Drawing.Point(94, 80);
            this.Txt_OutputMsg.Multiline = true;
            this.Txt_OutputMsg.Name = "Txt_OutputMsg";
            this.Txt_OutputMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Txt_OutputMsg.Size = new System.Drawing.Size(406, 198);
            this.Txt_OutputMsg.TabIndex = 2;
            // 
            // Pb_Progress
            // 
            this.Pb_Progress.Location = new System.Drawing.Point(94, 313);
            this.Pb_Progress.Name = "Pb_Progress";
            this.Pb_Progress.Size = new System.Drawing.Size(406, 23);
            this.Pb_Progress.TabIndex = 3;
            // 
            // Btn_SelectFile
            // 
            this.Btn_SelectFile.Location = new System.Drawing.Point(94, 373);
            this.Btn_SelectFile.Name = "Btn_SelectFile";
            this.Btn_SelectFile.Size = new System.Drawing.Size(75, 23);
            this.Btn_SelectFile.TabIndex = 4;
            this.Btn_SelectFile.Text = "选取文件";
            this.Btn_SelectFile.UseVisualStyleBackColor = true;
            this.Btn_SelectFile.Click += new System.EventHandler(this.Btn_SelectFile_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_SelectFile);
            this.Controls.Add(this.Pb_Progress);
            this.Controls.Add(this.Txt_OutputMsg);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Pause);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Btn_Pause;
        private Button Btn_Cancel;
        private TextBox Txt_OutputMsg;
        private ProgressBar Pb_Progress;
        private Button Btn_SelectFile;
    }
}