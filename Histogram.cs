using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZedGraph;

namespace SS_OpenCV
{
    public partial class Histogram : Form
    {
        public Histogram(int[] array)
        {
            InitializeComponent();

            DataPointCollection gray = chart1.Series[0].Points;

            for (int i = 0; i < array.Length; i++)
            {
                gray.AddXY(i, array[i]);
            }

            chart1.Series[0].Color = Color.Gray;
            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Intensity";
            chart1.ChartAreas[0].AxisY.Title = "Number of Pixels";
            chart1.ResumeLayout();
        }

        public Histogram(int[,] matrix)
        {
            InitializeComponent();

            if (matrix.GetLength(0) == 4)
            {
                chart1.Series.Add(new Series());
                chart1.Series.Add(new Series());
                chart1.Series.Add(new Series());
                DataPointCollection gray = chart1.Series[0].Points;
                DataPointCollection blue = chart1.Series[1].Points;
                DataPointCollection green = chart1.Series[2].Points;
                DataPointCollection red = chart1.Series[3].Points;

                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    gray.AddXY(i, matrix[0, i]);
                    blue.AddXY(i, matrix[1, i]);
                    green.AddXY(i, matrix[2, i]);
                    red.AddXY(i, matrix[3, i]);
                }

                chart1.Series[0].Color = Color.Gray;
                chart1.Series[1].Color = Color.Blue;
                chart1.Series[2].Color = Color.Green;
                chart1.Series[3].Color = Color.Red;
            } else
            {
                chart1.Series.Add(new Series());
                chart1.Series.Add(new Series());

                DataPointCollection blue = chart1.Series[0].Points;
                DataPointCollection green = chart1.Series[1].Points;
                DataPointCollection red = chart1.Series[2].Points;

                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    blue.AddXY(i, matrix[0, i]);
                    green.AddXY(i, matrix[1, i]);
                    red.AddXY(i, matrix[2, i]);
                }

                chart1.Series[0].Color = Color.Blue;
                chart1.Series[1].Color = Color.Green;
                chart1.Series[2].Color = Color.Red;
            }

            for (int i = 0; i < chart1.Series.Count(); i++)
            {
                chart1.Series[i].ChartType = SeriesChartType.Line;
            }

            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Intensity";
            chart1.ChartAreas[0].AxisY.Title = "Number of Pixels";
            chart1.ResumeLayout();
        }
    }
}
