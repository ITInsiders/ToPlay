using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.Entities;
using TP.ML.IOEntities;
using TP.BL.Services;
using TP.BL.IO.Models;

namespace TP.BL.IO.Actions
{
    public class GameMechanic
    {
        public long Id { get; set; }
        public int IndexTask { get; set; }
        public bool isRun { get; set; }
        public bool isEnd { get; set; }
        public IO_GameSession Game { get; set; }

        public List<Gamer> Gamers => Game.GameGamers.Select(x => x.Gamer).ToList();
        public Gamer Gamer(long Id) => Gamers.FirstOrDefault(x => x.Id == Id);
        public List<IO_GameGamer> GameGamers => Game.GameGamers.Select(x => x.Get<IO_GameGamer>()).ToList();
        public IO_GameGamer GameGamer(long Id) => GameGamers.FirstOrDefault(x => x.GamerId == Id);
        public List<IO_Task> Tasks => Game.GameTasks.Select(x => x.Task).ToList();
        public IO_Task LastTask => Tasks.LastOrDefault();
        public List<IO_Answer> Answers => LastTask.Answers;


        private void AddTasks()
        {
            Random R = new Random();

            List<IO_Task> tasks = Service<IO_Task>.I.Get();
            List<IO_Task> result = new List<IO_Task>();

            int Count = tasks.Count();
            for (int i = 0; i < Count && i < 9; ++i)
            {
                int Index = 0;
                do { Index = R.Next(0, Count); } while (!result.Any(x => x.Id == tasks[Index].Id));
                result.Add(tasks[Index]);
            }

            foreach (IO_Task task in result)
            {
                Game.GameTasks.Add(new IO_GameTask()
                {
                    GameSession = Game,
                    Task = task,
                    TaskId = task.Id
                });
            }
        }
        public GameMechanic(long Id)
        {
            this.Id = Id;
            this.IndexTask = -1;
            this.isRun = false;
            this.isEnd = false;
            Game = new IO_GameSession();
            AddTasks();
        }

        public void AddGamer(long Id, Gamer gamer = null)
        {
            gamer = gamer ?? Service<Gamer>.I.Get(Id);
            IO_GameGamer gameGamer = new IO_GameGamer(gamer, Game);
            Game.GameGamers.Add(gameGamer);
        }

        public void RemoveGamer(long Id)
        {
            IO_GameGamer gameGamer = Game.GameGamers.FirstOrDefault(x => x.GamerId == Id)?.Get<IO_GameGamer>();
            if (gameGamer != null)
            {
                foreach (IO_Task task in Game.GameTasks.Select(x => x.Task).ToList())
                {
                    foreach (IO_Answer answer in task.Answers.Where(x => x.RecipientId == Id || x.SenderId == Id))
                    {
                        if (answer.SenderId == Id)
                        {
                            answer.SenderId = null;
                            answer.Sender = null;
                        } else
                        {
                            task.Answers.Remove(answer);
                        }
                    }
                }
                Game.GameGamers.Remove(gameGamer);
            }
        }

        public long SaveGame()
        {
            Service<IO_GameSession> service = Service<IO_GameSession>.I;
            service.Create(Game);
            service.SaveFromDataBase();
            return Game.Id;
        }

        public bool IsAllAnswer => Gamers.Count() == LastTask.Answers.Count(); 
        public IO_Answer AddAnswer(long SenderId, long RecipientId)
        {
            Gamer Sender = Gamer(SenderId);
            Gamer Recipient = Gamer(RecipientId);

            if (Sender != null && Recipient != null && LastTask != null)
            {
                IO_Answer answer =
                    LastTask.Answers.FirstOrDefault(x => x.SenderId == SenderId);

                if (answer != null)
                {
                    answer.Recipient = Recipient;
                    answer.RecipientId = RecipientId;
                }
                else
                {
                    answer = new IO_Answer()
                    {
                        Sender = Sender,
                        SenderId = SenderId,
                        Recipient = Recipient,
                        RecipientId = RecipientId,
                        Task = LastTask,
                        TaskId = LastTask.Id
                    };

                    Sender.Answers.Add(answer);
                    Recipient.Answers.Add(answer);
                    LastTask.Answers.Add(answer);
                }

                return answer;
            }

            return null;
        }
        public void CalculatorCoints()
        {
            List<IO_Answer> answers = Answers;
            foreach (IO_Answer answer in answers)
                answer.Sender.Coint = 50 * answers.Where(x => x.RecipientId == answer.RecipientId).Count();
        }

        public bool IsAllTask => Tasks.Count() == IndexTask;
        public List<Result> Results()
        {
            List<Result> results = new List<Result>();

            foreach (Gamer gamer in Gamers)
            {
                List<IO_Answer> answers = gamer.Answers.Where(x => x.RecipientId == gamer.Id).ToList();
                List<IO_Attribute> attributes = answers
                    .SelectMany(x => x.Task.TaskAttributes.Select(z => z.Attribute)).ToList();
                List<IO_Feature> features = Service<IO_Feature>.I
                    .Get(x => x.FeatureAttributes.All(z => attributes.Any(a => a.Id == z.AttributeId)))
                    .OrderByDescending(x => x.FeatureAttributes.Count()).ToList();

                foreach(IO_Feature feature in features)
                    results.Add(new Result() { Gamer = gamer, Feature = feature, Count = feature.FeatureAttributes.Count() });
            }

            results = Result.SearchMax(results);

            foreach (Result R in results)
            {
                IO_GameGamer gamer = GameGamer(R.Gamer.Id);
                gamer.Feature = R.Feature;
                gamer.FeatureId = R.Feature.Id;
            }

            Service<IO_GameSession> service = Service<IO_GameSession>.I;
            service.Create(Game);
            service.SaveFromDataBase();
            isEnd = true;

            return results;
        }

        
        /*
        public long GameId { get; set; }
        public IOGameSession Game { get; set; }
        public int Level { get; set; }
        public string Page { get; set; }

        public Dictionary<long, IOGameGamer> Gamers =>
            Game.GameGamers.Select(x => x.Get<IOGameGamer>()).GroupBy(x => x.GamerId).ToDictionary(x => x.First().GamerId, y => y.First());
        public Dictionary<long, IOGameTask> GameTasks =>
            Game.GameTasks.ToDictionary(x => x.TaskId, y => y);
        public Dictionary<long, IOTask> Tasks =>
            Game.GameTasks.Select(x => x.Task).ToDictionary(x => x.Id, y => y);

        public IOTask LastTask => 
            Game.GameTasks.LastOrDefault()?.Task;
        public bool IsAllAnswers =>
            LastTaskAnswers.Count == Gamers.Count();
        public List<IOAnswer> LastTaskAnswers =>
            Gamers.Values
            .Select(x => x.Answers.LastOrDefault())
            .Where(x => (x?.TaskId ?? 0) == (LastTask?.Id ?? 0))
            .ToList();

        public GameMechanic(long GameId)
        {
            this.GameId = GameId;

            Game = new IOGameSession()
            {
                GameId = 2,
                GameGamers = new List<GameGamer>(),
                GameTasks = new List<IOGameTask>()
            };

            Level = 0;
            Page = "/IO/Main";
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

            IOGameGamer Sender = Gamers[SenderId];
            IOGameGamer Recipient = Gamers[RecipientId];

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

        public bool isResult => IsAllAnswers && Tasks.Count == 12;
        public List<IOGameGamer> Result()
        {
            if (isResult)
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

        public JsonGame JsonGame => new JsonGame()
        {
            Id = GameId,
            Level = Level,
            Page = Page
        };

        public List<JsonGamer> JsonGamers => Gamers.Values.Select(x => new JsonGamer() {
            Id = x.Gamer.Id,
            Login = x.Gamer.Login,
            Coints = x.Coint,
            Ready = x.Ready,
            URL = x.Gamer.MainImage.URL
        }).ToList();

        public List<JsonResult> JsonResults => Result()
            .Select(x => new JsonResult() { Id = x.Id, Login = x.Gamer.Login, Сharacteristic = x.Characteristic.En })
            .ToList();

        public List<JsonAnswer> JsonAnswers => LastTaskAnswers
            .Select(x => new JsonAnswer()
            {
                Recipient = x.Recipient.Gamer.Login,
                RecipientId = x.Recipient.Gamer.Id,
                Sender = x.Sender.Gamer.Login,
                SenderId = x.Sender.Gamer.Id
            }).ToList();
    }
    */
    }
