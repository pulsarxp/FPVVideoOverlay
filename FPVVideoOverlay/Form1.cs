using System.Diagnostics;
using System.Globalization;


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
            openFileDialog.Filter =
                "Videófájlok|*.mp4;*.mov;*.mkv;*.avi|Minden fájl|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtMainVideo.Text = openFileDialog.FileName;
            }
        }

        private void btnOverlay_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Overlay videó kiválasztása";
            openFileDialog.Filter =
                "Videófájlok|*.mp4;*.mov;*.mkv;*.avi|Minden fájl|*.*";

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

            btnStart.Enabled = false;
            progressBar.Value = 0;
            lblStatus.Text = "Állapot: Előkészítés...";

            try
            {
                double duration =
                    await GetVideoDurationAsync(txtMainVideo.Text);

                string overlayFilter;

                if (chkCropBlackBars.Checked)
                {
                    overlayFilter =
                        "crop=1080:1080:420:0,scale=500:500";
                }
                else
                {
                    // Normál overlay
                    overlayFilter = "scale=500:500";
                }

                string arguments =
                    $"-i \"{txtMainVideo.Text}\" " +
                    $"-i \"{txtOverlay.Text}\" " +

                    $"-filter_complex " +
                    $"\"[1:v]{overlayFilter}[map];" +
                    $"[0:v][map]overlay=W-w-30:H-h-30\" " +

                    "-c:v libx264 " +
                    "-crf 18 " +
                    "-preset medium " +
                    "-c:a copy " +

                    "-progress pipe:1 " +
                    "-nostats " +

                    $"\"{txtOutput.Text}\"";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = arguments,

                    UseShellExecute = false,
                    CreateNoWindow = true,

                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using Process process = new Process();

                process.StartInfo = startInfo;

                process.Start();

                lblStatus.Text = "Állapot: Feldolgozás... 0%";

                Task<string> errorTask =
                    process.StandardError.ReadToEndAsync();

                while (true)
                {
                    string? line =
                        await process.StandardOutput.ReadLineAsync();

                    if (line == null)
                        break;

                    if (line.StartsWith("out_time_us="))
                    {
                        string value =
                            line.Substring("out_time_us=".Length);

                        if (long.TryParse(value, out long microseconds))
                        {
                            double currentSeconds =
                                microseconds / 1_000_000.0;

                            int percent =
                                (int)((currentSeconds / duration) * 100);

                            percent =
                                Math.Clamp(percent, 0, 100);

                            progressBar.Value = percent;

                            lblStatus.Text =
                                $"Állapot: Feldolgozás... {percent}%";
                        }
                    }
                }

                await process.WaitForExitAsync();

                string ffmpegOutput =
                    await errorTask;

                if (process.ExitCode == 0)
                {
                    progressBar.Value = 100;

                    lblStatus.Text =
                        "Állapot: Kész! 100%";

                    MessageBox.Show(
                        "A videó elkészült!",
                        "Kész",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text =
                        "Állapot: Hiba!";

                    MessageBox.Show(
                        "Az FFmpeg hibával állt le.\n\n" +
                        ffmpegOutput,
                        "FFmpeg hiba",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text =
                    "Állapot: Hiba!";

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

        private async Task<double> GetVideoDurationAsync(
            string videoPath)
        {
            ProcessStartInfo startInfo =
                new ProcessStartInfo
                {
                    FileName = "ffprobe",

                    Arguments =
                        "-v error " +
                        "-show_entries format=duration " +
                        "-of default=noprint_wrappers=1:nokey=1 " +
                        $"\"{videoPath}\"",

                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

            using Process process = new Process();

            process.StartInfo = startInfo;

            process.Start();

            lblStatus.Text = "Állapot: Feldolgozás... 0%";
            lblStatus.Refresh();

            Task<string> errorTask =
                process.StandardError.ReadToEndAsync();

            string output =
                await process.StandardOutput.ReadToEndAsync();

            string error =
                await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception(
                    "Az FFprobe hibával állt le:\n\n" + error);
            }

            if (double.TryParse(
                output.Trim(),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double duration))
            {
                return duration;
            }

            throw new Exception(
                "Nem sikerült meghatározni a videó hosszát.");
        }
    }
}