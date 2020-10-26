using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cradle
{
    public partial class Form1 : Form
    {
        public int currentlySelectedDialogue;

        
        public Byte[] VanillaRomJP_Hash = new Byte[] { 0x51, 0xF8, 0xA9, 0xE9, 0x5E, 0xC5, 0x8F, 0xA5, 0xB9, 0xC3, 0xEC, 0x2D, 0xA5, 0x30, 0xD5, 0x98 };
        public Byte[] VanillaRomJP_bHash = new Byte[] { 0x3A, 0xC3, 0x19, 0x6C, 0x9C, 0x2C, 0x7B, 0x08, 0xCD, 0xA6, 0xF0, 0x36, 0x55, 0x50, 0x76, 0xFC };
        public Byte[] VanillaRomJP_b_c_Hash = new Byte[] { 0x3D, 0xAB, 0xB8, 0x97, 0x35, 0x6F, 0x55, 0x4B, 0xDA, 0x58, 0xB9, 0x39, 0x25, 0xBE, 0x73, 0xA1 };
        public Byte[] RomAGHash = new Byte[] { 0x2F, 0xDC, 0x76, 0x59, 0x67, 0x50, 0x69, 0x40, 0x94, 0x50, 0x84, 0xEA, 0x7F, 0x42, 0xEC, 0xAA };
        public Byte[] RomAGHash2 = new Byte[] { 0xD0, 0x17, 0x40, 0x68, 0xFF, 0x7F, 0x06, 0x09, 0x2E, 0x0F, 0x66, 0x9A, 0x61, 0x88, 0xDB, 0xF2 };

        
        public List<string> SpoilerLog = new List<string>();

        public Form1()
        {
            InitializeComponent();

            Dictionaries.LoadDictionaries();

            utility.form1 = this;

            this.Icon = Properties.Resources.jenicon;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            rom.isStandaloneCradle = true;

            rom.form1 = this;

            


            /*
            string ENUM_TEMPLATE = "public enum ENUMNAME {\n";
            int enum_count = 10000;

            for (int i = 0; i < enum_count; i++)
                {
                ENUM_TEMPLATE += "NAME = "+i+",\n";
                }

            ENUM_TEMPLATE += "}";
            File.WriteAllText("ENUM_TEMPLATE.txt", ENUM_TEMPLATE);*/
        }
     
        

        public void LoadRomInCradle(string romFileName) {

            rom.StartUp(romFileName);
            this.RoomListBox.MouseDoubleClick += new MouseEventHandler(RoomListBox_MouseDoubleClick);
        }


        void RoomListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.RoomListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches && RoomListBox.Items[index].ToString() != "Invalid")
            {
                utility.LoadRoom(index);
                MessageBox.Show("specialsprite and animation were temporarily set to scissorman!");
                rom.CurrentRoom.objects[0].SpecialSprite = Object.SpecialSpriteType.Scissorman;    //TEMP
                rom.CurrentRoom.objects[0].LoadAnimation(0x70, false);      //TEMP
                rom.CurrentRoom.objects[0].DisplaySprite(rom.CurrentRoom.objects[0].animationFrames[0].SpriteID, 0);           //TEMP
            }
        }


       

        public void UpdateObjectPanel() {

            ObjectXPos_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].Xpos.ToString();
            ObjectYPos_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].Ypos.ToString();
            ObjectUnk1_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].unk1.ToString();
            ObjectCursorOffsetX_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].cursorXPosOffset.ToString();
            ObjectCursorOffsetY_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].cursorYPosOffset.ToString();
            ObjectUnk2_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].unk2.ToString();
            ObjectPlayerPosX_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].playerXPos.ToString();
            ObjectPlayerPosY_box.Text = rom.CurrentRoom.objects[(int)numericUpDown1.Value].playerYPos.ToString();
            CurrentObjectOffset.Text = "Offset (d): "+(rom.CurrentRoom.objectListOffset + (0x14 * (int)numericUpDown1.Value)).ToString();


            if (Dictionaries.ScriptOffsetsAndNames.Keys.Contains(rom.CurrentRoom.objects[(int)numericUpDown1.Value].offsetOfInteractionScript))
            {
                ObjectInteractionScriptButton.Text = Dictionaries.ScriptOffsetsAndNames[rom.CurrentRoom.objects[(int)numericUpDown1.Value].offsetOfInteractionScript];
            }
            else
            {
                ObjectInteractionScriptButton.Text = ByteArrayToHexString(BitConverter.GetBytes((uint)(rom.CurrentRoom.objects[(int)numericUpDown1.Value].offsetOfInteractionScript)));
            }
        }

        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);

            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }
            string newstring = Result.ToString();

            string outputstring = "";

            outputstring += newstring[6];
            outputstring += newstring[7];
            outputstring += newstring[4];
            outputstring += newstring[5];
            outputstring += newstring[2];
            outputstring += newstring[3];
            outputstring += newstring[0];
            outputstring += newstring[1];

            return outputstring;
        }

        public static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
            0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }


        public void WriteUInt16(Byte[] bytes, int offset, ushort data)
        {
            bytes[offset] = BitConverter.GetBytes(data)[0];
            bytes[offset+1] = BitConverter.GetBytes(data)[1];
        }

        public void WriteInt16(Byte[] bytes, int offset, short data)
        {
            bytes[offset] = BitConverter.GetBytes(data)[0];
            bytes[offset + 1] = BitConverter.GetBytes(data)[1];
        }

        public void WriteUInt32(Byte[] bytes, int offset, uint data)
        {
            bytes[offset] = BitConverter.GetBytes(data)[0];
            bytes[offset + 1] = BitConverter.GetBytes(data)[1];
            bytes[offset + 2] = BitConverter.GetBytes(data)[2];
            bytes[offset + 3] = BitConverter.GetBytes(data)[3];
        }


        


       

        private void openRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Select Clock Tower SNES rom";
            openFileDialog1.DefaultExt = "sfc";
            openFileDialog1.Filter = "SNES rom (*.smc;*.sfc;*.swc)|*.smc;*.sfc;*.swc";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadRomInCradle(openFileDialog1.FileName);
            }
        }

        public void Save()
        {
            if (rom.filebytes.Length == 0)
                {
                MessageBox.Show("You need to open a rom first!", "Rom required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
                }
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Title = "Save new rom";
            saveFileDialog1.DefaultExt = "sfc";
            saveFileDialog1.Filter = "SNES rom (*.smc;*.sfc;*.swc)|*.smc;*.sfc;*.swc";
            saveFileDialog1.CheckPathExists = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int currentoffset = 0;

                foreach (ushort ID in Dictionaries.IDsAndRooms.Keys)
                {
                    currentoffset = (int)Dictionaries.IDsAndRooms[ID].objectListOffset;

                    foreach (Object o in Dictionaries.IDsAndRooms[ID].objects)
                    {
                        WriteUInt16(rom.filebytes, currentoffset, o.Ypos);
                        currentoffset += 2;
                        WriteUInt16(rom.filebytes, currentoffset, o.Xpos);
                        currentoffset += 2;
                        WriteUInt16(rom.filebytes, currentoffset, o.unk1);
                        currentoffset += 2;
                        WriteUInt16(rom.filebytes, currentoffset, o.cursorXPosOffset);
                        currentoffset += 2;
                        WriteUInt16(rom.filebytes, currentoffset, o.cursorYPosOffset);
                        currentoffset += 2;
                        WriteUInt16(rom.filebytes, currentoffset, o.unk2);
                        currentoffset += 2;
                        WriteUInt32(rom.filebytes, currentoffset, utility.ConvertToSNESOffset(o.offsetOfInteractionScript));
                        currentoffset += 4;
                        WriteUInt16(rom.filebytes, currentoffset, o.playerXPos);
                        currentoffset += 2;
                        WriteUInt16(rom.filebytes, currentoffset, o.playerYPos);
                        currentoffset += 2;
                    }

                }

                if (rom.isTranslatedVersion)
                {
                    for (int i = 0; i < Dictionaries.DialogueIDAndDialogue.Keys.Count; i++)
                    {
                        List<Byte> potentialNewDialogue = Dictionaries.DialogueIDAndDialogue[i].ReconstructBytes();
                        currentoffset = 0x301000 + Dictionaries.DialogueIDAndDialogue[i].offset;

                        if (potentialNewDialogue.Count != Dictionaries.DialogueIDAndDialogue[i].originalLength)  //if it's a different length compared to how it was originally
                        {
                            for (int j = i + 1; j < Dictionaries.DialogueIDAndDialogue.Keys.Count; j++) //adjust all the other offsets to match the new length
                            {
                                Dictionaries.DialogueIDAndDialogue[j].offset += potentialNewDialogue.Count - Dictionaries.DialogueIDAndDialogue[i].originalLength;
                            }
                        }

                        foreach (Byte b in potentialNewDialogue)
                            {
                            rom.filebytes[currentoffset] = b;
                            currentoffset++;
                            }
                    }
                    currentoffset = 0x301000;
                    for (int i = 0; i < Dictionaries.DialogueIDAndDialogue.Keys.Count; i++)
                    {
                        WriteUInt16(rom.filebytes, currentoffset, (ushort)Dictionaries.DialogueIDAndDialogue[i].offset);
                        currentoffset += 0x02;
                    }
                }
                File.Delete(saveFileDialog1.FileName);
                File.WriteAllBytes(saveFileDialog1.FileName, rom.filebytes.ToArray());

                MessageBox.Show("Saved new rom to "+ Path.GetFileName(saveFileDialog1.FileName), "Rom saved successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (SpoilerLog.Count != 0)
                    {
                    File.WriteAllLines(saveFileDialog1.FileName + "_spoilerlog.txt", SpoilerLog);
                 }
            }
        }


        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RoomListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateObjectPanel();
        }

        private void ObjectsBox_Enter(object sender, EventArgs e)
        {

        }

        private void ObjectXPos_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].Xpos = (ushort)ObjectXPos_box.Value;
        }

        private void ObjectYPos_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].Ypos = (ushort)ObjectYPos_box.Value;
        }

        private void ObjectUnk1_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].unk1 = (ushort)ObjectUnk1_box.Value;
        }

        private void ObjectCursorOffsetX_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].cursorXPosOffset = (ushort)ObjectCursorOffsetX_box.Value;
        }

        private void ObjectCursorOffsetY_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].cursorYPosOffset = (ushort)ObjectCursorOffsetY_box.Value;
        }

        private void ObjectUnk2_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].unk2 = (ushort)ObjectUnk2_box.Value;
        }

        private void ObjectPlayerPosX_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].playerXPos = (ushort)ObjectPlayerPosX_box.Value;
        }

        private void ObjectPlayerPosY_box_ValueChanged(object sender, EventArgs e)
        {
            rom.CurrentRoom.objects[(int)numericUpDown1.Value].playerYPos = (ushort)ObjectPlayerPosY_box.Value;
        }

        private void saveRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void RoomOffsetLabel_Click(object sender, EventArgs e)
        {

        }


        private void romInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RomInfo rominfo = new RomInfo();
            rominfo.Show();
            if (rom.isTranslatedVersion)
            {
                rominfo.aeonYesNo.Text = "Translation patch detected.";
            }
            else
            {
                rominfo.aeonYesNo.Text = "No translation detected.";
            }

            /*
            if (activeRomHash == VanillaRomJP_Hash)
                {
                rominfo.hashMatchLabel.Text = "Hash matches Vanilla JP rom.";
                }
            else if (activeRomHash == VanillaRomJP_bHash)
                {
                rominfo.hashMatchLabel.Text = "Hash matches Vanilla JP rom.";
                }
            else if (activeRomHash == VanillaRomJP_b_c_Hash)
                {
                rominfo.hashMatchLabel.Text = "Hash matches Vanilla JP rom.";
                }
            else if (activeRomHash == RomAGHash)
                {
                rominfo.hashMatchLabel.Text = "Hash matches Aeon Genesis v1.01 rom.";
                }
            else if (activeRomHash == RomAGHash2)
                {
                rominfo.hashMatchLabel.Text = "Hash matches Aeon Genesis v1.01 rom.";
                }
            else
            {
                Console.WriteLine();
                rominfo.hashMatchLabel.Text = "Unknown hash. Rom is probably not vanilla.";
            }*/
        }

        private void dialogueEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.filebytes.Length == 0)
                {
                MessageBox.Show("You need to open a rom first!", "Rom required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            else
                {
                TextEditor texteditor = new TextEditor();
                texteditor.form1 = this;

                if (Dictionaries.DialogueIDAndDialogue.Keys.Count == 0)
                    {
                    //Create your private font collection object.
                    PrivateFontCollection pfc = new PrivateFontCollection();

                    //Select your font from the resources.
                    int fontLength = Properties.Resources.rockwell.Length;

                    // create a buffer to read in to
                    byte[] fontdata = Properties.Resources.rockwell;

                    // create an unsafe memory block for the font data
                    System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

                    // copy the bytes to the unsafe memory block
                    Marshal.Copy(fontdata, 0, data, fontLength);

                    // pass the font to the font collection
                    pfc.AddMemoryFont(data, fontLength);

                    texteditor.PreviewTextBox.Font = new Font(pfc.Families[0], texteditor.PreviewTextBox.Font.Size);
                    text.LoadAllText();
                    }
                texteditor.DisplayText();
                texteditor.Show();
                }
        }

        private void randomizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.filebytes.Length == 0)
                {
                MessageBox.Show("You need to open a rom first!", "Rom required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            else
                {
                Randomizer randomizer = new Randomizer();

                randomizer.form1 = this;

                randomizer.Show();
                }
        }

        private void randoTrackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandoTracker randoTracker = new RandoTracker();
            randoTracker.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("export.png");
        }
    }
}
