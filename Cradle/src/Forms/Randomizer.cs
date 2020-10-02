using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cradle
{

    public partial class Randomizer : Form
    {
        public Form1 form1;

        public Dictionary<string,int> ItemsAndAddresses= new Dictionary<string, int>();

        public List<string> items = new List<string>() {"Perfume","Ham","CarKey","Insecticide","BlackRobe","Rope","Rock","Lantern","Staff","DemonIdol","CageKey","GreenKey"};
       


        public Randomizer()
        {
            this.Icon = Properties.Resources.jenicon;
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ItemsAndAddresses.Add("Perfume",0x0178E7);
            ItemsAndAddresses.Add("Ham", 0x2FE085);
            ItemsAndAddresses.Add("CarKey", 0x0196AD);
            ItemsAndAddresses.Add("Insecticide", 0x6B61);
            ItemsAndAddresses.Add("BlackRobe1", 0x6A22);
            ItemsAndAddresses.Add("BlackRobe2", 0x2FECFD);
            ItemsAndAddresses.Add("Rope1", 0x6C67);
            ItemsAndAddresses.Add("Rope2", 0x01121A);
            ItemsAndAddresses.Add("Rock1", 0x11E73);
            ItemsAndAddresses.Add("Rock2", 0x2FE8EF);
            ItemsAndAddresses.Add("Lantern1", 0x7716);
            ItemsAndAddresses.Add("Lantern2", 0x7D9E);
            ItemsAndAddresses.Add("Staff", 0xC63B);
            ItemsAndAddresses.Add("DemonIdol", 0xBA8C);
            ItemsAndAddresses.Add("CageKey1", 0xC903);
            ItemsAndAddresses.Add("CageKey2", 0xCD06);
            ItemsAndAddresses.Add("GreenKey", 0x2FE616);
            
            
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            if (randomizeItemsBox.Checked)
                {
                List<string> RandomizedItems = new List<string>(items);
                Random rnd = new Random();

                randomagain:
                
                rnd = new Random((int)seedUpDown.Value);

                RandomizedItems = Shuffle(RandomizedItems, rnd);

                if (CouldBeUnobtainable("DemonIdol", RandomizedItems))
                    {
                    seedUpDown.Value++;
                    goto randomagain;
                    }
                else if (CouldBeUnobtainable("Staff", RandomizedItems))
                    {
                    seedUpDown.Value++;
                    goto randomagain;
                    }

                List<byte> filebytesBeforeRando = new List<Byte>(rom.filebytes);
                form1.SpoilerLog = new List<string>();

                form1.SpoilerLog.Add("Seed: "+ seedUpDown.Value);
                foreach (string item in items)
                    {
                    if (ItemsAndAddresses.Keys.Contains(item))
                        {
                        rom.filebytes[ItemsAndAddresses[item] + 0x01] = (Byte)Dictionaries.ItemsAndIDs[RandomizedItems[items.IndexOf(item)]];
                        }
                    else
                        {
                        rom.filebytes[ItemsAndAddresses[item+"1"] + 0x01] = (Byte)Dictionaries.ItemsAndIDs[RandomizedItems[items.IndexOf(item)]];
                        rom.filebytes[ItemsAndAddresses[item + "2"] + 0x01] = (Byte)Dictionaries.ItemsAndIDs[RandomizedItems[items.IndexOf(item)]];
                        }

                    form1.SpoilerLog.Add(item + " REPLACED WITH " + RandomizedItems[items.IndexOf(item)]);
                
                    }

                

            }


            MessageBox.Show("Randomization complete - but remember to click File -> Save!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool CouldBeUnobtainable(string item, List<string> NewItemPositions) {
            if (items.IndexOf(item) == NewItemPositions.IndexOf("Lantern1") || items.IndexOf(item) == NewItemPositions.IndexOf("Lantern2") || items.IndexOf(item) == NewItemPositions.IndexOf("GreenKey") || items.IndexOf(item) == NewItemPositions.IndexOf("Staff"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void GenerateNewSeed_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            seedUpDown.Value = NextInt32(rnd);
        }

        

        public int NextInt32(Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        private void randomizeItemsBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        public List<string> Shuffle(List<string> list, Random rnd)
        {
            for (var i = list.Count; i > 0; i--)
            {
                Swap(list, 0, rnd.Next(0, i));
            }


            return list;
        }

        public List<string> Swap(List<string> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
            return list;
        }

        private void ultimateHardMode_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    
}
