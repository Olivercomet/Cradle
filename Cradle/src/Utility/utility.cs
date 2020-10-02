using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    public static class utility
    {
		public static Form1 form1;

		public static uint ConvertToPCOffset(uint input)
		{   //converts an offset to a pc offset

			Byte[] snesOffsetAsBytes = BitConverter.GetBytes(input);

			if (snesOffsetAsBytes[2] >= 0x80 && snesOffsetAsBytes[2] < 0xC0)
			{
				snesOffsetAsBytes[2] -= 0x80;
			}
			else if (snesOffsetAsBytes[2] >= 0xC0)
			{
				snesOffsetAsBytes[2] -= 0xC0;
			}

			return BitConverter.ToUInt32(snesOffsetAsBytes, 0);
		}

		public static uint ConvertToSNESOffset(uint addr)
        {
            addr = 0xc00000 + (addr & 0x3fffff);
            return addr;
        }


		public static uint Read3Bytes(Byte[] bytes, int offset)
		{
			uint output = (bytes[offset + 2] * (uint)0x10000) + (bytes[offset + 1] * (uint)0x100) + (bytes[offset]);

			return output;
		}

		public static int GetPCOffset(Byte[] input, int pos)
		{   //reads three bytes and converts them to a PC offset

			Byte[] snesOffsetAsBytes = new Byte[4];

			Array.Copy(input, pos, snesOffsetAsBytes, 0, 3);

			if (snesOffsetAsBytes[2] >= 0x80 && snesOffsetAsBytes[2] < 0xC0)
			{
				snesOffsetAsBytes[2] -= 0x80;
			}
			else if (snesOffsetAsBytes[2] >= 0xC0)
			{
				snesOffsetAsBytes[2] -= 0xC0;
			}

			return BitConverter.ToInt32(snesOffsetAsBytes, 0);
		}
	}
}
