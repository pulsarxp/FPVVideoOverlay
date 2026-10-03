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

            lblOverlayPosition = new Label();
            cmbOverlayPosition = new ComboBox();

            lblOverlayMargin = new Label();
            numOverlayMargin = new NumericUpDown();
            lblMarginPx = new Label();

            lblOverlaySize = new Label();
            numOverlaySize = new NumericUpDown();
            lblOverlaySizePx = new Label();

            lblLanguage = new Label();
            cmbLanguage = new ComboBox();

            btnStart = new Button();
            lblStatus = new Label();
            progressBar = new TextProgressBar();

            ((System.ComponentModel.ISupportInitialize)numOverlayMargin)
                .BeginInit();

            ((System.ComponentModel.ISupportInitialize)numOverlaySize)
                .BeginInit();

            SuspendLayout();

            // 
            // lblMainVideo
            // 
            lblMainVideo.AutoSize = true;
            lblMainVideo.Location = new Point(12, 37);
            lblMainVideo.Name = "lblMainVideo";
            lblMainVideo.Size = new Size(55, 15);
            lblMainVideo.TabIndex = 0;
            lblMainVideo.Text = "Main video:";

            // 
            // txtMainVideo
            // 
            txtMainVideo.Location = new Point(112, 34);
            txtMainVideo.Name = "txtMainVideo";
            txtMainVideo.Size = new Size(338, 23);
            txtMainVideo.TabIndex = 1;

            // 
            // btnMainVideo
            // 
            btnMainVideo.Location = new Point(496, 34);
            btnMainVideo.Name = "btnMainVideo";
            btnMainVideo.Size = new Size(90, 23);
            btnMainVideo.TabIndex = 2;
            btnMainVideo.Text = "Browse...";
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
            txtOverlay.Location = new Point(112, 76);
            txtOverlay.Name = "txtOverlay";
            txtOverlay.Size = new Size(338, 23);
            txtOverlay.TabIndex = 4;

            // 
            // btnOverlay
            // 
            btnOverlay.Location = new Point(496, 76);
            btnOverlay.Name = "btnOverlay";
            btnOverlay.Size = new Size(90, 23);
            btnOverlay.TabIndex = 5;
            btnOverlay.Text = "Browse...";
            btnOverlay.UseVisualStyleBackColor = true;
            btnOverlay.Click += btnOverlay_Click;

            // 
            // chkCropBlackBars
            // 
            chkCropBlackBars.AutoSize = true;
            chkCropBlackBars.Location = new Point(610, 78);
            chkCropBlackBars.Name = "chkCropBlackBars";
            chkCropBlackBars.Size = new Size(306, 19);
            chkCropBlackBars.TabIndex = 6;
            chkCropBlackBars.Text =
                "Crop black sidebars (1920×1080 → 1080×1080)";
            chkCropBlackBars.UseVisualStyleBackColor = true;

            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 130);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(47, 15);
            lblOutput.TabIndex = 7;
            lblOutput.Text = "Output:";

            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(112, 122);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(338, 23);
            txtOutput.TabIndex = 8;

            // 
            // btnOutput
            // 
            btnOutput.Location = new Point(496, 122);
            btnOutput.Name = "btnOutput";
            btnOutput.Size = new Size(90, 23);
            btnOutput.TabIndex = 9;
            btnOutput.Text = "Browse...";
            btnOutput.UseVisualStyleBackColor = true;
            btnOutput.Click += btnOutput_Click;

            // 
            // lblOverlayPosition
            // 
            lblOverlayPosition.AutoSize = true;
            lblOverlayPosition.Location = new Point(12, 186);
            lblOverlayPosition.Name = "lblOverlayPosition";
            lblOverlayPosition.Size = new Size(94, 15);
            lblOverlayPosition.TabIndex = 10;
            lblOverlayPosition.Text = "Overlay position:";

            // 
            // cmbOverlayPosition
            // 
            cmbOverlayPosition.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbOverlayPosition.FormattingEnabled = true;
            cmbOverlayPosition.Location = new Point(125, 182);
            cmbOverlayPosition.Name = "cmbOverlayPosition";
            cmbOverlayPosition.Size = new Size(150, 23);
            cmbOverlayPosition.TabIndex = 11;

            // 
            // lblOverlayMargin
            // 
            lblOverlayMargin.AutoSize = true;
            lblOverlayMargin.Location = new Point(304, 186);
            lblOverlayMargin.Name = "lblOverlayMargin";
            lblOverlayMargin.Size = new Size(47, 15);
            lblOverlayMargin.TabIndex = 12;
            lblOverlayMargin.Text = "Margin:";

            // 
            // numOverlayMargin
            // 
            numOverlayMargin.Location = new Point(357, 182);

            numOverlayMargin.Maximum =
                new decimal(
                    new int[] { 500, 0, 0, 0 });

            numOverlayMargin.Name = "numOverlayMargin";
            numOverlayMargin.Size = new Size(70, 23);
            numOverlayMargin.TabIndex = 13;
            numOverlayMargin.TextAlign =
                HorizontalAlignment.Right;

            numOverlayMargin.Value =
                new decimal(
                    new int[] { 30, 0, 0, 0 });

            // 
            // lblMarginPx
            // 
            lblMarginPx.AutoSize = true;
            lblMarginPx.Location = new Point(433, 186);
            lblMarginPx.Name = "lblMarginPx";
            lblMarginPx.Size = new Size(18, 15);
            lblMarginPx.TabIndex = 14;
            lblMarginPx.Text = "px";

            // 
            // lblOverlaySize
            // 
            lblOverlaySize.AutoSize = true;
            lblOverlaySize.Location = new Point(500, 186);
            lblOverlaySize.Name = "lblOverlaySize";
            lblOverlaySize.Size = new Size(86, 15);
            lblOverlaySize.TabIndex = 15;
            lblOverlaySize.Text = "Overlay size:";

            // 
            // numOverlaySize
            // 
            numOverlaySize.Increment =
                new decimal(
                    new int[] { 10, 0, 0, 0 });

            numOverlaySize.Location = new Point(592, 182);

            numOverlaySize.Maximum =
                new decimal(
                    new int[] { 2000, 0, 0, 0 });

            numOverlaySize.Minimum =
                new decimal(
                    new int[] { 50, 0, 0, 0 });

            numOverlaySize.Name = "numOverlaySize";
            numOverlaySize.Size = new Size(80, 23);
            numOverlaySize.TabIndex = 16;
            numOverlaySize.TextAlign =
                HorizontalAlignment.Right;

            numOverlaySize.Value =
                new decimal(
                    new int[] { 500, 0, 0, 0 });

            // 
            // lblOverlaySizePx
            // 
            lblOverlaySizePx.AutoSize = true;
            lblOverlaySizePx.Location = new Point(678, 186);
            lblOverlaySizePx.Name = "lblOverlaySizePx";
            lblOverlaySizePx.Size = new Size(18, 15);
            lblOverlaySizePx.TabIndex = 17;
            lblOverlaySizePx.Text = "px";

            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(714, 35);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(62, 15);
            lblLanguage.TabIndex = 18;
            lblLanguage.Text = "Language:";

            // 
            // cmbLanguage
            // 
            cmbLanguage.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(790, 31);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(130, 23);
            cmbLanguage.TabIndex = 19;

            cmbLanguage.SelectedIndexChanged +=
                cmbLanguage_SelectedIndexChanged;

            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 281);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(199, 72);
            btnStart.TabIndex = 20;
            btnStart.Text = "Create video";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;

            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(267, 310);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(78, 15);
            lblStatus.TabIndex = 21;
            lblStatus.Text = "Status: Ready";

            // 
            // progressBar
            // 
            progressBar.Location = new Point(51, 397);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(862, 23);
            progressBar.TabIndex = 22;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(959, 450);

            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            Controls.Add(btnStart);

            Controls.Add(cmbLanguage);
            Controls.Add(lblLanguage);

            Controls.Add(lblOverlaySizePx);
            Controls.Add(numOverlaySize);
            Controls.Add(lblOverlaySize);

            Controls.Add(lblMarginPx);
            Controls.Add(numOverlayMargin);
            Controls.Add(lblOverlayMargin);

            Controls.Add(cmbOverlayPosition);
            Controls.Add(lblOverlayPosition);

            Controls.Add(chkCropBlackBars);

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

            ((System.ComponentModel.ISupportInitialize)numOverlayMargin)
                .EndInit();

            ((System.ComponentModel.ISupportInitialize)numOverlaySize)
                .EndInit();

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

        private Label lblOverlayPosition;
        private ComboBox cmbOverlayPosition;

        private Label lblOverlayMargin;
        private NumericUpDown numOverlayMargin;
        private Label lblMarginPx;

        private Label lblOverlaySize;
        private NumericUpDown numOverlaySize;
        private Label lblOverlaySizePx;

        private Label lblLanguage;
        private ComboBox cmbLanguage;

        private Button btnStart;
        private Label lblStatus;
        private TextProgressBar progressBar;
    }
}