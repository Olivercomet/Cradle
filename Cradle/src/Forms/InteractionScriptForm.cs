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
    public partial class InteractionScriptForm : Form
    {
        public Dictionary<Byte, string> CommandsAndNames = new Dictionary<Byte, string>();

        


        public InteractionScriptForm()
        {
            CommandsAndNames.Add(0x03, "Fade to black");
            CommandsAndNames.Add(0x12, "Jump to script");
            CommandsAndNames.Add(0x14, "Set sprite position");
            CommandsAndNames.Add(0x32,"Give item");
            CommandsAndNames.Add(0x3C, "Display dialogue");

            InitializeComponent();
        }

        private void InteractionScriptForm_Load(object sender, EventArgs e)
        {

        }

        
    }
}
