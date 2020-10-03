using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle
{
    class Command
    {
        public CommandType commandType;
        public List<int> parameters = new List<int>();



        public enum CommandType { 
        
        SetSpritePosition = 0x14,
        AnimationRelated = 0x17,
        PlayMusic = 0x1E,
        PlaySFX = 0x1F,
        GiveItem = 0x32,
        ShowText = 0x3C
     
        }
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
