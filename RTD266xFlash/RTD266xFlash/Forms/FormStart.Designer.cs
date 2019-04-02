namespace RTD266xFlash.Forms
{
    partial class FormStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHint = new System.Windows.Forms.Label();
            this.radioButtonArduino = new System.Windows.Forms.RadioButton();
            this.radioButtonFile = new System.Windows.Forms.RadioButton();
            this.lblHintArduino = new System.Windows.Forms.Label();
            this.lblHintImages = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHint
            // 
            this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHint.Location = new System.Drawing.Point(12, 9);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(279, 39);
            this.lblHint.TabIndex = 0;
            this.lblHint.Text = "Please select the way you interface with your RTD266x chip:";
            this.lblHint.DoubleClick += new System.EventHandler(this.lblHint_DoubleClick);
            // 
            // radioButtonArduino
            // 
            this.radioButtonArduino.AutoSize = true;
            this.radioButtonArduino.Checked = true;
            this.radioButtonArduino.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonArduino.Location = new System.Drawing.Point(12, 51);
            this.radioButtonArduino.Name = "radioButtonArduino";
            this.radioButtonArduino.Size = new System.Drawing.Size(197, 17);
            this.radioButtonArduino.TabIndex = 1;
            this.radioButtonArduino.TabStop = true;
            this.radioButtonArduino.Text = "Connect directly with an Arduino";
            this.radioButtonArduino.UseVisualStyleBackColor = true;
            // 
            // radioButtonFile
            // 
            this.radioButtonFile.AutoSize = true;
            this.radioButtonFile.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFile.Location = new System.Drawing.Point(12, 129);
            this.radioButtonFile.Name = "radioButtonFile";
            this.radioButtonFile.Size = new System.Drawing.Size(113, 17);
            this.radioButtonFile.TabIndex = 2;
            this.radioButtonFile.Text = "Firmware images";
            this.radioButtonFile.UseVisualStyleBackColor = true;
            // 
            // lblHintArduino
            // 
            this.lblHintArduino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHintArduino.Location = new System.Drawing.Point(12, 72);
            this.lblHintArduino.Name = "lblHintArduino";
            this.lblHintArduino.Size = new System.Drawing.Size(279, 54);
            this.lblHintArduino.TabIndex = 3;
            this.lblHintArduino.Text = "Select this option if you are using an Arduino and the RTD266xArduino sketch to c" +
    "onnect to the I2C lines using a modified HDMI cable.";
            // 
            // lblHintImages
            // 
            this.lblHintImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHintImages.Location = new System.Drawing.Point(12, 149);
            this.lblHintImages.Name = "lblHintImages";
            this.lblHintImages.Size = new System.Drawing.Size(279, 47);
            this.lblHintImages.TabIndex = 4;
            this.lblHintImages.Text = "Select this option if you are using the Python script RTD266xPy on a Raspberry Pi" +
    " to read and write the firmware via HDMI and use firmware image files.";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Location = new System.Drawing.Point(12, 216);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(89, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(202, 216);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(89, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAbout.Location = new System.Drawing.Point(107, 216);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(89, 23);
            this.btnAbout.TabIndex = 7;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // FormStart
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(303, 251);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblHintImages);
            this.Controls.Add(this.lblHintArduino);
            this.Controls.Add(this.radioButtonFile);
            this.Controls.Add(this.radioButtonArduino);
            this.Controls.Add(this.lblHint);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTD266xFlash";
            this.Load += new System.EventHandler(this.FormStart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.RadioButton radioButtonArduino;
        private System.Windows.Forms.RadioButton radioButtonFile;
        private System.Windows.Forms.Label lblHintArduino;
        private System.Windows.Forms.Label lblHintImages;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAbout;
    }
}