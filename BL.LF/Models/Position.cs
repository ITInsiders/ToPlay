using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.LF.Extensions;

namespace TP.BL.LF.Models
{
    public class EasyPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Position : EasyPosition
    {
        public Position() { X = 0; Y = 0; }

        public double[] Easy => new double[2] { this.X, this.Y };

        public static Position operator +(Position P1, Position P2) =>
            new Position() { X = P1.X + P2.X, Y = P1.Y + P2.Y };
        public static Position operator -(Position P1, Position P2) =>
            new Position() { X = P1.X - P2.X, Y = P1.Y - P2.Y };
        public static Position operator ++(Position P1) =>
            new Position() { X = P1.X + 1, Y = P1.Y + 1 };
        public static Position operator --(Position P1) =>
            new Position() { X = P1.X - 1, Y = P1.Y - 1 };

        public Position Plus => 
            new Position() { X = this.X + 1, Y = this.Y + 1 };
        public Position Minus => 
            new Position() { X = this.X - 1, Y = this.Y - 1 };
        public Position Reverse => 
            new Position() { X = -this.X, Y = -this.Y };
        public Position Sum(Position P) => 
            new Position() { X = this.X + P.X, Y = this.Y + P.Y };
        public Position Sub(Position P) => 
            new Position() { X = this.X - P.X, Y = this.Y - P.Y };

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
