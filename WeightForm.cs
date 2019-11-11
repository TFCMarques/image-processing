using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class WeightForm : Form
    {
        private bool formStatus;
        private class Matrix
        {
            public string filterName;
            public double[] filterMatrix;
            public double filterMatrixWeight;

            public Matrix(string  name, double[] filter, double weight)
            {
                filterName = name;
                filterMatrix = filter;
                filterMatrixWeight = weight;
            }

            public override string ToString()
            {
                return filterName;
            }
        }
        public WeightForm()
        {
            InitializeComponent();
            double[] sharpen = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            comboBox1.Items.Add(new Matrix("Sharpen", sharpen, 1));
            double[] gaussian = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            comboBox1.Items.Add(new Matrix("Gaussian", gaussian, 16));
            double[] laplacian = { 1, -2, 1, -2, 4, -2, 1, -2, 1 };
            comboBox1.Items.Add(new Matrix("Laplacian Hard", laplacian, 1));
            double[] vertical = { 0, 0, 0, -1, 2, -1, 0, 0, 0 };
            comboBox1.Items.Add(new Matrix("Vertical Lines", vertical, 1));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            formStatus = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            formStatus = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Matrix selected = (Matrix)comboBox1.SelectedItem;
            textBox1.Text = Convert.ToString(selected.filterMatrix[0]);
            textBox2.Text = Convert.ToString(selected.filterMatrix[1]);
            textBox3.Text = Convert.ToString(selected.filterMatrix[2]);
            textBox4.Text = Convert.ToString(selected.filterMatrix[3]);
            textBox5.Text = Convert.ToString(selected.filterMatrix[4]);
            textBox6.Text = Convert.ToString(selected.filterMatrix[5]);
            textBox7.Text = Convert.ToString(selected.filterMatrix[6]);
            textBox8.Text = Convert.ToString(selected.filterMatrix[7]);
            textBox9.Text = Convert.ToString(selected.filterMatrix[8]);
            textBox10.Text = Convert.ToString(selected.filterMatrixWeight);
        }

        public bool getFormStatus()
        {
            return formStatus;
        }

        public float[,] getMatrix()
        {
            return new float[3, 3] {
                {Convert.ToSingle(textBox1.Text), Convert.ToSingle(textBox2.Text), Convert.ToSingle(textBox3.Text)},
                {Convert.ToSingle(textBox4.Text), Convert.ToSingle(textBox5.Text), Convert.ToSingle(textBox6.Text)},
                {Convert.ToSingle(textBox7.Text), Convert.ToSingle(textBox8.Text), Convert.ToSingle(textBox9.Text)}
            };
        }
        public float getMatrixWeight()
        {
            return Convert.ToSingle(textBox10.Text);
        }
    }
}
