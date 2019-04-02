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
            Form form;

            if (radioButtonArduino.Checked)
            {
                form = new FormArduino();
            }
            else
            {
                form = new FormFileImage();
            }

            Properties.Settings.Default.FileImageMode = radioButtonFile.Checked;
            Properties.Settings.Default.Save();

            form.ShowDialog();
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
    }
}
