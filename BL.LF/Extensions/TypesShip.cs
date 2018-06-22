using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.LF.Models;

namespace TP.BL.LF.Extensions
{
    public class TypesShip
    {
        private static TypesShip i;
        public static TypesShip I { get { return i ?? (i = new TypesShip()); } }

        public TypesShip()
        {
            typesShip = new TypeShip[5] {
                (new TypeShip {
                    Steps = new bool[3,3] { { false, false, false }, { false, false, false }, { false, false, false } },
                    Rotation = false,
                    Reflection = new byte[3,3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }
                }),
                (new TypeShip {
                    Steps = new bool[3,3] { { false, false, false }, { true, false, true }, { false, false, false } },
                    Rotation = false,
                    Reflection = new byte[3,3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }
                }),
                (new TypeShip {
                    Steps = new bool[3,3] { { true, true, true }, { true, true, true }, { true, true, true } },
                    Rotation = false,
                    Reflection = new byte[3,3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }
                }),
                (new TypeShip {
                    Steps = new bool[3,3] { { false, true, false }, { true, false, true }, { false, true, false } },
                    Rotation = true,
                    Reflection = new byte[3,3] { { 0, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } }
                }),
                (new TypeShip {
                    Steps = new bool[3,3] { { false, false, true }, { false, false, false }, { true, false, false } },
                    Rotation = true,
                    Reflection = new byte[3,3] { { 0, 2, 0 }, { 2, 0, 1 }, { 0, 1, 0 } }
                })
            };
        }

        private TypeShip[] typesShip;

        public TypeShip this[byte key] => key < typesShip.Length
            ? typesShip[key]
            : null;
    }
}
