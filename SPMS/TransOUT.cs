using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPMS
{
    public partial class TransOUT : UserControl
    {
        string connetionString = "Data Source=192.168.2.1;Initial Catalog=Plant5;User ID=roni@ipg;Password=AutoCasting";

        //string connetionString = "Data Source=192.168.5.4;Initial Catalog=DMS;User ID=dimas;Password=Satusampai9";
        SqlConnection cnn;
        string partname_before;
        string partname_current;
        public TransOUT()
        {
            InitializeComponent();
            
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
                MessageBox.Show(er.Message);
            }

            SqlCommand cmd = new SqlCommand("select * from SparepartPlant5", cnn); // "list aplikator" ("select id,hobby from table 1", conn)
            //cmd.Parameters.AddWithValue("@apli", rjTextBox1.Texts);
            SqlDataReader da = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(da);
            dataGridView1.DataSource = dt;
            //return dt;
            datagridget();
            InitializeDomainUpDown();
        }

        private void buttonIncrement_Click(object sender, EventArgs e)
        {
            IncrementDomainUpDown();
        }

        private void buttonDecrement_Click(object sender, EventArgs e)
        {
            DecrementDomainUpDown();
        }

        private void IncrementDomainUpDown()
        {
            if (domainUpDown1.SelectedIndex < domainUpDown1.Items.Count - 1)
            {
                domainUpDown1.SelectedIndex++;
            }
        }

        // Decrement the selected number
        private void DecrementDomainUpDown()
        {
            if (domainUpDown1.SelectedIndex > 0)
            {
                domainUpDown1.SelectedIndex--;
            }
        }

        private void InitializeDomainUpDown()
        {
            // Add numbers to the DomainUpDown control
            for (int i = 0; i <= 200; i++)
            {
                domainUpDown1.Items.Add(i);
            }

            
            domainUpDown1.Text = domainUpDown1.Items[0].ToString();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {


        }
        private void datagridget()
        {

            int numRows = dataGridView1.Rows.Count;
            //string[] Datavalue = new string[numRows];
            //foreach (DataGridViewRow row in SF_CNC.dataGridView1.Rows)
            string datavalue;
            for (int j = 0; j < numRows; j++)
            {
                try
                {
                    datavalue = Convert.ToString(dataGridView1.Rows[j].Cells[1].Value);
                    Console.WriteLine(datavalue);
                    if (datavalue != null)
                        comboBox1.Items.Add(datavalue);
                }
                catch { }
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox2.Text != "")
            {
                cnn.Close();
                addreq addreqq = new addreq(this);
                //ucDays.day_show(i);
                flowLayoutPanel1.Controls.Add(addreqq);
                flowLayoutPanel1.ScrollControlIntoView(addreqq);
                addreqq.label1.Text = comboBox1.Text;
                addreqq.label2.Text = domainUpDown1.Text;
                addreqq.label4.Text = textBox1.Text;
                addreqq.label3.Text = comboBox2.Text;
                addreqq.textBox1.Text = textBox2.Text;

                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("exec UpdateSparepartAndInsertHistory @qty = @jumlah, @prodname = @name ,@itemNumber ='-' ,@pic = @noreg,@dept = @depart,@status = @stats,@keterangan = @ket;", cnn);
                    cmd.Parameters.AddWithValue("@jumlah", domainUpDown1.Text);
                    cmd.Parameters.AddWithValue("@name", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@noreg", textBox1.Text);
                    cmd.Parameters.AddWithValue("@depart", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@stats", "OUT");
                    cmd.Parameters.AddWithValue("@ket", textBox2.Text);

                    //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                    //AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                }
                finally
                {


                    AutoClosingMessageBox.Show(comboBox1.Text + " OUT", "Success", 1000);
                    cnn.Close();
                }
            }
            else
            {
                MessageBox.Show("Input Noreg & Dept");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnn.Close();
            try
            {
                cnn.Open();
                SqlDataReader dataReader, dataReader2;
                //SqlCommand cmd = new SqlCommand("select * from historical_pattern_basepattern_transactions where pattern_name = @pattern", cnn);
                SqlCommand cmd = new SqlCommand("select Qty from SparepartPlant5 where ProductName = @prodnname", cnn);
                cmd.Parameters.AddWithValue("@prodnname", comboBox1.Text);
                //cmd.ExecuteNonQuery();

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {


                    label7.Text = String.Format("{0}", dataReader["Qty"]);


                    MessageBox.Show("Process Success");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}
