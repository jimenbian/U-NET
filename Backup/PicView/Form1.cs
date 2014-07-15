using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PicView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            if (IsMouseInPanel())
            {
                if (timer1 != null)
                {
                    timer1.Dispose();
                }
                InitialTimer1();
            }
            else
            {
                this.label1.Focus();
                this.toolStrip1.Visible = false;
            }
        }

        private void max()
        {
            int w = pictureBox1.Image.Width;
            int h = pictureBox1.Image.Height;
            double div = Convert.ToDouble(h) / Convert.ToDouble(w);

            if (w < 1100 && h < 790)
            {
                w = w + 30;
                h = Convert.ToInt32(w * div);
                this.pictureBox1.Left -= 15;
                this.pictureBox1.Top -= (h - pictureBox1.Image.Height) / 2;
            }
            Bitmap NewBitmap = new Bitmap(this.pictureBox1.InitialImage, w, h);
            this.pictureBox1.Image = NewBitmap;
        }
        private void min()
        {
            int w = pictureBox1.Image.Width;
            int h = pictureBox1.Image.Height;
            double div = Convert.ToDouble(h) / Convert.ToDouble(w);

            if (w > 30 && (w - 30) * div > 1)
            {
                w = w - 30;
                h = Convert.ToInt32(w * div);
                this.pictureBox1.Left += 15;
                this.pictureBox1.Top -= (h - pictureBox1.Image.Height) / 2;
            }
            Bitmap NewBitmap = new Bitmap(this.pictureBox1.InitialImage, w, h);
            this.pictureBox1.Image = NewBitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            max();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            min();
        }

        private void pictureBox1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

            if (numberOfTextLinesToMove > 0)
            {
                for (int i = 0; i < numberOfTextLinesToMove; i++)
                {
                    max();
                }
            }
            else if (numberOfTextLinesToMove < 0)
            {
                for (int i = 0; i > numberOfTextLinesToMove; i--)
                {
                    min();
                }
            }
        }

        private Point mouseDownPoint = new Point();
        private bool isSelected = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                this.pictureBox1.Left = this.pictureBox1.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureBox1.Top = this.pictureBox1.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }
            this.toolStrip1.Visible = false;
        }

        private bool IsMouseInPanel()
        {
            if (this.panel1.Left < PointToClient(Cursor.Position).X && PointToClient(Cursor.Position).X < this.panel1.Left + this.panel1.Width && this.panel1.Top < PointToClient(Cursor.Position).Y && PointToClient(Cursor.Position).Y < this.panel1.Top + this.panel1.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            reset100();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (IsMouseInPanel())
            {
                if (timer1 != null)
                {
                    timer1.Dispose();
                }
                InitialTimer1();
            }
            else
            {
                this.label1.Focus();
                this.toolStrip1.Visible = false;
            }
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
        }

        private Timer timer1;

        private void InitialTimer1()
        {
            this.timer1 = new Timer();
            timer1.Enabled = true;
            timer1.Interval = 1200;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStrip1.Location = PointToClient(Cursor.Position);
            this.toolStrip1.Visible = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            this.toolStrip1.Visible = false;
        }

        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
        }

        private void toolStripButton1_MouseDown(object sender, MouseEventArgs e)
        {
            InitialTimer2();
        }

        private void toolStripButton2_MouseDown(object sender, MouseEventArgs e)
        {
            InitialTimer3();
        }

        private Timer timer2;

        private void InitialTimer2()
        {
            this.timer2 = new Timer();
            timer2.Enabled = true;
            timer2.Interval = 200;
            timer2.Tick += new EventHandler(timer2_Tick);
        }

        void timer2_Tick(object sender, EventArgs e)
        {
            max();
        }

        private Timer timer3;

        private void InitialTimer3()
        {
            this.timer3 = new Timer();
            timer3.Enabled = true;
            timer3.Interval = 200;
            timer3.Tick += new EventHandler(timer3_Tick);
        }

        void timer3_Tick(object sender, EventArgs e)
        {
            min();
        }

        private void toolStripButton2_MouseUp(object sender, MouseEventArgs e)
        {
            if (timer3 != null)
            {
                timer3.Dispose();
            }
        }

        private void toolStripButton1_MouseUp(object sender, MouseEventArgs e)
        {
            if (timer2 != null)
            {
                timer2.Dispose();
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0:
                    reset50();
                    break;
                case 1:
                    reset100();
                    break;
                case 2:
                    reset150();
                    break;
                case 3:
                    reset200();
                    break;
                default:
                    reset100();
                    break;

            }
        }

        private void reset50()
        {
            this.pictureBox1.Left = 293;
            this.pictureBox1.Top = 179;
            Bitmap NewBitmap = new Bitmap(this.pictureBox1.InitialImage, 275, 198);
            this.pictureBox1.Image = NewBitmap;
        }

        private void reset100()
        {
            this.pictureBox1.Left = 155;
            this.pictureBox1.Top = 80;
            Bitmap NewBitmap = new Bitmap(this.pictureBox1.InitialImage, 550, 395);
            this.pictureBox1.Image = NewBitmap;
        }

        private void reset150()
        {
            this.pictureBox1.Left = 17;
            this.pictureBox1.Top = -19;
            Bitmap NewBitmap = new Bitmap(this.pictureBox1.InitialImage, 825, 593);
            this.pictureBox1.Image = NewBitmap;
        }

        private void reset200()
        {
            this.pictureBox1.Left = -120;
            this.pictureBox1.Top = -118;
            Bitmap NewBitmap = new Bitmap(this.pictureBox1.InitialImage, 1100, 790);
            this.pictureBox1.Image = NewBitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reset100();
            this.toolStripComboBox1.SelectedIndex = 1;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            max();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            min();
        }
    }
}