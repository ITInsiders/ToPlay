using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.LF.Models
{
    public class JSMap
    {
        public class Max
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class Block
        {
            public int T { get; set; }
            public int R { get; set; }
            public int U { get; set; }
        }

        public Max Size { get; set; }
        public Block[,] Map { get; set; }
    }
}
