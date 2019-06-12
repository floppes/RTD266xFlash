using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RTD266xFlash.Forms
{
    public partial class FormFileImage : Form
    {
        public FormFileImage()
        {
            InitializeComponent();

            modificationSettings.UpdateModifyFirmware();
        }

        private void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnInputFileNameBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtInputFileName.Text = openFileDialog.FileName;
            }
        }

        private void modificationSettings_ModifyClickedEvent()
        {
            string error;
            string inputFileName = txtInputFileName.Text;

            if (string.IsNullOrEmpty(inputFileName))
            {
                ShowErrorMessage("No input file specified!");
                return;
            }

            if (!File.Exists(inputFileName))
            {
                ShowErrorMessage($"The file {inputFileName} does not exist!");
                return;
            }

            if (!string.IsNullOrEmpty(modificationSettings.LogoFileName))
            {
                if (!FontCoder.CheckFile(modificationSettings.LogoFileName, FontCoder.FontWidthKedei, FontCoder.FontHeightKedei, out error))
                {
                    ShowErrorMessage(error);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(modificationSettings.HdmiReplacementText))
            {
                if (!FirmwareModifier.CheckHdmiReplacement(modificationSettings.HdmiReplacementText, out error))
                {
                    ShowErrorMessage($"Invalid HDMI replacement text: {error}");
                    return;
                }
            }

            byte[] firmware;

            try
            {
                firmware = File.ReadAllBytes(inputFileName);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Cannot load firmware from file {inputFileName}! {ex.Message}");
                return;
            }

            Firmware detectedFirmware = FirmwareModifier.DetectFirmware(firmware);

            if (detectedFirmware == null)
            {
                ShowErrorMessage("Error! Unknown firmware. You can send your firmware to the author (floppes@gmx.de), maybe it can be added to the known firmwares.");
                return;
            }

            if (!string.IsNullOrEmpty(modificationSettings.LogoFileName))
            {
                if (!FirmwareModifier.ChangeLogo(modificationSettings.LogoFileName, firmware, detectedFirmware, out error))
                {
                    ShowErrorMessage(error);
                    return;
                }
            }

            if (modificationSettings.LogoBackgroundColor != Color.Empty)
            {
                FirmwareModifier.ChangeLogoBackgroundColor(modificationSettings.LogoBackgroundColor, firmware, detectedFirmware);
            }

            if (modificationSettings.LogoForegroundColor != Color.Empty)
            {
                FirmwareModifier.ChangeLogoForegroundColor(modificationSettings.LogoForegroundColor, firmware, detectedFirmware);
            }

            if (modificationSettings.BackgroundColor != Color.Empty)
            {
                FirmwareModifier.ChangeBackgroundColor(modificationSettings.BackgroundColor, firmware, detectedFirmware);
            }

            FirmwareModifier.ToggleHdmiPopup(!modificationSettings.RemoveHdmiPopup, firmware, detectedFirmware);

            if (!string.IsNullOrEmpty(modificationSettings.HdmiReplacementText))
            {
                FirmwareModifier.ReplaceHdmiPopup(modificationSettings.HdmiReplacementText, firmware, detectedFirmware);
            }

            FirmwareModifier.ToggleNoSignalPopup(!modificationSettings.RemoveNoSignalPopup, firmware, detectedFirmware);

            string outputFileName = Path.GetFileNameWithoutExtension(inputFileName) + "_modified.bin";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bin files (*.bin)|*.bin";
            saveFileDialog.FileName = outputFileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(saveFileDialog.FileName, firmware);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Could not write file {saveFileDialog.FileName}! {ex.Message}");
                    return;
                }
            }

            MessageBox.Show($"The modified firmware was successfully saved to {saveFileDialog.FileName}.\r\n\r\nYou can now flash it to your RTD266x chip.", "Modify firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
