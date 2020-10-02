namespace Cradle
{
    partial class RomInfo
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
            this.aeonYesNo = new System.Windows.Forms.Label();
            this.hashMatchLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aeonYesNo
            // 
            this.aeonYesNo.AutoSize = true;
            this.aeonYesNo.Location = new System.Drawing.Point(12, 46);
            this.aeonYesNo.Name = "aeonYesNo";
            this.aeonYesNo.Size = new System.Drawing.Size(180, 17);
            this.aeonYesNo.TabIndex = 0;
            this.aeonYesNo.Text = "Translation patch detected!";
            this.aeonYesNo.Click += new System.EventHandler(this.aeonYesNo_Click);
            // 
            // hashMatchLabel
            // 
            this.hashMatchLabel.AutoSize = true;
            this.hashMatchLabel.Location = new System.Drawing.Point(21, 193);
            this.hashMatchLabel.Name = "hashMatchLabel";
            this.hashMatchLabel.Size = new System.Drawing.Size(322, 17);
            this.hashMatchLabel.TabIndex = 1;
            this.hashMatchLabel.Text = "Hash not recognised. The rom may not be vanilla.";
            // 
            // RomInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 105);
            this.Controls.Add(this.hashMatchLabel);
            this.Controls.Add(this.aeonYesNo);
            this.Name = "RomInfo";
            this.Text = "RomInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label aeonYesNo;
        public System.Windows.Forms.Label hashMatchLabel;
    }
}