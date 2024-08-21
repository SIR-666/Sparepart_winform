using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sparepart_Management_System
{
    public partial class Form2 : Form
    {
        string connetionString = "Data Source=192.168.2.1;Initial Catalog=Plant5;User ID=roni@ipg;Password=AutoCasting";
        SqlConnection cnn;
        string partname_receive;
        public Form2()
        {
            InitializeComponent();
            label6.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            //Form1 home = new Form1();
            //home.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            cnn = new SqlConnection(connetionString);
            SqlDataReader dataReader;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cnn.Close();
            try
            {
                cnn.Open();
            }
            catch (Exception er)
            {
                //AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                MessageBox.Show(er.ToString());
            }
            finally
            {
                /*
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    String partnum = textBox1.Text;
                    SqlCommand cmd = new SqlCommand("Select ProductName From SparepartPlant5 Where ItemNumber = @zip", cnn);
                    cmd.Parameters.AddWithValue("@zip", partnum);

                    dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        partname_receive = dataReader["ItemNumber"].ToString();
                        label5.Text = partname_receive;
                    }
                    cnn.Close();
                }
                */
                
                if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text!="0" && comboBox2.Text != "0" && comboBox3.Text != "0" && comboBox4.Text != "0" && comboBox5.Text != "0")
                {
                    label6.Visible = false;
                    SqlCommand cmd = new SqlCommand("Insert into SparepartPlant5 (ItemNumber,ProductName,Unit,Qty,KategoriGolongan,UnitMesin,Kategori,SafetyStock,Location) values(@pnum,@pnam,@unit,@qty,@kategori_golong,@unitmesin,@kategori,@safety,@rak)", cnn);
                    cmd.Parameters.AddWithValue("@pnum", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pnam", textBox2.Text);
                    cmd.Parameters.AddWithValue("@unit", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@qty", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@kategori_golong", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@unitmesin", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@kategori", comboBox5.Text);
                    cmd.Parameters.AddWithValue("@safety", numericUpDown2.Value);
                    cmd.Parameters.AddWithValue("@rak", comboBox4.Text);
                    //cmd.Parameters.AddWithValue("@rk", textBox4.Text);
                    try
                    {
                       // cnn.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception error)
                    {
                    //AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    MessageBox.Show(error.ToString());
                    }
                    finally
                    {
                        AutoClosingMessageBox.Show("Connection Succeed", "Saved", 1000);
                        cnn.Close();
                    }

                    /*
                    SqlCommand cmd2 = new SqlCommand("Insert into SPMS_MASTER (partnumber,partname) values(@pnum,@pnam)", cnn);
                    cmd2.Parameters.AddWithValue("@pnum", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@pnam", textBox2.Text);
                    try
                    {
                        cnn.Open();
                        cmd2.ExecuteNonQuery();

                    }
                    catch (Exception error)
                    {
                        AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    }
                    finally
                    {
                        AutoClosingMessageBox.Show("Connection Succeed", "Saved", 1000);
                        cnn.Close();
                    }
                    */

                }
                else
                {
                    AutoClosingMessageBox.Show("Fill all form", "ERROR", 1000);
                    //label6.Visible = true;
                    //label6.Text = "partnumber already exist";
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
