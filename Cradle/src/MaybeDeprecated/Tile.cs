using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public class Tile
    {
        public Byte flags;
        public ushort tileID;  //this is overall, not per sprite. It indicates where the tile can be found in the tile list starting at 050000.
        public Byte Ypos;
        public Byte Xpos;
        public List<Byte> image = new List<Byte>();
    }
}
