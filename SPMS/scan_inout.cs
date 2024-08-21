using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace Sparepart_Management_System
{
    public partial class scan_inout : Form
    {
        MqttClient mqttClient;
        String stats;
        public scan_inout()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            mqttClient = new MqttClient("192.168.6.100");
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            mqttClient.Subscribe(new string[] { "/DMS/OutAssy" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            mqttClient.Connect("DMS");
            if (mqttClient.IsConnected)
            {
                label1.Text = "Connected OK";
            }
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
            // listBox1.Invoke((MethodInvoker)(() => listBox1.Items.Add(message)));
            //  listView1.Invoke((MethodInvoker)(() => listView1.Items.Add(message)));
            // listView.Items.Add(subStrings[3]);
            //  listBox2.Invoke((MethodInvoker)(() => listBox2.Items.Add(message)));

            String datareceive = message;

            string[] subStrings = datareceive.Split('#');
            Console.WriteLine(subStrings[0]);
            int total_apli = int.Parse(subStrings[0]);

            


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String area;
                if (stats=="IN")
                area = comboBox1.Text+ comboBox3.Text;
                else
                area = comboBox2.Text;

                String pic = Interaction.InputBox("Part Number :" + textBox1.Text, "Input PIC", "PIC Name :");
                if (mqttClient != null && mqttClient.IsConnected)
                {
                    try
                    {
                        String senddata = textBox1.Text+"#"+stats+"#"+ area + "#" +pic;
                        label2.Text = senddata;
                        mqttClient.Publish("/DMS/InAssy", Encoding.UTF8.GetBytes(senddata));
                    }
                    catch (InvalidCastException er)
                    {
                        // recover from exception
                        AutoClosingMessageBox.Show("Error Connection", "ERROR", 1000);
                    }
                    finally
                    {
                        AutoClosingMessageBox.Show("Data Inputed", "Success", 1000);
                        textBox1.Text = "";
                    }
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            stats = "IN";
            button1.BackColor = Color.Red;
            button2.BackColor = Color.Azure;
            comboBox1.Enabled = true;
            comboBox3.Enabled = true;
            comboBox2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            stats = "OUT";
            button2.BackColor = Color.Red;
            button1.BackColor = Color.Azure;
            comboBox2.Enabled = true;
            comboBox3.Enabled = false;
            comboBox1.Enabled = false;
        }
    }
}
