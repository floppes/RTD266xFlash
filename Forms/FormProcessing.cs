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
using RTD266xFlash;

namespace RTD266xFlash.Forms
{
    public partial class FormProcessing : Form
    {
        public FormProcessing(Form parent)
        {
            InitializeComponent();
            _parent = parent;
            _parent.Enabled = false;
        }

        public void CloseForm() { _close = true; _parent.Enabled = true; Close(); }
        public void ChangeText(string text) { progressInfo.Text = text; Update(); }

        private bool _close;
        private Form _parent;
        private void FormProcessing_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_close;
        }

        private void FormProcessing_Load(object sender, EventArgs e)
        {
            Location = _parent.Location;
        }
    }
}
