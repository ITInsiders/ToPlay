using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.LF.Extensions;

namespace TP.BL.LF.Models
{
    public class Ship
    {
        public byte Type { get; set; }
        public int Deg { get; set; }
        public byte User { get; set; }

        public TypeShip isType => TypesShip.I[Type];
        
        public bool Step(Position P) => isType.Step(P.Rotate(Deg).Plus);
        public bool Rotation => isType.Rotation;

        public void Rotate(bool R)
        {
            Deg += (R) ? 90 : -90;
            Deg = (Deg > 360) ? Deg - 360 : Deg;
            Deg = (Deg < 0) ? Deg + 360 : Deg;
        }

        public Position Reflect(Position P)
        {
            Position T = isType.Reflect(P.Rotate(Deg).Plus);
            return (T != null) ? T.Minus.Rotate(-Deg) : T;
        }
    }
}
