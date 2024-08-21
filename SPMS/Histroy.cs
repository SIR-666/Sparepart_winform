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
using Excel = Microsoft.Office.Interop.Excel;

namespace SPMS
{
    public partial class Histroy : UserControl
    {
        string connetionString = "Data Source=192.168.2.1;Initial Catalog=Plant5;User ID=roni@ipg;Password=AutoCasting";
        SqlConnection cnn;
        int dataterima;
        bool stats_database = false;
        int[] qtytotal = new int[15];
        public Histroy()
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

                SqlCommand cmd2 = new SqlCommand("Select * From sparepart_management_history_sparepart_transaction order by tanggal desc", cnn);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;


                SqlCommand cmd3 = new SqlCommand("SELECT TOP 10 product_name AS MODE FROM sparepart_management_history_sparepart_transaction where status = 'OUT'  GROUP BY product_name ORDER BY COUNT(*) DESC", cnn);
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

                    SqlCommand command = new SqlCommand("SELECT qty FROM sparepart_management_history_sparepart_transaction where status='OUT' and product_name = @zip", cnn);
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

        private void button4_Click(object sender, EventArgs e)
        {
            String data_search;
            data_search = textBox1.Text;
           

            if (comboBox1.Text == "Part Name")
            {
                SqlCommand cmd2 = new SqlCommand("Select * From sparepart_management_history_sparepart_transaction Where product_name like '%'+@zip+'%' order by tanggal desc", cnn);
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
                SqlCommand cmd2 = new SqlCommand("Select * From sparepart_management_history_sparepart_transaction Where status like '%'+@zip+'%' order by tanggal desc", cnn);
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

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Select * From sparepart_management_history_sparepart_transaction order by tanggal desc", cnn);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);

                // Add column headers
                for (int i = 1; i < dataGridView.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
                }

                // Add row data
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        excelApp.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }

                // Save the excel file
                excelApp.ActiveWorkbook.SaveCopyAs(filePath);
                excelApp.ActiveWorkbook.Saved = true;
                excelApp.Quit();

                MessageBox.Show("Data Exported Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save as Excel File";
            saveFileDialog.FileName = "sparepart.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(dataGridView1, saveFileDialog.FileName);
            }
        }
    }
}
