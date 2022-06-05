namespace RTD266xFlash.Forms
{
    partial class FormArduino
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormArduino));
            this.groupConnection = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.numericBaudRate = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.groupRead = new System.Windows.Forms.GroupBox();
            this.chkReadConsole = new System.Windows.Forms.CheckBox();
            this.chkReadFile = new System.Windows.Forms.CheckBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.lblReadLength = new System.Windows.Forms.Label();
            this.lblReadStartAddress = new System.Windows.Forms.Label();
            this.numericReadLength = new System.Windows.Forms.NumericUpDown();
            this.numericReadStartAddress = new System.Windows.Forms.NumericUpDown();
            this.btnReadFileNameBrowse = new System.Windows.Forms.Button();
            this.txtReadFileName = new System.Windows.Forms.TextBox();
            this.groupWrite = new System.Windows.Forms.GroupBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.lblWriteStartAddress = new System.Windows.Forms.Label();
            this.numericWriteStartAddress = new System.Windows.Forms.NumericUpDown();
            this.btnWriteFileNameBrowse = new System.Windows.Forms.Button();
            this.lblWriteFileName = new System.Windows.Forms.Label();
            this.txtWriteFileName = new System.Windows.Forms.TextBox();
            this.groupMisc = new System.Windows.Forms.GroupBox();
            this.btnClearLock = new System.Windows.Forms.Button();
            this.btnEraseChip = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnReadStatus = new System.Windows.Forms.Button();
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.radioModeExpert = new System.Windows.Forms.RadioButton();
            this.radioModeSimple = new System.Windows.Forms.RadioButton();
            this.groupModify = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.modificationSettings = new RTD266xFlash.ModificationSettings();
            this.groupConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBaudRate)).BeginInit();
            this.groupRead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericReadLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericReadStartAddress)).BeginInit();
            this.groupWrite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWriteStartAddress)).BeginInit();
            this.groupMisc.SuspendLayout();
            this.groupMode.SuspendLayout();
            this.groupModify.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupConnection
            // 
            this.groupConnection.Controls.Add(this.btnStop);
            this.groupConnection.Controls.Add(this.numericBaudRate);
            this.groupConnection.Controls.Add(this.btnStart);
            this.groupConnection.Controls.Add(this.lblBaudRate);
            this.groupConnection.Controls.Add(this.lblPort);
            this.groupConnection.Controls.Add(this.comboBoxPorts);
            this.groupConnection.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupConnection.Location = new System.Drawing.Point(146, 56);
            this.groupConnection.Name = "groupConnection";
            this.groupConnection.Size = new System.Drawing.Size(198, 90);
            this.groupConnection.TabIndex = 1;
            this.groupConnection.TabStop = false;
            this.groupConnection.Text = "Connection";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(100, 63);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(95, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Disconnect";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // numericBaudRate
            // 
            this.numericBaudRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(43)))));
            this.numericBaudRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericBaudRate.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.numericBaudRate.Location = new System.Drawing.Point(69, 38);
            this.numericBaudRate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericBaudRate.Minimum = new decimal(new int[] {
            9600,
            0,
            0,
            0});
            this.numericBaudRate.Name = "numericBaudRate";
            this.numericBaudRate.Size = new System.Drawing.Size(114, 21);
            this.numericBaudRate.TabIndex = 5;
            this.numericBaudRate.Value = new decimal(new int[] {
            115200,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Location = new System.Drawing.Point(3, 63);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(98, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Connect";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(6, 40);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(61, 13);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 18);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(60, 13);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Serial Port:";
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(27)))), ((int)(((byte)(61)))));
            this.comboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPorts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxPorts.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(69, 15);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(114, 21);
            this.comboBoxPorts.TabIndex = 0;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txtConsole.Location = new System.Drawing.Point(0, 0);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(694, 168);
            this.txtConsole.TabIndex = 2;
            // 
            // groupRead
            // 
            this.groupRead.Controls.Add(this.chkReadConsole);
            this.groupRead.Controls.Add(this.chkReadFile);
            this.groupRead.Controls.Add(this.btnRead);
            this.groupRead.Controls.Add(this.lblReadLength);
            this.groupRead.Controls.Add(this.lblReadStartAddress);
            this.groupRead.Controls.Add(this.numericReadLength);
            this.groupRead.Controls.Add(this.numericReadStartAddress);
            this.groupRead.Controls.Add(this.btnReadFileNameBrowse);
            this.groupRead.Controls.Add(this.txtReadFileName);
            this.groupRead.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupRead.Location = new System.Drawing.Point(10, 149);
            this.groupRead.Name = "groupRead";
            this.groupRead.Size = new System.Drawing.Size(334, 90);
            this.groupRead.TabIndex = 3;
            this.groupRead.TabStop = false;
            this.groupRead.Text = "Read EEPROM";
            // 
            // chkReadConsole
            // 
            this.chkReadConsole.AutoSize = true;
            this.chkReadConsole.Checked = true;
            this.chkReadConsole.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReadConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkReadConsole.Location = new System.Drawing.Point(6, 42);
            this.chkReadConsole.Name = "chkReadConsole";
            this.chkReadConsole.Size = new System.Drawing.Size(87, 17);
            this.chkReadConsole.TabIndex = 15;
            this.chkReadConsole.Text = "Output to log";
            this.chkReadConsole.UseVisualStyleBackColor = true;
            // 
            // chkReadFile
            // 
            this.chkReadFile.AutoSize = true;
            this.chkReadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkReadFile.Location = new System.Drawing.Point(6, 19);
            this.chkReadFile.Name = "chkReadFile";
            this.chkReadFile.Size = new System.Drawing.Size(78, 17);
            this.chkReadFile.TabIndex = 14;
            this.chkReadFile.Text = "Output file:";
            this.chkReadFile.UseVisualStyleBackColor = true;
            this.chkReadFile.CheckedChanged += new System.EventHandler(this.chkReadFile_CheckedChanged);
            // 
            // btnRead
            // 
            this.btnRead.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRead.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRead.Location = new System.Drawing.Point(3, 64);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(328, 23);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // lblReadLength
            // 
            this.lblReadLength.AutoSize = true;
            this.lblReadLength.Location = new System.Drawing.Point(207, 41);
            this.lblReadLength.Name = "lblReadLength";
            this.lblReadLength.Size = new System.Drawing.Size(44, 13);
            this.lblReadLength.TabIndex = 6;
            this.lblReadLength.Text = "Length:";
            // 
            // lblReadStartAddress
            // 
            this.lblReadStartAddress.AutoSize = true;
            this.lblReadStartAddress.Location = new System.Drawing.Point(95, 41);
            this.lblReadStartAddress.Name = "lblReadStartAddress";
            this.lblReadStartAddress.Size = new System.Drawing.Size(35, 13);
            this.lblReadStartAddress.TabIndex = 5;
            this.lblReadStartAddress.Text = "Start:";
            // 
            // numericReadLength
            // 
            this.numericReadLength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(43)))));
            this.numericReadLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericReadLength.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.numericReadLength.Location = new System.Drawing.Point(257, 39);
            this.numericReadLength.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.numericReadLength.Name = "numericReadLength";
            this.numericReadLength.Size = new System.Drawing.Size(67, 21);
            this.numericReadLength.TabIndex = 4;
            this.numericReadLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericReadLength.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // numericReadStartAddress
            // 
            this.numericReadStartAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(43)))));
            this.numericReadStartAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericReadStartAddress.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.numericReadStartAddress.Location = new System.Drawing.Point(134, 39);
            this.numericReadStartAddress.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.numericReadStartAddress.Name = "numericReadStartAddress";
            this.numericReadStartAddress.Size = new System.Drawing.Size(67, 21);
            this.numericReadStartAddress.TabIndex = 3;
            this.numericReadStartAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReadFileNameBrowse
            // 
            this.btnReadFileNameBrowse.Enabled = false;
            this.btnReadFileNameBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadFileNameBrowse.Location = new System.Drawing.Point(297, 14);
            this.btnReadFileNameBrowse.Name = "btnReadFileNameBrowse";
            this.btnReadFileNameBrowse.Size = new System.Drawing.Size(31, 22);
            this.btnReadFileNameBrowse.TabIndex = 2;
            this.btnReadFileNameBrowse.Text = "...";
            this.btnReadFileNameBrowse.UseVisualStyleBackColor = true;
            this.btnReadFileNameBrowse.Click += new System.EventHandler(this.btnReadFileNameBrowse_Click);
            // 
            // txtReadFileName
            // 
            this.txtReadFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(43)))));
            this.txtReadFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReadFileName.Enabled = false;
            this.txtReadFileName.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txtReadFileName.Location = new System.Drawing.Point(98, 15);
            this.txtReadFileName.Name = "txtReadFileName";
            this.txtReadFileName.ReadOnly = true;
            this.txtReadFileName.Size = new System.Drawing.Size(193, 21);
            this.txtReadFileName.TabIndex = 0;
            // 
            // groupWrite
            // 
            this.groupWrite.Controls.Add(this.btnWrite);
            this.groupWrite.Controls.Add(this.lblWriteStartAddress);
            this.groupWrite.Controls.Add(this.numericWriteStartAddress);
            this.groupWrite.Controls.Add(this.btnWriteFileNameBrowse);
            this.groupWrite.Controls.Add(this.lblWriteFileName);
            this.groupWrite.Controls.Add(this.txtWriteFileName);
            this.groupWrite.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupWrite.Location = new System.Drawing.Point(10, 245);
            this.groupWrite.Name = "groupWrite";
            this.groupWrite.Size = new System.Drawing.Size(334, 67);
            this.groupWrite.TabIndex = 4;
            this.groupWrite.TabStop = false;
            this.groupWrite.Text = "Write EEPROM";
            // 
            // btnWrite
            // 
            this.btnWrite.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWrite.Location = new System.Drawing.Point(201, 39);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(127, 23);
            this.btnWrite.TabIndex = 11;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // lblWriteStartAddress
            // 
            this.lblWriteStartAddress.AutoSize = true;
            this.lblWriteStartAddress.Location = new System.Drawing.Point(8, 41);
            this.lblWriteStartAddress.Name = "lblWriteStartAddress";
            this.lblWriteStartAddress.Size = new System.Drawing.Size(76, 13);
            this.lblWriteStartAddress.TabIndex = 10;
            this.lblWriteStartAddress.Text = "Start address:";
            // 
            // numericWriteStartAddress
            // 
            this.numericWriteStartAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(43)))));
            this.numericWriteStartAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericWriteStartAddress.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.numericWriteStartAddress.Location = new System.Drawing.Point(90, 39);
            this.numericWriteStartAddress.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.numericWriteStartAddress.Name = "numericWriteStartAddress";
            this.numericWriteStartAddress.Size = new System.Drawing.Size(105, 21);
            this.numericWriteStartAddress.TabIndex = 9;
            this.numericWriteStartAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnWriteFileNameBrowse
            // 
            this.btnWriteFileNameBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWriteFileNameBrowse.Location = new System.Drawing.Point(297, 15);
            this.btnWriteFileNameBrowse.Name = "btnWriteFileNameBrowse";
            this.btnWriteFileNameBrowse.Size = new System.Drawing.Size(31, 22);
            this.btnWriteFileNameBrowse.TabIndex = 8;
            this.btnWriteFileNameBrowse.Text = "...";
            this.btnWriteFileNameBrowse.UseVisualStyleBackColor = true;
            this.btnWriteFileNameBrowse.Click += new System.EventHandler(this.btnWriteFileNameBrowse_Click);
            // 
            // lblWriteFileName
            // 
            this.lblWriteFileName.AutoSize = true;
            this.lblWriteFileName.Location = new System.Drawing.Point(30, 20);
            this.lblWriteFileName.Name = "lblWriteFileName";
            this.lblWriteFileName.Size = new System.Drawing.Size(54, 13);
            this.lblWriteFileName.TabIndex = 7;
            this.lblWriteFileName.Text = "Input file:";
            // 
            // txtWriteFileName
            // 
            this.txtWriteFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(43)))));
            this.txtWriteFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWriteFileName.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txtWriteFileName.Location = new System.Drawing.Point(90, 15);
            this.txtWriteFileName.Name = "txtWriteFileName";
            this.txtWriteFileName.ReadOnly = true;
            this.txtWriteFileName.Size = new System.Drawing.Size(200, 21);
            this.txtWriteFileName.TabIndex = 6;
            // 
            // groupMisc
            // 
            this.groupMisc.Controls.Add(this.btnClearLock);
            this.groupMisc.Controls.Add(this.btnEraseChip);
            this.groupMisc.Controls.Add(this.btnReadStatus);
            this.groupMisc.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupMisc.Location = new System.Drawing.Point(10, 56);
            this.groupMisc.Name = "groupMisc";
            this.groupMisc.Size = new System.Drawing.Size(130, 90);
            this.groupMisc.TabIndex = 5;
            this.groupMisc.TabStop = false;
            this.groupMisc.Text = "Miscellaneous";
            // 
            // btnClearLock
            // 
            this.btnClearLock.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClearLock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnClearLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLock.Location = new System.Drawing.Point(3, 63);
            this.btnClearLock.Name = "btnClearLock";
            this.btnClearLock.Size = new System.Drawing.Size(124, 23);
            this.btnClearLock.TabIndex = 3;
            this.btnClearLock.Text = "Clear lock bits";
            this.btnClearLock.UseVisualStyleBackColor = true;
            this.btnClearLock.Click += new System.EventHandler(this.btnClearLock_Click);
            // 
            // btnEraseChip
            // 
            this.btnEraseChip.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEraseChip.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnEraseChip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEraseChip.Location = new System.Drawing.Point(3, 40);
            this.btnEraseChip.Name = "btnEraseChip";
            this.btnEraseChip.Size = new System.Drawing.Size(124, 23);
            this.btnEraseChip.TabIndex = 2;
            this.btnEraseChip.Text = "Erase chip";
            this.btnEraseChip.UseVisualStyleBackColor = true;
            this.btnEraseChip.Click += new System.EventHandler(this.btnEraseChip_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearLog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Location = new System.Drawing.Point(0, 168);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(694, 25);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "Clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnReadStatus
            // 
            this.btnReadStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReadStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(30)))));
            this.btnReadStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadStatus.Location = new System.Drawing.Point(3, 17);
            this.btnReadStatus.Name = "btnReadStatus";
            this.btnReadStatus.Size = new System.Drawing.Size(124, 23);
            this.btnReadStatus.TabIndex = 0;
            this.btnReadStatus.Text = "Read status";
            this.btnReadStatus.UseVisualStyleBackColor = true;
            this.btnReadStatus.Click += new System.EventHandler(this.btnReadStatus_Click);
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.radioModeExpert);
            this.groupMode.Controls.Add(this.radioModeSimple);
            this.groupMode.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupMode.Location = new System.Drawing.Point(10, 6);
            this.groupMode.Name = "groupMode";
            this.groupMode.Size = new System.Drawing.Size(128, 44);
            this.groupMode.TabIndex = 6;
            this.groupMode.TabStop = false;
            this.groupMode.Text = "Mode";
            // 
            // radioModeExpert
            // 
            this.radioModeExpert.AutoSize = true;
            this.radioModeExpert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioModeExpert.Location = new System.Drawing.Point(66, 17);
            this.radioModeExpert.Name = "radioModeExpert";
            this.radioModeExpert.Size = new System.Drawing.Size(56, 17);
            this.radioModeExpert.TabIndex = 1;
            this.radioModeExpert.Text = "Expert";
            this.radioModeExpert.UseVisualStyleBackColor = true;
            this.radioModeExpert.CheckedChanged += new System.EventHandler(this.radioModeExpert_CheckedChanged);
            // 
            // radioModeSimple
            // 
            this.radioModeSimple.AutoSize = true;
            this.radioModeSimple.Checked = true;
            this.radioModeSimple.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioModeSimple.Location = new System.Drawing.Point(6, 17);
            this.radioModeSimple.Name = "radioModeSimple";
            this.radioModeSimple.Size = new System.Drawing.Size(54, 17);
            this.radioModeSimple.TabIndex = 0;
            this.radioModeSimple.TabStop = true;
            this.radioModeSimple.Text = "Simple";
            this.radioModeSimple.UseVisualStyleBackColor = true;
            // 
            // groupModify
            // 
            this.groupModify.Controls.Add(this.modificationSettings);
            this.groupModify.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupModify.Location = new System.Drawing.Point(350, 6);
            this.groupModify.Name = "groupModify";
            this.groupModify.Size = new System.Drawing.Size(334, 245);
            this.groupModify.TabIndex = 7;
            this.groupModify.TabStop = false;
            this.groupModify.Text = "Modify firmware";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Location = new System.Drawing.Point(144, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 44);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display Numbers as";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton1.Location = new System.Drawing.Point(85, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Hexadecimal";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton2.Location = new System.Drawing.Point(6, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(73, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Numerical ";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtConsole);
            this.panel1.Controls.Add(this.btnClearLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 318);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(694, 193);
            this.panel1.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::RTD266xFlash.Properties.Resources.icon_anim_noMotherboard;
            this.pictureBox1.Location = new System.Drawing.Point(143, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(350, 257);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 55);
            this.panel2.TabIndex = 9;
            // 
            // modificationSettings
            // 
            this.modificationSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(27)))), ((int)(((byte)(61)))));
            this.modificationSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modificationSettings.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modificationSettings.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.modificationSettings.Location = new System.Drawing.Point(3, 17);
            this.modificationSettings.ModifyEnabled = true;
            this.modificationSettings.Name = "modificationSettings";
            this.modificationSettings.Size = new System.Drawing.Size(328, 225);
            this.modificationSettings.TabIndex = 0;
            this.modificationSettings.ModifyClickedEvent += new RTD266xFlash.ModificationSettings.ModifyClickedHandler(this.btnModify_Click);
            // 
            // FormArduino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(27)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(694, 511);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupMisc);
            this.Controls.Add(this.groupModify);
            this.Controls.Add(this.groupMode);
            this.Controls.Add(this.groupWrite);
            this.Controls.Add(this.groupRead);
            this.Controls.Add(this.groupConnection);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormArduino";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTD266x EEPROM Flasher";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupConnection.ResumeLayout(false);
            this.groupConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBaudRate)).EndInit();
            this.groupRead.ResumeLayout(false);
            this.groupRead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericReadLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericReadStartAddress)).EndInit();
            this.groupWrite.ResumeLayout(false);
            this.groupWrite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWriteStartAddress)).EndInit();
            this.groupMisc.ResumeLayout(false);
            this.groupMode.ResumeLayout(false);
            this.groupMode.PerformLayout();
            this.groupModify.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupConnection;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.NumericUpDown numericBaudRate;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.GroupBox groupRead;
        private System.Windows.Forms.GroupBox groupWrite;
        private System.Windows.Forms.Button btnReadFileNameBrowse;
        private System.Windows.Forms.TextBox txtReadFileName;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label lblReadLength;
        private System.Windows.Forms.Label lblReadStartAddress;
        private System.Windows.Forms.NumericUpDown numericReadLength;
        private System.Windows.Forms.NumericUpDown numericReadStartAddress;
        private System.Windows.Forms.Label lblWriteStartAddress;
        private System.Windows.Forms.NumericUpDown numericWriteStartAddress;
        private System.Windows.Forms.Button btnWriteFileNameBrowse;
        private System.Windows.Forms.Label lblWriteFileName;
        private System.Windows.Forms.TextBox txtWriteFileName;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.GroupBox groupMisc;
        private System.Windows.Forms.Button btnReadStatus;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnEraseChip;
        private System.Windows.Forms.Button btnClearLock;
        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.RadioButton radioModeExpert;
        private System.Windows.Forms.RadioButton radioModeSimple;
        private System.Windows.Forms.GroupBox groupModify;
        private System.Windows.Forms.CheckBox chkReadConsole;
        private System.Windows.Forms.CheckBox chkReadFile;
        private ModificationSettings modificationSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
    }
}

