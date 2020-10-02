using System;
using System.Collections.Generic;
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
    }
}
