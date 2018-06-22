using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.LF.Extensions;

namespace TP.BL.LF.Models
{
    public class Map
    {
        public Ship[,] Ships { get; set; }

        public Ship this[int i, int j]
        { get { return (Ships != null && i >= 0 && i < Ships.GetLength(0) && j >= 0 && j < Ships.GetLength(1)) ? Ships[i, j] : null; } }

        public Map() { Ships = null; }
        public Map(Ship[,] S) { Ships = S; }

        public bool CheckStep(Position Start, Position Move, byte User)
        {
            Position End = Start.Sum(Move);
            if (Start.X < 0 || Start.X >= Ships.GetLength(0) || Start.Y < 0 || Start.Y >= Ships.GetLength(1)) return false;
            if (End.X < 0 || End.X >= Ships.GetLength(0) || End.Y < 0 || End.Y >= Ships.GetLength(1)) return false;
            if (Move.X < -1 || Move.X > 1 || Move.Y < -1 || Move.Y > 1) return false;
            if (Ships[Start.X, Start.Y] == null || Ships[End.X, End.Y] != null) return false;
            if (Ships[Start.X, Start.Y].User != User) return false;

            return Ships[Start.X, Start.Y].Step(Move);
        }

        public bool CheckRotate(Position P, byte User)
        {
            if (P.X < 0 || P.X >= Ships.GetLength(0) || P.Y < 0 || P.Y >= Ships.GetLength(1)) return false;
            if (Ships[P.X, P.Y] == null || Ships[P.X, P.Y].User != User) return false;

            return Ships[P.X, P.Y].Rotation;
        }

        public Position Step(Position Start, Position Move, byte User)
        {
            if (CheckStep(Start, Move, User))
            {
                Position End = Start.Sum(Move);
                Ships[End.X, End.Y] = Ships[Start.X, Start.Y];
                Ships[Start.X, Start.Y] = null;
                return End;
            }
            else return null;
        }

        public string Rotate(Position P, bool R, byte User)
        {
            if (CheckRotate(P, User))
            {
                Ships[P.X, P.Y].Rotate(R);
                return Ships[P.X, P.Y].Deg.ToString();
            }
            else return null;
        }

        public EasyPosition[] Laser(byte User)
        {
            Position Pos = new Position { X = (User == 1) ? 0 : Ships.GetLength(0) - 1, Y = (User == 1) ? 0 : Ships.GetLength(1) - 1 };
            Position Step = new Position { X = 0, Y = (User == 1) ? 1 : -1 };
            List<EasyPosition> PL = new List<EasyPosition>();

            PL.Add(Pos);

            do
            {
                Pos = Pos.Sum(Step);

                if (Pos.X < 0 || Pos.X >= Ships.GetLength(0) || Pos.Y < 0 || Pos.Y >= Ships.GetLength(1)) break;

                PL.Add(Pos);

                if (Ships[Pos.X, Pos.Y] != null) Step = Ships[Pos.X, Pos.Y].Reflect(Step.Reverse);
            }
            while (Step != null);

            Remove(Pos);

            return PL.ToArray();
        }

        public JSMap JSMap
        {
            get
            {
                JSMap Result = new JSMap { Size = new JSMap.Max { X = Ships.GetLength(0), Y = Ships.GetLength(1) } };
                Result.Map = new JSMap.Block[Ships.GetLength(0), Ships.GetLength(1)];

                for (int i = 0; i < Ships.GetLength(0); ++i) for (int j = 0; j < Ships.GetLength(1); ++j)
                    {
                        if (Ships[i, j] == null) Result.Map[i, j] = null;
                        else Result.Map[i, j] = new JSMap.Block { R = Ships[i, j].Deg, T = Ships[i, j].Type, U = Ships[i, j].User };
                    }

                return Result;
            }
        }

        public bool Remove(Position P)
        {
            if (P.X < 0 || P.X >= Ships.GetLength(0) || P.Y < 0 || P.Y >= Ships.GetLength(1) || Ships[P.X, P.Y] == null) return false;
            if (Ships[P.X, P.Y] != null && Ships[P.X, P.Y].Type == 0) return false;
            Ships[P.X, P.Y] = null; return true;
        }

        public int Win()
        {
            int count = 0; int user = 0;
            for (int i = 0; i < Ships.GetLength(0); ++i) for (int j = 0; j < Ships.GetLength(1); ++j)
                    if (Ships[i, j] != null && Ships[i, j].Type == 1) { count++; user = Ships[i, j].User; }

            if (count == 2) return 0;
            else return user;
        }

        public Functions Function(Position P, byte User)
        {
            if (P.X < 0 || P.X >= Ships.GetLength(0) || P.Y < 0 || P.Y >= Ships.GetLength(1)) return null;
            if (Ships[P.X, P.Y] == null || Ships[P.X, P.Y].User != User) return null;

            Functions Result = new Functions();
            Result.Route = Ships[P.X, P.Y].Rotation;
            Result.Step = new bool[3,3];

            bool Flag = false;
            for (int i = 0; i < 3; ++i) for (int j = 0; j < 3; ++j)
                {
                    Result.Step[i, j] = CheckStep(P, new Position { X = i - 1, Y = j - 1 }, User);
                    if (Result.Step[i, j] == true) Flag = true;
                }

            if (!Flag) Result.Step = null;

            return Result;
        }
    }
}
