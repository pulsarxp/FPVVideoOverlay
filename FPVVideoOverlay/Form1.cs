using System.Diagnostics;
using System.Globalization;

namespace FPVVideoOverlay
{
    public partial class Form1 : Form
    {
        private Process? ffmpegProcess;

        private bool applicationClosing = false;
        private bool processingCancelled = false;

        // Megakadályozza, hogy a nyelvi ComboBox feltöltése
        // közben lefusson a nyelvváltás.
        private bool languageComboLoading = false;

        public Form1()
        {
            InitializeComponent();

            // Alapértelmezett overlay beállítások
            numOverlayMargin.Value = 30;
            numOverlaySize.Value = 500;

            // Windows nyelvének vizsgálata.
            // A SetLanguage -> ApplyLanguage tölti fel
            // az overlay pozíció ComboBox elemeit.
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "hu")
            {
                SetLanguage("hu");
            }
            else
            {
                SetLanguage("en");
            }

            // A ComboBox ekkor már fel van töltve.
            cmbOverlayPosition.SelectedIndex = 3;
        }

        // ------------------------------------------------
        // LOKALIZÁCIÓ
        // ------------------------------------------------

        private void SetLanguage(string languageCode)
        {
            if (languageCode == "hu")
            {
                CultureInfo.CurrentUICulture =
                    new CultureInfo("hu-HU");
            }
            else
            {
                CultureInfo.CurrentUICulture =
                    new CultureInfo("en-US");
            }

            ApplyLanguage();

            languageComboLoading = true;

            cmbLanguage.Items.Clear();
            cmbLanguage.Items.Add("Magyar");
            cmbLanguage.Items.Add("English");

            if (languageCode == "hu")
            {
                cmbLanguage.SelectedIndex = 0;
            }
            else
            {
                cmbLanguage.SelectedIndex = 1;
            }

            languageComboLoading = false;
        }

        private void ApplyLanguage()
        {
            // Ablak
            Text = "FPV Video Overlay";

            // Fájlok
            lblMainVideo.Text =
                Resources.MainVideo;

            lblOverlay.Text =
                Resources.Overlay;

            lblOutput.Text =
                Resources.Output;

            btnMainVideo.Text =
                Resources.Browse;

            btnOverlay.Text =
                Resources.Browse;

            btnOutput.Text =
                Resources.Browse;

            // Overlay beállítások
            chkCropBlackBars.Text =
                Resources.CropBlackBars;

            lblOverlayPosition.Text =
                Resources.OverlayPosition;

            lblOverlayMargin.Text =
                Resources.Margin;

            lblOverlaySize.Text =
                Resources.OverlaySize;

            // Nyelv
            lblLanguage.Text =
                Resources.Language;

            // Pozíciók újratöltése úgy,
            // hogy a kiválasztás megmaradjon.
            int selectedPosition =
                cmbOverlayPosition.SelectedIndex;

            if (selectedPosition < 0)
            {
                selectedPosition = 3;
            }

            cmbOverlayPosition.Items.Clear();

            cmbOverlayPosition.Items.Add(
                Resources.PositionTopLeft);

            cmbOverlayPosition.Items.Add(
                Resources.PositionTopRight);

            cmbOverlayPosition.Items.Add(
                Resources.PositionBottomLeft);

            cmbOverlayPosition.Items.Add(
                Resources.PositionBottomRight);

            cmbOverlayPosition.SelectedIndex =
                selectedPosition;

            // Start/Stop gomb
            if (ffmpegProcess != null &&
                !ffmpegProcess.HasExited)
            {
                btnStart.Text =
                    Resources.StopProcessing;
            }
            else
            {
                btnStart.Text =
                    Resources.CreateVideo;
            }

            // Ha éppen nem fut render,
            // az alap státuszt is lokalizáljuk.
            if (ffmpegProcess == null ||
                ffmpegProcess.HasExited)
            {
                lblStatus.Text =
                    Resources.StatusReady;
            }
        }

        private void cmbLanguage_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (languageComboLoading)
            {
                return;
            }

            if (cmbLanguage.SelectedIndex == 0)
            {
                SetLanguage("hu");
            }
            else if (cmbLanguage.SelectedIndex == 1)
            {
                SetLanguage("en");
            }
        }

        // ------------------------------------------------
        // FÁJLVÁLASZTÁS
        // ------------------------------------------------

        private void btnMainVideo_Click(
            object sender,
            EventArgs e)
        {
            using OpenFileDialog openFileDialog =
                new OpenFileDialog();

            openFileDialog.Title =
                Resources.MainVideoDialogTitle;

            openFileDialog.Filter =
                "Video files|*.mp4;*.mov;*.mkv;*.avi|All files|*.*";

            if (openFileDialog.ShowDialog() ==
                DialogResult.OK)
            {
                txtMainVideo.Text =
                    openFileDialog.FileName;
            }
        }

        private void btnOverlay_Click(
            object sender,
            EventArgs e)
        {
            using OpenFileDialog openFileDialog =
                new OpenFileDialog();

            openFileDialog.Title =
                Resources.OverlayDialogTitle;

            openFileDialog.Filter =
                "Video files|*.mp4;*.mov;*.mkv;*.avi|All files|*.*";

            if (openFileDialog.ShowDialog() ==
                DialogResult.OK)
            {
                txtOverlay.Text =
                    openFileDialog.FileName;
            }
        }

        private void btnOutput_Click(
            object sender,
            EventArgs e)
        {
            using SaveFileDialog saveFileDialog =
                new SaveFileDialog();

            saveFileDialog.Title =
                Resources.OutputDialogTitle;

            saveFileDialog.Filter =
                "MP4 video|*.mp4";

            saveFileDialog.DefaultExt =
                "mp4";

            saveFileDialog.FileName =
                "output.mp4";

            if (saveFileDialog.ShowDialog() ==
                DialogResult.OK)
            {
                txtOutput.Text =
                    saveFileDialog.FileName;
            }
        }

        // ------------------------------------------------
        // START / STOP
        // ------------------------------------------------

        private async void btnStart_Click(
            object sender,
            EventArgs e)
        {
            // Ha már fut az FFmpeg,
            // ugyanaz a gomb STOP-ként működik.
            if (ffmpegProcess != null &&
                !ffmpegProcess.HasExited)
            {
                DialogResult result =
                    MessageBox.Show(
                        Resources.StopConfirmMessage,
                        Resources.StopConfirmTitle,
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
                            Resources.StatusStopping;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            Resources.StopError +
                            "\n\n" +
                            ex.Message,
                            Resources.Error,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                return;
            }

            // ------------------------------------------------
            // ELLENŐRZÉSEK
            // ------------------------------------------------

            if (!File.Exists(txtMainVideo.Text))
            {
                MessageBox.Show(
                    Resources.InvalidMainVideo,
                    Resources.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (!File.Exists(txtOverlay.Text))
            {
                MessageBox.Show(
                    Resources.InvalidOverlay,
                    Resources.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtOutput.Text))
            {
                MessageBox.Show(
                    Resources.InvalidOutput,
                    Resources.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // ------------------------------------------------
            // FELDOLGOZÁS INDÍTÁSA
            // ------------------------------------------------

            processingCancelled = false;

            btnStart.Text =
                Resources.StopProcessing;

            progressBar.Value = 0;

            lblStatus.Text =
                Resources.StatusPreparing;

            try
            {
                double duration =
                    await GetVideoDurationAsync(
                        txtMainVideo.Text);

                // ------------------------------------------------
                // OVERLAY MÉRET
                // ------------------------------------------------

                int overlaySize =
                    (int)numOverlaySize.Value;

                // ------------------------------------------------
                // OVERLAY FILTER
                // ------------------------------------------------

                string overlayFilter;

                if (chkCropBlackBars.Checked)
                {
                    overlayFilter =
                        $"crop=1080:1080:420:0," +
                        $"scale={overlaySize}:{overlaySize}";
                }
                else
                {
                    overlayFilter =
                        $"scale={overlaySize}:{overlaySize}";
                }

                // ------------------------------------------------
                // POZÍCIÓ + MARGÓ
                // ------------------------------------------------

                int margin =
                    (int)numOverlayMargin.Value;

                string overlayPosition =
                    GetOverlayPosition(
                        cmbOverlayPosition.SelectedIndex,
                        margin);

                // ------------------------------------------------
                // FFMPEG ARGUMENTUMOK
                // ------------------------------------------------

                string arguments =
                    "-y " +

                    $"-i \"{txtMainVideo.Text}\" " +
                    $"-i \"{txtOverlay.Text}\" " +

                    "-filter_complex " +
                    $"\"[1:v]{overlayFilter}[map];" +
                    $"[0:v][map]overlay={overlayPosition}\" " +

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

                // ------------------------------------------------
                // FFMPEG INDÍTÁSA
                // ------------------------------------------------

                ffmpegProcess =
                    new Process();

                ffmpegProcess.StartInfo =
                    startInfo;

                ffmpegProcess.Start();

                lblStatus.Text =
                    $"{Resources.StatusProcessing} 0%";

                Task<string> errorTask =
                    ffmpegProcess.StandardError
                        .ReadToEndAsync();

                double renderSpeed = 0;
                double currentSeconds = 0;

                // ------------------------------------------------
                // PROGRESS
                // ------------------------------------------------

                while (true)
                {
                    string? line =
                        await ffmpegProcess.StandardOutput
                            .ReadLineAsync();

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

                    // ------------------------------------------------
                    // SZÁZALÉK + ETA
                    // ------------------------------------------------

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
                            $"{Resources.StatusProcessing} " +
                            $"{percent}% | " +
                            $"{Resources.Speed}: " +
                            $"{speedText} | " +
                            $"{Resources.Remaining}: " +
                            $"~{etaText}";
                    }
                }

                // ------------------------------------------------
                // FFMPEG VÉGE
                // ------------------------------------------------

                await ffmpegProcess.WaitForExitAsync();

                string ffmpegOutput =
                    await errorTask;

                if (ffmpegProcess.ExitCode == 0)
                {
                    progressBar.Value = 100;

                    lblStatus.Text =
                        Resources.StatusFinished;

                    MessageBox.Show(
                        Resources.VideoFinished,
                        Resources.Done,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (processingCancelled)
                {
                    lblStatus.Text =
                        Resources.StatusStopped;
                }
                else if (!applicationClosing)
                {
                    lblStatus.Text =
                        Resources.Error;

                    MessageBox.Show(
                        Resources.FFmpegError +
                        "\n\n" +
                        ffmpegOutput,
                        Resources.Error,
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
                        Resources.Error;

                    MessageBox.Show(
                        ex.Message,
                        Resources.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnStart.Text =
                    Resources.CreateVideo;

                btnStart.Enabled =
                    true;

                ffmpegProcess?.Dispose();
                ffmpegProcess = null;
            }
        }

        // ------------------------------------------------
        // OVERLAY POZÍCIÓ
        // ------------------------------------------------

        private string GetOverlayPosition(
            int selectedIndex,
            int margin)
        {
            return selectedIndex switch
            {
                0 => $"{margin}:{margin}",

                1 => $"W-w-{margin}:{margin}",

                2 => $"{margin}:H-h-{margin}",

                3 => $"W-w-{margin}:H-h-{margin}",

                _ => $"W-w-{margin}:H-h-{margin}"
            };
        }

        // ------------------------------------------------
        // VIDEÓ HOSSZA
        // ------------------------------------------------

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
                    Resources.FFprobeError +
                    "\n\n" +
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
                Resources.DurationError);
        }

        // ------------------------------------------------
        // PROGRAM BEZÁRÁSA
        // ------------------------------------------------

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
                }
            }
        }
    }
}