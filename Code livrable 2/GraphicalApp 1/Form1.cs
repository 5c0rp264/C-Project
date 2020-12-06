using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalApp_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            addPanel.Visible = false;
            editPanel.Visible = false;
            deletePanel.Visible = false;
            executePanel.Visible = false;
            homePanel.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addBackupButton_Click(object sender, EventArgs e)
        {
            
        }

        private void editBackupButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteBackupButton_Click(object sender, EventArgs e)
        {

        }

        private void executeBackupButton_Click(object sender, EventArgs e)
        {
            homePanel.Visible = false;
            addPanel.Visible = true;
            addPanel.BringToFront();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EasySave version 2.0");
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            homePanel.Show();
            homePanel.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
