using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.LF.Models
{
    public class TypeShip
    {
        public bool[,] Steps { get; set; }
        public bool Rotation { get; set; }
        public byte[,] Reflection { get; set; }

        public TypeShip() { Steps = new bool[3, 3]; Reflection = new byte[3, 3]; }

        public bool Step(Position P) => Steps[P.Y, P.X];

        public Position Reflect(Position P)
        {
            byte R = Reflection[P.Y, P.X];

            if (R == 0) return null;
            else for (int i = 0; i < 3; ++i) for (int j = 0; j < 3; ++j)
                        if (Reflection[i, j] == R && i != P.Y && j != P.X)
                            return new Position() { X = j, Y = i };
            return null;
        }
    }
}
