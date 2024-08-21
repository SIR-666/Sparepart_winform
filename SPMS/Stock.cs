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
using Sparepart_Management_System;

namespace SPMS
{
    public partial class Stock : UserControl
    {
        private Form activeform;
        string connetionString = "Data Source=192.168.2.1;Initial Catalog=Plant5;User ID=roni@ipg;Password=AutoCasting";
        SqlConnection cnn;
        string partname;
        string selecteddata;
        string getdata;
        bool stats_database = false;
        
        string usermasuk;
        Form1 form1;
        public Stock(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            button2.Visible = false;
            button2.Enabled = false;
            button3.Visible = false;
            button3.Enabled = false;
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

            if (stats_database == true)
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
            //label3.Visible = false;

            dataGridView1.Columns[2].Width = 35;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            String data_search;
            data_search = textBox1.Text;
            cnn = new SqlConnection(connetionString);
            if (comboBox1.Text == "Item Number")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SparepartPlant5 Where ItemNumber like '%'+@zip+'%'", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView1.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }
            else if (comboBox1.Text == "Part Name")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SparepartPlant5 Where ProductName like '%'+@zip+'%'", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView1.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }

            /*
            else if (comboBox1.Text == "ID Aplikator")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SPMS_STOCK Where aplikator like '%'+@zip+'%'", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView1.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }

            else if (comboBox1.Text == "Aplikator Name")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SPMS_STOCK Where name_aplikator like '%'+@zip+'%'", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView1.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }
            */

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select * From SparepartPlant5", cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception er)
            {
                //MessageBox.Show("Error Connection");
                AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                stats_database = false;
            }

            label5.Text = dataGridView1.Rows.Count.ToString();
            label7.Text = dataGridView2.Rows.Count.ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            String data_search;
            data_search = textBox2.Text;
            cnn = new SqlConnection(connetionString);
            if (comboBox2.Text == "Part Number")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SparepartPlant5 Where ItemNumber like '%'+@zip+'%' and Qty < SafetyStock", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView2.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }
            else if (comboBox2.Text == "Part Name")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SparepartPlant5 Where ProductName like '%'+@zip+'%' and Qty < SafetyStock", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView2.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }
            /*
            else if (comboBox2.Text == "ID Aplikator")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SPMS_STOCK Where aplikator like '%'+@zip+'%' and stock < 3", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView2.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }
            else if (comboBox2.Text == "Aplikator Name")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From SPMS_STOCK Where name_aplikator like '%'+@zip+'%' and stock < 3", cnn);
                cmd3.Parameters.AddWithValue("@zip", data_search);

                try
                {
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    dataGridView2.DataSource = dt3;
                }

                catch (Exception er)
                {
                    //MessageBox.Show("Error Connection");
                    AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    stats_database = false;
                }
            }
            */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select * From SparepartPlant5 Where Qty < SafetyStock", cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch (Exception er)
            {
                //MessageBox.Show("Error Connection");
                AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                stats_database = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form2 registerpart = new Form2();
            registerpart.Show();
        }

        

        
        private void button3_Click(object sender, EventArgs e)
        {
            cnn.Close();
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


                    AutoClosingMessageBox.Show(selecteddata + " deleted", "Success", 1000);
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

        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Part_Aplikator_All.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                copyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Format column D as text before pasting results, this was required for my data
                /*
                Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                rng.NumberFormat = "@";
                */
                xlWorkSheet.Cells[1, 2] = "Part Number";
                xlWorkSheet.Cells[1, 3] = "Part Name";
                xlWorkSheet.Cells[1, 4] = "Stock";
                xlWorkSheet.Cells[1, 5] = "Rak";
                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete(Type.Missing);
                xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dataGridView1.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }

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

        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboard2()
        {
            dataGridView2.SelectAll();
            DataObject dataObj = dataGridView2.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            register_user regist = new register_user();
            regist.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Part_Aplikator_Needtobuy.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                copyAlltoClipboard2();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Format column D as text before pasting results, this was required for my data
                /*
                Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                rng.NumberFormat = "@";
                */
                xlWorkSheet.Cells[1, 2] = "Part Number";
                xlWorkSheet.Cells[1, 3] = "Part Name";
                xlWorkSheet.Cells[1, 4] = "Stock";
                xlWorkSheet.Cells[1, 5] = "Rak";
                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete(Type.Missing);
                xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dataGridView2.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            history open_history = new history();
            open_history.Show();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (form1.login == false)
            {
                DataGridViewColumn coll = dataGridView1.Columns[e.ColumnIndex];

                if (coll.Index.ToString() == "0")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    selecteddata = row.Cells[0].Value.ToString();
                    MessageBox.Show(selecteddata);
                }
                if (coll.Index.ToString() == "7")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[0].Value.ToString();
                    String selectpartname = row.Cells[1].Value.ToString();
                    String jenis = row.Cells[1].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Number :" + selecteddata, "Stock:", row.Cells[7].Value.ToString());
                    if (tes != row.Cells[2].Value.ToString())
                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update SparepartPlant5 Set Qty=@stok Where ItemNumber =@zip", cnn);
                        cmd4.Parameters.AddWithValue("@stok", tes);
                        cmd4.Parameters.AddWithValue("@zip", selecteddata);

                        try
                        {
                            cnn.Open();
                            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                            DataTable dt4 = new DataTable();
                            da4.Fill(dt4);
                            // dataGridView2.DataSource = dt4;
                        }

                        catch (Exception er)
                        {
                            //MessageBox.Show("Error Connection");
                            AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                            //  stats_database = false;
                        }

                        //-----------------------history----------------------------------------------------------------------------------------------------------
                        /*
                        SqlCommand cmd5 = new SqlCommand("Insert into SPMS_TRANSAKSI (partnumber,partname,qty,jenis,noreg) values (@pnum,@pnam,@stok,@jens,@usr)", cnn);
                        cmd5.Parameters.AddWithValue("@stok", row.Cells[2].Value.ToString());
                        cmd5.Parameters.AddWithValue("@pnum", selecteddata);
                        cmd5.Parameters.AddWithValue("@pnam", selectpartname);
                        cmd5.Parameters.AddWithValue("@jens", "BEFORE REVISE");
                        cmd5.Parameters.AddWithValue("@usr", usermasuk);

                        
                        try
                        {
                           // cnn.Open();
                            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                            DataTable dt5 = new DataTable();
                            da5.Fill(dt5);
                            // dataGridView2.DataSource = dt4;
                        }

                        catch (Exception er)
                        {
                            //MessageBox.Show("Error Connection");
                            AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                            stats_database = false;
                        }

                        SqlCommand cmd6 = new SqlCommand("Insert into SPMS_TRANSAKSI (partnumber,partname,qty,jenis,noreg) values (@pnum,@pnam,@stok,@jens,@usr)", cnn);
                        cmd6.Parameters.AddWithValue("@stok", tes);
                        cmd6.Parameters.AddWithValue("@pnum", selecteddata);
                        cmd6.Parameters.AddWithValue("@pnam", selectpartname);
                        cmd6.Parameters.AddWithValue("@jens", "AFTER REVISE");
                        cmd6.Parameters.AddWithValue("@usr", usermasuk);


                        try
                        {
                            // cnn.Open();
                            SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
                            DataTable dt6 = new DataTable();
                            da6.Fill(dt6);
                            // dataGridView2.DataSource = dt4;
                        }

                        catch (Exception er)
                        {
                            //MessageBox.Show("Error Connection");
                            AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                            stats_database = false;
                        }

                        */
                        //-----------------------history----------------------------------------------------------------------------------------------------------

                    }
                }

                if (coll.Index.ToString() == "6")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[0].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Number :" + selecteddata, "rak:", row.Cells[6].Value.ToString());
                    if (tes != row.Cells[8].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update SparepartPlant5 Set SafetyStock=@zip Where ItemNumber =@pn", cnn);
                        cmd4.Parameters.AddWithValue("@pn", selecteddata);
                        cmd4.Parameters.AddWithValue("@zip", tes);

                        try
                        {
                            cnn.Open();
                            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                            DataTable dt4 = new DataTable();
                            da4.Fill(dt4);
                            // dataGridView2.DataSource = dt4;
                        }

                        catch (Exception er)
                        {
                            //MessageBox.Show("Error Connection");
                            AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                            stats_database = false;
                        }


                    }
                }

                if (coll.Index.ToString() == "8")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[0].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Number :" + selecteddata, "rak:", row.Cells[8].Value.ToString());
                    if (tes != row.Cells[8].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update SparepartPlant5 Set Location=@zip Where ItemNumber =@pn", cnn);
                        cmd4.Parameters.AddWithValue("@pn", selecteddata);
                        cmd4.Parameters.AddWithValue("@zip", tes);

                        try
                        {
                            cnn.Open();
                            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                            DataTable dt4 = new DataTable();
                            da4.Fill(dt4);
                            // dataGridView2.DataSource = dt4;
                        }

                        catch (Exception er)
                        {
                            //MessageBox.Show("Error Connection");
                            AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                            stats_database = false;
                        }


                    }
                }
                /*
                if (coll.Index.ToString() == "4")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[0].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Number :" + selecteddata, "Aplikator:", row.Cells[4].Value.ToString());
                    if (tes != row.Cells[4].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update SPMS_STOCK Set aplikator=@apli Where partnumber =@zip", cnn);
                        cmd4.Parameters.AddWithValue("@apli", tes);
                        cmd4.Parameters.AddWithValue("@zip", selecteddata);

                        try
                        {
                            cnn.Open();
                            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                            DataTable dt4 = new DataTable();
                            da4.Fill(dt4);
                            // dataGridView2.DataSource = dt4;
                        }

                        catch (Exception er)
                        {
                            //MessageBox.Show("Error Connection");
                            AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                            stats_database = false;
                        }
                        
                    }
                }

                if (coll.Index.ToString() == "5")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[0].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Number :" + selecteddata, "Aplikator Name:", row.Cells[5].Value.ToString());
                    if (tes != row.Cells[5].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update SPMS_STOCK Set name_aplikator=@apli Where partnumber =@zip", cnn);
                        cmd4.Parameters.AddWithValue("@apli", tes);
                        cmd4.Parameters.AddWithValue("@zip", selecteddata);

                        try
                        {
                            cnn.Open();
                            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                            DataTable dt4 = new DataTable();
                            da4.Fill(dt4);
                            // dataGridView2.DataSource = dt4;
                        }

                        catch (Exception er)
                        {
                            //MessageBox.Show("Error Connection");
                            AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                            stats_database = false;
                        }
                        
                    }
                }
                */
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


     

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //guna2Button1.CustomBorderThickness.Equals(guna2Button2.CustomBorderThickness);
            //guna2Button1.CustomBorderThickness.Bottom = 0;
            //guna2Button1.CustomBorderThickness = 0b0;
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
                        
                       
                        button2.Visible = form1.button2_visible;
                        button2.Enabled = form1.button2_enabled;
                        button3.Visible = form1.button3_enabled;
                        button3.Enabled = form1.button3_visible;
                        //button1.Enabled = true;

                    }
                }
            }
        }
    }
}


