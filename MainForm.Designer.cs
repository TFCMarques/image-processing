﻿namespace SS_OpenCV
{
    partial class MainForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightContrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.translationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nonUniformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramRGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImageViewer = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Images (*.png, *.bmp, *.jpg)|*.png;*.bmp;*.jpg";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.autoresToolStripMenuItem,
            this.evalToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(962, 30);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.saveToolStripMenuItem.Text = "Save As...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem,
            this.transformsToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.histogramGrayToolStripMenuItem,
            this.histogramRGBToolStripMenuItem,
            this.histogramAllToolStripMenuItem,
            this.autoZoomToolStripMenuItem,
            this.signsToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.negativeToolStripMenuItem,
            this.grayToolStripMenuItem,
            this.brightContrastToolStripMenuItem,
            this.blueChannelToolStripMenuItem,
            this.greenChannelToolStripMenuItem,
            this.redChannelToolStripMenuItem});
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.colorToolStripMenuItem.Text = "Color";
            // 
            // negativeToolStripMenuItem
            // 
            this.negativeToolStripMenuItem.Name = "negativeToolStripMenuItem";
            this.negativeToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.negativeToolStripMenuItem.Text = "Negative";
            this.negativeToolStripMenuItem.Click += new System.EventHandler(this.negativeToolStripMenuItem_Click);
            // 
            // grayToolStripMenuItem
            // 
            this.grayToolStripMenuItem.Name = "grayToolStripMenuItem";
            this.grayToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.grayToolStripMenuItem.Text = "Gray";
            this.grayToolStripMenuItem.Click += new System.EventHandler(this.grayToolStripMenuItem_Click);
            // 
            // brightContrastToolStripMenuItem
            // 
            this.brightContrastToolStripMenuItem.Name = "brightContrastToolStripMenuItem";
            this.brightContrastToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.brightContrastToolStripMenuItem.Text = "BrightContrast";
            this.brightContrastToolStripMenuItem.Click += new System.EventHandler(this.brightContrastToolStripMenuItem_Click);
            // 
            // blueChannelToolStripMenuItem
            // 
            this.blueChannelToolStripMenuItem.Name = "blueChannelToolStripMenuItem";
            this.blueChannelToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.blueChannelToolStripMenuItem.Text = "BlueChannel";
            this.blueChannelToolStripMenuItem.Click += new System.EventHandler(this.blueChannelToolStripMenuItem_Click);
            // 
            // greenChannelToolStripMenuItem
            // 
            this.greenChannelToolStripMenuItem.Name = "greenChannelToolStripMenuItem";
            this.greenChannelToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.greenChannelToolStripMenuItem.Text = "GreenChannel";
            this.greenChannelToolStripMenuItem.Click += new System.EventHandler(this.greenChannelToolStripMenuItem_Click);
            // 
            // redChannelToolStripMenuItem
            // 
            this.redChannelToolStripMenuItem.Name = "redChannelToolStripMenuItem";
            this.redChannelToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.redChannelToolStripMenuItem.Text = "RedChannel";
            this.redChannelToolStripMenuItem.Click += new System.EventHandler(this.redChannelToolStripMenuItem_Click);
            // 
            // transformsToolStripMenuItem
            // 
            this.transformsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.translationToolStripMenuItem,
            this.rotationToolStripMenuItem,
            this.zoomToolStripMenuItem});
            this.transformsToolStripMenuItem.Name = "transformsToolStripMenuItem";
            this.transformsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.transformsToolStripMenuItem.Text = "Transforms";
            // 
            // translationToolStripMenuItem
            // 
            this.translationToolStripMenuItem.Name = "translationToolStripMenuItem";
            this.translationToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.translationToolStripMenuItem.Text = "Translation";
            this.translationToolStripMenuItem.Click += new System.EventHandler(this.translationToolStripMenuItem_Click);
            // 
            // rotationToolStripMenuItem
            // 
            this.rotationToolStripMenuItem.Name = "rotationToolStripMenuItem";
            this.rotationToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.rotationToolStripMenuItem.Text = "Rotation";
            this.rotationToolStripMenuItem.Click += new System.EventHandler(this.rotationToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.zoomToolStripMenuItem.Text = "Zoom";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nonUniformToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // nonUniformToolStripMenuItem
            // 
            this.nonUniformToolStripMenuItem.Name = "nonUniformToolStripMenuItem";
            this.nonUniformToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.nonUniformToolStripMenuItem.Text = "Non Uniform";
            this.nonUniformToolStripMenuItem.Click += new System.EventHandler(this.nonUniformToolStripMenuItem_Click);
            // 
            // histogramGrayToolStripMenuItem
            // 
            this.histogramGrayToolStripMenuItem.Name = "histogramGrayToolStripMenuItem";
            this.histogramGrayToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.histogramGrayToolStripMenuItem.Text = "Histogram (Gray)";
            this.histogramGrayToolStripMenuItem.Click += new System.EventHandler(this.histogramGrayToolStripMenuItem_Click);
            // 
            // histogramRGBToolStripMenuItem
            // 
            this.histogramRGBToolStripMenuItem.Name = "histogramRGBToolStripMenuItem";
            this.histogramRGBToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.histogramRGBToolStripMenuItem.Text = "Histogram (RGB)";
            this.histogramRGBToolStripMenuItem.Click += new System.EventHandler(this.histogramRGBToolStripMenuItem_Click);
            // 
            // histogramAllToolStripMenuItem
            // 
            this.histogramAllToolStripMenuItem.Name = "histogramAllToolStripMenuItem";
            this.histogramAllToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.histogramAllToolStripMenuItem.Text = "Histogram (All)";
            this.histogramAllToolStripMenuItem.Click += new System.EventHandler(this.histogramAllToolStripMenuItem_Click);
            // 
            // autoZoomToolStripMenuItem
            // 
            this.autoZoomToolStripMenuItem.CheckOnClick = true;
            this.autoZoomToolStripMenuItem.Name = "autoZoomToolStripMenuItem";
            this.autoZoomToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.autoZoomToolStripMenuItem.Text = "Auto Zoom";
            this.autoZoomToolStripMenuItem.Click += new System.EventHandler(this.autoZoomToolStripMenuItem_Click);
            // 
            // signsToolStripMenuItem
            // 
            this.signsToolStripMenuItem.Name = "signsToolStripMenuItem";
            this.signsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.signsToolStripMenuItem.Text = "Signs";
            this.signsToolStripMenuItem.Click += new System.EventHandler(this.signsToolStripMenuItem_Click);
            // 
            // autoresToolStripMenuItem
            // 
            this.autoresToolStripMenuItem.Name = "autoresToolStripMenuItem";
            this.autoresToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.autoresToolStripMenuItem.Text = "Autores...";
            this.autoresToolStripMenuItem.Click += new System.EventHandler(this.autoresToolStripMenuItem_Click);
            // 
            // evalToolStripMenuItem
            // 
            this.evalToolStripMenuItem.Name = "evalToolStripMenuItem";
            this.evalToolStripMenuItem.Size = new System.Drawing.Size(50, 24);
            this.evalToolStripMenuItem.Text = "Eval";
            this.evalToolStripMenuItem.Click += new System.EventHandler(this.evalToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ImageViewer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 515);
            this.panel1.TabIndex = 6;
            // 
            // ImageViewer
            // 
            this.ImageViewer.Location = new System.Drawing.Point(0, 0);
            this.ImageViewer.Margin = new System.Windows.Forms.Padding(4);
            this.ImageViewer.Name = "ImageViewer";
            this.ImageViewer.Size = new System.Drawing.Size(576, 427);
            this.ImageViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImageViewer.TabIndex = 6;
            this.ImageViewer.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 545);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Sistemas Sensoriais 2019/2020 - Image processing";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem translationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoZoomToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ImageViewer;
        private System.Windows.Forms.ToolStripMenuItem evalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightContrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nonUniformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramRGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signsToolStripMenuItem;
    }
}

