namespace RTD266xFlash
{
    partial class FormMain
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
            this.lblReadLengthHex = new System.Windows.Forms.Label();
            this.lblReadLengthDec = new System.Windows.Forms.Label();
            this.lblReadStartAddressHex = new System.Windows.Forms.Label();
            this.lblReadStartAddressDec = new System.Windows.Forms.Label();
            this.txtReadLength = new System.Windows.Forms.TextBox();
            this.txtReadStartAddress = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.lblReadLength = new System.Windows.Forms.Label();
            this.lblReadStartAddress = new System.Windows.Forms.Label();
            this.numericReadLength = new System.Windows.Forms.NumericUpDown();
            this.numericReadStartAddress = new System.Windows.Forms.NumericUpDown();
            this.btnReadFileNameBrowse = new System.Windows.Forms.Button();
            this.txtReadFileName = new System.Windows.Forms.TextBox();
            this.groupWrite = new System.Windows.Forms.GroupBox();
            this.lblWriteStartAddressHex = new System.Windows.Forms.Label();
            this.lblWriteStartAddressDec = new System.Windows.Forms.Label();
            this.txtWriteStartAddress = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.lblWriteStartAddress = new System.Windows.Forms.Label();
            this.numericWriteStartAddress = new System.Windows.Forms.NumericUpDown();
            this.btnWriteFileNameBrowse = new System.Windows.Forms.Button();
            this.lblWriteFileName = new System.Windows.Forms.Label();
            this.txtWriteFileName = new System.Windows.Forms.TextBox();
            this.groupMisc = new System.Windows.Forms.GroupBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnDecodeFont = new System.Windows.Forms.Button();
            this.btnClearLock = new System.Windows.Forms.Button();
            this.btnEraseChip = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnReadStatus = new System.Windows.Forms.Button();
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.radioModeExpert = new System.Windows.Forms.RadioButton();
            this.radioModeSimple = new System.Windows.Forms.RadioButton();
            this.groupModify = new System.Windows.Forms.GroupBox();
            this.picBackgroundColor = new System.Windows.Forms.PictureBox();
            this.picLogoForegroundColor = new System.Windows.Forms.PictureBox();
            this.picLogoBackgroundColor = new System.Windows.Forms.PictureBox();
            this.numericLogoForegroundBlue = new System.Windows.Forms.NumericUpDown();
            this.lblLogoForegroundBlue = new System.Windows.Forms.Label();
            this.numericLogoForegroundGreen = new System.Windows.Forms.NumericUpDown();
            this.lblLogoForegroundGreen = new System.Windows.Forms.Label();
            this.lblLogoForegroundRed = new System.Windows.Forms.Label();
            this.numericLogoForegroundRed = new System.Windows.Forms.NumericUpDown();
            this.chkChangeLogoForegroundColor = new System.Windows.Forms.CheckBox();
            this.txtChangeHdmi = new System.Windows.Forms.TextBox();
            this.chkChangeHdmi = new System.Windows.Forms.CheckBox();
            this.chkRemoveHdmi = new System.Windows.Forms.CheckBox();
            this.numericBackgroundBlue = new System.Windows.Forms.NumericUpDown();
            this.lblBackgroundBlue = new System.Windows.Forms.Label();
            this.numericBackgroundGreen = new System.Windows.Forms.NumericUpDown();
            this.lblBackgroundGreen = new System.Windows.Forms.Label();
            this.lblBackgroundRed = new System.Windows.Forms.Label();
            this.numericBackgroundRed = new System.Windows.Forms.NumericUpDown();
            this.numericLogoBackgroundBlue = new System.Windows.Forms.NumericUpDown();
            this.lblLogoBackgroundBlue = new System.Windows.Forms.Label();
            this.numericLogoBackgroundGreen = new System.Windows.Forms.NumericUpDown();
            this.lblLogoBackgroundGreen = new System.Windows.Forms.Label();
            this.lblLogoBackgroundRed = new System.Windows.Forms.Label();
            this.numericLogoBackgroundRed = new System.Windows.Forms.NumericUpDown();
            this.chkChangeBackgroundColor = new System.Windows.Forms.CheckBox();
            this.chkChangeLogoBackgroundColor = new System.Windows.Forms.CheckBox();
            this.chkChangeLogo = new System.Windows.Forms.CheckBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnLogoFileNameBrowse = new System.Windows.Forms.Button();
            this.txtLogoFileName = new System.Windows.Forms.TextBox();
            this.lblLogoFileName = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoForegroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoBackgroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoForegroundBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoForegroundGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoForegroundRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackgroundBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackgroundGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackgroundRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoBackgroundBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoBackgroundGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoBackgroundRed)).BeginInit();
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
            this.groupConnection.Location = new System.Drawing.Point(12, 62);
            this.groupConnection.Name = "groupConnection";
            this.groupConnection.Size = new System.Drawing.Size(296, 90);
            this.groupConnection.TabIndex = 1;
            this.groupConnection.TabStop = false;
            this.groupConnection.Text = "Connection";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(126, 61);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(114, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Disconnect";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // numericBaudRate
            // 
            this.numericBaudRate.Location = new System.Drawing.Point(126, 35);
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
            this.numericBaudRate.Size = new System.Drawing.Size(72, 22);
            this.numericBaudRate.TabIndex = 5;
            this.numericBaudRate.Value = new decimal(new int[] {
            115200,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 61);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(114, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Connect";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(123, 18);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(63, 13);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(3, 18);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(62, 13);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Serial Port:";
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(6, 34);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(114, 21);
            this.comboBoxPorts.TabIndex = 0;
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.BackColor = System.Drawing.SystemColors.Window;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.Location = new System.Drawing.Point(616, 12);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(427, 437);
            this.txtConsole.TabIndex = 2;
            // 
            // groupRead
            // 
            this.groupRead.Controls.Add(this.chkReadConsole);
            this.groupRead.Controls.Add(this.chkReadFile);
            this.groupRead.Controls.Add(this.lblReadLengthHex);
            this.groupRead.Controls.Add(this.lblReadLengthDec);
            this.groupRead.Controls.Add(this.lblReadStartAddressHex);
            this.groupRead.Controls.Add(this.lblReadStartAddressDec);
            this.groupRead.Controls.Add(this.txtReadLength);
            this.groupRead.Controls.Add(this.txtReadStartAddress);
            this.groupRead.Controls.Add(this.btnRead);
            this.groupRead.Controls.Add(this.lblReadLength);
            this.groupRead.Controls.Add(this.lblReadStartAddress);
            this.groupRead.Controls.Add(this.numericReadLength);
            this.groupRead.Controls.Add(this.numericReadStartAddress);
            this.groupRead.Controls.Add(this.btnReadFileNameBrowse);
            this.groupRead.Controls.Add(this.txtReadFileName);
            this.groupRead.Location = new System.Drawing.Point(12, 272);
            this.groupRead.Name = "groupRead";
            this.groupRead.Size = new System.Drawing.Size(296, 151);
            this.groupRead.TabIndex = 3;
            this.groupRead.TabStop = false;
            this.groupRead.Text = "Read EEPROM";
            // 
            // chkReadConsole
            // 
            this.chkReadConsole.AutoSize = true;
            this.chkReadConsole.Checked = true;
            this.chkReadConsole.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReadConsole.Location = new System.Drawing.Point(6, 42);
            this.chkReadConsole.Name = "chkReadConsole";
            this.chkReadConsole.Size = new System.Drawing.Size(98, 17);
            this.chkReadConsole.TabIndex = 15;
            this.chkReadConsole.Text = "Output to log";
            this.chkReadConsole.UseVisualStyleBackColor = true;
            // 
            // chkReadFile
            // 
            this.chkReadFile.AutoSize = true;
            this.chkReadFile.Location = new System.Drawing.Point(6, 19);
            this.chkReadFile.Name = "chkReadFile";
            this.chkReadFile.Size = new System.Drawing.Size(86, 17);
            this.chkReadFile.TabIndex = 14;
            this.chkReadFile.Text = "Output file:";
            this.chkReadFile.UseVisualStyleBackColor = true;
            this.chkReadFile.CheckedChanged += new System.EventHandler(this.chkReadFile_CheckedChanged);
            // 
            // lblReadLengthHex
            // 
            this.lblReadLengthHex.AutoSize = true;
            this.lblReadLengthHex.Location = new System.Drawing.Point(265, 96);
            this.lblReadLengthHex.Name = "lblReadLengthHex";
            this.lblReadLengthHex.Size = new System.Drawing.Size(25, 13);
            this.lblReadLengthHex.TabIndex = 13;
            this.lblReadLengthHex.Text = "hex";
            // 
            // lblReadLengthDec
            // 
            this.lblReadLengthDec.AutoSize = true;
            this.lblReadLengthDec.Location = new System.Drawing.Point(163, 96);
            this.lblReadLengthDec.Name = "lblReadLengthDec";
            this.lblReadLengthDec.Size = new System.Drawing.Size(25, 13);
            this.lblReadLengthDec.TabIndex = 12;
            this.lblReadLengthDec.Text = "dec";
            // 
            // lblReadStartAddressHex
            // 
            this.lblReadStartAddressHex.AutoSize = true;
            this.lblReadStartAddressHex.Location = new System.Drawing.Point(267, 68);
            this.lblReadStartAddressHex.Name = "lblReadStartAddressHex";
            this.lblReadStartAddressHex.Size = new System.Drawing.Size(25, 13);
            this.lblReadStartAddressHex.TabIndex = 11;
            this.lblReadStartAddressHex.Text = "hex";
            // 
            // lblReadStartAddressDec
            // 
            this.lblReadStartAddressDec.AutoSize = true;
            this.lblReadStartAddressDec.Location = new System.Drawing.Point(163, 68);
            this.lblReadStartAddressDec.Name = "lblReadStartAddressDec";
            this.lblReadStartAddressDec.Size = new System.Drawing.Size(25, 13);
            this.lblReadStartAddressDec.TabIndex = 10;
            this.lblReadStartAddressDec.Text = "dec";
            // 
            // txtReadLength
            // 
            this.txtReadLength.Location = new System.Drawing.Point(194, 93);
            this.txtReadLength.Name = "txtReadLength";
            this.txtReadLength.Size = new System.Drawing.Size(67, 22);
            this.txtReadLength.TabIndex = 9;
            this.txtReadLength.TextChanged += new System.EventHandler(this.txtReadLength_TextChanged);
            // 
            // txtReadStartAddress
            // 
            this.txtReadStartAddress.Location = new System.Drawing.Point(194, 65);
            this.txtReadStartAddress.Name = "txtReadStartAddress";
            this.txtReadStartAddress.Size = new System.Drawing.Size(67, 22);
            this.txtReadStartAddress.TabIndex = 8;
            this.txtReadStartAddress.TextChanged += new System.EventHandler(this.txtReadStartAddress_TextChanged);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(6, 122);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(114, 23);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // lblReadLength
            // 
            this.lblReadLength.AutoSize = true;
            this.lblReadLength.Location = new System.Drawing.Point(3, 96);
            this.lblReadLength.Name = "lblReadLength";
            this.lblReadLength.Size = new System.Drawing.Size(46, 13);
            this.lblReadLength.TabIndex = 6;
            this.lblReadLength.Text = "Length:";
            // 
            // lblReadStartAddress
            // 
            this.lblReadStartAddress.AutoSize = true;
            this.lblReadStartAddress.Location = new System.Drawing.Point(3, 68);
            this.lblReadStartAddress.Name = "lblReadStartAddress";
            this.lblReadStartAddress.Size = new System.Drawing.Size(77, 13);
            this.lblReadStartAddress.TabIndex = 5;
            this.lblReadStartAddress.Text = "Start address:";
            // 
            // numericReadLength
            // 
            this.numericReadLength.Location = new System.Drawing.Point(90, 94);
            this.numericReadLength.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.numericReadLength.Name = "numericReadLength";
            this.numericReadLength.Size = new System.Drawing.Size(67, 22);
            this.numericReadLength.TabIndex = 4;
            this.numericReadLength.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericReadLength.ValueChanged += new System.EventHandler(this.numericReadLength_ValueChanged);
            // 
            // numericReadStartAddress
            // 
            this.numericReadStartAddress.Location = new System.Drawing.Point(90, 65);
            this.numericReadStartAddress.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.numericReadStartAddress.Name = "numericReadStartAddress";
            this.numericReadStartAddress.Size = new System.Drawing.Size(67, 22);
            this.numericReadStartAddress.TabIndex = 3;
            this.numericReadStartAddress.ValueChanged += new System.EventHandler(this.numericReadStartAddress_ValueChanged);
            // 
            // btnReadFileNameBrowse
            // 
            this.btnReadFileNameBrowse.Enabled = false;
            this.btnReadFileNameBrowse.Location = new System.Drawing.Point(259, 15);
            this.btnReadFileNameBrowse.Name = "btnReadFileNameBrowse";
            this.btnReadFileNameBrowse.Size = new System.Drawing.Size(31, 22);
            this.btnReadFileNameBrowse.TabIndex = 2;
            this.btnReadFileNameBrowse.Text = "...";
            this.btnReadFileNameBrowse.UseVisualStyleBackColor = true;
            this.btnReadFileNameBrowse.Click += new System.EventHandler(this.btnReadFileNameBrowse_Click);
            // 
            // txtReadFileName
            // 
            this.txtReadFileName.Enabled = false;
            this.txtReadFileName.Location = new System.Drawing.Point(98, 15);
            this.txtReadFileName.Name = "txtReadFileName";
            this.txtReadFileName.Size = new System.Drawing.Size(155, 22);
            this.txtReadFileName.TabIndex = 0;
            // 
            // groupWrite
            // 
            this.groupWrite.Controls.Add(this.lblWriteStartAddressHex);
            this.groupWrite.Controls.Add(this.lblWriteStartAddressDec);
            this.groupWrite.Controls.Add(this.txtWriteStartAddress);
            this.groupWrite.Controls.Add(this.btnWrite);
            this.groupWrite.Controls.Add(this.lblWriteStartAddress);
            this.groupWrite.Controls.Add(this.numericWriteStartAddress);
            this.groupWrite.Controls.Add(this.btnWriteFileNameBrowse);
            this.groupWrite.Controls.Add(this.lblWriteFileName);
            this.groupWrite.Controls.Add(this.txtWriteFileName);
            this.groupWrite.Location = new System.Drawing.Point(314, 315);
            this.groupWrite.Name = "groupWrite";
            this.groupWrite.Size = new System.Drawing.Size(296, 108);
            this.groupWrite.TabIndex = 4;
            this.groupWrite.TabStop = false;
            this.groupWrite.Text = "Write EEPROM";
            // 
            // lblWriteStartAddressHex
            // 
            this.lblWriteStartAddressHex.AutoSize = true;
            this.lblWriteStartAddressHex.Location = new System.Drawing.Point(267, 45);
            this.lblWriteStartAddressHex.Name = "lblWriteStartAddressHex";
            this.lblWriteStartAddressHex.Size = new System.Drawing.Size(25, 13);
            this.lblWriteStartAddressHex.TabIndex = 14;
            this.lblWriteStartAddressHex.Text = "hex";
            // 
            // lblWriteStartAddressDec
            // 
            this.lblWriteStartAddressDec.AutoSize = true;
            this.lblWriteStartAddressDec.Location = new System.Drawing.Point(163, 45);
            this.lblWriteStartAddressDec.Name = "lblWriteStartAddressDec";
            this.lblWriteStartAddressDec.Size = new System.Drawing.Size(25, 13);
            this.lblWriteStartAddressDec.TabIndex = 13;
            this.lblWriteStartAddressDec.Text = "dec";
            // 
            // txtWriteStartAddress
            // 
            this.txtWriteStartAddress.Location = new System.Drawing.Point(194, 43);
            this.txtWriteStartAddress.Name = "txtWriteStartAddress";
            this.txtWriteStartAddress.Size = new System.Drawing.Size(67, 22);
            this.txtWriteStartAddress.TabIndex = 12;
            this.txtWriteStartAddress.TextChanged += new System.EventHandler(this.txtWriteStartAddress_TextChanged);
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWrite.Location = new System.Drawing.Point(6, 79);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(114, 23);
            this.btnWrite.TabIndex = 11;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // lblWriteStartAddress
            // 
            this.lblWriteStartAddress.AutoSize = true;
            this.lblWriteStartAddress.Location = new System.Drawing.Point(3, 45);
            this.lblWriteStartAddress.Name = "lblWriteStartAddress";
            this.lblWriteStartAddress.Size = new System.Drawing.Size(77, 13);
            this.lblWriteStartAddress.TabIndex = 10;
            this.lblWriteStartAddress.Text = "Start address:";
            // 
            // numericWriteStartAddress
            // 
            this.numericWriteStartAddress.Location = new System.Drawing.Point(90, 43);
            this.numericWriteStartAddress.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.numericWriteStartAddress.Name = "numericWriteStartAddress";
            this.numericWriteStartAddress.Size = new System.Drawing.Size(67, 22);
            this.numericWriteStartAddress.TabIndex = 9;
            this.numericWriteStartAddress.ValueChanged += new System.EventHandler(this.numericWriteStartAddress_ValueChanged);
            // 
            // btnWriteFileNameBrowse
            // 
            this.btnWriteFileNameBrowse.Location = new System.Drawing.Point(259, 15);
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
            this.lblWriteFileName.Location = new System.Drawing.Point(3, 18);
            this.lblWriteFileName.Name = "lblWriteFileName";
            this.lblWriteFileName.Size = new System.Drawing.Size(57, 13);
            this.lblWriteFileName.TabIndex = 7;
            this.lblWriteFileName.Text = "Input file:";
            // 
            // txtWriteFileName
            // 
            this.txtWriteFileName.Location = new System.Drawing.Point(66, 15);
            this.txtWriteFileName.Name = "txtWriteFileName";
            this.txtWriteFileName.Size = new System.Drawing.Size(187, 22);
            this.txtWriteFileName.TabIndex = 6;
            // 
            // groupMisc
            // 
            this.groupMisc.Controls.Add(this.btnAbout);
            this.groupMisc.Controls.Add(this.btnDecodeFont);
            this.groupMisc.Controls.Add(this.btnClearLock);
            this.groupMisc.Controls.Add(this.btnEraseChip);
            this.groupMisc.Controls.Add(this.btnClearLog);
            this.groupMisc.Controls.Add(this.btnReadStatus);
            this.groupMisc.Location = new System.Drawing.Point(12, 158);
            this.groupMisc.Name = "groupMisc";
            this.groupMisc.Size = new System.Drawing.Size(296, 108);
            this.groupMisc.TabIndex = 5;
            this.groupMisc.TabStop = false;
            this.groupMisc.Text = "Miscellaneous";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(6, 79);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(114, 23);
            this.btnAbout.TabIndex = 5;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnDecodeFont
            // 
            this.btnDecodeFont.Location = new System.Drawing.Point(126, 79);
            this.btnDecodeFont.Name = "btnDecodeFont";
            this.btnDecodeFont.Size = new System.Drawing.Size(114, 23);
            this.btnDecodeFont.TabIndex = 4;
            this.btnDecodeFont.Text = "Decode font...";
            this.btnDecodeFont.UseVisualStyleBackColor = true;
            this.btnDecodeFont.Click += new System.EventHandler(this.btnDecodeFont_Click);
            // 
            // btnClearLock
            // 
            this.btnClearLock.Location = new System.Drawing.Point(126, 50);
            this.btnClearLock.Name = "btnClearLock";
            this.btnClearLock.Size = new System.Drawing.Size(114, 23);
            this.btnClearLock.TabIndex = 3;
            this.btnClearLock.Text = "Clear lock bits";
            this.btnClearLock.UseVisualStyleBackColor = true;
            this.btnClearLock.Click += new System.EventHandler(this.btnClearLock_Click);
            // 
            // btnEraseChip
            // 
            this.btnEraseChip.Location = new System.Drawing.Point(126, 21);
            this.btnEraseChip.Name = "btnEraseChip";
            this.btnEraseChip.Size = new System.Drawing.Size(114, 23);
            this.btnEraseChip.TabIndex = 2;
            this.btnEraseChip.Text = "Erase chip";
            this.btnEraseChip.UseVisualStyleBackColor = true;
            this.btnEraseChip.Click += new System.EventHandler(this.btnEraseChip_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(6, 50);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(114, 23);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "Clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnReadStatus
            // 
            this.btnReadStatus.Location = new System.Drawing.Point(6, 21);
            this.btnReadStatus.Name = "btnReadStatus";
            this.btnReadStatus.Size = new System.Drawing.Size(114, 23);
            this.btnReadStatus.TabIndex = 0;
            this.btnReadStatus.Text = "Read status";
            this.btnReadStatus.UseVisualStyleBackColor = true;
            this.btnReadStatus.Click += new System.EventHandler(this.btnReadStatus_Click);
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.radioModeExpert);
            this.groupMode.Controls.Add(this.radioModeSimple);
            this.groupMode.Location = new System.Drawing.Point(12, 12);
            this.groupMode.Name = "groupMode";
            this.groupMode.Size = new System.Drawing.Size(296, 44);
            this.groupMode.TabIndex = 6;
            this.groupMode.TabStop = false;
            this.groupMode.Text = "Mode";
            // 
            // radioModeExpert
            // 
            this.radioModeExpert.AutoSize = true;
            this.radioModeExpert.Location = new System.Drawing.Point(126, 21);
            this.radioModeExpert.Name = "radioModeExpert";
            this.radioModeExpert.Size = new System.Drawing.Size(57, 17);
            this.radioModeExpert.TabIndex = 1;
            this.radioModeExpert.Text = "Expert";
            this.radioModeExpert.UseVisualStyleBackColor = true;
            this.radioModeExpert.CheckedChanged += new System.EventHandler(this.radioModeExpert_CheckedChanged);
            // 
            // radioModeSimple
            // 
            this.radioModeSimple.AutoSize = true;
            this.radioModeSimple.Checked = true;
            this.radioModeSimple.Location = new System.Drawing.Point(6, 21);
            this.radioModeSimple.Name = "radioModeSimple";
            this.radioModeSimple.Size = new System.Drawing.Size(59, 17);
            this.radioModeSimple.TabIndex = 0;
            this.radioModeSimple.TabStop = true;
            this.radioModeSimple.Text = "Simple";
            this.radioModeSimple.UseVisualStyleBackColor = true;
            // 
            // groupModify
            // 
            this.groupModify.Controls.Add(this.picBackgroundColor);
            this.groupModify.Controls.Add(this.picLogoForegroundColor);
            this.groupModify.Controls.Add(this.picLogoBackgroundColor);
            this.groupModify.Controls.Add(this.numericLogoForegroundBlue);
            this.groupModify.Controls.Add(this.lblLogoForegroundBlue);
            this.groupModify.Controls.Add(this.numericLogoForegroundGreen);
            this.groupModify.Controls.Add(this.lblLogoForegroundGreen);
            this.groupModify.Controls.Add(this.lblLogoForegroundRed);
            this.groupModify.Controls.Add(this.numericLogoForegroundRed);
            this.groupModify.Controls.Add(this.chkChangeLogoForegroundColor);
            this.groupModify.Controls.Add(this.txtChangeHdmi);
            this.groupModify.Controls.Add(this.chkChangeHdmi);
            this.groupModify.Controls.Add(this.chkRemoveHdmi);
            this.groupModify.Controls.Add(this.numericBackgroundBlue);
            this.groupModify.Controls.Add(this.lblBackgroundBlue);
            this.groupModify.Controls.Add(this.numericBackgroundGreen);
            this.groupModify.Controls.Add(this.lblBackgroundGreen);
            this.groupModify.Controls.Add(this.lblBackgroundRed);
            this.groupModify.Controls.Add(this.numericBackgroundRed);
            this.groupModify.Controls.Add(this.numericLogoBackgroundBlue);
            this.groupModify.Controls.Add(this.lblLogoBackgroundBlue);
            this.groupModify.Controls.Add(this.numericLogoBackgroundGreen);
            this.groupModify.Controls.Add(this.lblLogoBackgroundGreen);
            this.groupModify.Controls.Add(this.lblLogoBackgroundRed);
            this.groupModify.Controls.Add(this.numericLogoBackgroundRed);
            this.groupModify.Controls.Add(this.chkChangeBackgroundColor);
            this.groupModify.Controls.Add(this.chkChangeLogoBackgroundColor);
            this.groupModify.Controls.Add(this.chkChangeLogo);
            this.groupModify.Controls.Add(this.btnModify);
            this.groupModify.Controls.Add(this.btnLogoFileNameBrowse);
            this.groupModify.Controls.Add(this.txtLogoFileName);
            this.groupModify.Controls.Add(this.lblLogoFileName);
            this.groupModify.Location = new System.Drawing.Point(314, 12);
            this.groupModify.Name = "groupModify";
            this.groupModify.Size = new System.Drawing.Size(296, 297);
            this.groupModify.TabIndex = 7;
            this.groupModify.TabStop = false;
            this.groupModify.Text = "Modify firmware";
            // 
            // picBackgroundColor
            // 
            this.picBackgroundColor.BackColor = System.Drawing.Color.Black;
            this.picBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBackgroundColor.Location = new System.Drawing.Point(247, 189);
            this.picBackgroundColor.Name = "picBackgroundColor";
            this.picBackgroundColor.Size = new System.Drawing.Size(22, 22);
            this.picBackgroundColor.TabIndex = 36;
            this.picBackgroundColor.TabStop = false;
            this.picBackgroundColor.Click += new System.EventHandler(this.picBackgroundColor_Click);
            // 
            // picLogoForegroundColor
            // 
            this.picLogoForegroundColor.BackColor = System.Drawing.Color.White;
            this.picLogoForegroundColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picLogoForegroundColor.Location = new System.Drawing.Point(247, 138);
            this.picLogoForegroundColor.Name = "picLogoForegroundColor";
            this.picLogoForegroundColor.Size = new System.Drawing.Size(22, 22);
            this.picLogoForegroundColor.TabIndex = 35;
            this.picLogoForegroundColor.TabStop = false;
            this.picLogoForegroundColor.Click += new System.EventHandler(this.picLogoForegroundColor_Click);
            // 
            // picLogoBackgroundColor
            // 
            this.picLogoBackgroundColor.BackColor = System.Drawing.Color.Black;
            this.picLogoBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picLogoBackgroundColor.Location = new System.Drawing.Point(247, 87);
            this.picLogoBackgroundColor.Name = "picLogoBackgroundColor";
            this.picLogoBackgroundColor.Size = new System.Drawing.Size(22, 22);
            this.picLogoBackgroundColor.TabIndex = 34;
            this.picLogoBackgroundColor.TabStop = false;
            this.picLogoBackgroundColor.Click += new System.EventHandler(this.picLogoBackgroundColor_Click);
            // 
            // numericLogoForegroundBlue
            // 
            this.numericLogoForegroundBlue.Location = new System.Drawing.Point(189, 138);
            this.numericLogoForegroundBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoForegroundBlue.Name = "numericLogoForegroundBlue";
            this.numericLogoForegroundBlue.Size = new System.Drawing.Size(52, 22);
            this.numericLogoForegroundBlue.TabIndex = 33;
            this.numericLogoForegroundBlue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoForegroundBlue.ValueChanged += new System.EventHandler(this.numericLogoForeground_ValueChanged);
            // 
            // lblLogoForegroundBlue
            // 
            this.lblLogoForegroundBlue.AutoSize = true;
            this.lblLogoForegroundBlue.Location = new System.Drawing.Point(166, 140);
            this.lblLogoForegroundBlue.Name = "lblLogoForegroundBlue";
            this.lblLogoForegroundBlue.Size = new System.Drawing.Size(17, 13);
            this.lblLogoForegroundBlue.TabIndex = 32;
            this.lblLogoForegroundBlue.Text = "B:";
            // 
            // numericLogoForegroundGreen
            // 
            this.numericLogoForegroundGreen.Location = new System.Drawing.Point(108, 138);
            this.numericLogoForegroundGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoForegroundGreen.Name = "numericLogoForegroundGreen";
            this.numericLogoForegroundGreen.Size = new System.Drawing.Size(52, 22);
            this.numericLogoForegroundGreen.TabIndex = 31;
            this.numericLogoForegroundGreen.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoForegroundGreen.ValueChanged += new System.EventHandler(this.numericLogoForeground_ValueChanged);
            // 
            // lblLogoForegroundGreen
            // 
            this.lblLogoForegroundGreen.AutoSize = true;
            this.lblLogoForegroundGreen.Location = new System.Drawing.Point(84, 140);
            this.lblLogoForegroundGreen.Name = "lblLogoForegroundGreen";
            this.lblLogoForegroundGreen.Size = new System.Drawing.Size(18, 13);
            this.lblLogoForegroundGreen.TabIndex = 30;
            this.lblLogoForegroundGreen.Text = "G:";
            // 
            // lblLogoForegroundRed
            // 
            this.lblLogoForegroundRed.AutoSize = true;
            this.lblLogoForegroundRed.Location = new System.Drawing.Point(3, 140);
            this.lblLogoForegroundRed.Name = "lblLogoForegroundRed";
            this.lblLogoForegroundRed.Size = new System.Drawing.Size(17, 13);
            this.lblLogoForegroundRed.TabIndex = 29;
            this.lblLogoForegroundRed.Text = "R:";
            // 
            // numericLogoForegroundRed
            // 
            this.numericLogoForegroundRed.Location = new System.Drawing.Point(26, 138);
            this.numericLogoForegroundRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoForegroundRed.Name = "numericLogoForegroundRed";
            this.numericLogoForegroundRed.Size = new System.Drawing.Size(52, 22);
            this.numericLogoForegroundRed.TabIndex = 28;
            this.numericLogoForegroundRed.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoForegroundRed.ValueChanged += new System.EventHandler(this.numericLogoForeground_ValueChanged);
            // 
            // chkChangeLogoForegroundColor
            // 
            this.chkChangeLogoForegroundColor.AutoSize = true;
            this.chkChangeLogoForegroundColor.Location = new System.Drawing.Point(6, 115);
            this.chkChangeLogoForegroundColor.Name = "chkChangeLogoForegroundColor";
            this.chkChangeLogoForegroundColor.Size = new System.Drawing.Size(185, 17);
            this.chkChangeLogoForegroundColor.TabIndex = 27;
            this.chkChangeLogoForegroundColor.Text = "Change logo foreground color";
            this.chkChangeLogoForegroundColor.UseVisualStyleBackColor = true;
            this.chkChangeLogoForegroundColor.CheckedChanged += new System.EventHandler(this.chkModifyFirmware_CheckedChanged);
            // 
            // txtChangeHdmi
            // 
            this.txtChangeHdmi.Location = new System.Drawing.Point(177, 238);
            this.txtChangeHdmi.Name = "txtChangeHdmi";
            this.txtChangeHdmi.Size = new System.Drawing.Size(113, 22);
            this.txtChangeHdmi.TabIndex = 26;
            this.txtChangeHdmi.Text = "HDMI";
            // 
            // chkChangeHdmi
            // 
            this.chkChangeHdmi.AutoSize = true;
            this.chkChangeHdmi.Location = new System.Drawing.Point(6, 240);
            this.chkChangeHdmi.Name = "chkChangeHdmi";
            this.chkChangeHdmi.Size = new System.Drawing.Size(165, 17);
            this.chkChangeHdmi.TabIndex = 25;
            this.chkChangeHdmi.Text = "Change \"HDMI\" pop-up to:";
            this.chkChangeHdmi.UseVisualStyleBackColor = true;
            this.chkChangeHdmi.CheckedChanged += new System.EventHandler(this.chkModifyFirmware_CheckedChanged);
            // 
            // chkRemoveHdmi
            // 
            this.chkRemoveHdmi.AutoSize = true;
            this.chkRemoveHdmi.Location = new System.Drawing.Point(6, 217);
            this.chkRemoveHdmi.Name = "chkRemoveHdmi";
            this.chkRemoveHdmi.Size = new System.Drawing.Size(148, 17);
            this.chkRemoveHdmi.TabIndex = 24;
            this.chkRemoveHdmi.Text = "Remove \"HDMI\" pop-up";
            this.chkRemoveHdmi.UseVisualStyleBackColor = true;
            this.chkRemoveHdmi.CheckedChanged += new System.EventHandler(this.chkModifyFirmware_CheckedChanged);
            // 
            // numericBackgroundBlue
            // 
            this.numericBackgroundBlue.Location = new System.Drawing.Point(189, 189);
            this.numericBackgroundBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericBackgroundBlue.Name = "numericBackgroundBlue";
            this.numericBackgroundBlue.Size = new System.Drawing.Size(52, 22);
            this.numericBackgroundBlue.TabIndex = 23;
            this.numericBackgroundBlue.ValueChanged += new System.EventHandler(this.numericBackground_ValueChanged);
            // 
            // lblBackgroundBlue
            // 
            this.lblBackgroundBlue.AutoSize = true;
            this.lblBackgroundBlue.Location = new System.Drawing.Point(166, 191);
            this.lblBackgroundBlue.Name = "lblBackgroundBlue";
            this.lblBackgroundBlue.Size = new System.Drawing.Size(17, 13);
            this.lblBackgroundBlue.TabIndex = 22;
            this.lblBackgroundBlue.Text = "B:";
            // 
            // numericBackgroundGreen
            // 
            this.numericBackgroundGreen.Location = new System.Drawing.Point(108, 189);
            this.numericBackgroundGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericBackgroundGreen.Name = "numericBackgroundGreen";
            this.numericBackgroundGreen.Size = new System.Drawing.Size(52, 22);
            this.numericBackgroundGreen.TabIndex = 21;
            this.numericBackgroundGreen.ValueChanged += new System.EventHandler(this.numericBackground_ValueChanged);
            // 
            // lblBackgroundGreen
            // 
            this.lblBackgroundGreen.AutoSize = true;
            this.lblBackgroundGreen.Location = new System.Drawing.Point(84, 191);
            this.lblBackgroundGreen.Name = "lblBackgroundGreen";
            this.lblBackgroundGreen.Size = new System.Drawing.Size(18, 13);
            this.lblBackgroundGreen.TabIndex = 20;
            this.lblBackgroundGreen.Text = "G:";
            // 
            // lblBackgroundRed
            // 
            this.lblBackgroundRed.AutoSize = true;
            this.lblBackgroundRed.Location = new System.Drawing.Point(3, 191);
            this.lblBackgroundRed.Name = "lblBackgroundRed";
            this.lblBackgroundRed.Size = new System.Drawing.Size(17, 13);
            this.lblBackgroundRed.TabIndex = 19;
            this.lblBackgroundRed.Text = "R:";
            // 
            // numericBackgroundRed
            // 
            this.numericBackgroundRed.Location = new System.Drawing.Point(26, 189);
            this.numericBackgroundRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericBackgroundRed.Name = "numericBackgroundRed";
            this.numericBackgroundRed.Size = new System.Drawing.Size(52, 22);
            this.numericBackgroundRed.TabIndex = 18;
            this.numericBackgroundRed.ValueChanged += new System.EventHandler(this.numericBackground_ValueChanged);
            // 
            // numericLogoBackgroundBlue
            // 
            this.numericLogoBackgroundBlue.Location = new System.Drawing.Point(189, 87);
            this.numericLogoBackgroundBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoBackgroundBlue.Name = "numericLogoBackgroundBlue";
            this.numericLogoBackgroundBlue.Size = new System.Drawing.Size(52, 22);
            this.numericLogoBackgroundBlue.TabIndex = 17;
            this.numericLogoBackgroundBlue.ValueChanged += new System.EventHandler(this.numericLogoBackground_ValueChanged);
            // 
            // lblLogoBackgroundBlue
            // 
            this.lblLogoBackgroundBlue.AutoSize = true;
            this.lblLogoBackgroundBlue.Location = new System.Drawing.Point(166, 89);
            this.lblLogoBackgroundBlue.Name = "lblLogoBackgroundBlue";
            this.lblLogoBackgroundBlue.Size = new System.Drawing.Size(17, 13);
            this.lblLogoBackgroundBlue.TabIndex = 16;
            this.lblLogoBackgroundBlue.Text = "B:";
            // 
            // numericLogoBackgroundGreen
            // 
            this.numericLogoBackgroundGreen.Location = new System.Drawing.Point(108, 87);
            this.numericLogoBackgroundGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoBackgroundGreen.Name = "numericLogoBackgroundGreen";
            this.numericLogoBackgroundGreen.Size = new System.Drawing.Size(52, 22);
            this.numericLogoBackgroundGreen.TabIndex = 15;
            this.numericLogoBackgroundGreen.ValueChanged += new System.EventHandler(this.numericLogoBackground_ValueChanged);
            // 
            // lblLogoBackgroundGreen
            // 
            this.lblLogoBackgroundGreen.AutoSize = true;
            this.lblLogoBackgroundGreen.Location = new System.Drawing.Point(84, 89);
            this.lblLogoBackgroundGreen.Name = "lblLogoBackgroundGreen";
            this.lblLogoBackgroundGreen.Size = new System.Drawing.Size(18, 13);
            this.lblLogoBackgroundGreen.TabIndex = 14;
            this.lblLogoBackgroundGreen.Text = "G:";
            // 
            // lblLogoBackgroundRed
            // 
            this.lblLogoBackgroundRed.AutoSize = true;
            this.lblLogoBackgroundRed.Location = new System.Drawing.Point(3, 89);
            this.lblLogoBackgroundRed.Name = "lblLogoBackgroundRed";
            this.lblLogoBackgroundRed.Size = new System.Drawing.Size(17, 13);
            this.lblLogoBackgroundRed.TabIndex = 13;
            this.lblLogoBackgroundRed.Text = "R:";
            // 
            // numericLogoBackgroundRed
            // 
            this.numericLogoBackgroundRed.Location = new System.Drawing.Point(26, 87);
            this.numericLogoBackgroundRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericLogoBackgroundRed.Name = "numericLogoBackgroundRed";
            this.numericLogoBackgroundRed.Size = new System.Drawing.Size(52, 22);
            this.numericLogoBackgroundRed.TabIndex = 12;
            this.numericLogoBackgroundRed.ValueChanged += new System.EventHandler(this.numericLogoBackground_ValueChanged);
            // 
            // chkChangeBackgroundColor
            // 
            this.chkChangeBackgroundColor.AutoSize = true;
            this.chkChangeBackgroundColor.Location = new System.Drawing.Point(6, 166);
            this.chkChangeBackgroundColor.Name = "chkChangeBackgroundColor";
            this.chkChangeBackgroundColor.Size = new System.Drawing.Size(200, 17);
            this.chkChangeBackgroundColor.TabIndex = 11;
            this.chkChangeBackgroundColor.Text = "Change display background color";
            this.chkChangeBackgroundColor.UseVisualStyleBackColor = true;
            this.chkChangeBackgroundColor.CheckedChanged += new System.EventHandler(this.chkModifyFirmware_CheckedChanged);
            // 
            // chkChangeLogoBackgroundColor
            // 
            this.chkChangeLogoBackgroundColor.AutoSize = true;
            this.chkChangeLogoBackgroundColor.Location = new System.Drawing.Point(6, 64);
            this.chkChangeLogoBackgroundColor.Name = "chkChangeLogoBackgroundColor";
            this.chkChangeLogoBackgroundColor.Size = new System.Drawing.Size(188, 17);
            this.chkChangeLogoBackgroundColor.TabIndex = 10;
            this.chkChangeLogoBackgroundColor.Text = "Change logo background color";
            this.chkChangeLogoBackgroundColor.UseVisualStyleBackColor = true;
            this.chkChangeLogoBackgroundColor.CheckedChanged += new System.EventHandler(this.chkModifyFirmware_CheckedChanged);
            // 
            // chkChangeLogo
            // 
            this.chkChangeLogo.AutoSize = true;
            this.chkChangeLogo.Location = new System.Drawing.Point(6, 19);
            this.chkChangeLogo.Name = "chkChangeLogo";
            this.chkChangeLogo.Size = new System.Drawing.Size(93, 17);
            this.chkChangeLogo.TabIndex = 9;
            this.chkChangeLogo.Text = "Change logo";
            this.chkChangeLogo.UseVisualStyleBackColor = true;
            this.chkChangeLogo.CheckedChanged += new System.EventHandler(this.chkModifyFirmware_CheckedChanged);
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModify.Location = new System.Drawing.Point(6, 268);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(114, 23);
            this.btnModify.TabIndex = 8;
            this.btnModify.Text = "Modify firmware";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnLogoFileNameBrowse
            // 
            this.btnLogoFileNameBrowse.Location = new System.Drawing.Point(259, 36);
            this.btnLogoFileNameBrowse.Name = "btnLogoFileNameBrowse";
            this.btnLogoFileNameBrowse.Size = new System.Drawing.Size(31, 22);
            this.btnLogoFileNameBrowse.TabIndex = 4;
            this.btnLogoFileNameBrowse.Text = "...";
            this.btnLogoFileNameBrowse.UseVisualStyleBackColor = true;
            this.btnLogoFileNameBrowse.Click += new System.EventHandler(this.btnLogoFileNameBrowse_Click);
            // 
            // txtLogoFileName
            // 
            this.txtLogoFileName.Location = new System.Drawing.Point(95, 36);
            this.txtLogoFileName.Name = "txtLogoFileName";
            this.txtLogoFileName.Size = new System.Drawing.Size(158, 22);
            this.txtLogoFileName.TabIndex = 3;
            // 
            // lblLogoFileName
            // 
            this.lblLogoFileName.AutoSize = true;
            this.lblLogoFileName.Location = new System.Drawing.Point(3, 39);
            this.lblLogoFileName.Name = "lblLogoFileName";
            this.lblLogoFileName.Size = new System.Drawing.Size(86, 13);
            this.lblLogoFileName.TabIndex = 2;
            this.lblLogoFileName.Text = "Logo input file:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 461);
            this.Controls.Add(this.groupModify);
            this.Controls.Add(this.groupMode);
            this.Controls.Add(this.groupMisc);
            this.Controls.Add(this.groupWrite);
            this.Controls.Add(this.groupRead);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.groupConnection);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FormMain";
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
            this.groupModify.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoForegroundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoBackgroundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoForegroundBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoForegroundGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoForegroundRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackgroundBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackgroundGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackgroundRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoBackgroundBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoBackgroundGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLogoBackgroundRed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button btnDecodeFont;
        private System.Windows.Forms.TextBox txtReadStartAddress;
        private System.Windows.Forms.TextBox txtReadLength;
        private System.Windows.Forms.TextBox txtWriteStartAddress;
        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.RadioButton radioModeExpert;
        private System.Windows.Forms.RadioButton radioModeSimple;
        private System.Windows.Forms.Label lblReadLengthHex;
        private System.Windows.Forms.Label lblReadLengthDec;
        private System.Windows.Forms.Label lblReadStartAddressHex;
        private System.Windows.Forms.Label lblReadStartAddressDec;
        private System.Windows.Forms.Label lblWriteStartAddressHex;
        private System.Windows.Forms.Label lblWriteStartAddressDec;
        private System.Windows.Forms.GroupBox groupModify;
        private System.Windows.Forms.Label lblLogoFileName;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnLogoFileNameBrowse;
        private System.Windows.Forms.TextBox txtLogoFileName;
        private System.Windows.Forms.CheckBox chkReadConsole;
        private System.Windows.Forms.CheckBox chkReadFile;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.CheckBox chkChangeLogo;
        private System.Windows.Forms.CheckBox chkChangeBackgroundColor;
        private System.Windows.Forms.CheckBox chkChangeLogoBackgroundColor;
        private System.Windows.Forms.NumericUpDown numericLogoBackgroundBlue;
        private System.Windows.Forms.Label lblLogoBackgroundBlue;
        private System.Windows.Forms.NumericUpDown numericLogoBackgroundGreen;
        private System.Windows.Forms.Label lblLogoBackgroundGreen;
        private System.Windows.Forms.Label lblLogoBackgroundRed;
        private System.Windows.Forms.NumericUpDown numericLogoBackgroundRed;
        private System.Windows.Forms.NumericUpDown numericBackgroundBlue;
        private System.Windows.Forms.Label lblBackgroundBlue;
        private System.Windows.Forms.NumericUpDown numericBackgroundGreen;
        private System.Windows.Forms.Label lblBackgroundGreen;
        private System.Windows.Forms.Label lblBackgroundRed;
        private System.Windows.Forms.NumericUpDown numericBackgroundRed;
        private System.Windows.Forms.TextBox txtChangeHdmi;
        private System.Windows.Forms.CheckBox chkChangeHdmi;
        private System.Windows.Forms.CheckBox chkRemoveHdmi;
        private System.Windows.Forms.NumericUpDown numericLogoForegroundBlue;
        private System.Windows.Forms.Label lblLogoForegroundBlue;
        private System.Windows.Forms.NumericUpDown numericLogoForegroundGreen;
        private System.Windows.Forms.Label lblLogoForegroundGreen;
        private System.Windows.Forms.Label lblLogoForegroundRed;
        private System.Windows.Forms.NumericUpDown numericLogoForegroundRed;
        private System.Windows.Forms.CheckBox chkChangeLogoForegroundColor;
        private System.Windows.Forms.PictureBox picLogoBackgroundColor;
        private System.Windows.Forms.PictureBox picBackgroundColor;
        private System.Windows.Forms.PictureBox picLogoForegroundColor;
    }
}

