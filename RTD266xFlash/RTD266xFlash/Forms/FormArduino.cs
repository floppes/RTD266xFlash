using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using RTD266xFlash.BackgroundWorkers;

namespace RTD266xFlash.Forms
{
    public partial class FormArduino : Form
    {
        private SerialPort _comPort;

        private RTD266x _rtd;

        private bool _guiUpdate;

        public FormArduino()
        {
            InitializeComponent();
        }

        private void AppendConsoleText(string text)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { AppendConsoleText(text); });
                return;
            }

            txtConsole.AppendText(text);
        }

        private void UpdateConnected(bool connected)
        {
            btnStart.Enabled = !connected;
            btnStop.Enabled = connected;

            btnRead.Enabled = connected;
            btnWrite.Enabled = connected;
            btnReadStatus.Enabled = connected;
            btnEraseChip.Enabled = connected;
            btnClearLock.Enabled = connected;
            modificationSettings.ModifyEnabled = connected;
        }

        private void UpdateBackgroundWorkerActive(bool active)
        {
            groupMode.Enabled = !active;
            groupConnection.Enabled = !active;
            groupModify.Enabled = !active;
            groupMisc.Enabled = !active;
            groupRead.Enabled = !active;
            groupWrite.Enabled = !active;
        }

        private void UpdateDecimalValue(TextBox textBox, NumericUpDown numericUpDown)
        {
            int val;

            if (!int.TryParse(textBox.Text, System.Globalization.NumberStyles.HexNumber, null, out val))
            {
                return;
            }

            _guiUpdate = true;

            if (numericUpDown.Maximum != 0 && val > numericUpDown.Maximum)
            {
                val = (int)numericUpDown.Maximum;
            }

            if (numericUpDown.Minimum != 0 && val < numericUpDown.Minimum)
            {
                val = (int)numericUpDown.Minimum;
            }

            numericUpDown.Value = val;

            _guiUpdate = false;
        }

        private void UpdateHexValue(NumericUpDown numericUpDown, TextBox textBox)
        {
            _guiUpdate = true;

            textBox.Text = ((int)numericUpDown.Value).ToString("X");

            _guiUpdate = false;
        }

        private void UpdateMode()
        {
            bool expertMode;

            expertMode = radioModeExpert.Checked;

            numericBaudRate.Visible = expertMode;
            lblBaudRate.Visible = expertMode;
            groupRead.Visible = expertMode;
            groupWrite.Visible = expertMode;
            btnEraseChip.Visible = expertMode;
            btnClearLock.Visible = expertMode;
        }

        #region Background workers

        private void ReadWorkerFinished(RTD266x.Result result, byte[] data)
        {
            UpdateBackgroundWorkerActive(false);

            if (result != RTD266x.Result.Ok)
            {
                AppendConsoleText(RTD266x.ResultToString(result) + "\r\n");
                return;
            }

            AppendConsoleText("done\r\n");

            if (chkReadConsole.Checked)
            {
                StringBuilder dataLog = new StringBuilder();
                int column = 0;

                foreach (byte dataByte in data)
                {
                    dataLog.Append($"{dataByte:X2} ");

                    column++;

                    if (column == 16)
                    {
                        dataLog.Append("\r\n");
                        column = 0;
                    }
                }

                AppendConsoleText(dataLog.ToString());
            }

            if (chkReadFile.Checked)
            {
                try
                {
                    File.WriteAllBytes(txtReadFileName.Text, data);
                    AppendConsoleText($"Data successfully written to \"{txtReadFileName.Text}\"\r\n");
                }
                catch (Exception ex)
                {
                    AppendConsoleText($"Cannot write file \"{txtReadFileName.Text}\"! {ex.Message}\r\n");
                }
            }
        }

        private void WriteWorkerFinished(RTD266x.Result result)
        {
            UpdateBackgroundWorkerActive(false);
        }

        private void ModifyFirmwareWorkerFinished(RTD266x.Result result)
        {
            UpdateBackgroundWorkerActive(false);
        }

        #endregion

        #region GUI events

        private void FormMain_Load(object sender, EventArgs e)
        {
            comboBoxPorts.Items.AddRange(SerialPort.GetPortNames());
            comboBoxPorts.SelectedIndex = comboBoxPorts.Items.Count - 1;

            _guiUpdate = true;

            if (Properties.Settings.Default.ExpertMode)
            {
                radioModeExpert.Checked = true;
            }
            else
            {
                radioModeSimple.Checked = true;
            }

            _guiUpdate = false;

            UpdateConnected(false);
            UpdateMode();
            modificationSettings.UpdateModifyFirmware();

            AppendConsoleText("Configure the connection and click \"Connect\"\r\n");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_comPort != null && _comPort.IsOpen)
            {
                _comPort.Close();
            }

            _comPort = new SerialPort(comboBoxPorts.Text, (int)numericBaudRate.Value);
            _comPort.Parity = Parity.None;
            _comPort.StopBits = StopBits.One;
            _comPort.DataBits = 8;
            _comPort.Handshake = Handshake.None;

            AppendConsoleText("Connecting...\r\n");

            try
            {
                _comPort.Open();
            }
            catch (Exception ex)
            {
                AppendConsoleText($"Error! Cannot open {comboBoxPorts.Text}: {ex.Message}");
                UpdateConnected(false);
                return;
            }

            _rtd = new RTD266x(_comPort);

            UpdateConnected(true);

            RTD266x.ErrorCode errorCode = RTD266x.ErrorCode.NoError;
            uint errorInfo = 0;
            RTD266x.Result result = RTD266x.Result.NotOk;
            int retries = 5;

            // try to connect a few times
            for (int i = 0; i < retries; i++)
            {
                result = _rtd.ReadErrorCode(out errorCode, out errorInfo);

                if (result == RTD266x.Result.Ok)
                {
                    break;
                }

                _rtd.ClearReadBuffer();

                AppendConsoleText($"Connection error: {RTD266x.ResultToString(result)}, retrying... ({i + 1}/{retries})\r\n");
            }

            if (result != RTD266x.Result.Ok)
            {
                AppendConsoleText($"Connection error: {RTD266x.ResultToString(result)}\r\n");
                AppendConsoleText("Did you download the sketch RTD266xArduino to your Arduino?\r\n");
                AppendConsoleText("Is the Arduino connected properly to the display?\r\n");
                AppendConsoleText("Did you select the correct COM port?\r\n");
                AppendConsoleText("Is the display powered?\r\n");
                AppendConsoleText("If the Arduino's user LED is on, the I2C connection to the display does not work.\r\n");
                AppendConsoleText("Press the Arduino's reset button and try again.\r\n\r\n");

                btnDisconnect_Click(null, null);
                return;
            }

            if (errorCode == RTD266x.ErrorCode.NoError)
            {
                AppendConsoleText("Connection successful!\r\n");
            }
            else
            {
                AppendConsoleText("Initialization error: ");
                AppendConsoleText(RTD266x.ErrorCodeToString(errorCode, errorInfo) + "\r\n");

                btnDisconnect_Click(null, null);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (_comPort != null && _comPort.IsOpen)
            {
                _comPort.Close();

                AppendConsoleText("Disconnected\r\n");
            }

            UpdateConnected(false);

            _rtd = null;
        }
        
        private void btnReadFileNameBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bin files (*.bin)|*.bin";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtReadFileName.Text = saveFileDialog.FileName;
            }
        }

        private void chkReadFile_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkReadFile.Checked;

            txtReadFileName.Enabled = enabled;
            btnReadFileNameBrowse.Enabled = enabled;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int address = (int)numericReadStartAddress.Value;
            int length = (int)numericReadLength.Value;

            AppendConsoleText($"Reading {length} bytes from address 0x{address:X}...\r\n");

            UpdateBackgroundWorkerActive(true);

            ReadWorker readWorker = new ReadWorker(_rtd, address, length, true);
            readWorker.WorkerReportStatus += AppendConsoleText;
            readWorker.ReadWorkerFinished += ReadWorkerFinished;
            readWorker.Start();
        }

        private void btnWriteFileNameBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtWriteFileName.Text = openFileDialog.FileName;
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtWriteFileName.Text))
            {
                AppendConsoleText($"Error! Cannot read file \"{txtWriteFileName.Text}\"\r\n");
                return;
            }

            byte[] data;

            try
            {
                data = File.ReadAllBytes(txtWriteFileName.Text);
            }
            catch (Exception ex)
            {
                AppendConsoleText($"Error! Cannot read file \"{txtWriteFileName.Text}\". {ex.Message}\r\n");
                return;
            }

            int address = (int)numericWriteStartAddress.Value;

            UpdateBackgroundWorkerActive(true);

            WriteWorker writeWorker = new WriteWorker(_rtd, address, data, true);
            writeWorker.WorkerReportStatus += AppendConsoleText;
            writeWorker.WriteWorkerFinished += WriteWorkerFinished;
            writeWorker.Start();
        }

        private void btnReadStatus_Click(object sender, EventArgs e)
        {
            AppendConsoleText("Reading status info... ");

            RTD266x.StatusInfo statusInfo;
                
            RTD266x.Result result = _rtd.ReadStatus(out statusInfo);

            if (result != RTD266x.Result.Ok)
            {
                AppendConsoleText(RTD266x.ResultToString(result) + "\r\n");
                return;
            }

            AppendConsoleText("done\r\n");

            AppendConsoleText($"Manufacturer ID: 0x{statusInfo.ManufacturerId:X2} ({statusInfo.Manufacturer})\r\n");
            AppendConsoleText($"Device ID: 0x{statusInfo.DeviceId:X2} ({statusInfo.Type})\r\n");
            AppendConsoleText($"JEDEC Manufacturer ID: 0x{statusInfo.JedecManufacturerId:X2} ({statusInfo.Manufacturer})\r\n");
            AppendConsoleText($"JEDEC Memory Type: 0x{statusInfo.JedecMemoryType:X2}\r\n");
            AppendConsoleText($"JEDEC Capacity: 0x{statusInfo.JedecCapacity:X2} ({statusInfo.Capacity})\r\n");
            AppendConsoleText($"Status: 0x{statusInfo.Status:X4}\r\n");
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtConsole.Text = "";
        }

        private void btnEraseChip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to erase the whole chip?", "Erase chip", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            AppendConsoleText("Erasing chip (all data)... ");

            RTD266x.Result result = _rtd.EraseChip();

            if (result == RTD266x.Result.Ok)
            {
                AppendConsoleText("done\r\n");
            }
            else
            {
                AppendConsoleText(RTD266x.ResultToString(result) + "\r\n");
            }
        }

        private void btnClearLock_Click(object sender, EventArgs e)
        {
            AppendConsoleText("Clearing lock bits... ");

            RTD266x.Result result = _rtd.WriteStatus(0x00, 0x00);

            if (result == RTD266x.Result.Ok)
            {
                AppendConsoleText("done\r\n");
            }
            else
            {
                AppendConsoleText(RTD266x.ResultToString(result) + "\r\n");
            }
        }

        private void txtReadStartAddress_TextChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            UpdateDecimalValue(txtReadStartAddress, numericReadStartAddress);
        }

        private void numericReadStartAddress_ValueChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            UpdateHexValue(numericReadStartAddress, txtReadStartAddress);
        }

        private void txtReadLength_TextChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            UpdateDecimalValue(txtReadLength, numericReadLength);
        }

        private void numericReadLength_ValueChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            UpdateHexValue(numericReadLength, txtReadLength);
        }

        private void txtWriteStartAddress_TextChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            UpdateDecimalValue(txtWriteStartAddress, numericWriteStartAddress);
        }

        private void numericWriteStartAddress_ValueChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            UpdateHexValue(numericWriteStartAddress, txtWriteStartAddress);
        }

        private void radioModeExpert_CheckedChanged(object sender, EventArgs e)
        {
            if (_guiUpdate)
            {
                return;
            }

            if (radioModeExpert.Checked)
            {
                if (MessageBox.Show("Do you really want to switch to expert mode?", "Expert mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    radioModeSimple.Checked = true;
                }
            }

            UpdateMode();

            Properties.Settings.Default.ExpertMode = radioModeExpert.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnModify_Click()
        {
            UpdateBackgroundWorkerActive(true);

            ModifyFirmwareWorker changeLogoWorker = new ModifyFirmwareWorker(
                _rtd,
                modificationSettings.LogoFileName,
                modificationSettings.LogoBackgroundColor,
                modificationSettings.LogoForegroundColor,
                modificationSettings.BackgroundColor,
                modificationSettings.RemoveHdmiPopup,
                modificationSettings.HdmiReplacementText,
                modificationSettings.RemoveNoSignalPopup);

            changeLogoWorker.WorkerReportStatus += AppendConsoleText;
            changeLogoWorker.ModifyFirmwareWorkerFinished += ModifyFirmwareWorkerFinished;
            changeLogoWorker.Start();
        }

        #endregion
    }
}
