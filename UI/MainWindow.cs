using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OsuReplace.UI
{
    public partial class MainWindow : Form
    {
        private Code.osu.Configuration OsuConfig;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void OsuFolderButton_Click(object sender, System.EventArgs e)
        {
            var result = OsuFolderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Code.Validation.ValidOsuDirectory(OsuFolderBrowserDialog.SelectedPath))
                {
                    OsuFolderLabel.Text = OsuFolderBrowserDialog.SelectedPath;
                    string configPath = $"{ OsuFolderBrowserDialog.SelectedPath }{ Path.DirectorySeparatorChar}osu!.{ Environment.UserName}.cfg";

                    try
                    {        
                        OsuConfig = new Code.osu.Configuration(configPath);

                        if (OsuConfig.GetValue("Fullscreen") == "1")
                        {
                            int fullscreenWidth = Convert.ToInt32(OsuConfig.GetValue("WidthFullscreen"));
                            int fullscreenHeight = Convert.ToInt32(OsuConfig.GetValue("HeightFullscreen"));
                            float ratio = (float)fullscreenWidth / (float)fullscreenHeight;

                            BackgroundPreviewPanel.Width = (int)Math.Ceiling(BackgroundPreviewPanel.Height * ratio);
                            ReplacementProgressBar.Width = BackgroundPreviewPanel.Width;
                            ImagePickerButton.Location = new Point(BackgroundPreviewPanel.Location.X + BackgroundPreviewPanel.Width - ImagePickerButton.Width, ImagePickerButton.Location.Y);
                            CurrentStatusPanel.BackColor = Color.Green;
                        }
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
        }

        private void ColorPickerButton_Click(object sender, System.EventArgs e)
        {
            var result = ColorPickerDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                BackgroundPreviewPanel.BackgroundImage = null;
                BackgroundPreviewPanel.BackColor = ColorPickerDialog.Color;
            }
        }

        private void ImagePickerButton_Click(object sender, System.EventArgs e)
        {
            var result = ImagePickerDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    BackgroundPreviewPanel.BackgroundImage = Bitmap.FromFile(ImagePickerDialog.FileName);
                }

                catch (Exception)
                {
                    MessageBox.Show("Could not open image file.", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
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

        private void BackgroundWorkerThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string imagePath = $"{OsuConfig.GetValue("BeatmapDirectory")}{Path.DirectorySeparatorChar}bg.jpg";
            int beatmapsSkipped = 0;

            if (BackgroundPreviewPanel.BackgroundImage != null)
            {
                BackgroundPreviewPanel.BackgroundImage.Save(imagePath, ImageFormat.Jpeg);
            }

            else
            {
                var image = new Bitmap(1920, 1080);
                var filledImage = Graphics.FromImage(image);

                using (SolidBrush B = new SolidBrush(Color.FromArgb(255, BackgroundPreviewPanel.BackColor)))
                    filledImage.FillRectangle(B, 0, 0, image.Width, image.Height);

                try
                {
                    image.Save(imagePath);
                }

                catch (Exception)
                {
                    Console.WriteLine("Error: Could not save solid color bitmap");
                }
            }

            var beatmaps = new Code.osu.Beatmaps(OsuConfig.GetValue("BeatmapDirectory"), imagePath);

            while (!beatmaps.ReplacementFinished())
            {
                BackgroundWorkerThread.ReportProgress(beatmaps.GetReplacementProgress(), new Code.osu.BeatmapState()
                {
                    CurrentBeatmap = beatmaps.GetCurrentBeatmap()
                });
                if (!beatmaps.ReplaceNext(RestoreBeatmapsCheck.Checked))
                    beatmapsSkipped++;
            }

            BackgroundWorkerThread.ReportProgress(100, new Code.osu.BeatmapState()
            {
                CurrentBeatmap = $"Completed - [{beatmaps.ReplacedBeatmapImages}] succeeded, [{beatmaps.GetSkippedFolders()}] skipped folders, [{beatmaps.BackgroundlessBeatmaps}] without backgrounds"
            });
        }

        private void BackgroundWorkerThread_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            var state = (Code.osu.BeatmapState)(e.UserState);
            ReplacementProgressLabel.Text = state.CurrentBeatmap;
            ReplacementProgressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorkerThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ApplyButton.Enabled = true;
            CurrentStatusPanel.BackColor = Color.Green;
        }

        private void ToolbarPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Code.WindowsCallbacks.SendDragMessage(Handle);
        }
    }
}
