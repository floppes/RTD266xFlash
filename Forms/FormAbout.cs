using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RTD266xFlash.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

            lblProgram.Text = Application.ProductName + " Version b" + Application.ProductVersion + "\nRTD266xFlash Version 2.12.0.0";
        }

        private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lblLink.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
