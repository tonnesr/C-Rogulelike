using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeCSharp
{
    class Tile
    {
        //Map tiles
        public char tileWall { get; set; }
        public char tileSpawn { get; set; }
        public char tileHealth { get; set; }
        public char tileEnemy { get; set; }
        public char tileNewLine { get; set; }
        public char tileNothing { get; set; }
        
        //Erase cordinate
        public string tileErase { get; set; }

        //Player tiles
        public char tilePlayer { get; set; }
        public char tileAttack_Up { get; set; }
        public char tileAttack_Down { get; set; }
        public char tileAttack_Right { get; set; }
        public char tileAttack_Left { get; set; }

        //Items

    }
}
