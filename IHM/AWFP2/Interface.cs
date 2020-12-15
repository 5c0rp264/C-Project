using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;

namespace AWFP2
{

    public partial class Interface : Form
    {

        public Interface()
        {
            InitializeComponent();
        }

        String button10Value = "";
        String button11Value = "";
        String button12Value = "";
        String button13Value = "";




        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {


            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            //textBox1

            String full = "false";
            if (radioButton1.Checked == true) full = "true";
            
            writer.WriteLine("Add," + textBox1.Text + "," + button13.Text + "," + button12.Text + "," + full + "," + textBox5.Text);
            writer.Flush();

        }

        private void label14_Click_1(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("Connection");
            writer.Flush();
            button9.Enabled = false;
            
            StreamReader reader = new StreamReader(stream);
            String txt = reader.ReadLine();
            string[] Text = txt.Split(',');
            foreach (var c in Text)
            {
                comboBox1.Items.Add(c);
                comboBox2.Items.Add(c);
                comboBox3.Items.Add(c);
            }
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    String msg = reader.ReadLine();

                    string[] Progress = msg.Split(',');

                    if (Progress[0] == "Stop")
                    {
                        removeProgressBar();
                    }

                    if (Progress[0] == "AddProgressBar") addProgressBar(Int32.Parse(Progress[1]));
                    if (Progress[0] == "Bar") updateTracking(Int32.Parse(Progress[1]), Int32.Parse(Progress[3]), Progress[2], "");
                }
            });
            t.Start();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("AddName");
            writer.Flush();
            */
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("Delete,"+ comboBox2.SelectedIndex);
            writer.Flush();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            String full = "false";
            if (radioButton4.Checked == true)full = "true";
            writer.WriteLine("Edit,"+ textBox6.Text+","+ button10.Text + "," + button11.Text+ ","+ full + "," + textBox4.Text + "," + comboBox1.SelectedIndex);
            writer.Flush();
          
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                button10Value = edit_browser_source.SelectedPath;
                button10.Text = button10Value;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                button11Value = edit_browser_source.SelectedPath;
                button11.Text = button11Value;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                button10Value = edit_browser_source.SelectedPath;
                button10.Text = button10Value;
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                button11Value = edit_browser_source.SelectedPath;
                button11.Text = button11Value;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                button13Value = edit_browser_source.SelectedPath;
                button13.Text = button13Value;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                button12Value = edit_browser_source.SelectedPath;
                button12.Text = button12Value;
            }
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("Execute," + comboBox3.SelectedIndex);
            writer.Flush();
        }

        private void button10_Click_2(object sender, EventArgs e)
        {

        }

        private void button11_Click_2(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Locking listener
        /// </summary>
       
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            button6.Enabled = true;
            button5.Enabled = false;
            writer.WriteLine(button5.Text);
            writer.Flush();
        }
        readonly List<ProgressBar> threadsTracking = new List<ProgressBar>();

        public delegate void addProgressBarDelegate();
        public addProgressBarDelegate trackingDelegate;
        public void addProgressBar(int id)
        {
            trackingDelegate = new addProgressBarDelegate(() => { newProgressBar(id); });
            Invoke(trackingDelegate);
        }

        public void newProgressBar(int id)
        {
            ProgressBar myProgressBar = new ProgressBar();
            myProgressBar.Name = "ProgressBar" + id; // Should add the ID here
            myProgressBar.Width = 190;
            threadsTracking.Add(myProgressBar);
            flowLayoutPanel1.Controls.Add(myProgressBar);
        }

        public delegate void removeProgressBarDelegate();
        public removeProgressBarDelegate removeTrackingDelegate;

        public void removeProgressBar()
        {
            removeTrackingDelegate = new removeProgressBarDelegate(deleteProgressBar);
            Invoke(removeTrackingDelegate);
        }
        public void deleteProgressBar()
        {
            flowLayoutPanel1.Controls.Clear();
        }


        public delegate void updateProgressBarDelegate();
        public updateProgressBarDelegate refreshTrackingDelegate;

        public void updateTracking(int id, int percent, string bname, string status = "")
        {
            refreshTrackingDelegate = new updateProgressBarDelegate(() => { refreshTracking(id, percent, bname, status); });
            Invoke(refreshTrackingDelegate);
        }
        public void refreshTracking(int id, int percent, string bname, string status)
        {
            // Update the value
            ProgressBar item = this.flowLayoutPanel1.Controls.Find("ProgressBar" + id.ToString(), false)[0] as ProgressBar;

            if (status == "end" || percent == 100)
            {
                //TODO: add persistent "complete"
                item.CreateGraphics().DrawString(bname + " : " + percent.ToString() + "COMPLETE", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(item.Width / 2 - 40, item.Height / 2 - 7));
                item.Value = 100;
            }
            else
            {
                item.CreateGraphics().DrawString(bname + " : " + percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(item.Width / 2 - 20, item.Height / 2 - 7));
                item.Value = percent;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }


        private void button7_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            
            writer.WriteLine(button7.Text);
            writer.Flush();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox7.Text);
            int port = Convert.ToInt32(numericUpDown2.Value);
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            button6.Enabled = false;
            button5.Enabled = true;
            writer.WriteLine(button6.Text);
            writer.Flush();
        }
    }
    
}
