namespace Cradle
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.romInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogueEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randoTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RoomListBox = new System.Windows.Forms.ListBox();
            this.ObjectsBox = new System.Windows.Forms.GroupBox();
            this.CurrentObjectOffset = new System.Windows.Forms.Label();
            this.ObjectPlayerPosY_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectPlayerPosX_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectUnk2_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectCursorOffsetY_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectCursorOffsetX_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectUnk1_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectYPos_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectXPos_box = new System.Windows.Forms.NumericUpDown();
            this.ObjectIgnorePlayerPos_box = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ObjectInteractionScriptButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.RoomOffsetLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.ObjectsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectPlayerPosY_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectPlayerPosX_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectUnk2_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectCursorOffsetY_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectCursorOffsetX_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectUnk1_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectYPos_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectXPos_box)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1283, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRomToolStripMenuItem,
            this.saveRomToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openRomToolStripMenuItem
            // 
            this.openRomToolStripMenuItem.Name = "openRomToolStripMenuItem";
            this.openRomToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.openRomToolStripMenuItem.Text = "Open rom";
            this.openRomToolStripMenuItem.Click += new System.EventHandler(this.openRomToolStripMenuItem_Click);
            // 
            // saveRomToolStripMenuItem
            // 
            this.saveRomToolStripMenuItem.Name = "saveRomToolStripMenuItem";
            this.saveRomToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.saveRomToolStripMenuItem.Text = "Save rom";
            this.saveRomToolStripMenuItem.Visible = false;
            this.saveRomToolStripMenuItem.Click += new System.EventHandler(this.saveRomToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.romInfoToolStripMenuItem,
            this.dialogueEditorToolStripMenuItem,
            this.randomizerToolStripMenuItem,
            this.randoTrackerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // romInfoToolStripMenuItem
            // 
            this.romInfoToolStripMenuItem.Name = "romInfoToolStripMenuItem";
            this.romInfoToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.romInfoToolStripMenuItem.Text = "Rom info";
            this.romInfoToolStripMenuItem.Click += new System.EventHandler(this.romInfoToolStripMenuItem_Click);
            // 
            // dialogueEditorToolStripMenuItem
            // 
            this.dialogueEditorToolStripMenuItem.Name = "dialogueEditorToolStripMenuItem";
            this.dialogueEditorToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.dialogueEditorToolStripMenuItem.Text = "Dialogue editor";
            this.dialogueEditorToolStripMenuItem.Click += new System.EventHandler(this.dialogueEditorToolStripMenuItem_Click);
            // 
            // randomizerToolStripMenuItem
            // 
            this.randomizerToolStripMenuItem.Name = "randomizerToolStripMenuItem";
            this.randomizerToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.randomizerToolStripMenuItem.Text = "Randomizer";
            this.randomizerToolStripMenuItem.Click += new System.EventHandler(this.randomizerToolStripMenuItem_Click);
            // 
            // randoTrackerToolStripMenuItem
            // 
            this.randoTrackerToolStripMenuItem.Name = "randoTrackerToolStripMenuItem";
            this.randoTrackerToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.randoTrackerToolStripMenuItem.Text = "Rando tracker";
            this.randoTrackerToolStripMenuItem.Click += new System.EventHandler(this.randoTrackerToolStripMenuItem_Click);
            // 
            // RoomListBox
            // 
            this.RoomListBox.FormattingEnabled = true;
            this.RoomListBox.ItemHeight = 16;
            this.RoomListBox.Location = new System.Drawing.Point(17, 32);
            this.RoomListBox.Name = "RoomListBox";
            this.RoomListBox.Size = new System.Drawing.Size(209, 372);
            this.RoomListBox.TabIndex = 1;
            this.RoomListBox.SelectedIndexChanged += new System.EventHandler(this.RoomListBox_SelectedIndexChanged);
            // 
            // ObjectsBox
            // 
            this.ObjectsBox.Controls.Add(this.CurrentObjectOffset);
            this.ObjectsBox.Controls.Add(this.ObjectPlayerPosY_box);
            this.ObjectsBox.Controls.Add(this.ObjectPlayerPosX_box);
            this.ObjectsBox.Controls.Add(this.ObjectUnk2_box);
            this.ObjectsBox.Controls.Add(this.ObjectCursorOffsetY_box);
            this.ObjectsBox.Controls.Add(this.ObjectCursorOffsetX_box);
            this.ObjectsBox.Controls.Add(this.ObjectUnk1_box);
            this.ObjectsBox.Controls.Add(this.ObjectYPos_box);
            this.ObjectsBox.Controls.Add(this.ObjectXPos_box);
            this.ObjectsBox.Controls.Add(this.ObjectIgnorePlayerPos_box);
            this.ObjectsBox.Controls.Add(this.label9);
            this.ObjectsBox.Controls.Add(this.label8);
            this.ObjectsBox.Controls.Add(this.label7);
            this.ObjectsBox.Controls.Add(this.ObjectInteractionScriptButton);
            this.ObjectsBox.Controls.Add(this.label6);
            this.ObjectsBox.Controls.Add(this.label5);
            this.ObjectsBox.Controls.Add(this.label4);
            this.ObjectsBox.Controls.Add(this.label3);
            this.ObjectsBox.Controls.Add(this.label2);
            this.ObjectsBox.Controls.Add(this.label1);
            this.ObjectsBox.Location = new System.Drawing.Point(1031, 107);
            this.ObjectsBox.Name = "ObjectsBox";
            this.ObjectsBox.Size = new System.Drawing.Size(240, 490);
            this.ObjectsBox.TabIndex = 3;
            this.ObjectsBox.TabStop = false;
            this.ObjectsBox.Text = "Object information";
            this.ObjectsBox.Enter += new System.EventHandler(this.ObjectsBox_Enter);
            // 
            // CurrentObjectOffset
            // 
            this.CurrentObjectOffset.AutoSize = true;
            this.CurrentObjectOffset.Location = new System.Drawing.Point(96, 336);
            this.CurrentObjectOffset.Name = "CurrentObjectOffset";
            this.CurrentObjectOffset.Size = new System.Drawing.Size(76, 17);
            this.CurrentObjectOffset.TabIndex = 6;
            this.CurrentObjectOffset.Text = "Offset (d): ";
            // 
            // ObjectPlayerPosY_box
            // 
            this.ObjectPlayerPosY_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectPlayerPosY_box.Location = new System.Drawing.Point(128, 276);
            this.ObjectPlayerPosY_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectPlayerPosY_box.Name = "ObjectPlayerPosY_box";
            this.ObjectPlayerPosY_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectPlayerPosY_box.TabIndex = 22;
            this.ObjectPlayerPosY_box.ValueChanged += new System.EventHandler(this.ObjectPlayerPosY_box_ValueChanged);
            // 
            // ObjectPlayerPosX_box
            // 
            this.ObjectPlayerPosX_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectPlayerPosX_box.Location = new System.Drawing.Point(128, 248);
            this.ObjectPlayerPosX_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectPlayerPosX_box.Name = "ObjectPlayerPosX_box";
            this.ObjectPlayerPosX_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectPlayerPosX_box.TabIndex = 21;
            this.ObjectPlayerPosX_box.ValueChanged += new System.EventHandler(this.ObjectPlayerPosX_box_ValueChanged);
            // 
            // ObjectUnk2_box
            // 
            this.ObjectUnk2_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectUnk2_box.Location = new System.Drawing.Point(128, 178);
            this.ObjectUnk2_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectUnk2_box.Name = "ObjectUnk2_box";
            this.ObjectUnk2_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectUnk2_box.TabIndex = 20;
            this.ObjectUnk2_box.ValueChanged += new System.EventHandler(this.ObjectUnk2_box_ValueChanged);
            // 
            // ObjectCursorOffsetY_box
            // 
            this.ObjectCursorOffsetY_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectCursorOffsetY_box.Location = new System.Drawing.Point(128, 150);
            this.ObjectCursorOffsetY_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectCursorOffsetY_box.Name = "ObjectCursorOffsetY_box";
            this.ObjectCursorOffsetY_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectCursorOffsetY_box.TabIndex = 19;
            this.ObjectCursorOffsetY_box.ValueChanged += new System.EventHandler(this.ObjectCursorOffsetY_box_ValueChanged);
            // 
            // ObjectCursorOffsetX_box
            // 
            this.ObjectCursorOffsetX_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectCursorOffsetX_box.Location = new System.Drawing.Point(128, 122);
            this.ObjectCursorOffsetX_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectCursorOffsetX_box.Name = "ObjectCursorOffsetX_box";
            this.ObjectCursorOffsetX_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectCursorOffsetX_box.TabIndex = 10;
            this.ObjectCursorOffsetX_box.ValueChanged += new System.EventHandler(this.ObjectCursorOffsetX_box_ValueChanged);
            // 
            // ObjectUnk1_box
            // 
            this.ObjectUnk1_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectUnk1_box.Location = new System.Drawing.Point(128, 94);
            this.ObjectUnk1_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectUnk1_box.Name = "ObjectUnk1_box";
            this.ObjectUnk1_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectUnk1_box.TabIndex = 9;
            this.ObjectUnk1_box.ValueChanged += new System.EventHandler(this.ObjectUnk1_box_ValueChanged);
            // 
            // ObjectYPos_box
            // 
            this.ObjectYPos_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectYPos_box.Location = new System.Drawing.Point(128, 66);
            this.ObjectYPos_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectYPos_box.Name = "ObjectYPos_box";
            this.ObjectYPos_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectYPos_box.TabIndex = 8;
            this.ObjectYPos_box.ValueChanged += new System.EventHandler(this.ObjectYPos_box_ValueChanged);
            // 
            // ObjectXPos_box
            // 
            this.ObjectXPos_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectXPos_box.Location = new System.Drawing.Point(128, 36);
            this.ObjectXPos_box.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectXPos_box.Name = "ObjectXPos_box";
            this.ObjectXPos_box.Size = new System.Drawing.Size(92, 22);
            this.ObjectXPos_box.TabIndex = 7;
            this.ObjectXPos_box.ValueChanged += new System.EventHandler(this.ObjectXPos_box_ValueChanged);
            // 
            // ObjectIgnorePlayerPos_box
            // 
            this.ObjectIgnorePlayerPos_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectIgnorePlayerPos_box.AutoSize = true;
            this.ObjectIgnorePlayerPos_box.Location = new System.Drawing.Point(80, 304);
            this.ObjectIgnorePlayerPos_box.Name = "ObjectIgnorePlayerPos_box";
            this.ObjectIgnorePlayerPos_box.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ObjectIgnorePlayerPos_box.Size = new System.Drawing.Size(140, 21);
            this.ObjectIgnorePlayerPos_box.TabIndex = 18;
            this.ObjectIgnorePlayerPos_box.Text = "Ignore player pos";
            this.ObjectIgnorePlayerPos_box.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 278);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "Player pos Y";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 248);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Player pos X";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Interaction script";
            // 
            // ObjectInteractionScriptButton
            // 
            this.ObjectInteractionScriptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectInteractionScriptButton.Location = new System.Drawing.Point(128, 207);
            this.ObjectInteractionScriptButton.Name = "ObjectInteractionScriptButton";
            this.ObjectInteractionScriptButton.Size = new System.Drawing.Size(92, 35);
            this.ObjectInteractionScriptButton.TabIndex = 12;
            this.ObjectInteractionScriptButton.Text = "...";
            this.ObjectInteractionScriptButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Unknown 2";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cursor offset Y";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cursor offset X";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Unknown";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y position";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "X position";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.RoomOffsetLabel);
            this.groupBox1.Controls.Add(this.RoomListBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 565);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rooms";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 470);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "Export background image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RoomOffsetLabel
            // 
            this.RoomOffsetLabel.AutoSize = true;
            this.RoomOffsetLabel.Location = new System.Drawing.Point(17, 411);
            this.RoomOffsetLabel.Name = "RoomOffsetLabel";
            this.RoomOffsetLabel.Size = new System.Drawing.Size(76, 17);
            this.RoomOffsetLabel.TabIndex = 2;
            this.RoomOffsetLabel.Text = "Offset (d): ";
            this.RoomOffsetLabel.Click += new System.EventHandler(this.RoomOffsetLabel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Location = new System.Drawing.Point(1031, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 59);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Object";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(82, 22);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(268, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(744, 555);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 609);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ObjectsBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Cradle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ObjectsBox.ResumeLayout(false);
            this.ObjectsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectPlayerPosY_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectPlayerPosX_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectUnk2_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectCursorOffsetY_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectCursorOffsetX_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectUnk1_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectYPos_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectXPos_box)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRomToolStripMenuItem;
        private System.Windows.Forms.GroupBox ObjectsBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem saveRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem romInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialogueEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randoTrackerToolStripMenuItem;
        public System.Windows.Forms.ListBox RoomListBox;
        public System.Windows.Forms.Button ObjectInteractionScriptButton;
        public System.Windows.Forms.CheckBox ObjectIgnorePlayerPos_box;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.NumericUpDown ObjectXPos_box;
        public System.Windows.Forms.NumericUpDown ObjectYPos_box;
        public System.Windows.Forms.NumericUpDown ObjectUnk1_box;
        public System.Windows.Forms.NumericUpDown ObjectCursorOffsetX_box;
        public System.Windows.Forms.NumericUpDown ObjectUnk2_box;
        public System.Windows.Forms.NumericUpDown ObjectCursorOffsetY_box;
        public System.Windows.Forms.NumericUpDown ObjectPlayerPosX_box;
        public System.Windows.Forms.NumericUpDown ObjectPlayerPosY_box;
        public System.Windows.Forms.Label CurrentObjectOffset;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label RoomOffsetLabel;
        public System.Windows.Forms.PictureBox pictureBox1;
    }
}

