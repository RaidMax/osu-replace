using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OsuReplace.Code;
using OsuReplace.Code.Utilities;

namespace OsuReplace.UI
{
    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
    [SuppressMessage("ReSharper", "LocalizableElement")]
    public partial class MainWindow : Form
    {
        private Configuration? _osuConfig;

        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
            {
                Start.Logger.Error("An unhandled exception occurred: {Exception}", eventArgs.ExceptionObject);
                MessageBox.Show("An unhandled exception occurred. Please report this to the developer.", "Error", MessageBoxButtons.OK);
                Environment.Exit(1);
            };
            InitializeComponent();
        }

        private void ExitButton_MouseClick(object sender, MouseEventArgs eventArgs)
        {
            Close();
        }

        private void OsuFolderButton_Click(object sender, EventArgs eventArgs)
        {
            var result = OsuFolderBrowserDialog.ShowDialog();

            if (result is not DialogResult.OK) return;

            if (Code.Validation.ValidOsuDirectory(OsuFolderBrowserDialog.SelectedPath))
            {
                OsuFolderLabel.Text = OsuFolderBrowserDialog.SelectedPath;
                var configPath =
                    $"{OsuFolderBrowserDialog.SelectedPath}{Path.DirectorySeparatorChar}osu!.{Environment.UserName}.cfg";

                try
                {
                    _osuConfig = new Configuration(configPath).Load();

                    if (_osuConfig.GetValue("Fullscreen") != "1") return;
                    var fullscreenWidth = Convert.ToInt32(_osuConfig.GetValue("WidthFullscreen"));
                    var fullscreenHeight = Convert.ToInt32(_osuConfig.GetValue("HeightFullscreen"));
                    var ratio = fullscreenWidth / (float)fullscreenHeight;

                    BackgroundPreviewPanel.Width = (int)Math.Ceiling(BackgroundPreviewPanel.Height * ratio);
                    ReplacementProgressBar.Width = BackgroundPreviewPanel.Width;
                    ImagePickerButton.Location = ImagePickerButton.Location with
                    {
                        X = BackgroundPreviewPanel.Location.X + BackgroundPreviewPanel.Width - ImagePickerButton.Width
                    };
                    CurrentStatusPanel.BackColor = Color.Green;
                }
                // if they don't have a config file for their user account.
                catch (FileNotFoundException)
                {
                    MessageBox.Show($"Could not find config file at {configPath}.", "Error", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("That does not appear to be a valid osu! folder.", "Error", MessageBoxButtons.OK);
            }
        }

        private void ColorPickerButton_Click(object sender, EventArgs eventArgs)
        {
            var result = ColorPickerDialog.ShowDialog();

            if (result is not DialogResult.OK) return;

            BackgroundPreviewPanel.BackgroundImage = null;
            BackgroundPreviewPanel.BackColor = ColorPickerDialog.Color;
        }

        private void ImagePickerButton_Click(object sender, EventArgs eventArgs)
        {
            var result = ImagePickerDialog.ShowDialog();

            if (result is not DialogResult.OK) return;

            try
            {
                BackgroundPreviewPanel.BackgroundImage = Image.FromFile(ImagePickerDialog.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open image file.", "Error", MessageBoxButtons.OK);
            }
        }

        private void ApplyButton_Click(object sender, EventArgs eventArgs)
        {
            if (OsuFolderBrowserDialog.SelectedPath == string.Empty)
            {
                MessageBox.Show("Please select your osu! folder before applying changes.", "Error", MessageBoxButtons.OK);
                return;
            }

            ApplyButton.Enabled = false;
            CurrentStatusPanel.BackColor = Color.Yellow;
            BackgroundWorkerThread.RunWorkerAsync();
        }

        private void BackgroundWorkerThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs eventArgs)
        {
            if (_osuConfig is null) throw new ApplicationException("osu! config file not loaded");

            var beatMapLocation = _osuConfig.GetValue("BeatmapDirectory");
            if (!Directory.Exists(beatMapLocation))
            {
                beatMapLocation = Path.Join(OsuFolderBrowserDialog.SelectedPath, beatMapLocation);
            }

            var imagePath = Path.Join(beatMapLocation, "bg.jpg");

            if (BackgroundPreviewPanel.BackgroundImage is not null)
            {
                BackgroundPreviewPanel.BackgroundImage.Save(imagePath, ImageFormat.Jpeg);
            }
            else
            {
                var image = new Bitmap(1920, 1080);
                var filledImage = Graphics.FromImage(image);

                using (var brush = new SolidBrush(Color.FromArgb(255, BackgroundPreviewPanel.BackColor)))
                {
                    filledImage.FillRectangle(brush, 0, 0, image.Width, image.Height);
                }

                try
                {
                    image.Save(imagePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Could not save solid color bitmap: {ex.Message}");
                }
            }

            var beatMaps = new BeatMap(imagePath, Directory.GetDirectories(beatMapLocation).ToList());
            Start.Logger.Information("Starting replacement of beat map: {BeatMapLocation}", beatMapLocation);
            while (!beatMaps.ReplacementFinished())
            {
                BackgroundWorkerThread.ReportProgress(beatMaps.GetReplacementProgress(), new BeatMapState(beatMaps.GetCurrentBeatMap()));
                beatMaps.ReplaceNext(RestoreBeatmapsCheck.Checked);
            }

            // @formatter:off
            var currentBeatMap = $"Completed - [{beatMaps.BackgroundsChanged}] succeeded, [{beatMaps.SkippedBeatMaps}] skipped/duplicate background images, [{beatMaps.BeatMapsBackgroundNotFound}] without backgrounds";
            // @formatter:on
            BackgroundWorkerThread.ReportProgress(100, new BeatMapState(currentBeatMap));
        }

        private void BackgroundWorkerThread_ProgressChanged(object? sender, System.ComponentModel.ProgressChangedEventArgs eventArgs)
        {
            var state = eventArgs.UserState as BeatMapState;
            ReplacementProgressLabel.Text = state?.CurrentBeatMap;
            ReplacementProgressBar.Value = eventArgs.ProgressPercentage;
        }

        private void BackgroundWorkerThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs eventArgs)
        {
            ApplyButton.Enabled = true;
            CurrentStatusPanel.BackColor = Color.Green;
        }

        private void ToolbarPanel_MouseMove(object sender, MouseEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Left)
            {
                WindowsCallbacks.SendDragMessage(Handle);
            }
        }
    }
}
