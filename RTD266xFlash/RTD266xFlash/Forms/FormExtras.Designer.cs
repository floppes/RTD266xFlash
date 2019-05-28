namespace RTD266xFlash.Forms
{
    partial class FormExtras
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
            this.btnIdentifyFirmware = new System.Windows.Forms.Button();
            this.btnCalculateHash = new System.Windows.Forms.Button();
            this.btnDecodeFont = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIdentifyFirmware
            // 
            this.btnIdentifyFirmware.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIdentifyFirmware.Location = new System.Drawing.Point(12, 12);
            this.btnIdentifyFirmware.Name = "btnIdentifyFirmware";
            this.btnIdentifyFirmware.Size = new System.Drawing.Size(183, 23);
            this.btnIdentifyFirmware.TabIndex = 6;
            this.btnIdentifyFirmware.Text = "Identify firmware...";
            this.btnIdentifyFirmware.UseVisualStyleBackColor = true;
            this.btnIdentifyFirmware.Click += new System.EventHandler(this.btnIdentifyFirmware_Click);
            // 
            // btnCalculateHash
            // 
            this.btnCalculateHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculateHash.Location = new System.Drawing.Point(12, 41);
            this.btnCalculateHash.Name = "btnCalculateHash";
            this.btnCalculateHash.Size = new System.Drawing.Size(183, 23);
            this.btnCalculateHash.TabIndex = 7;
            this.btnCalculateHash.Text = "Calculate firmware hash...";
            this.btnCalculateHash.UseVisualStyleBackColor = true;
            this.btnCalculateHash.Click += new System.EventHandler(this.btnCalculateHash_Click);
            // 
            // btnDecodeFont
            // 
            this.btnDecodeFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecodeFont.Location = new System.Drawing.Point(12, 70);
            this.btnDecodeFont.Name = "btnDecodeFont";
            this.btnDecodeFont.Size = new System.Drawing.Size(183, 23);
            this.btnDecodeFont.TabIndex = 8;
            this.btnDecodeFont.Text = "Decode font...";
            this.btnDecodeFont.UseVisualStyleBackColor = true;
            this.btnDecodeFont.Click += new System.EventHandler(this.btnDecodeFont_Click);
            // 
            // FormExtras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 104);
            this.Controls.Add(this.btnDecodeFont);
            this.Controls.Add(this.btnCalculateHash);
            this.Controls.Add(this.btnIdentifyFirmware);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormExtras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTD266x extras";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnIdentifyFirmware;
        private System.Windows.Forms.Button btnCalculateHash;
        private System.Windows.Forms.Button btnDecodeFont;
    }
}