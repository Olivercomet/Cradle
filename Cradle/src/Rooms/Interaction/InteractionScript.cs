using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public class InteractionScript
    {
        public Form1 form1;

        public string name;

        List<Command> commands = new List<Command>();

 
        public void ReadInteractionScript(int StartOffset)
            {
            int currentoffset = StartOffset;

            Command newCommand = new Command();

            newCommand.CommandType = rom.filebytes[currentoffset];
            switch (newCommand.CommandType)
                {

                case 0x12:  //jump to command
                    
                    break;

                case 0x14:  //set sprite position

                    break;

                case 0x32:  //give item

                    break;

                case 0x36:  //? maybe item related? testing if you have item?

                    break;

                case 0x3C:  //say text

                    break;
                }
            
        
        
        
            }




    }
}
