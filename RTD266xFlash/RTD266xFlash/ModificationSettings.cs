using System;
using System.Drawing;
using System.Windows.Forms;

namespace RTD266xFlash
{
    public partial class ModificationSettings : UserControl
    {
        #region Events

        public delegate void ModifyClickedHandler();

        /// <summary>
        /// Modify button clicked event
        /// </summary>
        public event ModifyClickedHandler ModifyClickedEvent;

        #endregion

        #region Members

        private bool _guiUpdate;

        #endregion

        #region Properties

        /// <summary>
        /// Enabled state of modify button
        /// </summary>
        public bool ModifyEnabled
        {
            get
            {
                return btnModify.Enabled;
            }
            set
            {
                btnModify.Enabled = value;
            }
        }

        /// <summary>
        /// Logo file name or null if none selected
        /// </summary>
        public string LogoFileName
        {
            get
            {
                if (chkChangeLogo.Checked)
                {
                    return txtLogoFileName.Text;
                }
                
                return null;
            }
        }

        /// <summary>
        /// Selected logo background color or empty if none selected
        /// </summary>
        public Color LogoBackgroundColor
        {
            get
            {
                if (chkChangeLogoBackgroundColor.Checked)
                {
                    return Color.FromArgb((int)numericLogoBackgroundRed.Value, (int)numericLogoBackgroundGreen.Value, (int)numericLogoBackgroundBlue.Value);
                }

                return Color.Empty;
            }
        }

        /// <summary>
        /// Selected logo foreground color or empty if none selected
        /// </summary>
        public Color LogoForegroundColor
        {
            get
            {
                if (chkChangeLogoForegroundColor.Checked)
                {
                    return Color.FromArgb((int)numericLogoForegroundRed.Value, (int)numericLogoForegroundGreen.Value, (int)numericLogoForegroundBlue.Value);
                }

                return Color.Empty;
            }
        }

        /// <summary>
        /// Selected background color or empty if none selected
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                if (chkChangeBackgroundColor.Checked)
                {
                    return Color.FromArgb((int)numericBackgroundRed.Value, (int)numericBackgroundGreen.Value, (int)numericBackgroundBlue.Value);
                }

                return Color.Empty;
            }
        }

        /// <summary>
        /// Remove "HDMI" popup
        /// </summary>
        public bool RemoveHdmiPopup
        {
            get
            {
                return chkRemoveHdmi.Checked;
            }
        }

        /// <summary>
        /// Replacement text for "HDMI" popup
        /// </summary>
        public string HdmiReplacementText
        {
            get
            {
                if (chkChangeHdmi.Checked)
                {
                    return txtChangeHdmi.Text;
                }

                return null;
            }
        }

        #endregion

        public ModificationSettings()
        {
            InitializeComponent();
        }

        public void UpdateModifyFirmware()
        {
            _guiUpdate = true;

            txtLogoFileName.Enabled = chkChangeLogo.Checked;
            btnLogoFileNameBrowse.Enabled = chkChangeLogo.Checked;

            numericLogoBackgroundRed.Enabled = chkChangeLogoBackgroundColor.Checked;
            numericLogoBackgroundGreen.Enabled = chkChangeLogoBackgroundColor.Checked;
            numericLogoBackgroundBlue.Enabled = chkChangeLogoBackgroundColor.Checked;
            picLogoBackgroundColor.Enabled = chkChangeLogoBackgroundColor.Checked;

            numericLogoForegroundRed.Enabled = chkChangeLogoForegroundColor.Checked;
            numericLogoForegroundGreen.Enabled = chkChangeLogoForegroundColor.Checked;
            numericLogoForegroundBlue.Enabled = chkChangeLogoForegroundColor.Checked;
            picLogoForegroundColor.Enabled = chkChangeLogoForegroundColor.Checked;

            numericBackgroundRed.Enabled = chkChangeBackgroundColor.Checked;
            numericBackgroundGreen.Enabled = chkChangeBackgroundColor.Checked;
            numericBackgroundBlue.Enabled = chkChangeBackgroundColor.Checked;
            picBackgroundColor.Enabled = chkChangeBackgroundColor.Checked;

            if (chkRemoveHdmi.Checked)
            {
                chkChangeHdmi.Checked = false;
                chkChangeHdmi.Enabled = false;
            }
            else
            {
                chkChangeHdmi.Enabled = true;
            }

            if (chkChangeHdmi.Checked)
            {
                chkRemoveHdmi.Checked = false;
                chkRemoveHdmi.Enabled = false;
            }
            else
            {
                chkRemoveHdmi.Enabled = true;
            }

            txtChangeHdmi.Enabled = chkChangeHdmi.Checked;

            _guiUpdate = false;
        }

        private void FillColorBox(PictureBox pictureBox, int red, int green, int blue)
        {
            pictureBox.BackColor = Color.FromArgb(red, green, blue);
        }

        private void ShowColorDialog(NumericUpDown numericRed, NumericUpDown numericGreen, NumericUpDown numericBlue)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = Color.FromArgb((int)numericRed.Value, (int)numericGreen.Value, (int)numericBlue.Value);

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                numericRed.Value = colorDialog.Color.R;
                numericGreen.Value = colorDialog.Color.G;
                numericBlue.Value = colorDialog.Color.B;
            }
        }

        private void btnLogoFileNameBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.png, *.bmp, *.tif, *.tiff)|*.png;*.bmp;*.tif;*.tiff";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string error;

                if (!FontCoder.CheckFile(openFileDialog.FileName, FontCoder.FontWidthKedei, FontCoder.FontHeightKedei, out error))
                {
                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    txtLogoFileName.Text = openFileDialog.FileName;
                }
            }
        }

        #region GUI events

        private void picLogoBackgroundColor_Click(object sender, EventArgs e)
        {
            ShowColorDialog(numericLogoBackgroundRed, numericLogoBackgroundGreen, numericLogoBackgroundBlue);
        }

        private void picLogoForegroundColor_Click(object sender, EventArgs e)
        {
            ShowColorDialog(numericLogoForegroundRed, numericLogoForegroundGreen, numericLogoForegroundBlue);
        }

        private void picBackgroundColor_Click(object sender, EventArgs e)
        {
            ShowColorDialog(numericBackgroundRed, numericBackgroundGreen, numericBackgroundBlue);
        }

        private void numericLogoBackground_ValueChanged(object sender, EventArgs e)
        {
            FillColorBox(picLogoBackgroundColor, (int)numericLogoBackgroundRed.Value, (int)numericLogoBackgroundGreen.Value, (int)numericLogoBackgroundBlue.Value);
        }

        private void numericLogoForeground_ValueChanged(object sender, EventArgs e)
        {
            FillColorBox(picLogoForegroundColor, (int)numericLogoForegroundRed.Value, (int)numericLogoForegroundGreen.Value, (int)numericLogoForegroundBlue.Value);
        }

        private void numericBackground_ValueChanged(object sender, EventArgs e)
        {
            FillColorBox(picBackgroundColor, (int)numericBackgroundRed.Value, (int)numericBackgroundGreen.Value, (int)numericBackgroundBlue.Value);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyClickedEvent?.Invoke();
        }

        private void chkModifyFirmware_CheckedChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            UpdateModifyFirmware();
        }

        #endregion
    }
}
