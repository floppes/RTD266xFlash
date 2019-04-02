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

            lblProgram.Text = Application.ProductName + " Version " + Application.ProductVersion;
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
