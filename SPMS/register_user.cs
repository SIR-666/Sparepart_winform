using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sparepart_Management_System
{
    public partial class register_user : Form
    {
        string connetionString = "Data Source=192.168.5.4;Initial Catalog=KUJNAGN;User ID=nganjuk;Password=Excited2020";
        SqlConnection cnn;
        string password, tipe;
        public register_user()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connetionString);
           // SqlDataReader dataReader;
           // SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                cnn.Open();
            }
            catch (Exception er)
            {
                AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
            }
            finally
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    int tip;
                    if (comboBox1.Text == "Admin")
                        tip = 1;
                    else
                        tip = 0;

                    SqlCommand cmd = new SqlCommand("Insert into SPMS_LOGIN (noreg,pass,tipe) values(@pnum,@pnam,@st)", cnn);
                    cmd.Parameters.AddWithValue("@pnum", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pnam", textBox2.Text);
                    cmd.Parameters.AddWithValue("@st", tip);
                    try
                    {
                        //cnn.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception error)
                    {
                        AutoClosingMessageBox.Show("Failed connect network", "ERROR", 1000);
                    }
                    finally
                    {
                        AutoClosingMessageBox.Show("Connection Succeed", "Saved", 1000);
                        cnn.Close();
                       
                    }
                }
            }
        }
    }
}
