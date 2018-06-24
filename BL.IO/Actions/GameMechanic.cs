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
        public IO_Task Task => Tasks[IndexTask];
        public List<IO_Answer> Answers => Task.Answers;


        private void AddTasks()
        {
            Random R = new Random();

            List<IO_Task> tasks = Service<IO_Task>.I.Get();
            List<IO_Task> result = new List<IO_Task>();

            int Count = tasks.Count();
            for (int i = 0; i < Count && i < 5; ++i)
            {
                int Index = 0;
                do { Index = R.Next(0, Count); } while (result.Any(x => x.Id == tasks[Index].Id));
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
            this.Game = new IO_GameSession();
            this.Game.Game = Service<Game>.I.Get(2);
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
                        }
                        else
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

        public bool IsAllAnswer => Gamers.Count() == Answers.Count();
        public IO_Answer AddAnswer(long SenderId, long RecipientId)
        {
            Gamer Sender = Gamer(SenderId);
            Gamer Recipient = Gamer(RecipientId);

            if (Sender != null && Recipient != null && Task != null)
            {
                IO_Answer answer =
                    Task.Answers.FirstOrDefault(x => x.SenderId == SenderId);

                if (answer == null)
                {
                    answer = new IO_Answer()
                    {
                        Sender = Sender,
                        SenderId = SenderId,
                        Recipient = Recipient,
                        RecipientId = RecipientId,
                        Task = Task,
                        TaskId = Task.Id,
                        GameSession = Game
                    };

                    Sender.Answers.Add(answer);
                    Recipient.Answers.Add(answer);
                    Task.Answers.Add(answer);
                    Game.Answers.Add(answer);
                } else
                {
                    answer.Recipient = Recipient;
                    answer.RecipientId = RecipientId;
                }

                return answer;
            }

            return null;
        }

        public void CalculatorCoints()
        {
            List<IO_Answer> answers = Answers;
            foreach (IO_Answer answer in answers)
            {
                answer.Coins = 25 * Math.Max(answers.Where(x => x.RecipientId == answer.RecipientId).Count() - 1, 0);
                answer.Sender.Coins += answer.Coins;
            }
        }

        public bool IsAllTask => Tasks.Count() == IndexTask;
        public List<Result> Results()
        {
            List<Result> results = new List<Result>();

            foreach (Gamer gamer in Gamers)
            {
                List<IO_Answer> answers = gamer.Answers
                    .Where(x => x.RecipientId == gamer.Id)
                    .GroupBy(x => x.TaskId).Select(x => x.First()).ToList();

                List<IO_Attribute> attributes = answers
                    .SelectMany(x => x.Task.TaskAttributes.Select(z => z.Attribute))
                    .GroupBy(x => x.Id).Select(x => x.First()).ToList();

                List<IO_Feature> features = Service<IO_Feature>.I
                    .Get(x => x.FeatureAttributes.All(z => attributes.Any(a => a.Id == z.AttributeId)))
                    .OrderByDescending(x => x.FeatureAttributes.Count()).ToList();

                if (features.Count() > 0)
                {
                    foreach (IO_Feature feature in features)
                        results.Add(new Result() { Gamer = gamer, Feature = feature, Count = feature.FeatureAttributes.Count() });
                }
                else
                {
                    IO_Feature feature = Service<IO_Feature>.I.Get(12);
                    results.Add(new Result() { Gamer = gamer, Feature = feature, Count = feature.FeatureAttributes.Count() });
                }
            }

            results = Result.SearchMax(results);

            foreach (Result R in results)
            {
                IO_GameGamer gamer = GameGamer(R.Gamer.Id);
                gamer.Feature = R.Feature;
                gamer.FeatureId = R.Feature.Id;
            }

            Game.End = DateTime.Now;
            Service<IO_GameSession> service = Service<IO_GameSession>.I;
            service.Create(Game);
            service.SaveFromDataBase();
            isEnd = true;

            return results;
        }

    }
}
