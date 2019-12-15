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
    public partial class SignsForm : Form
    {

        public SignsForm(List<string[]> limitSign, List<string[]> warningSign, List<string[]> prohibitionSign)
        {
            InitializeComponent();
            textBox1.Text = prohibitionSign.Count.ToString();
            textBox2.Text = warningSign.Count.ToString();
            textBox3.Text = limitSign.Count.ToString();

            if(limitSign.Count != 0)
            {
                foreach (string[] sign in limitSign)
                {
                    listView1.Items.Add(sign[0].ToString());
                }
            }

            foreach (string[] sign in prohibitionSign) {
                textBox5.AppendText("Type: Prohibition");
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Left side: " + sign[1].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Top side: " + sign[2].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Right side: " + sign[3].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Bottom side: " + sign[4].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText(Environment.NewLine);
            }

            foreach (string[] sign in warningSign)
            {
                textBox5.AppendText("Type: Warning");
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Left side: " + sign[1].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Top side: " + sign[2].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Right side: " + sign[3].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Bottom side: " + sign[4].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText(Environment.NewLine);
            }

            foreach (string[] sign in limitSign)
            {
                textBox5.AppendText("Type: Limit");
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Speed Limit: " + sign[0].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Left side: " + sign[1].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Top side: " + sign[2].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Right side: " + sign[3].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText("Bottom side: " + sign[4].ToString());
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText(Environment.NewLine);
            }
        }
    }
}
