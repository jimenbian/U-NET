using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Drawing.Drawing2D;
namespace PicView
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]  
    public partial class Form1 : Form
    {
        public int sitenumber;
        
        public Form1()
        {
            InitializeComponent();
           

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reset100();
            this.toolStripComboBox2.SelectedIndex = 1;
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(0, 0, 50, 50);

            this.pictureBox2.Region = new Region(myPath);
            this.pictureBox3.Region = new Region(myPath);
            this.pictureBox4.Region = new Region(myPath); 
            webBrowser1.Navigate(Application.StartupPath + " /map.html");
            webBrowser1.ObjectForScripting = this;
                     

        }
       

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel2.Visible = true;

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
            toolStripStatusLabel1.Text = "x轴为：" + Cursor.Position.X.ToString();
            toolStripStatusLabel2.Text = "y轴为：" + Cursor.Position.Y.ToString();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            InitialTimer1();
            toolStripStatusLabel1.Text = "x轴为：" + Cursor.Position.X.ToString();
            toolStripStatusLabel2.Text = "y轴为：" + Cursor.Position.Y.ToString();
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
            max();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            min();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            reset100();
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
            this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber*3).ToString();
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
                this.pictureBox2.Visible = false;
                sitenumber = sitenumber - 1;
                this.toolStripStatusLabel4.Text = "Site:" + sitenumber.ToString();
                this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber * 3).ToString();
                this.dataGridView1.Rows[0].Visible = false;
            }

            private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                this.pictureBox4.Visible = false;
                sitenumber = sitenumber - 1;
                this.toolStripStatusLabel4.Text = "Site:" + sitenumber.ToString();
                this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber * 3).ToString();
                this.dataGridView1.Rows[2].Visible = false;
            }

            private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e)
            {
                this.pictureBox3.Visible = false;
                sitenumber = sitenumber - 1;
                this.toolStripStatusLabel4.Text = "Site:" + sitenumber.ToString();
                this.toolStripStatusLabel5.Text = "Tranciever:" + (sitenumber * 3).ToString();
                this.dataGridView1.Rows[1].Visible = false;
            }

            private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
            {
               
                tabControl1.SelectTab(tabPage2);
            }

            private void 查看ToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                tabControl1.SelectTab(tabPage2);
            }

            private void 查看ToolStripMenuItem2_Click(object sender, EventArgs e)
            {
                tabControl1.SelectTab(tabPage2);
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
            }
 //显示圆圈   
           
          

    }
     }