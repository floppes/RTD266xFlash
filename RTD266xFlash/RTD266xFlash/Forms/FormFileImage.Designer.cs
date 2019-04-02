namespace RTD266xFlash.Forms
{
    partial class FormFileImage
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
            this.groupModify = new System.Windows.Forms.GroupBox();
            this.modificationSettings = new RTD266xFlash.ModificationSettings();
            this.groupInputFile = new System.Windows.Forms.GroupBox();
            this.btnInputFileNameBrowse = new System.Windows.Forms.Button();
            this.lblInputFileName = new System.Windows.Forms.Label();
            this.txtInputFileName = new System.Windows.Forms.TextBox();
            this.groupModify.SuspendLayout();
            this.groupInputFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupModify
            // 
            this.groupModify.Controls.Add(this.modificationSettings);
            this.groupModify.Location = new System.Drawing.Point(13, 71);
            this.groupModify.Name = "groupModify";
            this.groupModify.Size = new System.Drawing.Size(296, 297);
            this.groupModify.TabIndex = 8;
            this.groupModify.TabStop = false;
            this.groupModify.Text = "Modification options";
            // 
            // modificationSettings
            // 
            this.modificationSettings.Location = new System.Drawing.Point(3, 16);
            this.modificationSettings.ModifyEnabled = true;
            this.modificationSettings.Name = "modificationSettings";
            this.modificationSettings.Size = new System.Drawing.Size(291, 275);
            this.modificationSettings.TabIndex = 0;
            this.modificationSettings.ModifyClickedEvent += new RTD266xFlash.ModificationSettings.ModifyClickedHandler(this.modificationSettings_ModifyClickedEvent);
            // 
            // groupInputFile
            // 
            this.groupInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupInputFile.Controls.Add(this.btnInputFileNameBrowse);
            this.groupInputFile.Controls.Add(this.lblInputFileName);
            this.groupInputFile.Controls.Add(this.txtInputFileName);
            this.groupInputFile.Location = new System.Drawing.Point(12, 12);
            this.groupInputFile.Name = "groupInputFile";
            this.groupInputFile.Size = new System.Drawing.Size(297, 53);
            this.groupInputFile.TabIndex = 9;
            this.groupInputFile.TabStop = false;
            this.groupInputFile.Text = "Firmware image file";
            // 
            // btnInputFileNameBrowse
            // 
            this.btnInputFileNameBrowse.Location = new System.Drawing.Point(261, 20);
            this.btnInputFileNameBrowse.Name = "btnInputFileNameBrowse";
            this.btnInputFileNameBrowse.Size = new System.Drawing.Size(31, 22);
            this.btnInputFileNameBrowse.TabIndex = 11;
            this.btnInputFileNameBrowse.Text = "...";
            this.btnInputFileNameBrowse.UseVisualStyleBackColor = true;
            this.btnInputFileNameBrowse.Click += new System.EventHandler(this.btnInputFileNameBrowse_Click_1);
            // 
            // lblInputFileName
            // 
            this.lblInputFileName.AutoSize = true;
            this.lblInputFileName.Location = new System.Drawing.Point(5, 23);
            this.lblInputFileName.Name = "lblInputFileName";
            this.lblInputFileName.Size = new System.Drawing.Size(57, 13);
            this.lblInputFileName.TabIndex = 10;
            this.lblInputFileName.Text = "Input file:";
            // 
            // txtInputFileName
            // 
            this.txtInputFileName.Location = new System.Drawing.Point(68, 20);
            this.txtInputFileName.Name = "txtInputFileName";
            this.txtInputFileName.Size = new System.Drawing.Size(187, 22);
            this.txtInputFileName.TabIndex = 9;
            // 
            // FormFileImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 378);
            this.Controls.Add(this.groupInputFile);
            this.Controls.Add(this.groupModify);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormFileImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTD266xFlash";
            this.groupModify.ResumeLayout(false);
            this.groupInputFile.ResumeLayout(false);
            this.groupInputFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupModify;
        private ModificationSettings modificationSettings;
        private System.Windows.Forms.GroupBox groupInputFile;
        private System.Windows.Forms.Button btnInputFileNameBrowse;
        private System.Windows.Forms.Label lblInputFileName;
        private System.Windows.Forms.TextBox txtInputFileName;
    }
}