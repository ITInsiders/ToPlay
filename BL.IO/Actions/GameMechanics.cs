using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.Entities;
using TP.ML.IOEntities;
using TP.BL.Services;
using TP.BL.IO.Repositories;

namespace TP.BL.IO.Actions
{
    public class GameMechanics
    {
        public long GameId { get; set; }
        public IOGameSession Game { get; set; }

        public Dictionary<long, IOGameGamer> Gamers =>
            Game.GameGamers.Select(x => x.Get<IOGameGamer>()).ToDictionary(x => x.GamerId, y => y);
        public Dictionary<long, IOGameTask> GameTasks =>
            Game.GameTasks.ToDictionary(x => x.TaskId, y => y);
        public Dictionary<long, IOTask> Tasks =>
            Game.GameTasks.Select(x => x.Task).ToDictionary(x => x.Id, y => y);

        public IOTask LastTask => 
            Game.GameTasks.Last().Task;
        public bool IsAllAnswers =>
            LastTaskAnswers.Count == Gamers.Count();
        public List<IOAnswer> LastTaskAnswers =>
            Gamers.Values
            .Select(x => x.Answers.Last())
            .Where(x => x.TaskId == LastTask.Id)
            .ToList();

        public GameMechanics(long GameId)
        {
            this.GameId = GameId;

            Game = new IOGameSession()
            {
                GameId = 2,
                GameGamers = new List<GameGamer>()
            };

            Gamers = new Dictionary<long, IOGameGamer>();
        }

        private Object GamerLock = new Object();
        public void AddGamer(long GamerId)
        {
            lock (GamerLock)
            {
                IOGameGamer gamer = new IOGameGamer()
                {
                    GamerId = GamerId,
                    Gamer = Service<Gamer>.I.Get(GamerId),
                    GameSession = Game
                };
                
                Game.GameGamers.Add(gamer);
            }
        }
        public void RemoveGamer(long GamerId)
        {
            lock (GamerLock)
            {
                IOGameGamer gamer = Gamers[GamerId];

                Game.GameGamers.Remove(gamer);

                if (Game.GameGamers.Count() == 0)
                    GamesRepository.RemoveGame(GameId);
            }
        }

        public void SaveGame()
        {
            Service<IOGameSession> service = Service<IOGameSession>.I;
            service.Create(Game);
            service.SaveFromDataBase();

            GamesRepository.RemoveGame(GameId);
        }

        public IOTask NewTasks()
        {

            if (Tasks.Count() < 12)
            {
                Random r = new Random();

                List<IOTask> tasks = Service<IOTask>.I.Get(x => !Tasks.ContainsKey(x.Id));
                IOTask task = tasks[r.Next(tasks.Count() - 1)];

                IOGameTask gameTask = new IOGameTask()
                {
                    GameSession = Game,
                    Task = task,
                    TaskId = task.Id
                };

                Game.GameTasks.Add(gameTask);

                return task;
            }
            return null;
        }

        public IOAnswer SetAnswer(long SenderId, long RecipientId)
        {
            IOTask Task = Game.GameTasks.Last().Task;

            IOGameGamer Sender = Gamer(SenderId);
            IOGameGamer Recipient = Gamer(RecipientId);

            IOAnswer answer = new IOAnswer()
            {
                Sender = Sender,
                SenderId = SenderId,
                Recipient = Recipient,
                RecipientId = RecipientId,
                Task = Task,
                TaskId = Task.Id
            };

            Task.Answers.Add(answer);
            Sender.Answers.Add(answer);
            Recipient.Answers.Add(answer);

            return answer;
        }

        public List<IOGameGamer> Result()
        {
            if (IsAllAnswers && Tasks.Count == 12)
            {
                foreach (IOGameGamer gamer in Gamers.Values)
                {
                    List<IOAnswer> answers = gamer.Answers.Where(x => x.RecipientId == gamer.Id).ToList();

                    List<IOAdjective> adjectives = new List<IOAdjective>();

                    foreach (IOTask task in answers.Select(x => x.Task))
                        foreach (IOTaskAdjective TA in task.TaskAdjectives)
                            adjectives.Add(TA.Adjective);

                    List<IOCharacteristic> characteristics = Service<IOCharacteristic>.I.Get(
                        x => x.CharacteristicAdjective.All(y => adjectives.Any(z => z.Id == y.AdjectiveId)));

                    IOCharacteristic characteristic = characteristics
                        .OrderByDescending(x => x.CharacteristicAdjective.Count()).FirstOrDefault();

                    gamer.Characteristic = characteristic;
                    gamer.CharacteristicId = characteristic.Id;
                }

                return Gamers.Values.ToList();
            }
            return null;
        }

        /*
        private static GameMechanics instance;
        public static GameMechanics I => instance ?? (instance = new GameMechanics());

        public List<IOGameSession> Games;

        private static object GameKaysLock = new object();
        private static object AddGamerLock = new object();

        public List<long> Keys;
        public long FreeKey()
        {
            long key = 1;
            if (Keys.Count() > 0)
            {
                key = Keys.Min();
                Keys.Remove(key);
            }
            else if (Games.Count() > 0)
            {
                key = Games.Max(x => x.Id);
            }
            return key;
        }

        protected GameMechanics()
        {
            Games = new List<IOGameSession>();
            Keys = new List<long>();
        }
        
        public long SearchGame(long GamerId)
        {
            lock(AddGamerLock)
            {
                long Id = Games.First(x => x.GameGamers.Any(y => y.GamerId == GamerId))?.Id ?? 0;

                if (Id == 0)
                {
                    Id = Games.First(x => x.GameGamers.Count() < 8)?.Id ?? 0;

                    if (Id != 0)
                        AddGamer(Id, GamerId);
                }

                return Id == 0 ? CreateGame(GamerId) : Id;
            }
        }

        public long CreateGame(long GamerId)
        {
            lock(GameKaysLock)
            {
                long Id = FreeKey();

                IOGameSession game = new IOGameSession()
                {
                    Id = Id,
                    GameId = 2,
                    GameGamers = new List<GameGamer>()
                };

                Games.Add(game);

                AddGamer(Id, GamerId);

                return Id;
            }
        }

        public void SaveGame(long GameId)
        {
            lock (GameKaysLock)
            {
                IOGameSession game = Games.Find(x => x.Id == GameId);
                game.Id = 0;

                Service<IOGameSession> service = Service<IOGameSession>.I;
                service.Create(game);
                service.SaveFromDataBase();

                Games.Remove(game);

                Keys.Add(GameId);
            }
        }

        public void AddGamer(long GameId, long GamerId)
        {
            lock(AddGamerLock)
            {
                IOGameSession game = Games.Find(x => x.Id == GameId);

                IOGameGamer gamer = new IOGameGamer()
                {
                    GamerId = GamerId,
                    Gamer = Service<Gamer>.I.Get(GamerId),
                    GameSession = game
                };

                game.GameGamers.Add(gamer);
            }
        }

        public void RemoveGamer(long GamerId)
        {
            lock (AddGamerLock)
            {
                IOGameSession game = Games.Find(x => x.GameGamers.Any(y => y.GamerId == GamerId));

                GameGamer gamer = game.GameGamers.Find(x => x.GamerId == GamerId);
                game.GameGamers.Remove(gamer);

                if (game.GameGamers.Count == 0)
                {
                    Games.Remove(game);
                    Keys.Add(game.Id);
                }
            }
        }

        public IOTask NewTasks(long GameId)
        {
            IOGameSession game = Games.Find(x => x.GameId == GameId);

            if (game.GameTasks.Count() <= 8)
            {
                Random r = new Random();

                List<IOTask> tasks = Service<IOTask>.I.Get(x => !game.GameTasks.Any(y => y.TaskId == x.Id));
                IOTask task = tasks[r.Next(tasks.Count())];

                IOGameTask gameTask = new IOGameTask()
                {
                    GameSession = game,
                    Task = task,
                    TaskId = task.Id
                };

                return task;
            }
            return null;
        }

        public IOTask SetAnswer(long GameId, long GamerId, long AnswerId)
        {
            IOGameSession game = Games.Find(x => x.GameId == GameId);

            IOGameGamer Sender = game.GameGamers.Find(x => x.GamerId == GamerId).Get<IOGameGamer>();
            IOGameGamer Recipient = game.GameGamers.Find(x => x.GamerId == AnswerId).Get<IOGameGamer>();
            IOTask Task = game.GameTasks.Last().Task;
            
            IOAnswer answer = new IOAnswer()
            {
                Sender = Sender,
                SenderId = Sender.GamerId,
                Recipient = Recipient,
                RecipientId = Recipient.GamerId,
                Task = Task,
                TaskId = Task.Id
            };

            Sender.Answers.Add(answer);
            Recipient.Answers.Add(answer);

            List<IOGameGamer> gamers = game.GameGamers.Select(x => x.Get<IOGameGamer>()).ToList();

            return gamers.All(x => x.Answers.Count() == game.GameTasks.Count())
                ? NewTasks(GameId) : null;
        }

        public bool IsFinish(long GameId)
        {

        }



        /*
        public static long CreateGame(Gamer Gamer)
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

        public static void SetAnswer(long GameId, long TaskId, long SenderId, long RecipientId)
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

        /*public static ML.IOEntities.Task GetTask(long GameId, long TaskId)
        {
            IOGamingSession GS = Games.Find(x => x.Id == GameId);
            ML.IOEntities.Task Task = GS.Tasks.Find(x => x.Id == TaskId);

        }*/
    }
}
