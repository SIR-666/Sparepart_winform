using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using SPMS;
using static Guna.UI2.WinForms.Suite.Descriptions;
using SPMS.Properties;

namespace Sparepart_Management_System
{
    public partial class Form1 : Form
    {

        private Form activeform;
        string connetionString = "Data Source=192.168.2.1;Initial Catalog=Plant5;User ID=roni@ipg;Password=AutoCasting";
        SqlConnection cnn;
        string partname;
        string selecteddata;
        string getdata;
        bool stats_database = false;
        public bool login;
        string usermasuk;
        public bool button2_visible =false;
        public bool button2_enabled = false;
        public bool button3_visible = false;
        public bool button3_enabled = false;
        public bool button1_enabled = false;
        public Form1()
        {
            InitializeComponent();
            login = true;

            //button2.Visible = false;
            //button2.Enabled = false;
            //button3.Visible = false;
            //button3.Enabled = false;
            //button1.Enabled = false;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error Connection");
                AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                stats_database = false;
            }
            finally
            {
                AutoClosingMessageBox.Show("Database Connection Success", "WARNING", 1000);
                stats_database = true;
                //MessageBox.Show("Connected");
                //cnn.Close();
            }

            /*
            if (stats_database==true)
            {
                SqlCommand cmd = new SqlCommand("Select * From SparepartPlant5", cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                
                SqlCommand cmd2 = new SqlCommand("Select * From SparepartPlant5 Where Qty < SafetyStock", cnn);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;
                
            }
            //MSAPLIKATORTERMINAL2
            label5.Text = dataGridView1.Rows.Count.ToString();
            label7.Text = dataGridView2.Rows.Count.ToString();
            label3.Visible = false;

            dataGridView1.Columns[2].Width = 35;
            */
            //dataGridView2.Columns[2].Width = 35;
        }

   

        private void button2_Click(object sender, EventArgs e)
        {
                                                                                                                                                                                                                                                                                                                                       //this.Hide();
            Form2 registerpart = new Form2();
            registerpart.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getdata = ControlID.TextData;
            if (getdata != null)
            {

                int panjang = getdata.Length;
                if (panjang > 5)
                {
                    string[] split = getdata.Split(',');
                    if (split[1] == "OK")
                    {
                        label3.Visible = true;
                        label3.Text = "Welcome " + split[0];
                        usermasuk = split[0];
                        //button8.Text = "Logout";
                        login = false;
                        //Stock stock = new Stock();
                        button2_visible = true;
                        button2_enabled = true;
                        button3_visible = true;
                        button3_enabled = true;
                        button1_enabled = true;

                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (login == true)
            {
                login_user loginuser = new login_user(this);
                loginuser.Show();
                
            }
            else
            {
                button8.Image = Resources.log_in_25;
                label3.Visible = false;
                getdata = "";
                ControlID.TextData = "";
                label3.Text = "";
                //button8.Text = "Login";
               // Stock stock = new Stock();
                button2_visible = false;
                button2_enabled = false;
                button3_visible = false;
                button3_enabled = false;
                button1_enabled = true;
                //button2.Visible = false;
                //button2.Enabled = false;
                //button3.Visible = false;
                //button3.Enabled = false;
                button1.Enabled = false;
                login = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            cnn = new SqlConnection(connetionString);
            SqlDataReader dataReader;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            if (selecteddata != "")
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("Delete SparepartPlant5 Where ItemNumber = @deletpart", cnn);
                    cmd.Parameters.AddWithValue("@deletpart", selecteddata);
                    //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (Exception er)
                {
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                }
                finally
                {


                    AutoClosingMessageBox.Show(selecteddata+" deleted", "Success", 1000);
                    cnn.Close();
                }
            }
            //selecteddata = dataGridView1.SelectedCells;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            // {
            
           
                //MessageBox.Show(selecteddata, "OK");
           // }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            if (login == false)
            {
                register_user regist = new register_user();
                regist.Show();
            }
            else
                MessageBox.Show("User Login Needed");
        }



        private void button11_Click(object sender, EventArgs e)
        {
            history open_history = new history();
            open_history.Show();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Openchildform(Form childform)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panel9.Controls.Add(childform);
            this.panel9.Tag = childform;
            childform.BringToFront();
            childform.Show();
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //guna2Button1.CustomBorderThickness.Equals(guna2Button2.CustomBorderThickness);
            //guna2Button1.CustomBorderThickness.Bottom = 0;
            //guna2Button1.CustomBorderThickness = 0b0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock(this);
            addusercontrol(stock);
        }
        private void addusercontrol(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel9.Controls.Clear();
            panel9.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TransOUT transOUT = new TransOUT();
            addusercontrol(transOUT);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Histroy history = new Histroy();
            addusercontrol(history);
        }
    }
    }

    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }

public static class ControlID
{ 
    public static string TextData { get; set; }
}



