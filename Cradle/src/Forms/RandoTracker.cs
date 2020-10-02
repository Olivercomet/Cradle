using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cradle
{
    public partial class RandoTracker : Form
    {
        public RandoTracker()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jenicon;
        }

        bool AnneDead = false;
        bool LauraDead = false;
        bool LotteDead = false;

        bool DiscoveredTruth = false;
        bool HaveKnowledge = false;
        bool FoundFather = false;

        bool HavePerfume = false;
        bool HaveHam = false;
        bool HaveCarKey = false;
        bool HaveInsecticide = false;
        bool HaveBlackRobe = false;
        bool HaveRope = false;
        bool HaveRock = false;
        bool HaveCageKey = false;
        bool HaveGreenKey = false;
        bool HaveLantern = false;

        private void anne_Click(object sender, EventArgs e)
        {
            AnneDead = !AnneDead;

            if (AnneDead)
                {
                anne.Image = new Bitmap(Cradle.Properties.Resources.anne2);
                }
            else
                {
                anne.Image = new Bitmap(Cradle.Properties.Resources.anne);
                }
        }

        private void laura_Click(object sender, EventArgs e)
        {
            LauraDead = !LauraDead;

            if (LauraDead)
            {
                laura.Image = new Bitmap(Cradle.Properties.Resources.laura2);
            }
            else
            {
                laura.Image = new Bitmap(Cradle.Properties.Resources.laura);
            }
        }

        private void lotte_Click(object sender, EventArgs e)
        {
            LotteDead = !LotteDead;

            if (LotteDead)
            {
                lotte.Image = new Bitmap(Cradle.Properties.Resources.lotte2);
            }
            else
            {
                lotte.Image = new Bitmap(Cradle.Properties.Resources.lotte);
            }
        }

        private void truth_Click(object sender, EventArgs e)
        {
            DiscoveredTruth = !DiscoveredTruth;

            if (DiscoveredTruth)
            {
                truth.Image = new Bitmap(Cradle.Properties.Resources.truth);
            }
            else
            {
                truth.Image = new Bitmap(Cradle.Properties.Resources.NoTruth);
            }
        }

        private void knowledge_Click(object sender, EventArgs e)
        {
            HaveKnowledge = !HaveKnowledge;

            if (HaveKnowledge)
            {
                knowledge.Image = new Bitmap(Cradle.Properties.Resources.haveknowledge);
            }
            else
            {
                knowledge.Image = new Bitmap(Cradle.Properties.Resources.knowledge);
            }
        }

        private void father_Click(object sender, EventArgs e)
        {
            FoundFather = !FoundFather;

            if (FoundFather)
            {
                father.Image = new Bitmap(Cradle.Properties.Resources.father);
            }
            else
            {
                father.Image = new Bitmap(Cradle.Properties.Resources.notfoundfather);
            }
        }

        private void ham_Click(object sender, EventArgs e)
        {
            HaveHam = !HaveHam;

            if (HaveHam)
            {
                ham.Image = new Bitmap(Cradle.Properties.Resources.ham);
            }
            else
            {
                ham.Image = new Bitmap(Cradle.Properties.Resources.noham);
            }
        }

        private void carkey_Click(object sender, EventArgs e)
        {
            HaveCarKey = !HaveCarKey;

            if (HaveCarKey)
            {
                carkey.Image = new Bitmap(Cradle.Properties.Resources.carkey);
            }
            else
            {
                carkey.Image = new Bitmap(Cradle.Properties.Resources.nocarkey);
            }
        }

        private void insecticide_Click(object sender, EventArgs e)
        {
            HaveInsecticide = !HaveInsecticide;

            if (HaveInsecticide)
            {
                insecticide.Image = new Bitmap(Cradle.Properties.Resources.insecticide);
            }
            else
            {
                insecticide.Image = new Bitmap(Cradle.Properties.Resources.noinsecticide);
            }
        }

        private void blackrobe_Click(object sender, EventArgs e)
        {
            HaveBlackRobe = !HaveBlackRobe;

            if (HaveBlackRobe)
            {
                blackrobe.Image = new Bitmap(Cradle.Properties.Resources.blackrobe);
            }
            else
            {
                blackrobe.Image = new Bitmap(Cradle.Properties.Resources.noblackrobe);
            }
        }

        private void rope_Click(object sender, EventArgs e)
        {
            HaveRope = !HaveRope;

            if (HaveRope)
            {
                rope.Image = new Bitmap(Cradle.Properties.Resources.rope);
            }
            else
            {
                rope.Image = new Bitmap(Cradle.Properties.Resources.norope);
            }
        }

        private void rock_Click(object sender, EventArgs e)
        {
            HaveRock = !HaveRock;

            if (HaveRock)
            {
                rock.Image = new Bitmap(Cradle.Properties.Resources.rock);
            }
            else
            {
                rock.Image = new Bitmap(Cradle.Properties.Resources.norock);
            }
        }

        private void cagekey_Click(object sender, EventArgs e)
        {
            HaveCageKey = !HaveCageKey;

            if (HaveCageKey)
            {
                cagekey.Image = new Bitmap(Cradle.Properties.Resources.cagekey);
            }
            else
            {
                cagekey.Image = new Bitmap(Cradle.Properties.Resources.nocagekey);
            }
        }

        private void greenkey_Click(object sender, EventArgs e)
        {
            HaveGreenKey = !HaveGreenKey;

            if (HaveGreenKey)
            {
                greenkey.Image = new Bitmap(Cradle.Properties.Resources.greenkey);
            }
            else
            {
                greenkey.Image = new Bitmap(Cradle.Properties.Resources.nogreenkey);
            }
        }

        private void lantern_Click(object sender, EventArgs e)
        {
            HaveLantern = !HaveLantern;

            if (HaveLantern)
            {
                lantern.Image = new Bitmap(Cradle.Properties.Resources.lantern);
            }
            else
            {
                lantern.Image = new Bitmap(Cradle.Properties.Resources.nolantern);
            }
        }

        private void perfume_Click(object sender, EventArgs e)
        {
            HavePerfume = !HavePerfume;

            if (HavePerfume)
                {
                perfume.Image = new Bitmap(Cradle.Properties.Resources.perfume);
                }
            else
                {
                perfume.Image = new Bitmap(Cradle.Properties.Resources.noperfume);
                }
        }
    }
}
