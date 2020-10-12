using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public static class text
    {
        public static void LoadAllText()
        {

            int currentoffset = 0;

            if (rom.isTranslatedVersion)
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

                newDialogue.offset = BitConverter.ToUInt16(rom.filebytes, currentoffset);
                currentoffset += 0x02;

                newDialogue.NoSpeaker = true;

                Dictionaries.DialogueIDAndDialogue.Add(i, newDialogue);
            }

            for (int i = 0; i < 0x200; i++)
            {
                if (rom.isTranslatedVersion)
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
                            currentoffset++;
                            break;
                        case 0x04:
                            currentoffset++;
                            Dictionaries.DialogueIDAndDialogue[i].speaker = rom.filebytes[currentoffset];
                            Dictionaries.DialogueIDAndDialogue[i].NoSpeaker = false;
                            currentoffset++;
                            break;
                        case 0x05:
                            currentoffset++;
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

        public static string GetLetter(int b)
        {
            string output = "";

            switch (b)
            {
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
    }
}
