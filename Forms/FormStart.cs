using System;
using System.Windows.Forms;

namespace RTD266xFlash.Forms
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FileImageMode)
            {
                radioButtonFile.Checked = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Form form = null;

            if (radioButtonArduino.Checked)
            {
                form = new FormArduino();
            }
            else if (radioButtonFile.Checked)
            {
                string path;
                if (!SetFirmwareFile(out path)) { return; }
                form = new FormFileImage(path);
            }
            else if (radioButton1.Checked)
            {
                string path;
                if (!SetFirmwareFile(out path)) { return; }
                form = new FormFirmwareEditor(path);
            }

            Properties.Settings.Default.FileImageMode = radioButtonFile.Checked;
            Properties.Settings.Default.Save();

            form.ShowDialog();
        }

        private bool SetFirmwareFile(out string filename)
        {
            filename = null;
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Any Supported Filetypes|*.bin;*.rom;|Binary File|*.bin|ROM (Dump) File|*.rom|Any Files|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK) { filename = ofd.FileName; return true; }
            else { return false; }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblHint_DoubleClick(object sender, EventArgs e)
        {
            FormExtras formExtras = new FormExtras();
            formExtras.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormSettings().ShowDialog();
        }
    }
}
