using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.Entities;
using TP.ML.IOEntities;
using TP.BL.Services;

namespace TP.BL.IO.Actions
{
    public class GameMechanics
    {
        public static List<IOGamingSession> Games = new List<IOGamingSession>();
        public static List<long> FreeIds = new List<long>();

        public static long FreeId()
        {
            long id = 0;
            if (FreeIds.Count > 0)
            {
                id = FreeIds.First();
                FreeIds.Remove(id);
            }
            else if (FreeIds.Count > 0)
            {
                id = Games.Max(x => x.Id) + 1;
            }
            return id;
        }

        public static long AddGame(Gamer Gamer)
        {
            IOGamingSession GS = new IOGamingSession()
            {
                Id = FreeId(),
                GameId = 2
            };

            Games.Add(GS);
            AddGamer(GS.Id, Gamer);

            return GS.Id;
        }

        public static void AddGamer(long GameId, Gamer Gamer)
        {
            IOGamingSession GS = Games.Find(x => x.Id == GameId);

            if (GS != null)
            {
                IOGameGamer GG = new IOGameGamer()
                {
                    GamerId = Gamer.Id,
                    Gamer = Gamer,
                    GamingSessionId = GameId,
                    GamingSession = GS
                };

                GS.GameGamers.Add(GG);
            }
        }

        public static void RemoveGamer(long GameId, Gamer Gamer)
        {
            IOGamingSession GS = Games.Find(x => x.Id == GameId);

            if (GS != null)
            {
                GameGamer GG = GS.GameGamers.Find(x => x.GamerId == Gamer.Id);
                GS.GameGamers.Remove(GG);
            }
        }

        public static void CreateTasks(long GameId)
        {
            IOGamingSession GS = Games.Find(x => x.Id == GameId);
            GS.Tasks = new List<ML.IOEntities.Task>();

            List<ML.IOEntities.Task> Tasks = Service<ML.IOEntities.Task>.I.Get();
            long MinId = Tasks.Min(x => x.Id);
            long MaxId = Tasks.Max(x => x.Id);

            Random R = new Random();

            for (int i = 0; i < 12; i++)
            {
                long N = 0;
                do
                {
                    N = (long) (R.NextDouble() * (double) (MaxId - MinId)) + MinId;
                } while (!GS.Tasks.Any(x => x.Id == N) && Tasks.Any(x => x.Id == N));

                GS.Tasks.Add(Tasks.Find(x => x.Id == N)); 
            }
        }

        public static void SetAnswer(long GameId, int TaskId, long SenderId, long RecipientId)
        {
            IOGamingSession GS = Games.Find(x => x.Id == GameId);

            IOGameGamer Sender = GS.GameGamers.Find(x => x.GamerId == SenderId).Get<IOGameGamer>();
            IOGameGamer Recipient = GS.GameGamers.Find(x => x.GamerId == RecipientId).Get<IOGameGamer>();
            ML.IOEntities.Task Task = GS.Tasks.Find(x => x.Id == TaskId);

            Answer Answer = new Answer()
            {
                Recipient = Recipient,
                RecipientId = Recipient.Id,
                Sender = Sender,
                SenderId = Sender.Id,
                Task = Task,
                TaskId = TaskId
            };

            Task.Answer = Answer;
        }
    }
}
