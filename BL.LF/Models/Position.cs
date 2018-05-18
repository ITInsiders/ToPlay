using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.LF.Extensions;

namespace TP.BL.LF.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Position operator +(Position P1, Position P2) =>
            new Position() { X = P1.X + P2.X, Y = P1.Y + P2.Y };
        public static Position operator -(Position P1, Position P2) =>
            new Position() { X = P1.X - P2.X, Y = P1.Y - P2.Y };
        public static Position operator *(Position P1, Position P2) =>
            new Position() { X = P1.X * P2.X, Y = P1.Y * P2.Y };
        public static Position operator /(Position P1, Position P2) =>
            new Position() { X = P1.X / P2.X, Y = P1.Y / P2.Y };

        public Position Rotate(int Deg)
        {
            int X = Convert.ToInt32(this.X * Geometry.Cos(Deg) + this.Y * Geometry.Sin(Deg));
            int Y = Convert.ToInt32(-this.X * Geometry.Sin(Deg) + this.Y * Geometry.Cos(Deg));

            this.X = X;
            this.Y = Y;

            return this;
        }
    }
}
