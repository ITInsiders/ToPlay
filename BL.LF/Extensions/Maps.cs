using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.LF.Models;

namespace TP.BL.LF.Extensions
{
    public class Maps
    {
        private static Maps i;
        public static Maps I { get { return i ?? (i = new Maps()); } private set { i = value; } }

        public Map this[int key]
        {
            get
            {
                if (key == 1) return MAP1();
                else return null;
            }
        }

        private Map MAP1()
        {
            Ship[,] Ships = new Ship[10, 10];
            for (int i = 0; i < 10; i++) for (int j = 0; j < 10; j++) Ships[i, j] = null;

            Ships[0, 0] = new Ship { Type = 0, Deg = 0, User = 1 };
            Ships[3, 0] = new Ship { Type = 2, Deg = 0, User = 1 };
            Ships[4, 0] = new Ship { Type = 1, Deg = 0, User = 1 };
            Ships[5, 0] = new Ship { Type = 2, Deg = 0, User = 1 };
            Ships[6, 0] = new Ship { Type = 3, Deg = 0, User = 1 };
            Ships[7, 1] = new Ship { Type = 3, Deg = 0, User = 1 };
            Ships[2, 2] = new Ship { Type = 3, Deg = 90, User = 1 };
            Ships[0, 4] = new Ship { Type = 3, Deg = 270, User = 1 };
            Ships[2, 4] = new Ship { Type = 3, Deg = 90, User = 2 };
            Ships[4, 4] = new Ship { Type = 4, Deg = 90, User = 1 };
            Ships[5, 4] = new Ship { Type = 4, Deg = 0, User = 1 };
            Ships[7, 4] = new Ship { Type = 3, Deg = 0, User = 1 };
            Ships[9, 4] = new Ship { Type = 3, Deg = 180, User = 2 };
            Ships[0, 5] = new Ship { Type = 3, Deg = 0, User = 1 };
            Ships[2, 5] = new Ship { Type = 3, Deg = 180, User = 2 };
            Ships[4, 5] = new Ship { Type = 4, Deg = 0, User = 2 };
            Ships[5, 5] = new Ship { Type = 4, Deg = 90, User = 2 };
            Ships[7, 5] = new Ship { Type = 3, Deg = 270, User = 1 };
            Ships[9, 5] = new Ship { Type = 3, Deg = 90, User = 2 };
            Ships[7, 7] = new Ship { Type = 3, Deg = 270, User = 2 };
            Ships[2, 8] = new Ship { Type = 3, Deg = 180, User = 2 };
            Ships[3, 9] = new Ship { Type = 3, Deg = 180, User = 2 };
            Ships[4, 9] = new Ship { Type = 2, Deg = 180, User = 2 };
            Ships[5, 9] = new Ship { Type = 1, Deg = 180, User = 2 };
            Ships[6, 9] = new Ship { Type = 2, Deg = 180, User = 2 };
            Ships[9, 9] = new Ship { Type = 0, Deg = 180, User = 2 };

            return new Map(Ships);
        }
    }
}
