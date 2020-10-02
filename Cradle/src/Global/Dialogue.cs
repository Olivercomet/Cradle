using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public class Dialogue
    {
        public int offset; //beware!!! this is actually measured from the start of the dialogue offset table

        public int speaker;
        public string text;

        public bool blue;

        public bool NoSpeaker;

        public int originalLength = 0;

        public List<Byte> ReconstructBytes() {
            List<Byte> output = new List<Byte>();

            int currentPlaceInText = 0;
            bool addedSpeaker = false;

            while (currentPlaceInText < text.Length)
                {
                if (text[currentPlaceInText] == "["[0])
                {
                    //it's a command

                    currentPlaceInText++;

                    switch (text[currentPlaceInText].ToString() + text[currentPlaceInText + 1].ToString())
                    {
                        case "Wa":
                            //wait
                            int WaitTime = 0;
                            currentPlaceInText += 5;
                            if (text[currentPlaceInText + 1] + "" == "]")
                            {
                                WaitTime = text[currentPlaceInText];
                            }
                            else if (text[currentPlaceInText + 2] + "" == "]")
                            {
                                WaitTime = (int.Parse(text[currentPlaceInText] + "") * 10) + int.Parse(text[currentPlaceInText] + "");
                            }
                            else if (text[currentPlaceInText + 3] + "" == "]")
                            {
                                WaitTime = (int.Parse(text[currentPlaceInText] + "") * 100) + (int.Parse(text[currentPlaceInText] + "") * 10) + int.Parse(text[currentPlaceInText] + "");
                            }
                            output.Add(0x01);
                            output.Add((byte)WaitTime);

                            while (text[currentPlaceInText] + "" != "]")
                            {
                                currentPlaceInText++;
                            }
                            currentPlaceInText++;
                            break;
                        case "Sp":
                            //speed
                            string SpeedString = "";
                            currentPlaceInText += 6;
                            
                            while (text[currentPlaceInText] + "" != "]")
                            {
                                SpeedString += text[currentPlaceInText] + "";
                                currentPlaceInText++;
                            }
                            currentPlaceInText++;

                            output.Add(0x02);
                            output.Add((byte)(int.Parse(SpeedString)));
                            break;
                        case "Em":
                            //emotion
                            string EmotionString = "";
                            currentPlaceInText += 8;
                            while (text[currentPlaceInText] + "" != "]")
                            {
                                EmotionString += text[currentPlaceInText] + "";
                                currentPlaceInText++;
                            }
                            currentPlaceInText++;

                            output.Add(0x03);
                            switch (EmotionString)
                                {
                                case "Default":
                                    output.Add(0x00);
                                    break;
                                case "Speak":
                                    output.Add(0x01);
                                    break;
                                case "MouthOpen":
                                    output.Add(0x02);
                                    break;
                                default:
                                    output.Add(0x00);
                                    break;
                            }

                            break;

                    }

                }
                else
                {
                    if (!addedSpeaker)
                    {
                        output.Add(0x05);

                        if (blue)
                        {
                            output.Add(0x01);
                        }
                        else
                        {
                            output.Add(0x00);
                        }
                      
                        if (!NoSpeaker)
                        {
                            output.Add(0x04);
                            output.Add((byte)speaker);
                            Console.WriteLine("break");
                        }

                        addedSpeaker = true;
                    }

                    switch (text[currentPlaceInText] + "")
                        {
                        case "A":
                            output.Add(0x20);
                            currentPlaceInText++;
                            break;
                        case "B":
                            output.Add(0x21);
                            currentPlaceInText++;
                            break;
                        case "C":
                            output.Add(0x22);
                            currentPlaceInText++;
                            break;
                        case "D":
                            output.Add(0x23);
                            currentPlaceInText++;
                            break;
                        case "E":
                            output.Add(0x24);
                            currentPlaceInText++;
                            break;
                        case "F":
                            output.Add(0x25);
                            currentPlaceInText++;
                            break;
                        case "G":
                            output.Add(0x26);
                            currentPlaceInText++;
                            break;
                        case "H":
                            output.Add(0x27);
                            currentPlaceInText++;
                            break;
                        case "I":
                            output.Add(0x28);
                            currentPlaceInText++;
                            break;
                        case "J":
                            output.Add(0x29);
                            currentPlaceInText++;
                            break;
                        case "K":
                            output.Add(0x2A);
                            currentPlaceInText++;
                            break;
                        case "L":
                            output.Add(0x2B);
                            currentPlaceInText++;
                            break;
                        case "M":
                            output.Add(0x2C);
                            currentPlaceInText++;
                            break;
                        case "N":
                            output.Add(0x2D);
                            currentPlaceInText++;
                            break;
                        case "O":
                            output.Add(0x2E);
                            currentPlaceInText++;
                            break;
                        case "P":
                            output.Add(0x2F);
                            currentPlaceInText++;
                            break;
                        case "Q":
                            output.Add(0x30);
                            currentPlaceInText++;
                            break;
                        case "R":
                            output.Add(0x31);
                            currentPlaceInText++;
                            break;
                        case "S":
                            output.Add(0x32);
                            currentPlaceInText++;
                            break;
                        case "T":
                            output.Add(0x33);
                            currentPlaceInText++;
                            break;
                        case "U":
                            output.Add(0x34);
                            currentPlaceInText++;
                            break;
                        case "V":
                            output.Add(0x35);
                            currentPlaceInText++;
                            break;
                        case "W":
                            output.Add(0x36);
                            currentPlaceInText++;
                            break;
                        case "X":
                            output.Add(0x37);
                            currentPlaceInText++;
                            break;
                        case "Y":
                            output.Add(0x38);
                            currentPlaceInText++;
                            break;
                        case "Z":
                            output.Add(0x39);
                            currentPlaceInText++;
                            break;
                        case "'":
                            currentPlaceInText++;
                            switch (text[currentPlaceInText] + "")
                                {
                                case " ":
                                    output.Add(0x5A);
                                    currentPlaceInText++;
                                    break;
                                case "d":
                                    output.Add(0x3A);
                                    currentPlaceInText++;
                                    break;
                                case "l":
                                    output.Add(0x3B);
                                    currentPlaceInText++;
                                    break;
                                case "m":
                                    output.Add(0x3C);
                                    currentPlaceInText++;
                                    break;
                                case "r":
                                    output.Add(0x3D);
                                    currentPlaceInText++;
                                    break;
                                case "s":
                                    output.Add(0x3E);
                                    currentPlaceInText++;
                                    break;
                                case "t":
                                    output.Add(0x3F);
                                    currentPlaceInText++;
                                    break;
                                case "v":
                                    output.Add(0x5D);
                                    currentPlaceInText++;
                                    break;
                            }
                            break;
                        case "a":
                            output.Add(0x40);
                            currentPlaceInText++;
                            break;
                        case "b":
                            output.Add(0x41);
                            currentPlaceInText++;
                            break;
                        case "c":
                            output.Add(0x42);
                            currentPlaceInText++;
                            break;
                        case "d":
                            output.Add(0x43);
                            currentPlaceInText++;
                            break;
                        case "e":
                            output.Add(0x44);
                            currentPlaceInText++;
                            break;
                        case "f":
                            output.Add(0x45);
                            currentPlaceInText++;
                            break;
                        case "g":
                            output.Add(0x46);
                            currentPlaceInText++;
                            break;
                        case "h":
                            output.Add(0x47);
                            currentPlaceInText++;
                            break;
                        case "i":
                            output.Add(0x48);
                            currentPlaceInText++;
                            break;
                        case "j":
                            output.Add(0x49);
                            currentPlaceInText++;
                            break;
                        case "k":
                            output.Add(0x4A);
                            currentPlaceInText++;
                            break;
                        case "l":
                            output.Add(0x4B);
                            currentPlaceInText++;
                            break;
                        case "m":
                            output.Add(0x4C);
                            currentPlaceInText++;
                            break;
                        case "n":
                            output.Add(0x4D);
                            currentPlaceInText++;
                            break;
                        case "o":
                            output.Add(0x4E);
                            currentPlaceInText++;
                            break;
                        case "p":
                            output.Add(0x4F);
                            currentPlaceInText++;
                            break;
                        case "q":
                            output.Add(0x50);
                            currentPlaceInText++;
                            break;
                        case "r":
                            output.Add(0x51);
                            currentPlaceInText++;
                            break;
                        case "s":
                            output.Add(0x52);
                            currentPlaceInText++;
                            break;
                        case "t":
                            output.Add(0x53);
                            currentPlaceInText++;
                            break;
                        case "u":
                            output.Add(0x54);
                            currentPlaceInText++;
                            break;
                        case "v":
                            output.Add(0x55);
                            currentPlaceInText++;
                            break;
                        case "w":
                            output.Add(0x56);
                            currentPlaceInText++;
                            break;
                        case "x":
                            output.Add(0x57);
                            currentPlaceInText++;
                            break;
                        case "y":
                            output.Add(0x58);
                            currentPlaceInText++;
                            break;
                        case "z":
                            output.Add(0x59);
                            currentPlaceInText++;
                            break;
                        case ",":
                            output.Add(0x5B);
                            currentPlaceInText += 2;
                            break;
                        case ".":
                            currentPlaceInText++;
                                if (currentPlaceInText < text.Length && text[currentPlaceInText] + "" == ".")
                                    {
                                    output.Add(0x5E);
                                    }
                                else
                                    {
                                    output.Add(0x5C);
                                    }
                            currentPlaceInText++;
                            break;
                        case " ":
                            output.Add(0x5F);
                            currentPlaceInText++;
                            break;
                        case "0":
                            output.Add(0x60);
                            currentPlaceInText++;
                            break;
                        case "1":
                            output.Add(0x61);
                            currentPlaceInText++;
                            break;
                        case "2":
                            output.Add(0x62);
                            currentPlaceInText++;
                            break;
                        case "3":
                            output.Add(0x63);
                            currentPlaceInText++;
                            break;
                        case "4":
                            output.Add(0x64);
                            currentPlaceInText++;
                            break;
                        case "5":
                            output.Add(0x65);
                            currentPlaceInText++;
                            break;
                        case "6":
                            output.Add(0x66);
                            currentPlaceInText++;
                            break;
                        case "7":
                            output.Add(0x67);
                            currentPlaceInText++;
                            break;
                        case "8":
                            output.Add(0x68);
                            currentPlaceInText++;
                            break;
                        case "9":
                            output.Add(0x69);
                            currentPlaceInText++;
                            break;
                        case "?":
                            output.Add(0x73);
                            currentPlaceInText++;
                            break;
                        case "!":
                            output.Add(0x74);
                            currentPlaceInText++;
                            break;
                        default:
                            //assume it was a new line
                            output.Add(0x0D);
                            currentPlaceInText++;
                            break;

                    }

                    Console.WriteLine("break");

                }

                }

            output.Add(0xFF);


            return output;
        }
    }
}
