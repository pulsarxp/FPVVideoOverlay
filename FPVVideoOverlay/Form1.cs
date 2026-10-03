using System.IO;
using System.Diagnostics;


namespace FPVVideoOverlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMainVideo_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Fő videó kiválasztása";
            openFileDialog.Filter = "Videófájlok|*.mp4;*.mov;*.mkv;*.avi|Minden fájl|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtMainVideo.Text = openFileDialog.FileName;
            }
        }

        private void btnOverlay_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Overlay videó kiválasztása";
            openFileDialog.Filter = "Videófájlok|*.mp4;*.mov;*.mkv;*.avi|Minden fájl|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtOverlay.Text = openFileDialog.FileName;
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Kimeneti videó mentése";
            saveFileDialog.Filter = "MP4 videó|*.mp4";
            saveFileDialog.DefaultExt = "mp4";
            saveFileDialog.FileName = "output.mp4";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtOutput.Text = saveFileDialog.FileName;
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtMainVideo.Text))
            {
                MessageBox.Show(
                    "Válassz ki egy érvényes fő videót!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (!File.Exists(txtOverlay.Text))
            {
                MessageBox.Show(
                    "Válassz ki egy érvényes overlay videót!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutput.Text))
            {
                MessageBox.Show(
                    "Add meg a kimeneti videó helyét!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            lblStatus.Text = "Állapot: Feldolgozás...";
            btnStart.Enabled = false;

            string overlayFilter;

            if (chkCropBlackBars.Checked)
            {
                overlayFilter = "crop=1080:1080:420:0,scale=500:500";
            }
            else
            {
                overlayFilter = "scale=500:500";
            }

            try
            {
                string arguments =
                    $"-i \"{txtMainVideo.Text}\" " +
                    $"-i \"{txtOverlay.Text}\" " +
                    $"-filter_complex \"[1:v]{overlayFilter}[map];" +
                    "[0:v][map]overlay=W-w-30:H-h-30\" " +
                    "-c:v libx264 -crf 18 -preset medium -c:a copy " +
                    $"\"{txtOutput.Text}\"";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = new Process();
                process.StartInfo = startInfo;

                process.Start();

                await process.WaitForExitAsync();

                if (process.ExitCode == 0)
                {
                    lblStatus.Text = "Állapot: Kész!";

                    MessageBox.Show(
                        "A videó elkészült!",
                        "Kész",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Állapot: Hiba!";
                    MessageBox.Show("Az FFmpeg hibával állt le.");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Állapot: Hiba!";

                MessageBox.Show(
                    ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }
    }
}
