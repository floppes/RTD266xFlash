using System;
using System.IO;
using System.Windows.Forms;

namespace RTD266xFlash.Forms
{
    public partial class FormExtras : Form
    {
        public FormExtras()
        {
            InitializeComponent();
        }

        private void btnIdentifyFirmware_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";

            byte[] firmware;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                firmware = File.ReadAllBytes(openFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open file {openFileDialog.FileName}! {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Firmware detectedFirmware = null;

            foreach (Firmware fw in Firmware.KnownFirmwares)
            {
                if (fw.CheckHash(firmware))
                {
                    detectedFirmware = fw;
                    break;
                }
            }

            if (detectedFirmware == null)
            {
                MessageBox.Show("No matching firmware found!", "Firmware identification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MessageBox.Show($"Detected firmware is {detectedFirmware.Name}.", "Firmware identification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCalculateHash_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            byte[] firmwareData = File.ReadAllBytes(openFileDialog.FileName);

            HashInfo hashInfo = new HashInfo(0, 0x80000, string.Empty, new[]
            {
                new HashSkip(0xD2C9 + 0x1D, 48),
                new HashSkip(0x12346, 16),
                new HashSkip(0x13A31, 48),
                new HashSkip(0x14733, 1),
                new HashSkip(0x15577, 1),
                new HashSkip(0x260D8, 1507)
            });

            MessageBox.Show(hashInfo.GetHash(firmwareData), "Firmware hash", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDecodeFont_Click(object sender, EventArgs e)
        {
            FormFont formFont = new FormFont();
            formFont.Show();
        }
    }
}
