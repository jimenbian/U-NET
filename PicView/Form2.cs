using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicView
{
    public partial class Form2 : Form
    {

        int a1;
        int a2;
        int a3;
       int a4;
        double b;
      public double b1;
        public int distance11;
        public int distance22;
        public int distance33;
        public int data11;
        public int data22;
        public int data33;
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)this.Owner;
            textBox4.Text = f1.hight.ToString();
            a4 = f1.hight;
          
           distance11=f1.distance1;
           distance22 = f1.distance2;
           distance33 = f1.distance3;
           //data11 = f1.data1;
           //data22 = f1.data2;
           //data33 = f1.data3;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            valueb c = new valueb();
            b1 = c.Compute1(a1, a2, b, a4);
            //MessageBox.Show("路损为" + b1.ToString() + "db");

            Form3 f3 = new Form3();
            f3.Owner = this;
            f3.ShowDialog();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Valulea a = new Valulea();
            a1 = System.Int32.Parse(comboBox3.Text);
            a2 = System.Int32.Parse(comboBox1.Text);
            a3 = System.Int32.Parse(comboBox2.Text);
            b = a.Compute(a1, a2, a3);
            textBox5.Text = b.ToString("f2");
        }

        private void label2_Click(object sender, EventArgs e)
        {

            label2.BackColor = Color.DarkGray;
            label12.BackColor = Color.Empty;
           this. tabPage1.Show();
            tabControl1.SelectedIndex = 0;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            
            
            this.tabPage2.Show();
            tabControl1.SelectedIndex = 1;
            label12.BackColor = Color.DarkGray;
            label2.BackColor = Color.Empty;

        }
    }
}
