using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
	public class Object
	{
		public int offset;

		public room parentRoom;

		public ushort unk1;

		public ushort Xpos;
		public ushort Ypos;
		public ushort cursorXPosOffset;
		public ushort cursorYPosOffset;
		public ushort unk2;
		public uint offsetOfInteractionScript;
		public ushort playerXPos;
		public ushort playerYPos;

		public InteractionScript script;

		public Bitmap spriteImage;

		Palette palette = new Palette();

		public class AnimFrame
		{
			public int SpriteID;
			public int duration;
			public sbyte XPositionChange;
			public sbyte YPositionChange;
			public Byte flags;
		}

		public int currentAnimationIndex = 999999; //index of the animation currently playing

		int currentAnimRandomID = 0;

		int NextAnimationIndex = 999999;
		bool NextAnimationLoop = false;

		public bool cooldown = false;

		public List<AnimFrame> animationFrames = new List<AnimFrame>();

		public string SpecialSprite = ""; //scissorman, jennifer - will make it take the palette from the correct room index




		public void LoadAnimation(int animationIndex, bool loop)
		{
			int currentOffset = 0;

			if (currentAnimationIndex == animationIndex)
			{
				return;
			}

			if (NextAnimationIndex == animationIndex)
			{
				return;
			}


			switch (SpecialSprite)
			{
				case "scissorman":
					palette = parentRoom.foreground_palettes[6];
					break;
				case "jennifer":
					palette = parentRoom.foreground_palettes[7];
					break;
			}

			currentAnimationIndex = animationIndex;

			currentAnimRandomID = 0;

			currentOffset = 0x030000 + (animationIndex * 2);

			if (animationIndex > 0x024D)
			{
				currentOffset = 0x031000 + BitConverter.ToUInt16(rom.filebytes, currentOffset);
			}
			else
			{
				currentOffset = 0x030000 + BitConverter.ToUInt16(rom.filebytes, currentOffset);
			}

			animationFrames = new List<AnimFrame>();


			while (BitConverter.ToUInt16(rom.filebytes, currentOffset) != 0xFFFF)
			{
				AnimFrame newAnimFrame = new AnimFrame();

				newAnimFrame.SpriteID = BitConverter.ToUInt16(rom.filebytes, currentOffset);
				currentOffset += 0x02;

				newAnimFrame.duration = rom.filebytes[currentOffset];
				currentOffset++;

				newAnimFrame.XPositionChange = (sbyte)rom.filebytes[currentOffset];
				currentOffset++;

				newAnimFrame.YPositionChange = (sbyte)rom.filebytes[currentOffset];
				currentOffset++;

				Console.WriteLine("flags for this frame: " + rom.filebytes[currentOffset]);
				newAnimFrame.flags = rom.filebytes[currentOffset];
				currentOffset++;



				//flag information:

				//the first bit is whether or not to flip vertically on this anim frame
				//the second bit is whether or not to flip horizontally on this anim frame
				//the third bit in the flag is ???
				//the fourth bit in the flag is the layer the sprite is on

				//the fifth, sixth and seventh bits in the flag are palette related

				//it seems to be an index to one of the currently loaded palettes in SNES memory... They are viewable in No$sns. They are loaded in when the room is loaded, and are located in the bytes just prior to the room address

				//THE FOLLOWING BIT ABOUT PALETTE WILL PROBABLY NEED TO BE REDONE - I THINK IT TAKES THE PALETTE INDEX FROM THE OBJECT DEFINITION RATHER THAN THE ANIMATION 

				//newAnimFrame.palette = parentRoom.foreground_palettes[(newAnimFrame.flags & 0x0F) + 2];
				//Console.WriteLine("request room palette with index "+ ((newAnimFrame.flags & 0x0F) + 2));
				//Console.WriteLine(newAnimFrame.palette.colors[1].r);
				//newAnimFrame.palette.colors = RomLoader.LoadPalette(rom.filebytes, 0x1E77C + (newAnimFrame.flags * 30));	//wrong
				//Console.WriteLine("loaded palette from offset "+ (0x1E77C + (newAnimFrame.flags * 30)));		//wrong

				if (newAnimFrame.SpriteID < 0x02 && animationFrames.Count > 0)  //replacing those weird sprites that cut back to early ones (may need to increase this threshold)
				{
					//	newAnimFrame.SpriteID = animationFrames[animationFrames.Count-1].SpriteID;
					//	newAnimFrame.XPositionChange = animationFrames[animationFrames.Count-1].XPositionChange;
					//	newAnimFrame.YPositionChange = animationFrames[animationFrames.Count-1].YPositionChange;
					//newAnimFrame.duration = animationFrames[animationFrames.Count-1].duration;
				}
				else
				{
					animationFrames.Add(newAnimFrame);
				}

				Console.WriteLine("SpriteID: " + newAnimFrame.SpriteID);
			}


			Console.WriteLine("framecount in this animation: " + animationFrames.Count);

			PlayLoadedAnimation(animationIndex, loop);
		}


		public class TileInfo {
			public Bitmap image;
			public byte TileFlags;
			public int TileID;
			public sbyte TileXPos;
			public sbyte TileYPos;
		}


		public void DisplaySprite(int spriteIndex, int extraInfo)
		{   //display a sprite on this object

			List<TileInfo> tileInfo = new List<TileInfo>();

			//go the the list of sprites and find this one

			int currentOffset = 0x220000 + (spriteIndex * 2);

			if (spriteIndex > 0x2311)
			{
				currentOffset = 0x240000 + BitConverter.ToUInt16(rom.filebytes, currentOffset);
			}
			else if (spriteIndex > 0x0F61)
			{
				currentOffset = 0x230000 + BitConverter.ToUInt16(rom.filebytes, currentOffset);
			}
			else
			{
				currentOffset = 0x220000 + BitConverter.ToUInt16(rom.filebytes, currentOffset);
			}

			List<TileInfo> tileInfos = new List<TileInfo>();

			while (BitConverter.ToUInt16(rom.filebytes, currentOffset) != 0xFFFF)  //read info for each tile until we hit 0XFFFF, which is the end of the sprite
			{
				TileInfo newTileInfo = new TileInfo();

				newTileInfo.TileFlags = rom.filebytes[currentOffset];
				currentOffset++;

				int TileID = BitConverter.ToUInt16(rom.filebytes, currentOffset);
				if (TileID >= 0xFFF0)
				{
					Console.WriteLine("VRAM shadow tile requested!");
				}
				currentOffset += 0x02;
				newTileInfo.TileYPos = (sbyte)rom.filebytes[currentOffset];
				currentOffset++;
				newTileInfo.TileXPos = (sbyte)(rom.filebytes[currentOffset]);//); //it's +8 so that the tiles are spawned relative to the centre of the sprite... I hope
				currentOffset++;


				//btw, the shader will need to use Cutout for the shadows to draw properly (and not just be shadows of the entire quad)


				//now zoom over to the tile section and get the tile data, then make that into a texture and put it on the sprite


				if (TileID < 0xFFF0) //if not a VRAM shadow tile, load the tile. (Will need to add a special case for the shadow tiles though.)
				{
					newTileInfo.image = imageTools.GetTile(TileID, palette);
				}

				tileInfos.Add(newTileInfo);

				//and the while loop will keep looping until 0xFFFF is found - i.e. the end of the sprite.
			}

			short minX = 0;
			short maxX = 0;

			short minY = 0;
			short maxY = 0;

			foreach(TileInfo t in tileInfos)
				{
				if (t.TileXPos > maxX)
					{
					maxX = t.TileXPos;
					}
				if (t.TileXPos < minX)
					{
					minX = t.TileXPos;
					}

				if (t.TileYPos > maxY)
					{
					maxY = t.TileYPos;
					}
				if (t.TileYPos < minY)
					{
					minY = t.TileYPos;
					}
				}

			spriteImage = new Bitmap(Math.Abs(maxX - minX) + 16, Math.Abs(maxY - minY) + 16);

			foreach (TileInfo t in tileInfos)
				{
				if (t.TileID < 0xFFF0)	//if not a VRAM shadow tile
					{
					if (t.image != null)
						{
						imageTools.WriteImageIntoBiggerImage(spriteImage, t.image, t.TileXPos + Math.Abs(minX), t.TileYPos + Math.Abs(minY), false, false);
						}
					}
				}

			spriteImage.Save("test.png"); //TEMP
		}


		public void PlayLoadedAnimation(int animationIndex, bool loop)
		{
			Random rnd = new Random();
			int thisAnimRandomID = rnd.Next(0, 9999999);
			currentAnimRandomID = thisAnimRandomID;


			//remember that the animation must have been LOADED with LoadAnimation() before using this!

			for (int i = 0; i < animationFrames.Count; i++)
			{
				if (currentAnimRandomID != thisAnimRandomID)    //if this animation is no longer the current animation for this object, stop
				{
				//	yield break;
				}

				Console.WriteLine("anim frame :" + i);

				DisplaySprite(animationFrames[i].SpriteID, i);



				//change sprite flipping

				if ((animationFrames[i].flags & 0x80) == 0x80)
				{
				//flip y
				}

				if ((animationFrames[i].flags & 0x40) == 0x40 && !(currentAnimationIndex == 0x05))
				{
				//flip x
				}

				//change sprite location

				/*
				if (this.transform.localScale.x < 0)
				{
					this.transform.Translate(new Vector3(-animationFrames[i].XPositionChange / 4.0f, -animationFrames[i].YPositionChange / 4.0f, 0));
				}
				else
				{
					this.transform.Translate(new Vector3(animationFrames[i].XPositionChange / 4.0f, -animationFrames[i].YPositionChange / 4.0f, 0));
				}*/



				//yield return new WaitForSeconds(((float)animationFrames[i].duration) / 60.000f);

			}

			if (currentAnimRandomID == thisAnimRandomID && loop && animationFrames.Count > 1)
			{
				//StartCoroutine(PlayLoadedAnimation(animationIndex, loop));  //loop
			}
			else if (animationFrames.Count > 1)
			{
				currentAnimationIndex = 999999;
				cooldown = false;

				if (NextAnimationIndex != 999999)
				{
					int temp1 = NextAnimationIndex; //this weirdness is done because the NextAnimationIndex needs to be reset before the function is called
					NextAnimationIndex = 999999;

					LoadAnimation(temp1, NextAnimationLoop);
				}
			}
		}


	}
}
