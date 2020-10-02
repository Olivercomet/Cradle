namespace Cradle
{
    partial class Randomizer
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
            this.components = new System.ComponentModel.Container();
            this.randomizeItemsBox = new System.Windows.Forms.CheckBox();
            this.RandomizeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GenerateNewSeed = new System.Windows.Forms.Button();
            this.seedUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.seedUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // randomizeItemsBox
            // 
            this.randomizeItemsBox.AutoSize = true;
            this.randomizeItemsBox.Location = new System.Drawing.Point(30, 21);
            this.randomizeItemsBox.Name = "randomizeItemsBox";
            this.randomizeItemsBox.Size = new System.Drawing.Size(138, 21);
            this.randomizeItemsBox.TabIndex = 0;
            this.randomizeItemsBox.Text = "Randomize items";
            this.randomizeItemsBox.UseVisualStyleBackColor = true;
            this.randomizeItemsBox.CheckedChanged += new System.EventHandler(this.randomizeItemsBox_CheckedChanged);
            // 
            // RandomizeButton
            // 
            this.RandomizeButton.Location = new System.Drawing.Point(30, 61);
            this.RandomizeButton.Name = "RandomizeButton";
            this.RandomizeButton.Size = new System.Drawing.Size(138, 47);
            this.RandomizeButton.TabIndex = 3;
            this.RandomizeButton.Text = "Randomize";
            this.RandomizeButton.UseVisualStyleBackColor = true;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Seed:";
            // 
            // GenerateNewSeed
            // 
            this.GenerateNewSeed.Location = new System.Drawing.Point(56, 57);
            this.GenerateNewSeed.Name = "GenerateNewSeed";
            this.GenerateNewSeed.Size = new System.Drawing.Size(144, 26);
            this.GenerateNewSeed.TabIndex = 5;
            this.GenerateNewSeed.Text = "Generate new seed";
            this.GenerateNewSeed.UseVisualStyleBackColor = true;
            this.GenerateNewSeed.Click += new System.EventHandler(this.GenerateNewSeed_Click);
            // 
            // seedUpDown
            // 
            this.seedUpDown.Location = new System.Drawing.Point(74, 21);
            this.seedUpDown.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.seedUpDown.Minimum = new decimal(new int[] {
            1410065408,
            2,
            0,
            -2147483648});
            this.seedUpDown.Name = "seedUpDown";
            this.seedUpDown.Size = new System.Drawing.Size(126, 22);
            this.seedUpDown.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GenerateNewSeed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.seedUpDown);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 94);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.randomizeItemsBox);
            this.groupBox2.Controls.Add(this.RandomizeButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 125);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "This is a joke really";
            // 
            // Randomizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 244);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Randomizer";
            this.Text = "Randomizer";
            ((System.ComponentModel.ISupportInitialize)(this.seedUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox randomizeItemsBox;
        private System.Windows.Forms.Button RandomizeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GenerateNewSeed;
        private System.Windows.Forms.NumericUpDown seedUpDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}