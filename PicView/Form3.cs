using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PicView
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

            Form2 f2 = (Form2)this.Owner;
            textBox1.Text = f2.b1.ToString();
            string[] xvalue = new string[3] { "Transciever0", "Transciever1", "Transciever2" };
            int[] yvalue = new int[3] { f2.distance11, f2.distance22, f2.distance33 }; 
            Series series = new Series("标注点到三个Transciever的距离");
            series.ChartType = SeriesChartType.Column;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;

            // Populate new series with data
           
            
            series.Points.DataBindXY(xvalue, yvalue);

            // Add series into the chart's series collection
            chart1.Series.Add(series);
            Findmin a = new Findmin();
            int a1 = a.findmin(f2.distance11, f2.distance22, f2.distance33);
            if (a1 == f2.distance11)
            { textBox2.Text = "Transciever0";
           
            }
            if (a1 == f2.distance22)
            { textBox2.Text = "Transciever1";
           ;
            }
            if (a1 == f2.distance33)
            { textBox2.Text = "Transciever2";
            
            }
            listBox1.SelectedIndex = 0;
          
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = listBox1.SelectedItem.ToString();
            float a2 = float.Parse(a);
           float a1 = float.Parse(textBox1.Text);
           
            MessageBox.Show("接收功率为"+(a2-a1).ToString()+"w");
        }
    }
}
