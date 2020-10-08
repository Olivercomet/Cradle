using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public static class rom
    {
        public static bool isStandaloneCradle = false;
        
        public static Byte[] filebytes = new byte[0];

        public static Palette BGPalette0 = new Palette();

        public static room CurrentRoom;

        public static List<ushort> roomIDList = new List<ushort>();

        public static string activeRomFilename = "";

        public static byte[] activeRomHash;

        public static bool isTranslatedVersion = false;

        public static Form1 form1;


        public static void StartUp(string romFilename)
        {

            activeRomFilename = romFilename;

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
                filebytes = tempRom;
            }
            else        //it has a header. Remove the header from the byte array.
            {
                filebytes = new byte[tempRom.Length - 0x200];
                Array.Copy(tempRom, 0x200, filebytes, 0, filebytes.Length);
                tempRom = null;
            }


            for (int i = 0; i < Dictionaries.RoomIDsAndNames.Count; i++)    //read room list in rom
            {
                Dictionaries.RoomIndexAndOffset.Add(i, utility.GetPCOffset(filebytes, 0x86FC + (i * 3)));
            }

            switch (filebytes[0x24B0])
            {
                case 0x24:
                    isTranslatedVersion = false;
                    break;
                case 0xD0:
                    isTranslatedVersion = true;
                    break;
            }

            BGPalette0 = imageTools.GetPaletteAtOffset(filebytes, 0x003A59, true);   //not sure if should be true or false

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(activeRomFilename))
                {
                    activeRomHash = md5.ComputeHash(stream);

                }
            }

            if (isStandaloneCradle)
                {
                foreach (ushort room in rom.roomIDList)
                    {
                    form1.RoomListBox.Items.Add(Dictionaries.RoomIDsAndNames[room]);
                    }
                }
        }
    }
}
