namespace Cradle
{
    partial class TextEditor
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
            this.TextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SpeakerSelector = new System.Windows.Forms.ComboBox();
            this.textOffsetLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.PreviewTextBox = new System.Windows.Forms.RichTextBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.blueCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox.Location = new System.Drawing.Point(22, 30);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(736, 164);
            this.TextBox.TabIndex = 1;
            this.TextBox.Text = "";
            this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.blueCheckBox);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.SpeakerSelector);
            this.groupBox1.Controls.Add(this.textOffsetLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(778, 245);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dialogue";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 32);
            this.button1.TabIndex = 7;
            this.button1.Text = "Update preview";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SpeakerSelector
            // 
            this.SpeakerSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpeakerSelector.FormattingEnabled = true;
            this.SpeakerSelector.Items.AddRange(new object[] {
            "None",
            "Jennifer",
            "Mary",
            "Evil Mary",
            "Laura",
            "Anne",
            "Lotte",
            "Simon",
            "Jennifer shocked",
            "Jennifer eye zoom",
            "Jennifer annoyed",
            "Mary turns evil"});
            this.SpeakerSelector.Location = new System.Drawing.Point(97, 207);
            this.SpeakerSelector.Name = "SpeakerSelector";
            this.SpeakerSelector.Size = new System.Drawing.Size(119, 24);
            this.SpeakerSelector.TabIndex = 6;
            this.SpeakerSelector.SelectedIndexChanged += new System.EventHandler(this.SpeakerSelector_SelectedIndexChanged);
            // 
            // textOffsetLabel
            // 
            this.textOffsetLabel.AutoSize = true;
            this.textOffsetLabel.Location = new System.Drawing.Point(409, 210);
            this.textOffsetLabel.Name = "textOffsetLabel";
            this.textOffsetLabel.Size = new System.Drawing.Size(54, 17);
            this.textOffsetLabel.TabIndex = 5;
            this.textOffsetLabel.Text = "Offset: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Portrait";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 58);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conversation ID";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(93, 22);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(117, 22);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(105, 354);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(637, 132);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // PreviewTextBox
            // 
            this.PreviewTextBox.BackColor = System.Drawing.Color.Black;
            this.PreviewTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PreviewTextBox.Font = new System.Drawing.Font("Rockwell", 19F);
            this.PreviewTextBox.ForeColor = System.Drawing.Color.White;
            this.PreviewTextBox.Location = new System.Drawing.Point(249, 377);
            this.PreviewTextBox.Name = "PreviewTextBox";
            this.PreviewTextBox.ReadOnly = true;
            this.PreviewTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PreviewTextBox.Size = new System.Drawing.Size(324, 85);
            this.PreviewTextBox.TabIndex = 7;
            this.PreviewTextBox.Text = "";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(17, 22);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(406, 22);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(446, 17);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 30);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.searchButton);
            this.groupBox3.Controls.Add(this.searchBox);
            this.groupBox3.Location = new System.Drawing.Point(249, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(541, 58);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search for text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Disclaimer: The preview is not perfect and may sometimes get it wrong";
            // 
            // blueCheckBox
            // 
            this.blueCheckBox.AutoSize = true;
            this.blueCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blueCheckBox.Location = new System.Drawing.Point(254, 207);
            this.blueCheckBox.Name = "blueCheckBox";
            this.blueCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.blueCheckBox.Size = new System.Drawing.Size(104, 28);
            this.blueCheckBox.TabIndex = 8;
            this.blueCheckBox.Text = "Blue text";
            this.blueCheckBox.UseVisualStyleBackColor = true;
            this.blueCheckBox.CheckedChanged += new System.EventHandler(this.blueCheckBox_CheckedChanged);
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 514);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.PreviewTextBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TextEditor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "TextEditor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox TextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label textOffsetLabel;
        private System.Windows.Forms.ComboBox SpeakerSelector;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.RichTextBox PreviewTextBox;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox blueCheckBox;
    }
}