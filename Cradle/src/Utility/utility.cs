using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public static class utility
    {
		public static Form1 form1;

		public static uint ConvertToPCOffset(uint input)
		{   //converts an offset to a pc offset

			Byte[] snesOffsetAsBytes = BitConverter.GetBytes(input);

			if (snesOffsetAsBytes[2] >= 0x80 && snesOffsetAsBytes[2] < 0xC0)
			{
				snesOffsetAsBytes[2] -= 0x80;
			}
			else if (snesOffsetAsBytes[2] >= 0xC0)
			{
				snesOffsetAsBytes[2] -= 0xC0;
			}

			return BitConverter.ToUInt32(snesOffsetAsBytes, 0);
		}

		public static uint ConvertToSNESOffset(uint addr)
        {
            addr = 0xc00000 + (addr & 0x3fffff);
            return addr;
        }


		public static uint Read3Bytes(Byte[] bytes, int offset)
		{
			uint output = (bytes[offset + 2] * (uint)0x10000) + (bytes[offset + 1] * (uint)0x100) + (bytes[offset]);

			return output;
		}

		public static int GetPCOffset(Byte[] input, int pos)
		{   //reads three bytes and converts them to a PC offset

			Byte[] snesOffsetAsBytes = new Byte[4];

			Array.Copy(input, pos, snesOffsetAsBytes, 0, 3);

			if (snesOffsetAsBytes[2] >= 0x80 && snesOffsetAsBytes[2] < 0xC0)
			{
				snesOffsetAsBytes[2] -= 0x80;
			}
			else if (snesOffsetAsBytes[2] >= 0xC0)
			{
				snesOffsetAsBytes[2] -= 0xC0;
			}

			return BitConverter.ToInt32(snesOffsetAsBytes, 0);
		}


        public static void LoadRoom(int index)
        {
            ushort CurrentRoomID = rom.roomIDList[index];

            if (!Dictionaries.IDsAndRooms.Keys.Contains(CurrentRoomID))
            {
                room newRoom = new room();
                rom.CurrentRoom = newRoom;

                newRoom.offset = rom.roomIDList[index];
                newRoom.name = Dictionaries.RoomIDsAndNames[newRoom.offset];

                newRoom.objectListOffset = utility.ConvertToPCOffset(utility.Read3Bytes(rom.filebytes, newRoom.offset + 0x1C));
                newRoom.objectListLastOffset = utility.ConvertToPCOffset(utility.Read3Bytes(rom.filebytes, newRoom.offset + 0x22));

                int currentOffset = newRoom.offset = rom.roomIDList[index];

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

                Dictionaries.IDsAndRooms.Add(CurrentRoomID, newRoom);
            }
            else
            {
                rom.CurrentRoom = Dictionaries.IDsAndRooms[CurrentRoomID];
            }

            if (rom.isStandaloneCradle)
            {
                form1.pictureBox1.Image = rom.CurrentRoom.background;

                form1.RoomOffsetLabel.Text = "Offset (d): " + rom.roomIDList[form1.RoomListBox.SelectedIndex];

                form1.numericUpDown1.Maximum = rom.CurrentRoom.objects.Count - 1;
                form1.numericUpDown1.Minimum = 0;

                form1.numericUpDown1.Value = form1.numericUpDown1.Minimum;
                form1.UpdateObjectPanel();
            }
        }

        

    }
}
