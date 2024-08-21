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
    public partial class add_assy : Form
    {
        string connetionString = "Data Source=192.168.5.4;Initial Catalog=KUJNAGN;User ID=nganjuk;Password=Excited2020";
        SqlConnection cnn;
        String fillarea;
        public add_assy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // SqlCommand cmd = new SqlCommand("Insert into MSAssyBoard (PartName,Series,Area,Common,DateIn,Condition,PIC,Status) values(@pnum,@ser,@area,@cmn,@dt,@cnd,@pic,@stat)", cnn);


            

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
                // SqlCommand cmd = new SqlCommand("Insert into MSAssyBoard (PartName) values(@pnum)", cnn);
                 SqlCommand cmd = new SqlCommand("Insert into MSAssyBoard (PartName,Series,Area,Common,DateIn,Condition,PIC,Status) values(@pnum,@ser,@area,@cmn,@dt,@cnd,@pic,@stat)", cnn);


                // SqlCommand cmd = new SqlCommand("Insert into MSAssyBoard (PartName,Series,Area,Common,DateIn) values(@pnum,@ser,@area,@cmn,@dt)", cnn);


                cmd.Parameters.AddWithValue("@pnum", textBox2.Text);

                
                cmd.Parameters.AddWithValue("@ser", comboBox3.Text);

                if (comboBox2.Text != "")
                    fillarea = comboBox1.Text + "." + comboBox2.Text;
                else
                    fillarea = comboBox1.Text;




                cmd.Parameters.AddWithValue("@area", fillarea);
                cmd.Parameters.AddWithValue("@cmn", textBox3.Text);
               
                cmd.Parameters.AddWithValue("@dt", DateTime.Now);
                
               cmd.Parameters.AddWithValue("@cnd", comboBox5.Text);
               cmd.Parameters.AddWithValue("@pic", textBox1.Text);
               cmd.Parameters.AddWithValue("@stat", comboBox4.Text);
               
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
