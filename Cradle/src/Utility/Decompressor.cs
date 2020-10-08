using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public static class Decompressor
	{


		static ushort value70;

		static ushort value73;

		static ushort value80;

		static ushort ORvalue74;

		static ushort header76;

		static ushort value78;

		static ushort value7A;

		static ushort value7C;

		static ushort number_to_copy;


		static List<Byte> decompressedBytes = new List<Byte>();


		static bool cFlag;      //for ROL and ROR only


		public static ushort ROL(ushort input)  //rotate left... hopefully
		{
			ushort highbit = (ushort)(input & (ushort)0x8000);

			input = (ushort)((input << 1) & 0x0000FFFF);

			if (cFlag)
			{
				input = (ushort)(input | 0x0001);
			}

			if (highbit != 0)
			{
				cFlag = true;
			}
			else
			{
				cFlag = false;
			}

			return input;
		}

		public static ushort ROR(ushort input)  //rotate right... hopefully
		{

			ushort lowbit = (ushort)(input & (ushort)0x0001);

			input = (ushort)(input >> 1);

			if (cFlag)
			{
				input = (ushort)(input | 0x8000);
			}

			if (lowbit != 0)
			{
				cFlag = true;
			}
			else
			{
				cFlag = false;
			}

			return input;
		}

		public static ushort SwapShortBytes(ushort input)
		{
			ushort temp = (ushort)((input << 8) & 0x0000FFFF);
			input = (ushort)(input >> 8);
			input += temp;
			return input;
		}



		public static bool start_logging = false;

		public static uint currentOffset;


		public static List<Byte> Decompress(int compressedFileIndex)
		{
			//get address from table

			compressedFileIndex = compressedFileIndex << 3;

			compressedFileIndex += BitConverter.ToUInt16(rom.filebytes, 0x20000);    //add a predetermined value to get the address of the table entry for this compressed file

			currentOffset = utility.ConvertToPCOffset((uint)(0xEF0000 + compressedFileIndex));  //go to the table entry

			int uncompressedSize = BitConverter.ToUInt16(rom.filebytes, (int)currentOffset + 3);    //and gets stored at 0x60 in the original

			Byte compressionInfo = rom.filebytes[currentOffset + 7];

			currentOffset = (uint)(utility.GetPCOffset(rom.filebytes, (int)currentOffset));    //go to start of compressed data

			if ((compressionInfo >> 7) == 0x01) //then it's not actually compressed?
			{
				List<Byte> output = new List<Byte>();

				for (int i = 0; i < uncompressedSize; i++)
				{
					output.Add(rom.filebytes[currentOffset + i]);
				}

				return output;
			}


			//prepare for decompression

			//$81/81E2

			value70 = 0x7E;

			value73 = 0xEE;

			ORvalue74 = 0x8157; //how does it get this?

			header76 = 0xFFFF; // this one seems to begin like this

			value78 = 0x0000;

			value7A = 0x0020;

			value7C = 0x04;     //how many bytes to seek backwards

			value80 = 0x0000;

			number_to_copy = 0;

			decompressedBytes = new List<Byte>();

			//$81/8204 
			Function_8182C6();  //this function runs first, reads 16-bit header etc

			//$81/8207

			//$81/820B
			while (decompressedBytes.Count < uncompressedSize)
			{
				Byte commandByte = (Byte)rom.filebytes[currentOffset];    //read the next command byte
				currentOffset++;
				//Console.WriteLine("setting command byte to " + commandByte);

				for (int i = 7; i >= 0; i--)
				{
					//$81/821E

					//81/8220



					if (commandByte < 0x80) //if the command byte goes below 0x80 when it was above it before, at any point, stop transferring bytes to the output. 
					{
						//then go into 'copy from output' mode, at 81/8238

						//Console.WriteLine("hit a command point, most recent normal byte was "+decompressedBytes[decompressedBytes.Count - 1] + " and pos is "+currentOffset);


						ushort temp74 = ORvalue74;

						if (temp74 < 0x8000)    //if the first bit in ORvalue74 wasn't 1
						{
							temp74 = (ushort)((temp74 >> 1) & 0x0000FFFF);

							temp74 = SwapShortBytes(temp74);            //		ABBB BBB is at the end




							if ((temp74 & 0x00FF) == 0)
							{
								//branch to 81/8251
								temp74 = SwapShortBytes(temp74);
								temp74 = (ushort)(temp74 >> 1);
								temp74 = (ushort)(temp74 | 0x0080);


								if (temp74 == 0x00FF)
								{
									//RTL away to somewhere else, but perhaps come back later. This RTL is at $81/825E. It's possible that it signifies the end of the decompression
									Console.WriteLine("The mystery RTL was triggered");
									break;
								}

								number_to_copy = temp74;
							}
							else if ((temp74 & 0x00FF) < 8)
							{
								//$81/8248

								number_to_copy = SwapShortBytes(temp74);

								switch (temp74 & 0x00FF)    //it uses temp74 (the Y register) to find a value for X in the JSR, so effectively we can do it with just Y
								{
									case 0x0001:    //goes to instruction at 81/83AC 
										number_to_copy = (ushort)(number_to_copy >> 2);
										break;
									case 0x0002:    //goes to instruction at 81/83AA
									case 0x0003:
										number_to_copy = (ushort)(number_to_copy >> 4);
										break;
									case 0x0004:    //goes to instruction at 81/83A8 
									case 0x0005:
									case 0x0006:
									case 0x0007:
										number_to_copy = (ushort)(number_to_copy >> 6);
										break;
									default:
										Console.WriteLine("Unknown value in the switch statement near //$81/8248: " + temp74);
										break;
								}

								number_to_copy = (ushort)(number_to_copy & 0x00FF);
							}
							else
							{
								//branch to $81/825F 

								number_to_copy = rom.filebytes[0x0183CD + (temp74 & 0x00FF)];
							}

							//81/8263

							value7A = rom.filebytes[0x01840D + ((SwapShortBytes((ushort)(ORvalue74 >> 1))) & 0x00FF)];


							Function_8182C6();


						}
						else
						{
							//81/826C	

							number_to_copy = 1;
						}

						//picks up again at 81/826F with the 0x6000 stuff


						temp74 = (ushort)((ORvalue74 << 1) & 0x0000FFFF);   //in first D344 thing, should be 0x0020


						value7C = 0;





						if (temp74 < 0x6000)
						{
							//$81/8275



							temp74 = (ushort)((temp74 << 1) & 0x0000FFFF);
							temp74 = SwapShortBytes(temp74);

							value7C = rom.filebytes[0x018454 + (temp74 & 0x00FF)];

							temp74 = (ushort)(((temp74 & 0x00FF) >> 4) & 0x0000FFFF);

							value7A = rom.filebytes[0x018514 + (temp74 & 0x00FF)];
						}
						else
						{
							//81/8287

							if ((temp74 & 0x8000) == 0x8000) { cFlag = true; } else { cFlag = false; }  //the C flag is set by the ASL  (yes, it's meant to be before it)
							temp74 = (ushort)((temp74 << 1) & 0x0000FFFF);
							temp74 = ROL(temp74);
							temp74 = ROL(temp74);
							temp74 = ROL(temp74);
							temp74 = (ushort)(temp74 & 0x0007);

							value7C = (ushort)((ORvalue74 & 0x0FFF) | 0x1000);


							value7C = SwitchFunction2(rom.filebytes[0x01851D + temp74], value7C);

							/*
							switch (temp74)		
								{
								case 0x03:	//goes to instruction at 81/83A9
									value7C = (ushort)(value7C >> 5);
									break;
								case 0x04:	//goes to instruction at 81/83AA
									value7C = (ushort)(value7C >> 4);
									break;
								case 0x05:	//goes to instruction at 81/83AB
									value7C = (ushort)(value7C >> 3);
									break;	
								case 0x06:	//goes to instruction at 81/83AC
									value7C = (ushort)(value7C >> 2);
									break;	
								default:
									Console.WriteLine("The value "+temp74+"was not included in the switch statement at 81/829A!");
									break;
								}*/

							value7A = rom.filebytes[0x018522 + (temp74 & 0x00FF)];
						}

						//81/82A0

						Function_8182C6();

						int pos_to_copy_from = (decompressedBytes.Count - value7C) - 1;

						for (int j = 0; j < number_to_copy + 1; j++)
						{
							decompressedBytes.Add(decompressedBytes[pos_to_copy_from + j]);
						}
					}
					else
					{
						//81/8222
						decompressedBytes.Add(rom.filebytes[currentOffset]);  //add the next byte to the output
						currentOffset++;


						if (currentOffset == 0)
						{
							//81/822A
							currentOffset += 0x0100;
						}

						//81/8230

						//there's a branch if the storing address is not equal to zero... well, we don't quite have that luxury because I was just adding them to a list...

						//let's assume it isn't equal to zero and put in a special case if there's a problem later on

					}

					commandByte = (byte)(commandByte << 1);
				}
			}

			return decompressedBytes;
		}



		public static ushort SwitchFunction(ushort input1, ushort input2_for_output)
		{
			if (input1 > 0x20)
			{
				Console.WriteLine("??? unknown value " + input1 + " being tested near 81/82C6");
			}
			else if (input1 == 0x20)
			{
				input2_for_output = 0x0000;
			}
			else if (input1 == 0x10)
			{
				input2_for_output = SwapShortBytes(input2_for_output);
				input2_for_output = (ushort)(input2_for_output & 0xFF00);
			}
			else if (input1 < 0x1A)
			{
				input2_for_output = (ushort)((input2_for_output << ((input1 & 0x0F) / 2)) & 0x0000FFFF);

				if (input1 >> 4 == 0x01)
				{
					input2_for_output = SwapShortBytes(input2_for_output);
					input2_for_output = (ushort)(input2_for_output & 0xFF00);
				}
			}
			else
			{   //and then these are the ones that I couldn't simplify very well
				switch (input1)
				{
					case 0x1A:  //goes to instruction at 81/834E
						input2_for_output = (ushort)(input2_for_output & 0x0007);
						if ((input2_for_output & 0x0001) == 0x0001) { cFlag = true; } else { cFlag = false; }   //the C flag is set by the LSR  (yes, it's meant to be before it)
						input2_for_output = (ushort)(input2_for_output >> 1);
						input2_for_output = ROR(input2_for_output);
						input2_for_output = ROR(input2_for_output);
						input2_for_output = ROR(input2_for_output);
						break;

					case 0x1C:  //goes to instruction at 81/8347
						input2_for_output = (ushort)(input2_for_output & 0x0003);
						if ((input2_for_output & 0x0001) == 0x0001) { cFlag = true; } else { cFlag = false; }   //the C flag is set by the LSR  (yes, it's meant to be before it)
						input2_for_output = (ushort)(input2_for_output >> 1);
						input2_for_output = ROR(input2_for_output);
						input2_for_output = ROR(input2_for_output);
						break;

					case 0x1E:  //goes to instruction at 81/8341?
						input2_for_output = (ushort)(input2_for_output & 0x0001);
						if ((input2_for_output & 0x0001) == 0x0001) { cFlag = true; } else { cFlag = false; }   //the C flag is set by the LSR  (yes, it's meant to be before it)
						input2_for_output = (ushort)(input2_for_output >> 1);
						input2_for_output = ROR(input2_for_output);
						break;

					default:
						Console.WriteLine("unknown switch case at $81/82C6");
						break;
				}
			}
			return (ushort)input2_for_output;
		}



		public static void Function_8182C6()
		{
			//$81/82C6

			//here's the switch statement but simplified a bit



			ORvalue74 = SwitchFunction(value7A, ORvalue74);



			//$81/82CF

			if (value78 < value7A)
			{

				value7A = (ushort)(value7A - value78);

				ushort tempval = (ushort)(header76 & BitConverter.ToUInt16(rom.filebytes, 0x0183B3 + value78));

				tempval = SwitchFunction(value7A, tempval);

				ORvalue74 = (ushort)(ORvalue74 | tempval);  //TSB


				//$81/82E8

				header76 = (ushort)BitConverter.ToUInt16(rom.filebytes, (int)currentOffset);    //read 16-bit compression info (initial value 0x4084 for first rubble room tilemap)


				currentOffset += 0x02;

				if ((currentOffset & 0x0000FFFF) < 2)
				{
					//do something at 81/82F7, it rarely seems to trigger though, and if you force it by changing the registers, it seems to result in a crash
					Console.WriteLine("The mysterious 81/82F7 function was trigged by the decompression algorithm, but it's unhandled here so nothing happens");
				}

				value78 = 0x20;
			}

			//$81/830A

			value78 = (ushort)(value78 - value7A);


			//$81/8312

			ushort tempvalue = header76;

			tempvalue = SwitchFunction2(value78, tempvalue);


			//$81/8315


			tempvalue = (ushort)(tempvalue & BitConverter.ToUInt16(rom.filebytes, 0x0183B3 + value7A));


			ORvalue74 = (ushort)(ORvalue74 | tempvalue);



		}







		public static ushort SwitchFunction2(ushort input, ushort input2_for_output)
		{
			switch (input)
			{
				case 0x00:      //goes to the instruction at $81/83AE
								//don't do anything
					break;
				case 0x02:
					input2_for_output = (ushort)(input2_for_output >> 1);
					break;
				case 0x04:      //goes to the instruction at $81/83AC
					input2_for_output = (ushort)(input2_for_output >> 2);
					break;
				case 0x06:
					input2_for_output = (ushort)(input2_for_output >> 3);
					break;
				case 0x08:
					input2_for_output = (ushort)(input2_for_output >> 4);
					break;
				case 0x0A:
					input2_for_output = (ushort)(input2_for_output >> 5);
					break;
				case 0x0C:      //goes to the instruction at 81/83A8
					input2_for_output = (ushort)(input2_for_output >> 6);
					break;
				case 0x0E:      //goes to the instruction at 81/83A7
					input2_for_output = (ushort)(input2_for_output >> 7);
					break;
				case 0x10:      //goes to the instruction at 81/83A2
					input2_for_output = SwapShortBytes(input2_for_output);
					input2_for_output = (ushort)(input2_for_output & 0x00FF);
					break;
				case 0x12:      //goes to the instruction at 81/83A1
					input2_for_output = (ushort)(input2_for_output >> 1);
					input2_for_output = SwapShortBytes(input2_for_output);
					input2_for_output = (ushort)(input2_for_output & 0x00FF);
					break;
				case 0x14:      //goes to the instruction at 81/83A0
					input2_for_output = (ushort)(input2_for_output >> 2);
					input2_for_output = SwapShortBytes(input2_for_output);
					input2_for_output = (ushort)(input2_for_output & 0x00FF);
					break;
				case 0x16:      //goes to the instruction at 81/839F
					input2_for_output = (ushort)(input2_for_output >> 3);
					input2_for_output = SwapShortBytes(input2_for_output);
					input2_for_output = (ushort)(input2_for_output & 0x00FF);
					break;
				case 0x18:      //goes to the instruction at 81/839E
					input2_for_output = (ushort)(input2_for_output >> 4);
					input2_for_output = SwapShortBytes(input2_for_output);
					input2_for_output = (ushort)(input2_for_output & 0x00FF);
					break;
				case 0x1A:      //goes to the instruction at 81/8396
					input2_for_output = (ushort)(input2_for_output & 0xE000);
					if ((input2_for_output & 0x8000) == 0x8000) { cFlag = true; } else { cFlag = false; }   //the C flag is set by the ASL (yes, it's meant to be before it)
					input2_for_output = (ushort)(input2_for_output << 1);
					input2_for_output = ROL(input2_for_output);
					input2_for_output = ROL(input2_for_output);
					input2_for_output = ROL(input2_for_output);
					break;
				case 0x1C:      //goes to the instruction at 81/838F
					input2_for_output = (ushort)(input2_for_output & 0xC000);
					if ((input2_for_output & 0x8000) == 0x8000) { cFlag = true; } else { cFlag = false; }   //the C flag is set by the ASL (yes, it's meant to be before it)
					input2_for_output = (ushort)(input2_for_output << 1);
					input2_for_output = ROL(input2_for_output);
					input2_for_output = ROL(input2_for_output);
					break;
				case 0x1E:      //goes to the instruction at 81/8389
					input2_for_output = (ushort)(input2_for_output & 0x8000);
					if ((input2_for_output & 0x8000) == 0x8000) { cFlag = true; } else { cFlag = false; }   //the C flag is set by the ASL (yes, it's meant to be before it)
					input2_for_output = (ushort)(input2_for_output << 1);
					input2_for_output = ROL(input2_for_output);
					break;
				case 0x20:      //goes to the instruction at 81/83AF
					input2_for_output = 0x0000;
					break;
				default:
					Console.WriteLine("unknown value in switchfunction2");
					break;
			}

			return input2_for_output;

		}
	}
}
