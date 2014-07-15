using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

using System.Reflection;
namespace PicView
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        public int hight;
        public int sitenumber;
        public int distance1;
        public int distance2;
        public int distance3;
        public int data1;
        public int data2;
        public int data3;
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel4.Visible = true;
            reset100();
            this.toolStripComboBox2.SelectedIndex = 1;
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(0, 0, 50, 50);

            this.pictureBox2.Region = new Region(myPath);
            this.pictureBox3.Region = new Region(myPath);
            this.pictureBox4.Region = new Region(myPath);
            webBrowser1.Navigate(Application.StartupPath + " /map.html");
            webBrowser1.ObjectForScripting = this;
            timer5.Start();
            //int index = this.dataGridView1.Rows.Add();
            //this.dataGridView1.Rows[index].Cells[0].Value = "1";
            //this.dataGridView1.Rows[index].Cells[1].Value = "2";
            //this.dataGridView1.Rows[index].Cells[2].Value = "3";
           
            this.dataGridView1.Rows.Add(2);
            this.dataGridView1.Rows[0].Cells[0].Value = "site0";
            this.dataGridView1.Rows[1].Cells[0].Value = "site0";
            this.dataGridView1.Rows[2].Cells[0].Value = "site0";
            this.dataGridView1.Rows[0].Cells[1].Value = "Transciever0";
            this.dataGridView1.Rows[1].Cells[1].Value = "Transciever1";
            this.dataGridView1.Rows[2].Cells[1].Value = "Transciever2";
            this.dataGridView1.Rows[0].Cells[2].Value = "0度";
            this.dataGridView1.Rows[1].Cells[2].Value = "120度";
            this.dataGridView1.Rows[2].Cells[2].Value = "240度";
            this.dataGridView1.Rows[0].Cells[3].Value = "1000";
            this.dataGridView1.Rows[1].Cells[3].Value = "1000";
            this.dataGridView1.Rows[2].Cells[3].Value = "1000";
            //data1 = System.Int32.Parse(this.dataGridView1.Rows[0].Cells[3].Value.ToString());
            //data2 = System.Int32.Parse(this.dataGridView1.Rows[1].Cells[3].Value.ToString());
            //data3 = System.Int32.Parse(this.dataGridView1.Rows[2].Cells[3].Value.ToString());
          
        }


        private void panel1_MouseEnter(object sender, EventArgs e)
        {

          
           

            //toolStripStatusLabel1.Text = "x轴为："+Cursor.Position.X.ToString();
            //toolStripStatusLabel2.Text = "y轴为：" + Cursor.Position.Y.ToString();
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
            if (isSelected1 == true)
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
        }

        private Point mouseDownPoint = new Point();
        private bool isSelected = false;
        private bool isSelected1 = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;
            this.Cursor = System.Windows.Forms.Cursors.Default;
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
            isSelected1 = true;
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
                    isSelected1 = false;
                }
                InitialTimer1();

            }
            else
            {


            }
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {

            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
            //toolStripStatusLabel1.Text = "x轴为：" + Cursor.Position.X.ToString();
            //toolStripStatusLabel2.Text = "y轴为：" + Cursor.Position.Y.ToString();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
            //toolStripStatusLabel1.Text = "x轴为：" + Cursor.Position.X.ToString();
            //toolStripStatusLabel2.Text = "y轴为：" + Cursor.Position.Y.ToString();
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

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void toolStrip2_MouseEnter(object sender, EventArgs e)
        {

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

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox2.SelectedIndex)
            {
                case 0:
                    webBrowser1.Document.InvokeScript("setzoom1");
                    break;
                case 1:
                    webBrowser1.Document.InvokeScript("setzoom2");
                    break;
                case 2:
                    webBrowser1.Document.InvokeScript("setzoom3");
                    break;
                case 3:
                    webBrowser1.Document.InvokeScript("setzoom4");
                    break;
                default:
                    webBrowser1.Document.InvokeScript("setzoom2");
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



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            max();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            min();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //max();
            webBrowser1.Document.InvokeScript("maxmap");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //min();
            webBrowser1.Document.InvokeScript("minmap");
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("setzoom2");
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            this.toolStripStatusLabel4.Visible = true;
            this.toolStripStatusLabel5.Visible = true;
            sitenumber = 3;
            this.toolStripStatusLabel4.Text = "Site:" + sitenumber.ToString();
            this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber * 3).ToString();
            this.dataGridView1.Rows[0].Cells[0].Value = "Site0";
            this.dataGridView1.Rows[1].Cells[0].Value = "Site1";
            this.dataGridView1.Rows[2].Cells[0].Value = "Site2";
            this.dataGridView1.Rows[0].Cells[1].Value = this.pictureBox2.Location.X + "," + this.pictureBox2.Location.Y;
            this.dataGridView1.Rows[1].Cells[1].Value = this.pictureBox4.Location.X + "," + this.pictureBox2.Location.Y;
            this.dataGridView1.Rows[2].Cells[1].Value = this.pictureBox3.Location.X + "," + this.pictureBox2.Location.Y;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }
        //picturebox2 begin
        //
        //
        //

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Focus();
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (IsMouseInPanel())
            {
                if (timer1 != null)
                {
                    timer1.Dispose();
                }
                InitialTimer1();
            }
        }
        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
            //    if (e.Button == MouseButtons.Right)
            //{
            //    ContextMenu menu = new rightClickMenu();   //初始化menu
            //    menu.MenuItems.Add( "c1" );   //添加菜单项c1
            //    menu.MenuItems.Add( "c2" );   //添加菜单项c2
            //    menu.Show(control, new Point(e.X, e.Y));   //在点(e.X, e.Y)处显示menu
            //}
        }
        //class rightClickMenu : ContextMenuStrip
        //{
        //    //右键菜单
        //    public rightClickMenu()
        //    {
        //        Items.Add("发送消息");   //添加菜单项1
        //        Items.Add("发送文件");   //添加菜单项2
        //        Items.Add("断开连接");   //添加菜单项3
        //        Items[0].Click += new EventHandler(sendMsg);     //定义菜单项1上的Click事件处理函数
        //        Items[1].Click += new EventHandler(sendFile);     //定义菜单项2上的Click事件处理函数
        //        Items[2].Click += new EventHandler(cutCon);     //定义菜单项3上的Click事件处理函数
        //    }
        //    //发送消息
        //    private void sendMsg(object sender, EventArgs e)
        //    {
        //    }
        //    //发送文件
        //    private void sendFile(object sender, EventArgs e)
        //    {
        //    }
        //    //断开连接
        //    private void cutCon(object sender, EventArgs e)
        //    {
        //    }

        //}



        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
            isSelected = false;
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                this.pictureBox2.Left = this.pictureBox2.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureBox2.Top = this.pictureBox2.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }
        //picturebox3
        //
        //
        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Focus();
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (IsMouseInPanel())
            {
                if (timer1 != null)
                {
                    timer1.Dispose();
                }
                InitialTimer1();
            }
        }
        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
            isSelected = false;
        }
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                this.pictureBox3.Left = this.pictureBox3.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureBox3.Top = this.pictureBox3.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }
        //picture 4
        //
        //
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Focus();
        }
        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (IsMouseInPanel())
            {
                if (timer1 != null)
                {
                    timer1.Dispose();
                }
                InitialTimer1();
            }
        }
        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
            isSelected = false;
        }
        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                this.pictureBox4.Left = this.pictureBox4.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureBox4.Top = this.pictureBox4.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }
        //右键菜单
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.pictureBox2.Visible = false;
            //sitenumber = sitenumber - 1;
            //this.toolStripStatusLabel4.Text = "Site:" + sitenumber.ToString();
            //this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber * 3).ToString();
            //this.dataGridView1.Rows[0].Visible = false;
            webBrowser1.Document.InvokeScript("removeTransciever1");
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //this.pictureBox4.Visible = false;
            //sitenumber = sitenumber - 1;
            //this.toolStripStatusLabel4.Text = "Site:" + sitenumber.ToString();
            //this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber * 3).ToString();
            //this.dataGridView1.Rows[2].Visible = false;
            webBrowser1.Document.InvokeScript("removeTransciever2");
        }

        private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //this.pictureBox3.Visible = false;
            //sitenumber = sitenumber - 1;
            //this.toolStripStatusLabel4.Text = "Site:" + sitenumber.ToString();
            //this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber * 3).ToString();
            //this.dataGridView1.Rows[1].Visible = false;
            webBrowser1.Document.InvokeScript("removeTransciever3");
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("addTransciever1");
           // tabControl1.SelectTab(tabPage2);
        }

        private void 查看ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectTab(tabPage2);
            webBrowser1.Document.InvokeScript("addTransciever2");
        }

        private void 查看ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
           // tabControl1.SelectTab(tabPage2);
            webBrowser1.Document.InvokeScript("addTransciever3");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest_1(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            //try
            //{

            //    string tag_lng = webBrowser1.Document.GetElementById("mouselng").InnerText;
            //    string tag_lat = webBrowser1.Document.GetElementById("mouselat").InnerText;
            //    double dou_lng, dou_lat;
            //    if (double.TryParse(tag_lng, out dou_lng) && double.TryParse(tag_lat, out dou_lat))
            //    {
            //        toolStripStatusLabel3.Text = "当前坐标：" + dou_lng.ToString("F5") + "," + dou_lat.ToString("F5");
            //    }
            //}
            //catch (Exception ee)
            //{ MessageBox.Show(ee.Message); }
        }

        private void btnGetLocation_Click(object sender, EventArgs e)
        {

            //if (btnGetLocation.Text == "开启实时坐标")
            //{
            //    timer1.Enabled = true;
            //    btnGetLocation.Text = "关闭实时坐标";
            //}
            //else
            //{
            //    btnGetLocation.Text = "开启实时坐标";
            //    timer1.Enabled = false;
            //}  
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("addsite");
            toolStripStatusLabel4.Text = ("Site:1 Transciever:3");
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {

            webBrowser1.Document.InvokeScript("addline");
            var s = webBrowser1.Document.InvokeScript("matchdistance4");
            //System.Windows.Forms.MessageBox.Show(s.ToString());
            hight =Convert.ToInt32(s);

            var s1 = webBrowser1.Document.InvokeScript("matchdistance1");
            var s2 = webBrowser1.Document.InvokeScript("matchdistance2");
            var s3 = webBrowser1.Document.InvokeScript("matchdistance3");
            distance1 = Convert.ToInt32(s1);
            distance2 = Convert.ToInt32(s2);
            distance3 = Convert.ToInt32(s3);
            Form2 f2 = new Form2();
           f2.Owner=this;
            f2.ShowDialog();

        }
        //捕捉webbrowser中的事件
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (webBrowser1.Bounds.Contains(this.PointToClient(Cursor.Position)))
            {
                
                this.toolStripStatusLabel1.Text = webBrowser1.PointToClient(Cursor.Position).ToString();
            }




        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage2.Show();
            tabControl1.SelectedIndex = 1;
        }

        private void 设置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.tabPage2.Show();
            tabControl1.SelectedIndex = 1;
        }

        private void 设置ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.tabPage2.Show();
            tabControl1.SelectedIndex = 1;
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show(this.dataGridView1.Rows[0].Cells[3].Value.ToString());
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripComboBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            webBrowser1.Document.InvokeScript("refresh");

        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           //webBrowser1.Document.InvokeScript(" saveHtml");
            saveFileDialog1.Title = "保存";
            
            //SaveFileDialog sf =new SaveFileDialog ();
            saveFileDialog1.Filter = "备份文件（*.db3）| *.db3";

          
              
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

          

                //。。。
                //要备份数据库的代码
                //fs.FileName //(备份的数据库路径)
                MessageBox.Show("表成功");
            }
        }
    }
}