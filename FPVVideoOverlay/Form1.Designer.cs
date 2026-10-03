namespace FPVVideoOverlay
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblMainVideo = new Label();
            txtMainVideo = new TextBox();
            btnMainVideo = new Button();

            lblOverlay = new Label();
            txtOverlay = new TextBox();
            btnOverlay = new Button();

            lblOutput = new Label();
            txtOutput = new TextBox();
            btnOutput = new Button();

            chkCropBlackBars = new CheckBox();

            btnStart = new Button();
            lblStatus = new Label();
            progressBar = new TextProgressBar();

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
            btnMainVideo.Location = new Point(496, 34);
            btnMainVideo.Name = "btnMainVideo";
            btnMainVideo.Size = new Size(75, 23);
            btnMainVideo.TabIndex = 2;
            btnMainVideo.Text = "Tallózás...";
            btnMainVideo.UseVisualStyleBackColor = true;
            btnMainVideo.Click += btnMainVideo_Click;

            // 
            // lblOverlay
            // 
            lblOverlay.AutoSize = true;
            lblOverlay.Location = new Point(12, 84);
            lblOverlay.Name = "lblOverlay";
            lblOverlay.Size = new Size(50, 15);
            lblOverlay.TabIndex = 3;
            lblOverlay.Text = "Overlay:";

            // 
            // txtOverlay
            // 
            txtOverlay.Location = new Point(83, 76);
            txtOverlay.Name = "txtOverlay";
            txtOverlay.Size = new Size(367, 23);
            txtOverlay.TabIndex = 4;

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
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 130);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(54, 15);
            lblOutput.TabIndex = 6;
            lblOutput.Text = "Kimenet:";

            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(83, 122);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(367, 23);
            txtOutput.TabIndex = 7;

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
            // chkCropBlackBars
            // 
            chkCropBlackBars.AutoSize = true;
            chkCropBlackBars.Location = new Point(595, 78);
            chkCropBlackBars.Name = "chkCropBlackBars";
            chkCropBlackBars.Size = new Size(306, 19);
            chkCropBlackBars.TabIndex = 9;
            chkCropBlackBars.Text =
                "Fekete oldalsávok levágása (1920×1080 → 1080×1080)";
            chkCropBlackBars.UseVisualStyleBackColor = true;

            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 281);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(199, 72);
            btnStart.TabIndex = 10;
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
            lblStatus.TabIndex = 11;
            lblStatus.Text = "Állapot: Készen";

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
            Text = "FPV Video Overlay";

            FormClosing += Form1_FormClosing;

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMainVideo;
        private TextBox txtMainVideo;
        private Button btnMainVideo;

        private Label lblOverlay;
        private TextBox txtOverlay;
        private Button btnOverlay;

        private Label lblOutput;
        private TextBox txtOutput;
        private Button btnOutput;

        private CheckBox chkCropBlackBars;

        private Button btnStart;
        private Label lblStatus;
        private TextProgressBar progressBar;
    }
}