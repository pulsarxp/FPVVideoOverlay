using System.Diagnostics;
using System.Globalization;

namespace FPVVideoOverlay
{
    public partial class Form1 : Form
    {
        private Process? ffmpegProcess;

        private bool applicationClosing = false;

        private bool processingCancelled = false;

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
            if (ffmpegProcess != null &&
                !ffmpegProcess.HasExited)
            {
                DialogResult result =
                    MessageBox.Show(
                        "Biztosan le szeretnéd állítani a feldolgozást?",
                        "Feldolgozás leállítása",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    processingCancelled = true;

                    try
                    {
                        ffmpegProcess.Kill(
                            entireProcessTree: true);

                        lblStatus.Text =
                            "Állapot: Leállítás...";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Nem sikerült leállítani az FFmpeg-et.\n\n" +
                            ex.Message,
                            "Hiba",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                return;
            }

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

            processingCancelled = false;

            btnStart.Text =
                "⏹ Feldolgozás leállítása";

            progressBar.Value = 0;

            lblStatus.Text =
                "Állapot: Előkészítés...";

            try
            {
                // Fő videó hosszának lekérése FFprobe-bal.
                double duration =
                    await GetVideoDurationAsync(
                        txtMainVideo.Text);

                string overlayFilter;

                if (chkCropBlackBars.Checked)
                {
                    overlayFilter =
                        "crop=1080:1080:420:0,scale=500:500";
                }
                else
                {
                    overlayFilter =
                        "scale=500:500";
                }

                string arguments =
                    "-y " +

                    $"-i \"{txtMainVideo.Text}\" " +
                    $"-i \"{txtOverlay.Text}\" " +

                    "-filter_complex " +
                    $"\"[1:v]{overlayFilter}[map];" +
                    $"[0:v][map]overlay=W-w-30:H-h-30\" " +

                    "-c:v libx264 " +
                    "-crf 18 " +
                    "-preset medium " +
                    "-c:a copy " +

                    "-progress pipe:1 " +
                    "-nostats " +

                    $"\"{txtOutput.Text}\"";

                ProcessStartInfo startInfo =
                    new ProcessStartInfo
                    {
                        FileName = "ffmpeg",
                        Arguments = arguments,

                        UseShellExecute = false,
                        CreateNoWindow = true,

                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                ffmpegProcess =
                    new Process();

                ffmpegProcess.StartInfo =
                    startInfo;

                ffmpegProcess.Start();

                lblStatus.Text =
                    "Állapot: Feldolgozás... 0%";

                Task<string> errorTask =
                    ffmpegProcess.StandardError.ReadToEndAsync();

                double renderSpeed = 0;
                double currentSeconds = 0;

                while (true)
                {
                    string? line =
                        await ffmpegProcess.StandardOutput.ReadLineAsync();

                    if (line == null)
                    {
                        break;
                    }

                    if (line.StartsWith("out_time_us="))
                    {
                        string value =
                            line.Substring(
                                "out_time_us=".Length);

                        if (long.TryParse(
                            value,
                            out long microseconds))
                        {
                            currentSeconds =
                                microseconds /
                                1_000_000.0;
                        }
                    }

                    if (line.StartsWith("speed="))
                    {
                        string value =
                            line.Substring(
                                "speed=".Length)
                            .Trim()
                            .TrimEnd('x');

                        if (double.TryParse(
                            value,
                            NumberStyles.Any,
                            CultureInfo.InvariantCulture,
                            out double speed))
                        {
                            renderSpeed = speed;
                        }
                    }

                    if (currentSeconds > 0)
                    {
                        int percent =
                            (int)(
                                (currentSeconds / duration) *
                                100);

                        percent =
                            Math.Clamp(
                                percent,
                                0,
                                100);

                        progressBar.Value =
                            percent;

                        string etaText =
                            "--:--";

                        if (renderSpeed > 0)
                        {
                            double remainingVideoSeconds =
                                Math.Max(
                                    0,
                                    duration -
                                    currentSeconds);

                            double etaSeconds =
                                remainingVideoSeconds /
                                renderSpeed;

                            TimeSpan eta =
                                TimeSpan.FromSeconds(
                                    etaSeconds);

                            if (eta.TotalHours >= 1)
                            {
                                etaText =
                                    $"{(int)eta.TotalHours}:" +
                                    $"{eta.Minutes:00}:" +
                                    $"{eta.Seconds:00}";
                            }
                            else
                            {
                                etaText =
                                    $"{eta.Minutes:00}:" +
                                    $"{eta.Seconds:00}";
                            }
                        }

                        string speedText =
                            renderSpeed > 0
                                ? $"{renderSpeed:0.00}x"
                                : "--";

                        lblStatus.Text =
                            $"Állapot: Feldolgozás... {percent}% | " +
                            $"Sebesség: {speedText} | " +
                            $"Hátra: ~{etaText}";
                    }
                }

                await ffmpegProcess.WaitForExitAsync();

                string ffmpegOutput =
                    await errorTask;

                if (ffmpegProcess.ExitCode == 0)
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
                else if (processingCancelled)
                {
                    lblStatus.Text =
                        "Állapot: Leállítva";
                }
                else if (!applicationClosing)
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
                if (!applicationClosing &&
                    !processingCancelled)
                {
                    lblStatus.Text =
                        "Állapot: Hiba!";

                    MessageBox.Show(
                        ex.Message,
                        "Hiba",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnStart.Text =
                    "Videó készítése";

                btnStart.Enabled =
                    true;

                ffmpegProcess?.Dispose();
                ffmpegProcess = null;
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

            using Process probeProcess =
                new Process();

            probeProcess.StartInfo =
                startInfo;

            probeProcess.Start();

            string output =
                await probeProcess.StandardOutput
                    .ReadToEndAsync();

            string error =
                await probeProcess.StandardError
                    .ReadToEndAsync();

            await probeProcess.WaitForExitAsync();

            if (probeProcess.ExitCode != 0)
            {
                throw new Exception(
                    "Az FFprobe hibával állt le:\n\n" +
                    error);
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

        private void Form1_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            applicationClosing = true;

            if (ffmpegProcess != null &&
                !ffmpegProcess.HasExited)
            {
                try
                {
                    ffmpegProcess.Kill(
                        entireProcessTree: true);
                }
                catch
                {
                    // Bezárás közben már nincs értelme
                    // külön hibát megjeleníteni.
                }
            }
        }
    }
}