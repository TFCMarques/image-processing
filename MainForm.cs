using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SS_OpenCV
{ 
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        string title_bak = "";

        public MainForm()
        {
            InitializeComponent();
            title_bak = Text;
        }

        /// <summary>
        /// Opens a new image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves an image with a new name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// restore last undo copy of the working image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return; 
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Change visualization mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Show authors form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Calculate the image negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Call automated image processing check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();
        }

        /// <summary>
        /// Call image convertion to gray scale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void brightContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;
            Cursor = Cursors.WaitCursor;

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox brightnessForm = new InputBox("Brightness [0-255]?");
            brightnessForm.ShowDialog();
            int brightness = Convert.ToInt32(brightnessForm.ValueTextBox.Text);

            if (brightness > 255) brightness = 255;
            if (brightness < 0) brightness = 0;

            InputBox contrastForm = new InputBox("Contrast [0-3]?");
            contrastForm.ShowDialog();
            float contrast = Convert.ToInt32(contrastForm.ValueTextBox.Text);

            if (contrast > 3) contrast = 3;
            if (contrast < 0) contrast = 0;

            ImageClass.BrightContrast(img, brightness, contrast);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void blueChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.BlueChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void greenChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.GreenChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void redChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            WeightForm weightForm = new WeightForm();
            weightForm.ShowDialog();
            bool okay = weightForm.getFormStatus();

            //copy Undo Image
            imgUndo = img.Copy();
            Image<Bgr, Byte> imgCopy = img.Copy();

            if (okay)
            {
                ImageClass.NonUniform(img, imgCopy, weightForm.getMatrix(), weightForm.getMatrixWeight());
            }

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void histogramGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            Histogram grayHistogram = new Histogram(ImageClass.Histogram_Gray(img));
            grayHistogram.ShowDialog();
        }

        private void histogramRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            Histogram rgbHistogram = new Histogram(ImageClass.Histogram_RGB(img));
            rgbHistogram.ShowDialog();
        }

        private void histogramAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            Histogram allHistogram = new Histogram(ImageClass.Histogram_All(img));
            allHistogram.ShowDialog();
        }

        private void signsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            List<string[]> limitSign = new List<string[]>();
            List<string[]> warningSign = new List<string[]>();
            List<string[]> prohibitionSign = new List<string[]>();

            ImageClass.Signs(img, imgUndo, out limitSign, out warningSign, out prohibitionSign, 0);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen
            SignsForm signForm = new SignsForm(limitSign, warningSign, prohibitionSign);
            signForm.ShowDialog();

            Cursor = Cursors.Default; // normal cursor 
        }

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 
            InputBox dxForm = new InputBox("dx?");
            dxForm.ShowDialog();
            int dx = Convert.ToInt32(dxForm.ValueTextBox.Text);

            InputBox dyForm = new InputBox("dy?");
            dxForm.ShowDialog();
            int dy = Convert.ToInt32(dyForm.ValueTextBox.Text);

            //copy Undo Image
            imgUndo = img.Copy();
            ImageClass.Translation(img, imgUndo, dx, dy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            InputBox angleForm = new InputBox("Angle?");
            angleForm.ShowDialog();
            float angle = (float)Convert.ToDecimal(angleForm.ValueTextBox.Text);

            //copy Undo Image
            imgUndo = img.Copy();
            ImageClass.Rotation(img, imgUndo, angle);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            InputBox scaleForm = new InputBox("Scale?");
            scaleForm.ShowDialog();
            float scaleFactor = (float)Convert.ToDecimal(scaleForm.ValueTextBox.Text);

            //copy Undo Image
            imgUndo = img.Copy();
            ImageClass.Scale(img, imgUndo, scaleFactor);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
    }




}