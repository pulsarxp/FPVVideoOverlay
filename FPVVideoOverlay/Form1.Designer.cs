namespace FPVVideoOverlay
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
            lblMainVideo = new Label();
            txtMainVideo = new TextBox();
            btnMainVideo = new Button();
            btnOverlay = new Button();
            txtOverlay = new TextBox();
            lblOverlay = new Label();
            btnOutput = new Button();
            txtOutput = new TextBox();
            lblOutput = new Label();
            btnStart = new Button();
            lblStatus = new Label();
            chkCropBlackBars = new CheckBox();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // lblMainVideo
            // 
            lblMainVideo.AutoSize = true;
            lblMainVideo.Location = new Point(12, 37);
            lblMainVideo.Name = "lblMainVideo";
            lblMainVideo.Size = new Size(55, 15);
            lblMainVideo.TabIndex = 0;
            lblMainVideo.Text = "Fő videó:";
            lblMainVideo.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtMainVideo
            // 
            txtMainVideo.Location = new Point(83, 34);
            txtMainVideo.Name = "txtMainVideo";
            txtMainVideo.Size = new Size(367, 23);
            txtMainVideo.TabIndex = 1;
            // 
            // btnMainVideo
            // 
            btnMainVideo.Location = new Point(496, 29);
            btnMainVideo.Name = "btnMainVideo";
            btnMainVideo.Size = new Size(75, 23);
            btnMainVideo.TabIndex = 2;
            btnMainVideo.Text = "Tallózás...";
            btnMainVideo.UseVisualStyleBackColor = true;
            btnMainVideo.Click += btnMainVideo_Click;
            // 
            // btnOverlay
            // 
            btnOverlay.Location = new Point(496, 76);
            btnOverlay.Name = "btnOverlay";
            btnOverlay.Size = new Size(75, 23);
            btnOverlay.TabIndex = 5;
            btnOverlay.Text = "Tallózás...";
            btnOverlay.UseVisualStyleBackColor = true;
            btnOverlay.Click += btnOverlay_Click;
            // 
            // txtOverlay
            // 
            txtOverlay.Location = new Point(83, 76);
            txtOverlay.Name = "txtOverlay";
            txtOverlay.Size = new Size(367, 23);
            txtOverlay.TabIndex = 4;
            // 
            // lblOverlay
            // 
            lblOverlay.AutoSize = true;
            lblOverlay.Location = new Point(12, 84);
            lblOverlay.Name = "lblOverlay";
            lblOverlay.Size = new Size(50, 15);
            lblOverlay.TabIndex = 3;
            lblOverlay.Text = "Overlay:\n";
            lblOverlay.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnOutput
            // 
            btnOutput.Location = new Point(496, 122);
            btnOutput.Name = "btnOutput";
            btnOutput.Size = new Size(75, 23);
            btnOutput.TabIndex = 8;
            btnOutput.Text = "Tallózás...";
            btnOutput.UseVisualStyleBackColor = true;
            btnOutput.Click += btnOutput_Click;
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(83, 122);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(367, 23);
            txtOutput.TabIndex = 7;
            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 130);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(54, 15);
            lblOutput.TabIndex = 6;
            lblOutput.Text = "Kimenet:";
            lblOutput.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 281);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(199, 72);
            btnStart.TabIndex = 9;
            btnStart.Text = "Videó készítése";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(267, 310);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(87, 15);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "Állapot: Készen";
            // 
            // chkCropBlackBars
            // 
            chkCropBlackBars.AutoSize = true;
            chkCropBlackBars.Location = new Point(595, 78);
            chkCropBlackBars.Name = "chkCropBlackBars";
            chkCropBlackBars.Size = new Size(306, 19);
            chkCropBlackBars.TabIndex = 11;
            chkCropBlackBars.Text = "Fekete oldalsávok levágása (1920×1080 → 1080×1080)";
            chkCropBlackBars.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(51, 397);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(862, 23);
            progressBar.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(959, 450);
            Controls.Add(progressBar);
            Controls.Add(chkCropBlackBars);
            Controls.Add(lblStatus);
            Controls.Add(btnStart);
            Controls.Add(btnOutput);
            Controls.Add(txtOutput);
            Controls.Add(lblOutput);
            Controls.Add(btnOverlay);
            Controls.Add(txtOverlay);
            Controls.Add(lblOverlay);
            Controls.Add(btnMainVideo);
            Controls.Add(txtMainVideo);
            Controls.Add(lblMainVideo);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMainVideo;
        private TextBox txtMainVideo;
        private Button btnMainVideo;
        private Button btnOverlay;
        private TextBox txtOverlay;
        private Label lblOverlay;
        private Button btnOutput;
        private TextBox txtOutput;
        private Label lblOutput;
        private Button btnStart;
        private Label lblStatus;
        private CheckBox chkCropBlackBars;
        private ProgressBar progressBar;
    }
}
