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


    public partial class history : Form
    {



    string connetionString = "Data Source=192.168.5.4;Initial Catalog=KUJNAGN;User ID=nganjuk;Password=Excited2020";
        SqlConnection cnn;
        int dataterima;
        bool stats_database = false;
        int[] qtytotal = new int[15];
       

        public history()
        {
            InitializeComponent();

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

            if (stats_database == true)
            {
                
                SqlCommand cmd2 = new SqlCommand("Select * From SPMS_TRANSAKSI order by tanggal desc", cnn);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;

               
                SqlCommand cmd3 = new SqlCommand("SELECT TOP 10 partnumber AS MODE FROM SPMS_TRANSAKSI where jenis = 'OUT'  GROUP BY partnumber ORDER BY COUNT(*) DESC", cnn);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                dataGridView1.DataSource = dt3;


                dt3.Columns.Add("qty out", typeof(int));

                for (int i = 0; i < 10; i++)
                {
                    DataGridViewRow row1 = (DataGridViewRow)dataGridView1.Rows[i];
                    String dat1 = (string)row1.Cells[0].Value;
                    dataterima = 0;

                    SqlCommand command = new SqlCommand("SELECT qty FROM SPMS_TRANSAKSI where jenis='OUT' and partnumber = @zip", cnn);
                    command.Parameters.AddWithValue("@zip", dat1);
                    // int result = command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            String qty = String.Format("{0}", reader["qty"]);
                            int qtyint = Convert.ToInt32(qty);
                            
                            dataterima = dataterima + qtyint;
                        }
                    }
                    qtytotal[i] = dataterima;
                    //label1.Text = dataterima.ToString();
                    Console.WriteLine(dataterima.ToString());

                    
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[i];
                    row.Cells[1].Value = dataterima;

                }
              


            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Select * From SPMS_TRANSAKSI order by tanggal desc", cnn);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String data_search;
            data_search = textBox1.Text;
            if (comboBox1.Text == "Part Number")
            {
                SqlCommand cmd2 = new SqlCommand("Select * From SPMS_TRANSAKSI Where partnumber like '%'+@zip+'%' order by tanggal desc", cnn);
                cmd2.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    dataGridView2.DataSource = dt2;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }

            if (comboBox1.Text == "Part Name")
            {
                SqlCommand cmd2 = new SqlCommand("Select * From SPMS_TRANSAKSI Where partname like '%'+@zip+'%' order by tanggal desc", cnn);
                cmd2.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    dataGridView2.DataSource = dt2;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }

            if (comboBox1.Text == "ID Aplikator")
            {
                SqlCommand cmd2 = new SqlCommand("Select * From SPMS_TRANSAKSI Where aplikator like '%'+@zip+'%' order by tanggal desc", cnn);
                cmd2.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    dataGridView2.DataSource = dt2;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }
            

            if (comboBox1.Text == "Jenis Action")
            {
                SqlCommand cmd2 = new SqlCommand("Select * From SPMS_TRANSAKSI Where jenis like '%'+@zip+'%' order by tanggal desc", cnn);
                cmd2.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    dataGridView2.DataSource = dt2;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }


        }
    }

}
