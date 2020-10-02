using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public static class Dictionaries
    {

        public static Dictionary<ushort, string> BackgroundIDsAndNames = new Dictionary<ushort, string>();

        public static Dictionary<int, string> ItemIDsAndNames = new Dictionary<int, string>();

        public static Dictionary<string, int> ItemsAndIDs = new Dictionary<string,int>();

        public static Dictionary<int, string> RoomIDsAndNames = new Dictionary<int, string>();
        public static Dictionary<uint, string> ScriptOffsetsAndNames = new Dictionary<uint, string>();

        public static Dictionary<int, int> RoomIndexAndOffset = new Dictionary<int, int>();


        public static Dictionary<int, Dialogue> DialogueIDAndDialogue = new Dictionary<int, Dialogue>();

        public static void LoadDictionaries()
        {
            RoomIDsAndNames.Add(0xC1C1, "Elevator");
            RoomIDsAndNames.Add(0xA1E7, "Foyer");
            RoomIDsAndNames.Add(0x9F7A, "Outside East rubble room");
            RoomIDsAndNames.Add(0xA59E, "Living room");
            RoomIDsAndNames.Add(0xBFB1, "Master bedroom");
            RoomIDsAndNames.Add(0xA0B8, "Laura bathroom");
            RoomIDsAndNames.Add(0xA005, "Ending D room?");
            RoomIDsAndNames.Add(0x9C6B, "Top of tower (credits)");
            RoomIDsAndNames.Add(0x92EE, "Bottom of clock tower ladder");
            RoomIDsAndNames.Add(0xAA62, "Stained glass foyer");
            RoomIDsAndNames.Add(0xA331, "Kitchen");
            RoomIDsAndNames.Add(0x8DEF, "Garage");
            RoomIDsAndNames.Add(0xC139, "First storage");
            RoomIDsAndNames.Add(0xA675, "Courtyard hallway");
            RoomIDsAndNames.Add(0x9834, "Shed");
            RoomIDsAndNames.Add(0x992A, "Courtyard");
            RoomIDsAndNames.Add(0xA799, "First rubble room");
            RoomIDsAndNames.Add(0xA7E4, "West rubble room");
            RoomIDsAndNames.Add(0xC7E2, "Animal room");
            RoomIDsAndNames.Add(0xA6DB, "Outside lower West rubble");
            RoomIDsAndNames.Add(0xA46B, "Library");
            RoomIDsAndNames.Add(0xC075, "Ceremony room");
            RoomIDsAndNames.Add(0xBE96, "Second bathroom");
            RoomIDsAndNames.Add(0xAF14, "West wing hallway");
            RoomIDsAndNames.Add(0x8E5A, "Trophy room");
            RoomIDsAndNames.Add(0xA543, "Second storage");
            RoomIDsAndNames.Add(0x99D2, "Walter room");
            RoomIDsAndNames.Add(0xA733, "Outside upper West rubble");
            RoomIDsAndNames.Add(0xCB3E, "Study");
            RoomIDsAndNames.Add(0x925D, "Child's room");
            RoomIDsAndNames.Add(0xC2E1, "Music room");
            RoomIDsAndNames.Add(0x9BB5, "Mannequin room");
            RoomIDsAndNames.Add(0xAB03, "Dog cave");
            RoomIDsAndNames.Add(0x9A78, "Main cave");
            RoomIDsAndNames.Add(0xA285, "Elevator cave");
            RoomIDsAndNames.Add(0x9B05, "Dan cave");
            RoomIDsAndNames.Add(0x93B4, "Top of tower");
            RoomIDsAndNames.Add(0xA2EE, "Machine room (lower)");
            RoomIDsAndNames.Add(0x969B, "Machine room (upper)");
            RoomIDsAndNames.Add(0xA8D6, "Phone room");
            RoomIDsAndNames.Add(0xA98A, "Religous library");
            RoomIDsAndNames.Add(0x9CC3, "Laura bathroom corridor");
            RoomIDsAndNames.Add(0x9D70, "Ending C chase hallway");
            RoomIDsAndNames.Add(0x9ED2, "Garage corridor");
            RoomIDsAndNames.Add(0xB259, "Lower west wing corridor");
            RoomIDsAndNames.Add(0xB4DB, "Upper west wing corridor");
            RoomIDsAndNames.Add(0xB7A2, "Statue corridor");
            RoomIDsAndNames.Add(0x0000, "Invalid");





            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC045C2), "WindowExamine");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC045D8), "RubbleDoor");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC177A2), "Mirror");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC178DD), "GetPerfume");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE0B3), "KitchenLight");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0x660087), "Car");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC04D03), "GetCarKey");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0x4000BC), "DanCurtain");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0x009740), "GarageLadder");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC11585), "LightSwitchLivingRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC1156F), "PaintingLivingRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC115A1), "TVLivingRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC1157A), "SofaLivingRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC11635), "Painting2LivingRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC11730), "WindowLivingRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0x2FD518), "LauraShowerCurtain");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xFF009F), "ToiletLauraBathroom");//this one is incorrect
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFD51A), "ReactToLauraInShower"); //the one obtained by the program is incorrect. This one is close enough and will mostly work
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFD5E6), "ScissormanComesOutOfBathAndComesToForeground");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC06E36), "Elevator1stButton");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC06E41), "Elevator2ndButton");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC06ED9), "Elevator3rdButton");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFDFA4), "DrinkKitchenBeverage");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0x30189B), "ScissormanComesOutOfBathAndComesToForeground2?"); //this one is called by 0x17
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFF704), "GoToStainedGlassRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC06CF8), "...FirstStorage");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFF07B), "....ReligiousLibrary");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC0698D), "LightSwitchFirstStorage");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC06FE2), "InstaKillEndingE");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC09E23), "GoToBalconyEndingC");

            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFC5B0), "WaltersLetter");

            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC11153), "TablewareSecondStorage");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC11148), "ValuableLookingPicturesSecondStorage");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC110DA), "LightSwitchSecondStorage");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC17D92), "TimeWillCauseAdherenceCeremonyRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC17E1C), "...CeremonyRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xC17D87), "CrowCorpseCeremonyRoom");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE5E1), "libraryLightSwitch");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE5FD), "libraryCrackInWall");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE4BB), "libraryMaternityMagazine");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE4B0), "booksAreLinedUpHere");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE486), "InfoDemonIdol");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE470), "TechnicalBooks");
            ScriptOffsetsAndNames.Add(utility.ConvertToPCOffset(0xEFE47B), "TheAuthorsNameIsFaded");



            BackgroundIDsAndNames.Add(0x31, "Main cave");
            BackgroundIDsAndNames.Add(0x67, "Kitchen");
            BackgroundIDsAndNames.Add(0x82, "Dog cave");
            BackgroundIDsAndNames.Add(0x86, "Stained glass foyer");
            BackgroundIDsAndNames.Add(0xB1, "Mannequin room");
            BackgroundIDsAndNames.Add(0xB5, "Garage");
            BackgroundIDsAndNames.Add(0xC0, "Garage corridor");
            BackgroundIDsAndNames.Add(0xC1, "Elevator cave");
            BackgroundIDsAndNames.Add(0xCD, "Dan cave");
            BackgroundIDsAndNames.Add(0xD6, "Outside first rubble room");
            BackgroundIDsAndNames.Add(0xF8, "Elevator");


            ItemIDsAndNames.Add(0x00, "Perfume");
            ItemIDsAndNames.Add(0x01, "Ham");
            ItemIDsAndNames.Add(0x02, "CarKey");
            ItemIDsAndNames.Add(0x03, "Insecticide");
            ItemIDsAndNames.Add(0x04, "BlackRobe");
            ItemIDsAndNames.Add(0x05, "Rope");
            ItemIDsAndNames.Add(0x06, "Dagger");
            ItemIDsAndNames.Add(0x07, "Rock");
            ItemIDsAndNames.Add(0x08, "Lantern");
            ItemIDsAndNames.Add(0x09, "Staff");
            ItemIDsAndNames.Add(0x0A, "DemonIdol");
            ItemIDsAndNames.Add(0x0B, "CageKey");
            ItemIDsAndNames.Add(0x0C, "GreenKey");
            ItemIDsAndNames.Add(0x0D, "CeremonyKey");

            ItemsAndIDs.Add("Perfume", 0x00);
            ItemsAndIDs.Add("Ham", 0x01);
            ItemsAndIDs.Add("CarKey", 0x02);
            ItemsAndIDs.Add("Insecticide", 0x03);
            ItemsAndIDs.Add("BlackRobe", 0x04);
            ItemsAndIDs.Add("Rope", 0x05);
            ItemsAndIDs.Add("Dagger", 0x06);
            ItemsAndIDs.Add("Rock", 0x07);
            ItemsAndIDs.Add("Lantern", 0x08);
            ItemsAndIDs.Add("Staff", 0x09);
            ItemsAndIDs.Add("DemonIdol", 0x0A);
            ItemsAndIDs.Add("CageKey", 0x0B);
            ItemsAndIDs.Add("GreenKey", 0x0C);
            ItemsAndIDs.Add("CeremonyKey", 0x0D);
        }
    }
}
