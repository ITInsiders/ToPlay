using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.IO.Actions;

namespace TP.BL.IO.Repositories
{
    public class GamesRepository
    {
        public static Dictionary<long, GameMechanics> Games = new Dictionary<long, GameMechanics>();
        public static List<long> Keys = new List<long>();

        private static Object GamesLock = new Object();

        public static long FreeKey()
        {
            long key = 1;
            if (Keys.Count() > 0)
            {
                key = Keys.Min();
                Keys.Remove(key);
            }
            else if (Games.Count() > 0)
            {
                key = Games.Keys.Max() + 1;
            }
            return key;
        }
        public static long SearchGame(long GamerId)
        {
            lock (GamesLock)
            {
                long Id = SearchGameByGamer(GamerId);
                if (Id == 0) Id = SearchOpenGame();
                if (Id == 0) Id = CreateGame();

                Games[Id].AddGamer(GamerId);

                return Id;
            }
        }
        public static long SearchGameByGamer(long GamerId)
        {
            return Games.Values.FirstOrDefault(x => x.Gamers.ContainsKey(GamerId)).GameId;
        }
        public static long SearchOpenGame()
        {
            return Games.Values
                .FirstOrDefault(x => x.Gamers.Count < 8 && x.Tasks.Count == 0).GameId;
        }
        public static long CreateGame()
        {
            long Id = FreeKey();

            Games.Add(Id, new GameMechanics(Id));

            return Id;
        }
        public static void RemoveGame(long GameId)
        {
            lock (GamesLock)
            {
                if (Games.ContainsKey(GameId))
                {
                    Games.Remove(GameId);
                    Keys.Add(GameId);
                }
            }
        }
    }
}
