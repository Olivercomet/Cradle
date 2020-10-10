using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public static class imageTools
    {



		public static Palette GetPaletteAtOffset(Byte[] input, int offset, bool is_background)
		{

			Palette output = new Palette();
			output.colors = new Color[16];


			for (int i = 1; i < 16; i++)
			{
				int r = (((input[offset] & 0x1F) << 3)); /// 248.00f;
				int g = (input[offset] & 0xE0) >> 2;
				g += (input[offset + 1] & 0x03) << 6;
				//g = ((float)g); /// 248.00f;
				int b = ((input[offset + 1] & 0x7C) << 1);   /// 248.00f);
				offset += 2;

				if (is_background)
				{
					output.colors[i - 1] = Color.FromArgb(0xFF, r, g, b);
				}
				else
				{
					output.colors[i] = Color.FromArgb(0xFF, r, g, b);
				}
			}

			if (is_background)
			{
				output.colors[0] = Color.Black;
				int r = (((input[offset] & 0x1F) << 3));
				int g = (input[offset] & 0xE0) >> 2;
				g += (input[offset + 1] & 0x03) << 6;
				int b = ((input[offset + 1] & 0x7C) << 1);
				output.colors[15] = Color.FromArgb(0xFF, r, g, b);
				offset += 2;
			}
			else
			{
				output.colors[0] = Color.FromArgb(0, 0, 0, 0);
			}

			return output;
		}




		public static Palette GetPaletteWithIndex(Byte[] input, int index)
		{

			Palette output = new Palette();
			output.colors = new Color[16];
			output.colors[0] = Color.FromArgb(0, 0, 0, 0);

			int offset = 0x01E7F2 + (0x20 * index);
			offset += 2; //skip over the 'first' colour, it's part of the prior palette.

			for (int i = 1; i < 16; i++)
			{
				int r = (((input[offset] & 0x1F) << 3));
				int g = (input[offset] & 0xE0) >> 2;
				g += (input[offset + 1] & 0x03) << 6;
				int b = ((input[offset + 1] & 0x7C) << 1);
				offset += 2;
				output.colors[i] = Color.FromArgb(0xFF,r,g,b);
			}


			return output;
		}




		public static Bitmap GetTileRaw(Byte[] input, int TileID, Palette palette)
		{   //get the 8x8 tile that begins at this offset in the byte array. THIS IS FOR BACKGROUNDS ETC, NOT SPRITES

			int localCurrentOffset = (0x20 * TileID);

			Bitmap tex = new Bitmap(8, 8);

			//now load the tile

			int x = 0; //leftmost x of the 8x8 tile
			int y = 0; //topmost y of the 8x8 tile

			for (int i = 0; i < 8; i++) //for each of the 8 lines in the minitile
			{
				for (int xPixel = 0; xPixel < 8; xPixel++)
					{
					tex.SetPixel(xPixel, y + i, GetColourFromPalette(Get4BPPSNESPixel(input, localCurrentOffset, (byte)xPixel), palette));
					}
				localCurrentOffset += 2;
			}

			return tex;
		}





		public static Bitmap GetTile(int TileID, Palette palette)
		{   //for sprites

			int currentOffset;

			int locationFinder = TileID;

			//because every other row of 16x16 tiles would otherwise be out of sync with the grid:

			while (locationFinder % 8 != 0)
			{
				locationFinder -= 1;
			}

			currentOffset = 0x050000 + (0x80 * locationFinder) + (0x40 * (TileID - locationFinder));

			Bitmap tex = new Bitmap(16, 16);

			//now load the tile. It is composed of four 8x8 mini tiles.

			int x = 0; //leftmost x of the 8x8 tile
			int y = 0; //topmost y of the 8x8 tile

			//TOP LEFT MINITILE
			for (int i = 0; i < 8; i++) //for each of the 8 lines in the minitile
			{
				tex.SetPixel(x, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)0), palette));
				tex.SetPixel(x + 1, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)1), palette));
				tex.SetPixel(x + 2, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)2), palette));
				tex.SetPixel(x + 3, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)3), palette));
				tex.SetPixel(x + 4, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)4), palette));
				tex.SetPixel(x + 5, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)5), palette));
				tex.SetPixel(x + 6, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)6), palette));
				tex.SetPixel(x + 7, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)7), palette));
				currentOffset += 2;
			}

			currentOffset += 0x10;

			x = 8;

			//TOP RIGHT MINITILE
			for (int i = 0; i < 8; i++) //for each of the 8 lines in the minitile
			{
				tex.SetPixel(x, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)0), palette));
				tex.SetPixel(x + 1, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)1), palette));
				tex.SetPixel(x + 2, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)2), palette));
				tex.SetPixel(x + 3, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)3), palette));
				tex.SetPixel(x + 4, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)4), palette));
				tex.SetPixel(x + 5, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)5), palette));
				tex.SetPixel(x + 6, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)6), palette));
				tex.SetPixel(x + 7, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)7), palette));
				currentOffset += 2;
			}

			currentOffset += 0x10;
			x = 0;
			y = 8;

			currentOffset += 0x1C0;
			//BOTTOM LEFT MINITILE
			for (int i = 0; i < 8; i++) //for each of the 8 lines in the minitile
			{
				tex.SetPixel(x, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)0), palette));
				tex.SetPixel(x + 1, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)1), palette));
				tex.SetPixel(x + 2, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)2), palette));
				tex.SetPixel(x + 3, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)3), palette));
				tex.SetPixel(x + 4, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)4), palette));
				tex.SetPixel(x + 5, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)5), palette));
				tex.SetPixel(x + 6, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)6), palette));
				tex.SetPixel(x + 7, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)7), palette));
				currentOffset += 2;
			}
			currentOffset += 0x10;

			x = 8;
			y = 8;

			//BOTTOM RIGHT MINITILE
			for (int i = 0; i < 8; i++) //for each of the 8 lines in the minitile
			{
				tex.SetPixel(x, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)0), palette));
				tex.SetPixel(x + 1, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)1), palette));
				tex.SetPixel(x + 2, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)2), palette));
				tex.SetPixel(x + 3, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)3), palette));
				tex.SetPixel(x + 4, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)4), palette));
				tex.SetPixel(x + 5, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)5), palette));
				tex.SetPixel(x + 6, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)6), palette));
				tex.SetPixel(x + 7, y + i, GetColourFromPalette(Get4BPPSNESPixel(rom.filebytes, currentOffset, (byte)7), palette));
				currentOffset += 2;
			}
			currentOffset += 0x10;

			return tex;
		}



		public static Color GetColourFromPalette(Byte index, Palette palette)
		{
			Color color = palette.colors[index];
			return color;
		}


		public static Byte Get4BPPSNESPixel(Byte[] input, int offset, int indexInLine)  //indexInLine is 0 to 7 which bit it should take from
		{
			byte pixel = 0;

			/*
			explanation of 4bpp format:

			the bits to construct the 4-bit nibble are actually spread over four bytes

			A B C D

			So it uses the first bit from D, the first bit from C, the first bit from B, then the first bit from A. And you have your value.

			However, it's not quite as simple as that because C and D are not next to A and B. They are located 0x0E after the end of B.


			By the way, this means that ABCD makes up an 8 pixel line.
			*/

			switch (indexInLine)
			{
				case 0:
					pixel += (byte)((input[offset + 0x11] & 0x80) >> 4);
					pixel += (byte)((input[offset + 0x10] & 0x80) >> 5);
					pixel += (byte)((input[offset + 0x01] & 0x80) >> 6);
					pixel += (byte)((input[offset + 0x00] & 0x80) >> 7);
					break;
				case 1:
					pixel += (byte)((input[offset + 0x11] & 0x40) >> 3);
					pixel += (byte)((input[offset + 0x10] & 0x40) >> 4);
					pixel += (byte)((input[offset + 0x01] & 0x40) >> 5);
					pixel += (byte)((input[offset + 0x00] & 0x40) >> 6);
					break;
				case 2:
					pixel += (byte)((input[offset + 0x11] & 0x20) >> 2);
					pixel += (byte)((input[offset + 0x10] & 0x20) >> 3);
					pixel += (byte)((input[offset + 0x01] & 0x20) >> 4);
					pixel += (byte)((input[offset + 0x00] & 0x20) >> 5);
					break;
				case 3:
					pixel += (byte)((input[offset + 0x11] & 0x10) >> 1);
					pixel += (byte)((input[offset + 0x10] & 0x10) >> 2);
					pixel += (byte)((input[offset + 0x01] & 0x10) >> 3);
					pixel += (byte)((input[offset + 0x00] & 0x10) >> 4);
					break;
				case 4:
					pixel += (byte)((input[offset + 0x11] & 0x08));
					pixel += (byte)((input[offset + 0x10] & 0x08) >> 1);
					pixel += (byte)((input[offset + 0x01] & 0x08) >> 2);
					pixel += (byte)((input[offset + 0x00] & 0x08) >> 3);
					break;
				case 5:
					pixel += (byte)((input[offset + 0x11] & 0x04) << 1);
					pixel += (byte)((input[offset + 0x10] & 0x04));
					pixel += (byte)((input[offset + 0x01] & 0x04) >> 1);
					pixel += (byte)((input[offset + 0x00] & 0x04) >> 2);
					break;
				case 6:
					pixel += (byte)((input[offset + 0x11] & 0x02) << 2);
					pixel += (byte)((input[offset + 0x10] & 0x02) << 1);
					pixel += (byte)((input[offset + 0x01] & 0x02));
					pixel += (byte)((input[offset + 0x00] & 0x02) >> 1);
					break;
				case 7:
					pixel += (byte)((input[offset + 0x11] & 0x01) << 3);
					pixel += (byte)((input[offset + 0x10] & 0x01) << 2);
					pixel += (byte)((input[offset + 0x01] & 0x01) << 1);
					pixel += (byte)((input[offset + 0x00] & 0x01));
					break;
			}

			return pixel;
		}



		public static void WriteImageIntoBiggerImage(Bitmap BigImage, Bitmap SmallImage, int xpos, int ypos, bool flipX, bool flipY)
			{
			for (int y = 0; y < SmallImage.Height; y++)
				{
				for (int x = 0; x < SmallImage.Width; x++)
					{
					int destX = xpos + x;
					int destY = ypos + y;

					if (flipX)
						{
						destX = xpos + ((SmallImage.Width - 1) - x);
						}

					if (flipY)
						{
						destY = ypos + ((SmallImage.Height - 1) - y);
						}

					BigImage.SetPixel(destX, destY, SmallImage.GetPixel(x,y));	
					}
				}
			}



		public static byte[] BackgroundToIntermediateFormat(room r)	//not dead code! This is used by external projects to get an RGBA32 image byte array instead of a Bitmap (in case they can't use System.Drawing)
		{

			byte[] output = new byte[r.background.Width * r.background.Height * 4];

			int pos = 0;

			for (int y = 0; y < r.background.Height; y++)
			{
				for (int x = 0; x < r.background.Width; x++)
				{
					output[pos] = r.background.GetPixel(x, y).R;
					pos++;
					output[pos] = r.background.GetPixel(x, y).G;
					pos++;
					output[pos] = r.background.GetPixel(x, y).B;
					pos++;
					output[pos] = r.background.GetPixel(x, y).A;
					pos++;
				}
			}

			return output;
		}
	}
}
