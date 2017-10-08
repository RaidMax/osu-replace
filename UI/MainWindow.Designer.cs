using System.Drawing;

namespace OsuReplace.UI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ToolbarPanel = new System.Windows.Forms.Panel();
            this.ExitButton = new System.Windows.Forms.Button();
            this.TitleBarLabel = new System.Windows.Forms.Label();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.ReplacementProgressLabel = new System.Windows.Forms.Label();
            this.RestoreBeatmapsCheck = new System.Windows.Forms.CheckBox();
            this.ReplacementProgressBar = new System.Windows.Forms.ProgressBar();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.ImagePickerButton = new System.Windows.Forms.Button();
            this.ColorPickerButton = new System.Windows.Forms.Button();
            this.BackgroundPreviewPanel = new System.Windows.Forms.Panel();
            this.OsuFolderLabel = new System.Windows.Forms.Label();
            this.OsuFolderButton = new System.Windows.Forms.Button();
            this.OsuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ColorPickerDialog = new System.Windows.Forms.ColorDialog();
            this.ImagePickerDialog = new System.Windows.Forms.OpenFileDialog();
            this.BackgroundWorkerThread = new System.ComponentModel.BackgroundWorker();
            this.ToolbarPanel.SuspendLayout();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolbarPanel
            // 
            this.ToolbarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ToolbarPanel.Controls.Add(this.ExitButton);
            this.ToolbarPanel.Controls.Add(this.TitleBarLabel);
            this.ToolbarPanel.Location = new System.Drawing.Point(0, 0);
            this.ToolbarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ToolbarPanel.Name = "ToolbarPanel";
            this.ToolbarPanel.Size = new System.Drawing.Size(750, 40);
            this.ToolbarPanel.TabIndex = 0;
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExitButton.BackColor = System.Drawing.Color.DimGray;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.ExitButton.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.ExitButton.Location = new System.Drawing.Point(719, 7);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(5, 14, 5, 14);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(26, 26);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "❌";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ExitButton_MouseClick);
            // 
            // TitleBarLabel
            // 
            this.TitleBarLabel.AutoSize = true;
            this.TitleBarLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TitleBarLabel.Location = new System.Drawing.Point(12, 14);
            this.TitleBarLabel.Name = "TitleBarLabel";
            this.TitleBarLabel.Size = new System.Drawing.Size(66, 13);
            this.TitleBarLabel.TabIndex = 0;
            this.TitleBarLabel.Text = "osu!replace";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ContentPanel.Controls.Add(this.ReplacementProgressLabel);
            this.ContentPanel.Controls.Add(this.RestoreBeatmapsCheck);
            this.ContentPanel.Controls.Add(this.ReplacementProgressBar);
            this.ContentPanel.Controls.Add(this.ApplyButton);
            this.ContentPanel.Controls.Add(this.ImagePickerButton);
            this.ContentPanel.Controls.Add(this.ColorPickerButton);
            this.ContentPanel.Controls.Add(this.BackgroundPreviewPanel);
            this.ContentPanel.Controls.Add(this.OsuFolderLabel);
            this.ContentPanel.Controls.Add(this.OsuFolderButton);
            this.ContentPanel.Location = new System.Drawing.Point(0, 41);
            this.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(750, 458);
            this.ContentPanel.TabIndex = 1;
            // 
            // ReplacementProgressLabel
            // 
            this.ReplacementProgressLabel.AutoSize = true;
            this.ReplacementProgressLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.ReplacementProgressLabel.Location = new System.Drawing.Point(290, 39);
            this.ReplacementProgressLabel.Name = "ReplacementProgressLabel";
            this.ReplacementProgressLabel.Size = new System.Drawing.Size(26, 13);
            this.ReplacementProgressLabel.TabIndex = 8;
            this.ReplacementProgressLabel.Text = "Idle";
            // 
            // RestoreBeatmapsCheck
            // 
            this.RestoreBeatmapsCheck.AutoSize = true;
            this.RestoreBeatmapsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RestoreBeatmapsCheck.ForeColor = System.Drawing.SystemColors.Control;
            this.RestoreBeatmapsCheck.Location = new System.Drawing.Point(578, 427);
            this.RestoreBeatmapsCheck.Name = "RestoreBeatmapsCheck";
            this.RestoreBeatmapsCheck.Size = new System.Drawing.Size(63, 17);
            this.RestoreBeatmapsCheck.TabIndex = 7;
            this.RestoreBeatmapsCheck.Text = "Restore";
            this.RestoreBeatmapsCheck.UseVisualStyleBackColor = true;
            // 
            // ReplacementProgressBar
            // 
            this.ReplacementProgressBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ReplacementProgressBar.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.ReplacementProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.ReplacementProgressBar.Location = new System.Drawing.Point(293, 9);
            this.ReplacementProgressBar.Name = "ReplacementProgressBar";
            this.ReplacementProgressBar.Size = new System.Drawing.Size(445, 23);
            this.ReplacementProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ReplacementProgressBar.TabIndex = 6;
            // 
            // ApplyButton
            // 
            this.ApplyButton.BackColor = System.Drawing.Color.Transparent;
            this.ApplyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ApplyButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ApplyButton.Location = new System.Drawing.Point(647, 424);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(91, 23);
            this.ApplyButton.TabIndex = 5;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = false;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // ImagePickerButton
            // 
            this.ImagePickerButton.BackColor = System.Drawing.Color.Transparent;
            this.ImagePickerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ImagePickerButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ImagePickerButton.Location = new System.Drawing.Point(189, 162);
            this.ImagePickerButton.Name = "ImagePickerButton";
            this.ImagePickerButton.Size = new System.Drawing.Size(91, 23);
            this.ImagePickerButton.TabIndex = 4;
            this.ImagePickerButton.Text = "Image...";
            this.ImagePickerButton.UseVisualStyleBackColor = false;
            this.ImagePickerButton.Click += new System.EventHandler(this.ImagePickerButton_Click);
            // 
            // ColorPickerButton
            // 
            this.ColorPickerButton.BackColor = System.Drawing.Color.Transparent;
            this.ColorPickerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColorPickerButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ColorPickerButton.Location = new System.Drawing.Point(12, 162);
            this.ColorPickerButton.Name = "ColorPickerButton";
            this.ColorPickerButton.Size = new System.Drawing.Size(91, 23);
            this.ColorPickerButton.TabIndex = 3;
            this.ColorPickerButton.Text = "Color...";
            this.ColorPickerButton.UseVisualStyleBackColor = false;
            this.ColorPickerButton.Click += new System.EventHandler(this.ColorPickerButton_Click);
            // 
            // BackgroundPreviewPanel
            // 
            this.BackgroundPreviewPanel.BackColor = System.Drawing.Color.Black;
            this.BackgroundPreviewPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackgroundPreviewPanel.Location = new System.Drawing.Point(12, 191);
            this.BackgroundPreviewPanel.Name = "BackgroundPreviewPanel";
            this.BackgroundPreviewPanel.Size = new System.Drawing.Size(268, 256);
            this.BackgroundPreviewPanel.TabIndex = 2;
            // 
            // OsuFolderLabel
            // 
            this.OsuFolderLabel.AutoSize = true;
            this.OsuFolderLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.OsuFolderLabel.Location = new System.Drawing.Point(12, 9);
            this.OsuFolderLabel.Name = "OsuFolderLabel";
            this.OsuFolderLabel.Size = new System.Drawing.Size(71, 13);
            this.OsuFolderLabel.TabIndex = 1;
            this.OsuFolderLabel.Text = "Not selected";
            // 
            // OsuFolderButton
            // 
            this.OsuFolderButton.BackColor = System.Drawing.Color.Transparent;
            this.OsuFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OsuFolderButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.OsuFolderButton.Location = new System.Drawing.Point(12, 34);
            this.OsuFolderButton.Name = "OsuFolderButton";
            this.OsuFolderButton.Size = new System.Drawing.Size(91, 23);
            this.OsuFolderButton.TabIndex = 0;
            this.OsuFolderButton.Text = "osu! folder...";
            this.OsuFolderButton.UseVisualStyleBackColor = false;
            this.OsuFolderButton.Click += new System.EventHandler(this.OsuFolderButton_Click);
            // 
            // ImagePickerDialog
            // 
            this.ImagePickerDialog.SupportMultiDottedExtensions = true;
            // 
            // BackgroundWorkerThread
            // 
            this.BackgroundWorkerThread.WorkerReportsProgress = true;
            this.BackgroundWorkerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerThread_DoWork);
            this.BackgroundWorkerThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerThread_ProgressChanged);
            this.BackgroundWorkerThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerThread_RunWorkerCompleted);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.ToolbarPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.Text = "osu!replace";
            this.ToolbarPanel.ResumeLayout(false);
            this.ToolbarPanel.PerformLayout();
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ToolbarPanel;
        private System.Windows.Forms.Label TitleBarLabel;
        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button OsuFolderButton;
        private System.Windows.Forms.FolderBrowserDialog OsuFolderBrowserDialog;
        private System.Windows.Forms.Label OsuFolderLabel;
        private System.Windows.Forms.ColorDialog ColorPickerDialog;
        private System.Windows.Forms.Button ColorPickerButton;
        private System.Windows.Forms.Panel BackgroundPreviewPanel;
        private System.Windows.Forms.Button ImagePickerButton;
        private System.Windows.Forms.OpenFileDialog ImagePickerDialog;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.ProgressBar ReplacementProgressBar;
        private System.ComponentModel.BackgroundWorker BackgroundWorkerThread;
        private System.Windows.Forms.CheckBox RestoreBeatmapsCheck;
        private System.Windows.Forms.Label ReplacementProgressLabel;
    }
}