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
using SPMS.Properties;

namespace Sparepart_Management_System
{
    public partial class login_user : Form
    {
        string connetionString = "Data Source=192.168.5.4;Initial Catalog=KUJNAGN;User ID=nganjuk;Password=Excited2020";
        SqlConnection cnn;
        string password, tipe;
        Form1 form1;
        public login_user(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connetionString);
            SqlDataReader dataReader;
            SqlDataAdapter adapter = new SqlDataAdapter();
            
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
                string username = textBox1.Text;
                
                SqlCommand cmd = new SqlCommand("Select tipe,pass From SPMS_LOGIN Where noreg = @zip", cnn);
                cmd.Parameters.AddWithValue("@zip", username);

                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    password = dataReader["pass"].ToString();
                    tipe = dataReader["tipe"].ToString();
                    form1.button8.Image = Resources.log_out_25;
                }
                cnn.Close();
            }

            if(textBox2.Text == password && tipe == "0")
            { 
                ControlID.TextData = textBox1.Text+",OK";
                this.Close();
                form1.button2_visible=true;
                form1.button2_enabled = true;
                form1.button3_enabled = true;
                form1.button3_visible = true;
                form1.login = false;
            }
            else
                MessageBox.Show("Username and Password are wrong", "Warning");
        }

        private void login_user_Load(object sender, EventArgs e)
        {

        }
    }
}
