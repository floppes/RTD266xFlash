using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTD266xFlash.Forms
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            checkBox1.Checked = Properties.Settings.Default.UseFastAlgorithm;
            checkBox2.Checked = Properties.Settings.Default.UseLatestFound;
        }

        private void checkBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "Compares byte offsets instead of generating SHA256 hashes while searching in known firmwares\n\nPros:\n- Much Faster\n\nCons:\n- Less Accurate";
        }

        private void FormSettings_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseFastAlgorithm = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseLatestFound = checkBox2.Checked;
        }

        private void checkBox2_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "Uses latest index pattern found in the input firmware instead of the first (more than one instance of the pattern to be found eitherway in this version of RTD266xFlash)\n\nPros:\n|- Pattern is only found once: None\n|- Pattern is found multiple times: Unknown\n\nCons:\n- Could corrupt the input firmware";
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
