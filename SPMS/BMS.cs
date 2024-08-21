using Microsoft.VisualBasic;
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
    public partial class BMS : Form
    {
        string connetionString = "Data Source=192.168.5.4;Initial Catalog=KUJNAGN;User ID=nganjuk;Password=Excited2020";
        SqlConnection cnn;
        bool login=true;
        bool stats_database = false;
        String getdata;
        String usermasuk, selecteddata, datadeleted;
        public BMS()
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
                SqlCommand cmd = new SqlCommand("Select * From MSAssyBoard", cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                /*
                SqlCommand cmd2 = new SqlCommand("Select * From SPMS_STOCK Where stock < 3", cnn);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;
                */
            }

            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 65;
            dataGridView1.Columns[4].Width = 50;
            //dataGridView2.Columns[2].Width = 35;
        }

        private void BMS_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            add_assy add_assy = new add_assy();
            add_assy.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String data_search;
            data_search = textBox1.Text;
            cnn = new SqlConnection(connetionString);
            if (comboBox1.Text == "Part Name")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From MSAssyBoard Where PartName like '%'+@zip+'%' or Common like '%'+@zip+'%'", cnn);
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

            if (comboBox1.Text == "Series")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From MSAssyBoard Where Series like '%'+@zip+'%'", cnn);
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

            if (comboBox1.Text == "Common")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From MSAssyBoard Where Common like '%'+@zip+'%'", cnn);
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

            if (comboBox1.Text == "Area")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From MSAssyBoard Where Area like '%'+@zip+'%'", cnn);
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

            if (comboBox1.Text == "Status")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From MSAssyBoard Where Status like '%'+@zip+'%'", cnn);
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


            if (comboBox1.Text == "Condition")
            {
                SqlCommand cmd3 = new SqlCommand("Select * From MSAssyBoard Where Condition like '%'+@zip+'%'", cnn);
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * From MSAssyBoard", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }



        private void changedata(DataGridViewCellEventArgs e)
        {
            if (login == false)
            {
                DataGridViewColumn coll = dataGridView1.Columns[e.ColumnIndex];
                if (coll.Index.ToString() == "1")
                {
                    DataGridViewRow row_ = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    // label8.Text = coll.Index.ToString();
                    datadeleted = row_.Cells[1].Value.ToString();
                }
                /*
                if (coll.Index.ToString() == "2")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[0].Value.ToString();
                    String selectpartname = row.Cells[1].Value.ToString();
                    String jenis = row.Cells[1].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Number :" + selecteddata, "Stock:", row.Cells[2].Value.ToString());
                    if (tes != row.Cells[2].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update SPMS_STOCK Set stock=@stok Where partnumber =@zip", cnn);
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




                    }
                }

                */
                if (coll.Index.ToString() == "3")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    // label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[1].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Name :" + selecteddata, "Area:", row.Cells[3].Value.ToString());
                    if (tes != row.Cells[3].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update MSAssyBoard Set Area=@apli Where PartName =@zip and Series=@ser", cnn);
                        cmd4.Parameters.AddWithValue("@apli", tes);
                        cmd4.Parameters.AddWithValue("@zip", selecteddata);
                        String series_ = row.Cells[2].Value.ToString();
                        cmd4.Parameters.AddWithValue("@ser", series_);

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

                if (coll.Index.ToString() == "7")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    // label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[1].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Name :" + selecteddata, "Area:", row.Cells[7].Value.ToString());
                    if (tes != row.Cells[7].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update MSAssyBoard Set Condition=@apli Where PartName =@zip and Series=@ser", cnn);
                        cmd4.Parameters.AddWithValue("@apli", tes);
                        cmd4.Parameters.AddWithValue("@zip", selecteddata);
                        String series_ = row.Cells[2].Value.ToString();
                        cmd4.Parameters.AddWithValue("@ser", series_);

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

                if (coll.Index.ToString() == "9")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    //DataGridViewRow col = dataGridView1.Columns[e.ColumnIndex];

                    // label8.Text = coll.Index.ToString();
                    selecteddata = row.Cells[1].Value.ToString();
                    cnn = new SqlConnection(connetionString);
                    String tes = Interaction.InputBox("Part Name :" + selecteddata, "Area:", row.Cells[9].Value.ToString());
                    if (tes != row.Cells[9].Value.ToString())

                    {
                        Console.WriteLine(tes);
                        SqlCommand cmd4 = new SqlCommand("Update MSAssyBoard Set Status=@apli Where PartName =@zip and Series=@ser", cnn);
                        cmd4.Parameters.AddWithValue("@apli", tes);
                        cmd4.Parameters.AddWithValue("@zip", selecteddata);
                        String series_ = row.Cells[2].Value.ToString();
                        cmd4.Parameters.AddWithValue("@ser", series_);

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //changedata(e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            changedata(e);
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            scan_inout scan_inout = new scan_inout();
            scan_inout.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connetionString);
            SqlDataReader dataReader;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            Console.WriteLine(datadeleted);
            if (datadeleted != "")
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("Delete MSAssyBoard Where PartName = @deletpart", cnn);
                    cmd.Parameters.AddWithValue("@deletpart", datadeleted);
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


                    AutoClosingMessageBox.Show(datadeleted + " deleted", "Success", 1000);
                    cnn.Close();
                }
            }
            //selecteddata = dataGridView1.SelectedCells;
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
                      //  label3.Visible = true;
                      //  label3.Text = "Welcome " + split[0];
                        usermasuk = split[0];
                        button8.Text = "Logout";
                        login = false;
                       

                    }
                }
            }
        }
    }
}
