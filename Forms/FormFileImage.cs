using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic;
using System.Threading;

namespace RTD266xFlash.Forms
{
    public partial class FormFileImage : Form
    {
        public FormFileImage(string path)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            filePath = path;
            firmware = File.ReadAllBytes(filePath);
            label1.Text = Path.GetFileName(filePath);
            if (Directory.Exists(Environment.CurrentDirectory + "//firmware_presets"))
            {
                List<Firmware> knownFirms = new List<Firmware>();
                List<string> firmNames = new List<string>();
                FileInfo[] files = new DirectoryInfo(Environment.CurrentDirectory + "//firmware_presets").GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension != ".ini") { continue; }
                    try
                    {
                        Firmware firm = Firmware.FromFile(file.FullName);
                        knownFirms.Add(firm);
                        firmNames.Add(firm.Name);
                    }
                    catch
                    {
                        MessageBox.Show("Could not load preset '" + file.Name + "'!", "Error loading firmware preset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                firmNames.Sort();
                Firmware.KnownFirmwares = knownFirms.ToArray();
                comboBox1.Items.AddRange(firmNames.ToArray());
            }
            else
            {
                MessageBox.Show("No firmware presets found!\n\nYou can download known firmware presets at https://github.com/miso-xyz/RTD266xFlashX/firmware_presets.zip and extract them in a folder named 'firmware_presets' in the app folder");
            }

            modificationSettings.UpdateModifyFirmware();
        }

        private void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        string filePath;
        byte[] firmware;

        private void button1_Click(object sender, EventArgs e)
        {
            FormProcessing frm = new FormProcessing(this);
            //frm.Show();

            Thread tr = new Thread(new ThreadStart(delegate
            {
                Application.Run(frm);
            }));
            tr.Start();

            frm.progressInfo.Text = "Checking logo size...";
            frm.Update();

            string error;

            if (!string.IsNullOrEmpty(modificationSettings.LogoFileName))
            {
                if (!FontCoder.CheckFile(modificationSettings.LogoFileName, FontCoder.FontWidthKedei, FontCoder.FontHeightKedei, out error))
                {
                    frm.CloseForm();
                    ShowErrorMessage(error);
                    return;
                }
            }

            frm.progressInfo.Text = "Checking HDMI Replacement text...";
            
            if (!string.IsNullOrEmpty(modificationSettings.HdmiReplacementText))
            {
                if (!FirmwareModifier.CheckHdmiReplacement(modificationSettings.HdmiReplacementText, out error))
                {
                    
                    frm.CloseForm();
                    ShowErrorMessage($"Invalid HDMI Replacement text!\n\n{error}");
                    return;
                }
            }

            Firmware detectedFirmware = null;
            if (comboBox1.SelectedIndex == 0)
            {
                detectedFirmware = Properties.Settings.Default.UseFastAlgorithm ? FirmwareModifier.FindMatchPattern(firmware, frm) : FirmwareModifier.DetectFirmware(firmware, frm);

                if (detectedFirmware != null)
                {
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(detectedFirmware.Name);
                }
                else
                {
                    frm.progressInfo.Text = "Analyzing firmware...";
                    detectedFirmware = FirmwareAnalyzer.AnalyzeFirmware(firmware);
                    if (detectedFirmware == null)
                    {
                        
                        frm.CloseForm();
                        //ShowErrorMessage("Could not find known patterns in given firmware"); - AnalyzeFirmware already has errors
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Firmware Analyzed, Export generated preset?", "Firmware successfully analyzed", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            if (!Directory.Exists("firmware_presets")) { Directory.CreateDirectory("firmware_presets"); }
                            detectedFirmware.Name = Interaction.InputBox("Enter Analyzed Preset Name", "Save Generated Preset", "AnalysisGeneration-" + Path.GetFileNameWithoutExtension(filePath));
                            detectedFirmware.ToFile(Environment.CurrentDirectory + "\\firmware_presets\\" + detectedFirmware.HashInfo.Hash + ".ini");
                        }
                    }
                }
            }
            else
            {
                detectedFirmware = Firmware.KnownFirmwares[comboBox1.SelectedIndex];
            }

            frm.progressInfo.Text = "Patching...";
            if (!string.IsNullOrEmpty(modificationSettings.LogoFileName))
            {
                if (!FirmwareModifier.ChangeLogo(modificationSettings.LogoFileName, firmware, detectedFirmware, out error))
                {
                    
                    frm.CloseForm();
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

            string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_modified.bin";

            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Any Supported Filetypes|*.bin;*.rom;|Binary File|*.bin|ROM (Dump) File|*.rom|Any Files|*.*" };
            sfd.FileName = outputFileName;

            frm.progressInfo.Text = "Saving patched firmware...";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(sfd.FileName, firmware);
                }
                catch (Exception ex)
                {
                    
                    frm.CloseForm();
                    ShowErrorMessage($"Could not write file '{sfd.FileName}'!\n\n {ex.Message}");
                    return;
                }
            }
            else
            {
                frm.CloseForm();
                return;
            }
            frm.progressInfo.Text = "Done!";
            
            frm.CloseForm();
            MessageBox.Show($"The modified firmware was successfully saved to {sfd.FileName}.\r\n\r\nYou can now flash it to your RTD266x chip.", "Patch successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
