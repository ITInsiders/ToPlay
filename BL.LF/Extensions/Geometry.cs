using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.LF.Extensions
{
    public static class Geometry
    {
        private static double DPI => Math.PI / 180.0;
        public static double Cos(int Deg) => Math.Cos(DPI * Deg);
        public static double Sin(int Deg) => Math.Sin(DPI * Deg);
    }
}
