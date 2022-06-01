using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Windows.Forms;
using RTD266xFlash;
using Microsoft.VisualBasic;

namespace RTD266xFlash.Forms
{
    public partial class FormFirmwareEditor : Form
    {
        public FormFirmwareEditor(string path)
        {
            InitializeComponent();
            filePath = path;
            InitPresetEditor();
            hexBox1.Focus();
        }

        private Control[] GetAllControls(Control container = null)
        {
            List<Control> result = new List<Control>();
            Control.ControlCollection src = container == null ? fpeControl.Controls : container.Controls;
            foreach (Control control in src)
            {
                if (control is Panel) { result.AddRange(GetAllControls(control)); }
                else { result.Add(control); }
            }
            return result.ToArray();
        }

        private void SetNewLength(int value)
        {
            if (value > rawFile.Length)
            {
                if (MessageBox.Show("The preset's length is bigger than the firmware, do you want to use the new length given by the imported preset?\n\nNote: This might cause the editor to be unstable", "Invalid Length", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }
            if (value < rawFile.Length)
            {
                if (MessageBox.Show("The preset's length is smaller than the firmware, do you want to use the new length given by the imported preset?\n\nNote: Offsets might be off, corrupting the firmware", "Invalid Length", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }
            foreach (Control control in GetAllControls())
            {
                if (control is NumericUpDown) { ((NumericUpDown)control).Maximum = value; }
            }
        }

        public void InitPresetEditor()
        {
            fpeControl = new FirmwarePresetEditor();

            /*fpeNumericCTX = new ContextMenu();
            fpeNumericCTX.MenuItems.AddRange(new[]
            {
                new MenuItem("Change to", new [] { new MenuItem("Selected Byte Offset")})
            };*/
            if (firmwareHash != null) { fpeControl.firmwareHash.Text = firmwareHash; }
            fpeControl.presetName.Text = Path.GetFileNameWithoutExtension(filePath);

            rawFile = File.ReadAllBytes(filePath);

            hexBox1.ByteProvider = null;
            hexBox1.ByteProvider = new DynamicByteProvider(rawFile);
            hexBox1.ContextMenuStrip = null;
            hexBox1.ContextMenu = null;

            SetNewLength(rawFile.Length);
            fpeControl.lengthValue.Value = rawFile.Length;

            panel1.Controls.Add(fpeControl);
        }

        FirmwarePresetEditor fpeControl;

        string filePath, firmwareHash;
        byte[] rawFile;

        private void hexBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuItem5.Text = hexBox1.SelectionLength == 0 ? "Copy Character" : "Copy Characters";
                menuItem61.Text = hexBox1.SelectionLength == 0 ? "Copy Byte" : "Copy Bytes";
                menuItem10.Text = "Offset: 0x" + hexBox1.SelectionStart.ToString("X6");
                menuItem11.Text = "Value: 0x" + BitConverter.ToString(rawFile, (int)hexBox1.SelectionStart, 1);
                contextMenu1.Show(this, e.Location);
            }
        }

        private void menuItem34_Click(object sender, EventArgs e) { fpeControl.startValue.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem35_Click(object sender, EventArgs e) { fpeControl.lengthValue.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem16_Click(object sender, EventArgs e) { fpeControl.adjustBckOffset.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem17_Click(object sender, EventArgs e) { fpeControl.HDMIPopupOffset.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem18_Click(object sender, EventArgs e) { fpeControl.paletteOffset.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem19_Click(object sender, EventArgs e) { fpeControl.showNoteOffset.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem20_Click(object sender, EventArgs e) { fpeControl.noSignalOffset.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem21_Click(object sender, EventArgs e) { fpeControl.logoOffset.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem22_Click(object sender, EventArgs e) { fpeControl.adjustBckHashSkip.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem23_Click(object sender, EventArgs e) { fpeControl.HDMIPopupHashSkip.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem24_Click(object sender, EventArgs e) { fpeControl.paletteHashSkip.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem25_Click(object sender, EventArgs e) { fpeControl.showNoteHashSkip.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem26_Click(object sender, EventArgs e) { fpeControl.noSignalHashSkip.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem27_Click(object sender, EventArgs e) { fpeControl.logoHashSkip.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem28_Click(object sender, EventArgs e) { fpeControl.adjustBckLength.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem29_Click(object sender, EventArgs e) { fpeControl.HDMIPopupLength.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem30_Click(object sender, EventArgs e) { fpeControl.paletteLength.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem31_Click(object sender, EventArgs e) { fpeControl.showNoteLength.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem32_Click(object sender, EventArgs e) { fpeControl.noSignalLength.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem33_Click(object sender, EventArgs e) { fpeControl.logoLength.Value = Convert.ToInt32(rawFile[(int)hexBox1.SelectionStart]); }
        private void menuItem37_Click(object sender, EventArgs e) { fpeControl.startValue.Value = hexBox1.SelectionStart; }
        private void menuItem38_Click(object sender, EventArgs e) { fpeControl.lengthValue.Value = hexBox1.SelectionStart; }
        private void menuItem41_Click(object sender, EventArgs e) { fpeControl.adjustBckOffset.Value = hexBox1.SelectionStart; }
        private void menuItem42_Click(object sender, EventArgs e) { fpeControl.HDMIPopupOffset.Value = hexBox1.SelectionStart; }
        private void menuItem43_Click(object sender, EventArgs e) { fpeControl.paletteOffset.Value = hexBox1.SelectionStart; }
        private void menuItem44_Click(object sender, EventArgs e) { fpeControl.showNoteOffset.Value = hexBox1.SelectionStart; }
        private void menuItem45_Click(object sender, EventArgs e) { fpeControl.noSignalOffset.Value = hexBox1.SelectionStart; }
        private void menuItem46_Click(object sender, EventArgs e) { fpeControl.logoOffset.Value = hexBox1.SelectionStart; }
        private void menuItem48_Click(object sender, EventArgs e) { fpeControl.adjustBckHashSkip.Value = hexBox1.SelectionStart; }
        private void menuItem49_Click(object sender, EventArgs e) { fpeControl.HDMIPopupHashSkip.Value = hexBox1.SelectionStart; }
        private void menuItem50_Click(object sender, EventArgs e) { fpeControl.paletteHashSkip.Value = hexBox1.SelectionStart; }
        private void menuItem51_Click(object sender, EventArgs e) { fpeControl.showNoteHashSkip.Value = hexBox1.SelectionStart; }
        private void menuItem52_Click(object sender, EventArgs e) { fpeControl.noSignalHashSkip.Value = hexBox1.SelectionStart; }
        private void menuItem53_Click(object sender, EventArgs e) { fpeControl.logoHashSkip.Value = hexBox1.SelectionStart; }
        private void menuItem55_Click(object sender, EventArgs e) { fpeControl.adjustBckLength.Value = hexBox1.SelectionStart; }
        private void menuItem56_Click(object sender, EventArgs e) { fpeControl.HDMIPopupLength.Value = hexBox1.SelectionStart; }
        private void menuItem57_Click(object sender, EventArgs e) { fpeControl.paletteLength.Value = hexBox1.SelectionStart; }
        private void menuItem58_Click(object sender, EventArgs e) { fpeControl.showNoteLength.Value = hexBox1.SelectionStart; }
        private void menuItem59_Click(object sender, EventArgs e) { fpeControl.noSignalLength.Value = hexBox1.SelectionStart; }
        private void menuItem60_Click(object sender, EventArgs e) { fpeControl.logoLength.Value = hexBox1.SelectionStart; }

        private void hexBox1_SelectionStartChanged(object sender, EventArgs e)
        {
            label1.Text =
                "'" + Path.GetFileName(filePath) + "' - Size: " + rawFile.Length + " (0x" + rawFile.Length.ToString("X1") + ")" + 
            " | Position: " + hexBox1.SelectionStart + " (0x" + hexBox1.SelectionStart.ToString("X2") +
            ") - Selected " + (hexBox1.SelectionLength + 1) + " (0x" + (hexBox1.SelectionLength + 1).ToString("X1") + ") " + (hexBox1.SelectionLength == 0 ? "byte" : "bytes");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "INI File|*.ini" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ApplyPreset(Firmware.FromFile(ofd.FileName));
                Text = "Firmware Preset Editor - '" + Path.GetFileName(ofd.FileName) + "'";
            }
        }

        private void ApplyPreset(Firmware firm)
        {
            SetNewLength(firm.HashInfo.Length);
            if (firm.HashInfo.GetHash(rawFile) != firm.HashInfo.Hash)
            {
                MessageBox.Show("Firmware Hash is different than the hash in the imported preset\n(The preset will still load, it is recomended to load the intended firmware)\n\nFirmware Name: " + firm.Name, "Incorrect Firmware Hash", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            fpeControl.presetName.Text = firm.Name;
            fpeControl.firmwareHash.Text = firm.HashInfo.Hash;
            fpeControl.startValue.Value = firm.HashInfo.Start;
            fpeControl.lengthValue.Value = firm.HashInfo.Length;
            fpeControl.adjustBckOffset.Value = firm.AdjustBackgroundColorOffset;
            fpeControl.HDMIPopupOffset.Value = firm.HdmiStringOffset;
            fpeControl.paletteOffset.Value = firm.PaletteOffset;
            fpeControl.showNoteOffset.Value = firm.ShowNoteOffset;
            fpeControl.noSignalOffset.Value = firm.NoSignalOffset;
            fpeControl.logoOffset.Value = firm.LogoOffset;
            fpeControl.adjustBckHashSkip.Value = firm.HashInfo.SkippedSections[0].Offset > firm.AdjustBackgroundColorOffset ? (firm.HashInfo.SkippedSections[0].Offset - firm.AdjustBackgroundColorOffset) : (firm.AdjustBackgroundColorOffset - firm.HashInfo.SkippedSections[0].Offset);
            fpeControl.HDMIPopupHashSkip.Value = firm.HashInfo.SkippedSections[1].Offset > firm.HdmiStringOffset ? (firm.HashInfo.SkippedSections[1].Offset - firm.HdmiStringOffset) : (firm.HdmiStringOffset - firm.HashInfo.SkippedSections[1].Offset);
            fpeControl.paletteHashSkip.Value = firm.HashInfo.SkippedSections[2].Offset > firm.PaletteOffset ? (firm.HashInfo.SkippedSections[2].Offset - firm.PaletteOffset) : (firm.PaletteOffset - firm.HashInfo.SkippedSections[2].Offset);
            fpeControl.showNoteHashSkip.Value = firm.HashInfo.SkippedSections[3].Offset > firm.ShowNoteOffset ? (firm.HashInfo.SkippedSections[3].Offset - firm.ShowNoteOffset) : (firm.ShowNoteOffset - firm.HashInfo.SkippedSections[3].Offset);
            fpeControl.noSignalHashSkip.Value = firm.HashInfo.SkippedSections[4].Offset > firm.NoSignalOffset ? (firm.HashInfo.SkippedSections[4].Offset - firm.NoSignalOffset) : (firm.NoSignalOffset - firm.HashInfo.SkippedSections[4].Offset);
            fpeControl.logoHashSkip.Value = firm.HashInfo.SkippedSections[5].Offset > firm.LogoOffset ? (firm.HashInfo.SkippedSections[5].Offset - firm.LogoOffset) : (firm.LogoOffset - firm.HashInfo.SkippedSections[5].Offset);
            fpeControl.adjustBckLength.Value = firm.HashInfo.SkippedSections[0].Length;
            fpeControl.HDMIPopupLength.Value = firm.HashInfo.SkippedSections[1].Length;
            fpeControl.paletteLength.Value = firm.HashInfo.SkippedSections[2].Length;
            fpeControl.showNoteLength.Value = firm.HashInfo.SkippedSections[3].Length;
            fpeControl.noSignalLength.Value = firm.HashInfo.SkippedSections[4].Length;
            fpeControl.logoLength.Value = firm.HashInfo.SkippedSections[5].Length;
        }

        private Firmware ToFirmware()
        {
            return new Firmware(fpeControl.presetName.Text, Convert.ToInt32(fpeControl.logoOffset.Value), Convert.ToInt32(fpeControl.logoLength.Value), Convert.ToInt32(fpeControl.HDMIPopupOffset.Value), Convert.ToInt32(fpeControl.adjustBckOffset.Value), Convert.ToInt32(fpeControl.showNoteOffset.Value), Convert.ToInt32(fpeControl.paletteOffset.Value), Convert.ToInt32(fpeControl.noSignalOffset.Value),
                new HashInfo(Convert.ToInt32(fpeControl.startValue.Value), Convert.ToInt32(fpeControl.lengthValue.Value), fpeControl.firmwareHash.Text, new [] {
                new HashSkip(Convert.ToInt32(fpeControl.adjustBckOffset.Value) + Convert.ToInt32(fpeControl.adjustBckHashSkip.Value), Convert.ToInt32(fpeControl.adjustBckLength.Value)), // CAdjustBackgroundColor
                new HashSkip(Convert.ToInt32(fpeControl.HDMIPopupOffset.Value) + Convert.ToInt32(fpeControl.HDMIPopupHashSkip.Value), Convert.ToInt32(fpeControl.HDMIPopupLength.Value)), // "HDMI"
                new HashSkip(Convert.ToInt32(fpeControl.paletteOffset.Value) + Convert.ToInt32(fpeControl.paletteHashSkip.Value), Convert.ToInt32(fpeControl.paletteLength.Value)), // palette
                new HashSkip(Convert.ToInt32(fpeControl.showNoteOffset.Value) + Convert.ToInt32(fpeControl.showNoteHashSkip.Value), Convert.ToInt32(fpeControl.showNoteLength.Value)), // CShowNote
                new HashSkip(Convert.ToInt32(fpeControl.noSignalOffset.Value) + Convert.ToInt32(fpeControl.noSignalHashSkip.Value), Convert.ToInt32(fpeControl.noSignalLength.Value)), // CShowNoSignal
                new HashSkip(Convert.ToInt32(fpeControl.logoOffset.Value) + Convert.ToInt32(fpeControl.logoHashSkip.Value), Convert.ToInt32(fpeControl.logoLength.Value)) // logo
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "INI File|*.ini" };
            Firmware firm = ToFirmware();
            fpeControl.firmwareHash.Text = firmwareHash = sfd.FileName = firm.HashInfo.GetHash(rawFile);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                firm.ToFile(sfd.FileName, rawFile);
                Text = "Firmware Preset Editor - '" + Path.GetFileName(sfd.FileName) + "'";
            }
        }

        private bool SetFirmwareFile(out string filename)
        {
            filename = null;
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Any Supported Filetypes|*.bin;*.rom;|Binary File|*.bin|ROM (Dump) File|*.rom|Any Files|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK) { filename = ofd.FileName; return true; }
            else { return false; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SetFirmwareFile(out filePath))
            {
                if (fpeControl != null) { panel1.Controls.Remove(fpeControl); }
                InitPresetEditor();
            }
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            while (true)
            {
                string adr = Interaction.InputBox("Enter number (0 - " + rawFile.Length + ") / (0x0 - 0x" + rawFile.Length.ToString("X1") + ")", "Go to address", "0x" + hexBox1.SelectionStart.ToString("X1"), Location.X + 20, Location.Y + 20);
                if (string.IsNullOrEmpty(adr)) { break; }
                try
                {
                    long adrInt = long.Parse(adr.Replace("0x", null), adr.StartsWith("0x") ? System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Integer);
                    if (adrInt < 0) { adrInt = 0; }
                    if (adrInt > rawFile.Length) { adrInt = rawFile.Length; }
                    hexBox1.SelectionStart = adrInt;
                    hexBox1.SelectionLength = 0;
                    break;
                }
                catch
                {

                }
            }
        }

        private void menuItem61_Click(object sender, EventArgs e) { hexBox1.CopyHex(); }
        private void menuItem5_Click(object sender, EventArgs e) { hexBox1.Copy(); }

        private void hexBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G) { menuItem7.PerformClick(); }
        }

        private void menuItem13_Click(object sender, EventArgs e){ Clipboard.SetText("0x" + hexBox1.SelectionStart.ToString("X1")); }
    }
}
