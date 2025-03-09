namespace Test1
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
            btnStartCpuTask = new Button();
            btnStartIoTask = new Button();
            btnCheckThreads = new Button();
            lstResults = new ListBox();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // btnStartCpuTask
            // 
            btnStartCpuTask.Location = new Point(54, 304);
            btnStartCpuTask.Name = "btnStartCpuTask";
            btnStartCpuTask.Size = new Size(129, 35);
            btnStartCpuTask.TabIndex = 0;
            btnStartCpuTask.Text = "btnStartCpuTask";
            btnStartCpuTask.UseVisualStyleBackColor = true;
            btnStartCpuTask.Click += btnStartCpuTask_Click;
            // 
            // btnStartIoTask
            // 
            btnStartIoTask.Location = new Point(221, 304);
            btnStartIoTask.Name = "btnStartIoTask";
            btnStartIoTask.Size = new Size(117, 35);
            btnStartIoTask.TabIndex = 1;
            btnStartIoTask.Text = "btnStartIoTask";
            btnStartIoTask.UseVisualStyleBackColor = true;
            btnStartIoTask.Click += btnStartIoTask_Click;
            // 
            // btnCheckThreads
            // 
            btnCheckThreads.Location = new Point(370, 304);
            btnCheckThreads.Name = "btnCheckThreads";
            btnCheckThreads.Size = new Size(126, 35);
            btnCheckThreads.TabIndex = 2;
            btnCheckThreads.Text = "btnCheckThreads";
            btnCheckThreads.UseVisualStyleBackColor = true;
            btnCheckThreads.Click += btnCheckThreads_Click;
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.Location = new Point(57, 50);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(224, 191);
            lstResults.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(328, 151);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(57, 17);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "lblStatus";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblStatus);
            Controls.Add(lstResults);
            Controls.Add(btnCheckThreads);
            Controls.Add(btnStartIoTask);
            Controls.Add(btnStartCpuTask);
            Name = "Form1";
            Text = "Form1";
            FormClosing += OnFormClosing;
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Button btnStartCpuTask;
        private Button btnStartIoTask;
        private Button btnCheckThreads;
        private ListBox lstResults;
        private Label lblStatus;
    }
}
