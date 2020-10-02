using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public class room
    {
        public string name;
        public ushort offset;

        public uint backgroundID;

        public ushort alsoAffectsBackground;
        public ushort paletteID;

        public Byte[] background_tilemap = new Byte[0];

        public int tilemap_height;
        public int tilemap_width;

        public uint objectListOffset;
        public uint objectListLastOffset;       //not sure if it's the beginning of the last object, or the end of it. Maybe it varies... although that would be weird.

        public int foregroundSpritePalettesOffset;

        public Palette[] foreground_palettes = new Palette[8];
        public Palette[] background_palettes = new Palette[8];

        public Bitmap background;

        public List<Object> objects = new List<Object>();

        //0x00 background ID
        //0x0C also affects background
        //0x0E palette ID?
        //0x1C object list offset
        //0x1F ? the same object list offset
        //0x22 ? offset to a particular object? or the start of the last one? or the absolute end of the list?
        //0x25 offset to script that determines what happens when you touch the sides of the room
        //0x28 something to do with animation of objects in the room (or is it just the door objects?)
        //0x2B Jennifer palette

        public void LoadRoomObjects() {

            uint currentOffset = objectListOffset;

            while (currentOffset <= objectListLastOffset)
                {
                currentOffset += LoadObject(currentOffset);
                }
        }

        public uint LoadObject(uint currentOffset) {

            Object newObject = new Object();

            newObject.offset = (int)currentOffset;
            newObject.parentRoom = this;

            newObject.unk1 = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
            currentOffset += 0x02;

            if (newObject.unk1 == 0x0200)
            {
                //special case when it's only 0x0B in length
                newObject.offsetOfInteractionScript = (uint)utility.GetPCOffset(rom.filebytes, (int)currentOffset);
                currentOffset += 0x04;
                currentOffset += 0x05;
            }
            else
            {
                newObject.Xpos = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
                currentOffset += 0x02;
                newObject.Ypos = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
                currentOffset += 0x02;
                newObject.cursorXPosOffset = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
                currentOffset += 0x02;
                newObject.cursorYPosOffset = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
                currentOffset += 0x02;
                newObject.unk2 = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
                currentOffset += 0x02;
                newObject.offsetOfInteractionScript = (uint)utility.GetPCOffset(rom.filebytes, (int)currentOffset);
                currentOffset += 0x04;
                newObject.playerXPos = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
                currentOffset += 0x02;
                newObject.playerYPos = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);
                currentOffset += 0x02;

                if (newObject.offsetOfInteractionScript > 0x003F0000)   //blagging this, I don't know what it actually does in these cases
                {
                    currentOffset -= 0x01;
                    newObject.offsetOfInteractionScript = newObject.offsetOfInteractionScript + rom.filebytes[(int)currentOffset];
                    currentOffset++;
                }
            }


            InteractionScript newInteractionScript = new InteractionScript();
            newInteractionScript.form1 = utility.form1;

            if (Dictionaries.ScriptOffsetsAndNames.Keys.Contains(newObject.offsetOfInteractionScript))
            {
                newInteractionScript.name = Dictionaries.ScriptOffsetsAndNames[newObject.offsetOfInteractionScript];
            }

            newObject.script = newInteractionScript;

            objects.Add(newObject);

            return currentOffset - (uint)newObject.offset;
        }



        public void LoadBackgroundPalette(int index)
        {
            uint currentOffset;

            background_palettes[0] = rom.BGPalette0;

            if (index != 0)
            {
                background_palettes = new Palette[8];
                background_palettes[0] = rom.BGPalette0;

                index = index << 3;

                index += BitConverter.ToUInt16(rom.filebytes, 0x20000);

                currentOffset = utility.ConvertToPCOffset((uint)(0xEF0000 + index));

                //this brings us to the address for a block of palettes to load into the background. Note that there is actually a value here that seems to be *how long* the block is (usually E0, for writing 7 of the eight palettes). But some of these are bigger than the 8 palettes for the BG. How can this be? Do they write to the sprite palette too?

                Console.WriteLine("Palette block info is at " + currentOffset);

                int lengthOfBlock = BitConverter.ToUInt16(rom.filebytes, (int)(currentOffset + 3)); //usually 0x00E0
                currentOffset = (uint)utility.GetPCOffset(rom.filebytes, (int)currentOffset);

                Console.WriteLine("Loading palette block at address " + currentOffset);

                for (int i = 0x00; i < (lengthOfBlock / 0x20); i++) //load the palettes at the address into the background palette array
                {
                    if (lengthOfBlock <= 0xE0)  //7 palettes, so just write to the ones after the global palette
                    {
                        background_palettes[i + 1] = imageTools.GetPaletteAtOffset(rom.filebytes, (int)currentOffset, true);
                        currentOffset += 0x20;
                    }
                    else if (lengthOfBlock <= 0x100)    //8 palettes, so assume we're meant to write to the global palette too.
                    {
                        background_palettes[i] = imageTools.GetPaletteAtOffset(rom.filebytes, (int)currentOffset, true);
                        currentOffset += 0x20;
                    }
                    else
                    {
                        Console.WriteLine("Wtf? The palette block is bigger than the background palette count. Maybe it's meant to write to the sprite palettes, too?");
                    }
                }
            }
        }




		public void LoadBackgroundTileGraphics(int compressedFileIndex)
		{

			Byte[] background_graphics = Decompressor.Decompress(compressedFileIndex).ToArray();

			ushort tileAttribute = 0;
			ushort tileID = 0;
			Byte PaletteID = 0;
			Palette palette = null;

			float xPos = 0;
			float yPos = 0;
			float zPos = 0;

			int tilemap_pos_X = 0;
			int tilemap_pos_Y = 0;

            background = new Bitmap(tilemap_width * 8, tilemap_height * 8);


            for (int i = 0x04; i < background_tilemap.Length; i += 4)
			{
				if (tilemap_pos_Y == 0 && tilemap_pos_X != 0)
				{
					i += (tilemap_height * 2);
				}

				if (i >= background_tilemap.Length)
				{
					break;
				}

				for (int j = 0; j < 4; j++)
				{
					int tileAdditionalOffset = 0;   //to get the top left, top right, bottom left, bottom right minitiles

					if (j == 0) //if top left tile
					{
						tileAdditionalOffset = 0;
					}
					else if (j == 1)    //if top right tile
					{
						tileAdditionalOffset = (tilemap_height * 2);    //*2 because each value is 16bits
					}
					else if (j == 2)    //if bottom left tile
					{
						tileAdditionalOffset = 2;
					}
					else if (j == 3)    //if bottom right tile
					{
						tileAdditionalOffset = (tilemap_height * 2) + 2;    //*2 because each value is 16bits
					}

					if ((i + tileAdditionalOffset) >= background_tilemap.Length)
					{
						break;
					}

					tileAttribute = BitConverter.ToUInt16(background_tilemap, i + tileAdditionalOffset);

					tileID = (ushort)(tileAttribute & 0x03FF);
					PaletteID = (byte)(((tileAttribute & 0x3C00) >> 10) & 0xFF);

					palette = background_palettes[PaletteID];

                    bool flipY = false;
                    bool flipX = false;
                    
					if ((tileAttribute & 0x8000) == 0x8000) { flipY = true;  }    //flipY (not yet implemented?)

					if ((tileAttribute & 0x4000) == 0x4000) { flipX = true; }    //flipX (not yet implemented?)

					Bitmap tile = imageTools.GetTileRaw(background_graphics, tileID, palette);

                    imageTools.WriteImageIntoBiggerImage(background, tile, tilemap_pos_X * 8, tilemap_pos_Y * 8, flipX, flipY);

                    if (j == 0) //if top left tile just completed
					{
						tilemap_pos_X++;
					}
					else if (j == 1)    //if top right tile just completed
					{
						tilemap_pos_X--;
						tilemap_pos_Y++;

						//in case we need to go to a new column early
						if (tilemap_pos_Y >= (tilemap_height))
						{
							tilemap_pos_X += 2;
							tilemap_pos_Y = 0;
						}

						if (tilemap_pos_Y == 0 && tilemap_pos_X != 0)
						{
							i += (tilemap_height * 2);
						}

						if (i >= background_tilemap.Length)
						{
							break;
						}
					}
					else if (j == 2)    //if bottom left tile just completed
					{
						tilemap_pos_X++;
					}
					else if (j == 3)    //if bottom right tile just completed
					{
						tilemap_pos_X--;
						tilemap_pos_Y++;
					}
				}

				if (tilemap_pos_Y >= (tilemap_height))
				{
					tilemap_pos_X += 2;
					tilemap_pos_Y = 0;
				}
			}
		}


        public void LoadBackgroundTileMap(int compressedFileIndex)
        {

            background_tilemap = Decompressor.Decompress(compressedFileIndex).ToArray();

            //C0/0037

            //get number of tiles wide (seems to overshoot by one?)
            tilemap_width = BitConverter.ToUInt16(background_tilemap, 0x02); //stored at 0x0DE7 + 0x0A in original game (for outside first rubble room, at least.) And also store to 0x0A36

            //get number of tiles high (seems to overshoot by one?)
            tilemap_height = BitConverter.ToUInt16(background_tilemap, 0x00);    //stored at 0x0DE7 + 0x0C in original game (for outside first rubble room, at least.) And also store to 0x0A38

            //maybe the tilemaps aren't just straight data, obviously they've got those four bytes at the start for height and width (see immediately above), but could there still be other things mixed in with the data. It gets loaded in column-of-two by column-of-two - is there any data between those columns?
           //File.WriteAllBytes(Application.streamingAssetsPath + "\\decompressed", background_tilemap);
        }



    }
}
