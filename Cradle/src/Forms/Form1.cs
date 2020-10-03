using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cradle
{
    public partial class Form1 : Form
    {

        public string activeRomFilename = "";


        public Byte[] activeRomHash;


        public bool isTranslatedVersion = false;


        public int currentlySelectedDialogue;

        public List<ushort> roomIDList = new List<ushort>();

        public Dictionary<ushort, room> IDsAndRooms = new Dictionary<ushort, room>();


        public room CurrentRoom;

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
        }


       

     
        public void StartUp() {

            Dictionaries.RoomIndexAndOffset = new Dictionary<int, int>();
            Dictionaries.DialogueIDAndDialogue = new Dictionary<int, Dialogue>();

            using (BinaryReader reader = new BinaryReader(File.Open(activeRomFilename, FileMode.Open)))
            {
                reader.BaseStream.Position = 0x86FC;

                for (int i = 0; i <= 0x3A; i++)
                {
                    roomIDList.Add(reader.ReadUInt16());
                    reader.BaseStream.Position += 1;
                }
            }

            byte[] tempRom = File.ReadAllBytes(activeRomFilename);


            if (tempRom[0] == 0x8B) //It's a headerless rom. Proceed as usual.
            {
                rom.filebytes = tempRom;
            }
            else        //it has a header. Remove the header from the byte array.
            {
                rom.filebytes = new byte[tempRom.Length - 0x200];
                Array.Copy(tempRom, 0x200, rom.filebytes, 0, rom.filebytes.Length);
                tempRom = null;
            }


            for (int i = 0; i < Dictionaries.RoomIDsAndNames.Count; i++)    //read room list in rom
            {
                Dictionaries.RoomIndexAndOffset.Add(i, utility.GetPCOffset(rom.filebytes, 0x86FC + (i * 3)));
            }

            switch (rom.filebytes[0x24B0])
                {
                case 0x24:
                    isTranslatedVersion = false;
                    break;
                case 0xD0:
                    isTranslatedVersion = true;
                    break;
                }

            rom.BGPalette0 = imageTools.GetPaletteAtOffset(rom.filebytes, 0x003A59, true);   //not sure if should be true or false

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(activeRomFilename))
                {
                    activeRomHash = md5.ComputeHash(stream);
                    
                }
            }

            foreach (ushort room in roomIDList)
            {
                RoomListBox.Items.Add(Dictionaries.RoomIDsAndNames[room]);
            }

            this.RoomListBox.MouseDoubleClick += new MouseEventHandler(RoomListBox_MouseDoubleClick);
            }

        void RoomListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.RoomListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches && RoomListBox.Items[index].ToString() != "Invalid")
            {
                LoadRoom(index);
                CurrentRoom.objects[0].SpecialSprite = "scissorman";    //TEMP
                CurrentRoom.objects[0].LoadAnimation(0x70, false);      //TEMP
            }
        }


        public void LoadRoom(int index)
        {
            ushort CurrentRoomID = roomIDList[index];

            if (!IDsAndRooms.Keys.Contains(CurrentRoomID))
                {
                room newRoom = new room();
                CurrentRoom = newRoom;

                newRoom.offset = roomIDList[index];
                newRoom.name = Dictionaries.RoomIDsAndNames[newRoom.offset];

                newRoom.objectListOffset = utility.ConvertToPCOffset(utility.Read3Bytes(rom.filebytes, newRoom.offset + 0x1C));
                newRoom.objectListLastOffset = utility.ConvertToPCOffset(utility.Read3Bytes(rom.filebytes, newRoom.offset + 0x22));

                int currentOffset = newRoom.offset = roomIDList[index];

                int backgroundTilesValue = BitConverter.ToUInt16(rom.filebytes, currentOffset);

                currentOffset += 0x1C;

                newRoom.objectListOffset = (uint)utility.GetPCOffset(rom.filebytes, currentOffset);
                currentOffset += 0x03;
                currentOffset += 0x03; //skip an identical offset (although... is it always identical?)
                newRoom.objectListLastOffset = (uint)utility.GetPCOffset(rom.filebytes, currentOffset);
                currentOffset += 0x03;

                currentOffset = newRoom.offset + 0x2B;
                newRoom.foregroundSpritePalettesOffset = utility.GetPCOffset(rom.filebytes, currentOffset);

                currentOffset = newRoom.foregroundSpritePalettesOffset;

                newRoom.foreground_palettes = new Palette[8];

                newRoom.foreground_palettes[1] = imageTools.GetPaletteWithIndex(rom.filebytes, rom.filebytes[currentOffset]);
                currentOffset++;

                newRoom.foreground_palettes[2] = imageTools.GetPaletteWithIndex(rom.filebytes, rom.filebytes[currentOffset]);
                currentOffset++;

                newRoom.foreground_palettes[3] = imageTools.GetPaletteWithIndex(rom.filebytes, rom.filebytes[currentOffset]);
                currentOffset++;

                newRoom.foreground_palettes[4] = imageTools.GetPaletteWithIndex(rom.filebytes, rom.filebytes[currentOffset]);
                currentOffset++;

                newRoom.foreground_palettes[5] = imageTools.GetPaletteWithIndex(rom.filebytes, rom.filebytes[currentOffset]);
                currentOffset++;

                newRoom.foreground_palettes[6] = imageTools.GetPaletteWithIndex(rom.filebytes, rom.filebytes[currentOffset]); //usually used for scissorman
                currentOffset++;

                Console.WriteLine("begin reading foreground palette with index 7");
                newRoom.foreground_palettes[7] = imageTools.GetPaletteWithIndex(rom.filebytes, rom.filebytes[currentOffset]); //usually used for jennifer
                currentOffset++;


                int backgroundTileGraphicsValue = BitConverter.ToUInt16(rom.filebytes, newRoom.offset + 0x0C);
                int backgroundPaletteValue = BitConverter.ToUInt16(rom.filebytes, newRoom.offset + 0x0E);
                Console.WriteLine(backgroundPaletteValue);

                newRoom.LoadBackgroundPalette(backgroundPaletteValue);
                newRoom.LoadBackgroundTileMap(backgroundTilesValue);
                newRoom.LoadBackgroundTileGraphics(backgroundTileGraphicsValue);

                newRoom.LoadRoomObjects();

                IDsAndRooms.Add(CurrentRoomID, newRoom);
                }
            else
                {
                CurrentRoom = IDsAndRooms[CurrentRoomID];
                }

            pictureBox1.Image = CurrentRoom.background;

            RoomOffsetLabel.Text = "Offset (d): " + roomIDList[RoomListBox.SelectedIndex];

            numericUpDown1.Maximum = CurrentRoom.objects.Count - 1;
            numericUpDown1.Minimum = 0;

            numericUpDown1.Value = numericUpDown1.Minimum;
            UpdateObjectPanel();
        }

        public void UpdateObjectPanel() {

            ObjectXPos_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].Xpos.ToString();
            ObjectYPos_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].Ypos.ToString();
            ObjectUnk1_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].unk1.ToString();
            ObjectCursorOffsetX_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].cursorXPosOffset.ToString();
            ObjectCursorOffsetY_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].cursorYPosOffset.ToString();
            ObjectUnk2_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].unk2.ToString();
            ObjectPlayerPosX_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].playerXPos.ToString();
            ObjectPlayerPosY_box.Text = CurrentRoom.objects[(int)numericUpDown1.Value].playerYPos.ToString();
            CurrentObjectOffset.Text = "Offset (d): "+(CurrentRoom.objectListOffset + (0x14 * (int)numericUpDown1.Value)).ToString();


            if (Dictionaries.ScriptOffsetsAndNames.Keys.Contains(CurrentRoom.objects[(int)numericUpDown1.Value].offsetOfInteractionScript))
            {
                ObjectInteractionScriptButton.Text = Dictionaries.ScriptOffsetsAndNames[CurrentRoom.objects[(int)numericUpDown1.Value].offsetOfInteractionScript];
            }
            else
            {
                ObjectInteractionScriptButton.Text = ByteArrayToHexString(BitConverter.GetBytes((uint)(CurrentRoom.objects[(int)numericUpDown1.Value].offsetOfInteractionScript)));
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
                activeRomFilename = openFileDialog1.FileName;
                byte[] tempbytes = File.ReadAllBytes(activeRomFilename);
                if (tempbytes[0] == 0x00 && tempbytes[0x10] == 0x00 && tempbytes[0x20] == 0x00 && tempbytes[0x190] == 0x00)
                {
                    DialogResult dialogResult = MessageBox.Show("This rom appears to have a header. The header must be removed for the program to work. Remove it?", "Remove header?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (BinaryWriter writer = new BinaryWriter(File.Open(activeRomFilename, FileMode.Open)))
                        {
                            for (int i = 0x200; i < tempbytes.Length; i++)
                                {
                                writer.Write(tempbytes[i]);
                                }
                        }

                        tempbytes = File.ReadAllBytes(activeRomFilename);

                        StartUp();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Headered roms are not compatible with this program.", "Invalid rom", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    StartUp();
                }
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

                foreach (ushort ID in IDsAndRooms.Keys)
                {
                    currentoffset = (int)IDsAndRooms[ID].objectListOffset;

                    foreach (Object o in IDsAndRooms[ID].objects)
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

                if (isTranslatedVersion)
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


        public void LoadText() {

            int currentoffset = 0;

            if (isTranslatedVersion)
            {
                currentoffset = 0x301000;
            }
            else
            {
                currentoffset = 0x020010;
            }

            for (int i = 0; i < 0x200; i++)
                {
                Dialogue newDialogue = new Dialogue();

                newDialogue.offset = BitConverter.ToUInt16(rom.filebytes,currentoffset);
                currentoffset += 0x02;

                newDialogue.NoSpeaker = true;

                Dictionaries.DialogueIDAndDialogue.Add(i,newDialogue);
                }

            for (int i = 0; i < 0x200; i++)
            {
                if (isTranslatedVersion)
                {
                    currentoffset = 0x301000 + Dictionaries.DialogueIDAndDialogue[i].offset;
                }
                else
                {
                    currentoffset = 0x020010 + Dictionaries.DialogueIDAndDialogue[i].offset;
                }
                

            

                while (rom.filebytes[currentoffset] != 0xFF)
                {
                    Dictionaries.DialogueIDAndDialogue[i].originalLength++;
                switch (rom.filebytes[currentoffset])
                        {
                        case 0x01:
                            Dictionaries.DialogueIDAndDialogue[i].text += "[Wait=";
                            currentoffset++;
                            Dictionaries.DialogueIDAndDialogue[i].text += rom.filebytes[currentoffset].ToString();
                            Dictionaries.DialogueIDAndDialogue[i].text += "]";
                            currentoffset++;
                            break;
                        case 0x02:
                            Dictionaries.DialogueIDAndDialogue[i].text += "[Speed=";
                            currentoffset++;
                            Dictionaries.DialogueIDAndDialogue[i].text += rom.filebytes[currentoffset].ToString();
                            Dictionaries.DialogueIDAndDialogue[i].text += "]";
                            currentoffset++;
                            break;
                        case 0x03:
                            Dictionaries.DialogueIDAndDialogue[i].text += "[Emotion=";
                            currentoffset++;
                            switch (rom.filebytes[currentoffset])
                                {
                                case 0x00:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "Default";
                                    break;
                                case 0x01:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "Speak";
                                    break;
                                case 0x02:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "MouthOpen";
                                    break;
                                default:
                                    Dictionaries.DialogueIDAndDialogue[i].text += rom.filebytes[currentoffset].ToString();
                                    break;
                                }
                            Dictionaries.DialogueIDAndDialogue[i].text += "]";
                            currentoffset ++;
                            break;
                        case 0x04:
                            currentoffset++;
                            Dictionaries.DialogueIDAndDialogue[i].speaker = rom.filebytes[currentoffset];
                            Dictionaries.DialogueIDAndDialogue[i].NoSpeaker = false;
                            currentoffset++;
                            break;
                        case 0x05:
                            currentoffset ++;
                            switch (rom.filebytes[currentoffset])
                            {
                                case 0x00:
                                    Dictionaries.DialogueIDAndDialogue[i].blue = false;
                                    break;
                                case 0x01:
                                    Dictionaries.DialogueIDAndDialogue[i].blue = true;
                                    break;
                            }
                            currentoffset++;
                            break;

                        case 0xF0:
                            currentoffset++;

                            switch (rom.filebytes[currentoffset])
                                {
                                case 0x18:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "が";
                                    break;
                                case 0x40:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "っ";
                                    break;
                                case 0xB3:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "小";
                                    break;
                                case 0xBA:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "杖";
                                    break;
                                case 0xC2:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "水";
                                    break;
                                case 0xCC:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "石";
                                    break;
                                case 0x8B:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "香";
                                    break;
                                case 0x8F:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "黒";
                                    break;
                                case 0x94:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "剤";
                                    break;
                                case 0x95:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "殺";
                                    break;
                                case 0xE9:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "虫";
                                    break;
                                default:
                                    Dictionaries.DialogueIDAndDialogue[i].text += rom.filebytes[currentoffset] + " ";
                                    break;

                            }
                            currentoffset++;


                            break;

                        case 0xF1:
                            currentoffset++;

                            switch (rom.filebytes[currentoffset])
                            {
                                case 0x1C:
                                    Dictionaries.DialogueIDAndDialogue[i].text += "魔";
                                    break;
                                default:
                                    Dictionaries.DialogueIDAndDialogue[i].text += rom.filebytes[currentoffset] + "#";
                                    break;
                            }
                            break;

                        default:
                            Dictionaries.DialogueIDAndDialogue[i].text += GetLetter(rom.filebytes[currentoffset]);
                            currentoffset++;
                            break;
                        }
                    
                    }

                Dictionaries.DialogueIDAndDialogue[i].originalLength++; //to accommodate the 0xFF
            }

        }

        public string GetLetter(int b) {
            string output = "";

            switch (b) {
                case 0x0D:
                    output = "\n";
                    break;
                case 0x20:
                    output = "A";
                    break;
                case 0x21:
                    output = "B";
                    break;
                case 0x22:
                    output = "C";
                    break;
                case 0x23:
                    output = "D";
                    break;
                case 0x24:
                    output = "E";
                    break;
                case 0x25:
                    output = "F";
                    break;
                case 0x26:
                    output = "G";
                    break;
                case 0x27:
                    output = "H";
                    break;
                case 0x28:
                    output = "I";
                    break;
                case 0x29:
                    output = "J";
                    break;
                case 0x2A:
                    output = "K";
                    break;
                case 0x2B:
                    output = "L";
                    break;
                case 0x2C:
                    output = "M";
                    break;
                case 0x2D:
                    output = "N";
                    break;
                case 0x2E:
                    output = "O";
                    break;
                case 0x2F:
                    output = "P";
                    break;
                case 0x30:
                    output = "Q";
                    break;
                case 0x31:
                    output = "R";
                    break;
                case 0x32:
                    output = "S";
                    break;
                case 0x33:
                    output = "T";
                    break;
                case 0x34:
                    output = "U";
                    break;
                case 0x35:
                    output = "V";
                    break;
                case 0x36:
                    output = "W";
                    break;
                case 0x37:
                    output = "X";
                    break;
                case 0x38:
                    output = "Y";
                    break;
                case 0x39:
                    output = "Z";
                    break;
                case 0x3A:
                    output = "'d";
                    break;
                case 0x3B:
                    output = "'l";
                    break;
                case 0x3C:
                    output = "'m";
                    break;
                case 0x3D:
                    output = "'r";
                    break;
                case 0x3E:
                    output = "'s";
                    break;
                case 0x3F:
                    output = "'t";
                    break;

                case 0x40:
                    output = "a";
                    break;
                case 0x41:
                    output = "b";
                    break;
                case 0x42:
                    output = "c";
                    break;
                case 0x43:
                    output = "d";
                    break;
                case 0x44:
                    output = "e";
                    break;
                case 0x45:
                    output = "f";
                    break;
                case 0x46:
                    output = "g";
                    break;
                case 0x47:
                    output = "h";
                    break;
                case 0x48:
                    output = "i";
                    break;
                case 0x49:
                    output = "j";
                    break;
                case 0x4A:
                    output = "k";
                    break;
                case 0x4B:
                    output = "l";
                    break;
                case 0x4C:
                    output = "m";
                    break;
                case 0x4D:
                    output = "n";
                    break;
                case 0x4E:
                    output = "o";
                    break;
                case 0x4F:
                    output = "p";
                    break;
                case 0x50:
                    output = "q";
                    break;
                case 0x51:
                    output = "r";
                    break;
                case 0x52:
                    output = "s";
                    break;
                case 0x53:
                    output = "t";
                    break;
                case 0x54:
                    output = "u";
                    break;
                case 0x55:
                    output = "v";
                    break;
                case 0x56:
                    output = "w";
                    break;
                case 0x57:
                    output = "x";
                    break;
                case 0x58:
                    output = "y";
                    break;
                case 0x59:
                    output = "z";
                    break;
                case 0x5A:
                    output = "' ";
                    break;
                case 0x5B:
                    output = ", ";
                    break;
                case 0x5C:
                    output = ". ";
                    break;
                case 0x5D:
                    output = "'v";
                    break;
                case 0x5E:
                    output = "..";
                    break;
                case 0x5F:
                    output = " ";
                    break;
                case 0x60:
                    output = "0";
                    break;
                case 0x61:
                    output = "1";
                    break;
                case 0x62:
                    output = "2";
                    break;
                case 0x63:
                    output = "3";
                    break;
                case 0x64:
                    output = "4";
                    break;
                case 0x65:
                    output = "5";
                    break;
                case 0x66:
                    output = "6";
                    break;
                case 0x67:
                    output = "7";
                    break;
                case 0x68:
                    output = "8";
                    break;
                case 0x69:
                    output = "9";
                    break;

                case 0x73:
                    output = "?";
                    break;
                case 0x74:
                    output = "!";
                    break;
                case 0x86:
                    output = "ー";
                    break;
                case 0x8C:
                    output = "エ";
                    break;
                case 0x8F:
                    output = "キ";
                    break;
                case 0x98:
                    output = "タ";
                    break;
                case 0xA2:
                    output = "ハ";
                    break;
                case 0xA9:
                    output = "ム";
                    break;
                case 0xAF:
                    output = "ラ";
                    break;
                case 0xB3:
                    output = "ロ";
                    break;
                case 0xB6:
                    output = "ン";
                    break;
                case 0xBA:
                    output = "杖";
                    break;
                case 0xBD:
                    output = "ジ";
                    break;
                case 0xC8:
                    output = "ブ";
                    break;
                case 0xCD:
                    output = "プ";
                    break;
                case 0xDB:
                    output = "い";
                    break;
                case 0xE4:
                    output = "さ";
                    break;
                case 0xEE:
                    output = "な";
                    break;
                    
                case 0xE9:
                    output = "た";
                    break;
                case 0xED:
                    output = "と";
                    break;
             


                default:
                    output = b.ToString();
                    break;
            }

            return output;
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
            CurrentRoom.objects[(int)numericUpDown1.Value].Xpos = (ushort)ObjectXPos_box.Value;
        }

        private void ObjectYPos_box_ValueChanged(object sender, EventArgs e)
        {
            CurrentRoom.objects[(int)numericUpDown1.Value].Ypos = (ushort)ObjectYPos_box.Value;
        }

        private void ObjectUnk1_box_ValueChanged(object sender, EventArgs e)
        {
            CurrentRoom.objects[(int)numericUpDown1.Value].unk1 = (ushort)ObjectUnk1_box.Value;
        }

        private void ObjectCursorOffsetX_box_ValueChanged(object sender, EventArgs e)
        {
            CurrentRoom.objects[(int)numericUpDown1.Value].cursorXPosOffset = (ushort)ObjectCursorOffsetX_box.Value;
        }

        private void ObjectCursorOffsetY_box_ValueChanged(object sender, EventArgs e)
        {
            CurrentRoom.objects[(int)numericUpDown1.Value].cursorYPosOffset = (ushort)ObjectCursorOffsetY_box.Value;
        }

        private void ObjectUnk2_box_ValueChanged(object sender, EventArgs e)
        {
            CurrentRoom.objects[(int)numericUpDown1.Value].unk2 = (ushort)ObjectUnk2_box.Value;
        }

        private void ObjectPlayerPosX_box_ValueChanged(object sender, EventArgs e)
        {
            CurrentRoom.objects[(int)numericUpDown1.Value].playerXPos = (ushort)ObjectPlayerPosX_box.Value;
        }

        private void ObjectPlayerPosY_box_ValueChanged(object sender, EventArgs e)
        {
            CurrentRoom.objects[(int)numericUpDown1.Value].playerYPos = (ushort)ObjectPlayerPosY_box.Value;
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
            if (isTranslatedVersion)
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
                    LoadText();
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
