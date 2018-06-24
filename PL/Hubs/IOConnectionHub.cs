using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TP.BL.Services;
using TP.BL.IO.Actions;
using TP.BL.IO.Models;
using TP.ML.Entities;
using TP.ML.IOEntities;
using TP.PL.Helpers;

namespace TP.PL.Hubs
{
    [HubName("HubIO")]
    public class IOConnectionHub : Hub
    {
        public static List<ConnectGame> Games = new List<ConnectGame>();
        public ConnectGame Game(long Id) => Games.FirstOrDefault(x => x.Id == Id);

        public ConnectGamer Gamer() => Gamer(Context.ConnectionId);
        public ConnectGamer Gamer(string Id) => Games.SelectMany(x => x.Gamers).FirstOrDefault(x => x.Id == Id);
        public ConnectGamer GamerById(long Id) => Games.SelectMany(x => x.Gamers).FirstOrDefault(x => x.GamerId == Id);

        public List<string> GamersId() => Gamer()?.Game.Gamers.Select(x => x.Id).ToList();
        public List<string> GamersId(ConnectGamer gamer) => gamer?.Game.Gamers.Select(x => x.Id).ToList();

        private static List<long> Keys = new List<long>();
        private static long Key()
        {
            long key = 1;
            if (Keys.Count() > 0)
            {
                key = Keys.Min();
                Keys.Remove(key);
            }
            else if (Games.Count() > 0)
            {
                long minKey = Games.Min(x => x.Id);
                if (minKey > 1) key = minKey - 1;
                else
                {
                    foreach(long valueKey in Games.Select(x => x.Id))
                    {
                        if (valueKey == key) key++;
                        else break;
                    }
                }
            }
            return key;
        }

        private static Mutex MutexGame = new Mutex();
        private ConnectGamer Connection(long Id, Gamer gamer)
        {
            if (gamer == null) return null;

            MutexGame.WaitOne();

            string ConnectionId = Context.ConnectionId;
            ConnectGamer connectGamer = Gamer(ConnectionId) ?? GamerById(gamer.Id);
            ConnectGame connectGame = connectGamer?.Game;
            
            if (connectGamer == null)
            {
                if (Id != 0)
                {
                    connectGame = Game(Id);

                    if (connectGame == null)
                    {
                        connectGame = new ConnectGame(Id);
                        Games.Add(connectGame);
                    }
                    else if (connectGame.Game.isRun) Id = 0;
                }
                
                if (Id == 0)
                {
                    connectGame = Games.FirstOrDefault(x => x.Gamers.Count < 8 && !x.Game.isRun);

                    if (connectGame == null)
                    {
                        connectGame = new ConnectGame(Key());
                        Games.Add(connectGame);
                    }
                }

                connectGamer = new ConnectGamer()
                {
                    Id = ConnectionId,
                    Gamer = gamer,
                    Game = connectGame
                };
                connectGame.Game.AddGamer(connectGamer.GamerId, connectGamer.Gamer);
                connectGame.Gamers.Add(connectGamer);
            }
            else
            {
                connectGamer.Id = ConnectionId;
            }

            MutexGame.ReleaseMutex();

            return connectGamer;
        }

        public void Connect(long GameId)
        {
            Identity identity = new Identity();

            ConnectGamer connectGamer = null;
            if (identity.isAuth)
            {
                connectGamer = Connection(GameId, identity.User.Get<Gamer>());

                Clients.Caller.SetGame(new JsonGame(connectGamer.Game));
                Clients.Caller.SetYou(new JsonGamer(connectGamer));
                Clients.Caller.SetGamers(connectGamer.Game.Gamers.Select(x => new JsonGamer(x)).ToList());

                Clients.Clients(GamersId())
                    .SetGamers(new List<JsonGamer>() { new JsonGamer(connectGamer) });
            }
            else
            {
                Clients.Caller.Auth();
            }
        }

        public void SetReady(bool Ready)
        {
            ConnectGamer gamer = Gamer();

            if (gamer != null)
            {
                GameMechanic game = gamer.Game.Game;

                gamer.Gamer.Ready = Ready;
                Clients.Clients(GamersId(gamer)).SetReady(new JsonGamer(gamer));

                if (gamer.Game.Gamers.All(x => x.Gamer.Ready))
                {
                    if (!game.isRun) StartGame(gamer);
                    else if (game.IsAllAnswer) SetTask(gamer);
                }
            }
            else
            {
                Clients.Caller.Reload();
            }
        }

        private void StartGame(ConnectGamer gamer)
        {
            GameMechanic game = gamer.Game.Game;
            game.isRun = true;
            
            foreach(Gamer g in game.Gamers) g.Ready = false;

            SetTask(gamer);
        }

        private void SetTask(ConnectGamer gamer)
        {
            GameMechanic game = gamer.Game.Game;
            game.IndexTask++;

            if (game.IsAllTask) SetResult(gamer);
            else
            {
                Clients.Clients(GamersId(gamer)).SetTask(new JsonTask()
                {
                    Value = game.Tasks[game.IndexTask].Value(LanguageDictionary.GetLanguages())
                });
                Thread TimerThread = new Thread(new ParameterizedThreadStart(TimerStart));
                TimerThread.Start(gamer);
            }
        }

        public void SetAnswer(long RecipientId)
        {
            ConnectGamer gamer = Gamer();
            SetAnswer(gamer.GamerId, RecipientId, gamer);
        }

        private void SetAnswer(long SenderId, long RecipientId, ConnectGamer gamer = null)
        {
            gamer = gamer ?? GamerById(SenderId);
            GameMechanic game = gamer.Game.Game;

            IO_Answer answer = game.AddAnswer(SenderId, RecipientId);

            if (answer != null)
            {
                bool All = game.IsAllAnswer;

                if (!All)
                    Clients.Caller.SetAnswers(new List<JsonAnswer>() { new JsonAnswer(answer) });
                else
                {
                    game.CalculatorCoints();
                    foreach (Gamer g in game.Gamers) g.Ready = false;

                    Clients.Clients(GamersId(gamer))
                        .SetAnswers(game.Answers.Select(x => new JsonAnswer(x)).ToList());

                    Clients.Clients(GamersId(gamer))
                        .SetGamers(gamer.Game.Gamers.Select(x => new JsonGamer(x)).ToList());
                }
            }
            else
            {
                Clients.Caller.Reload();
            }
        }

        private void SetResult(ConnectGamer gamer)
        {
            LanguageDictionary language = new LanguageDictionary();
            GameMechanic game = gamer.Game.Game;

            List<JsonResult> results = game.Results().Select(x => new JsonResult() {
                Feature = x.Feature.Value(language.GetLanguage()),
                Id = x.Gamer.Id
            }).ToList();
            
            Clients.Clients(GamersId(gamer)).SetResult(results, language.Get("Result"));
        }

        protected void TimerStart(object Object)
        {
            ConnectGamer gamer = (ConnectGamer)Object;
            GameMechanic game = gamer.Game.Game;
            ConnectGame cgame = gamer.Game;
            int time = 1;
            do
            {
                Clients.Clients(GamersId(gamer)).SetTimer(time--);
                Thread.Sleep(1000);
            } while (time > 0 && !game.IsAllAnswer);

            if (!game.IsAllAnswer)
            {
                foreach (Gamer item in game.Gamers.Where(x => !game.Answers.Any(z => z.SenderId == x.Id)))
                {
                    SetAnswer(item.Id, item.Id);
                }
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            MutexGame.WaitOne();

            ConnectGamer gamer = Gamer();
            
            if (gamer != null)
            {
                Clients.Clients(GamersId(gamer)).DeleteGamer(gamer.GamerId);

                ConnectGame connectGame = gamer.Game;
                GameMechanic game = connectGame.Game;
                
                if (connectGame.Gamers.Count() == 1)
                {
                    Keys.Add(game.Id);
                    Games.Remove(connectGame);
                }
                else
                {
                    game.RemoveGamer(gamer.GamerId);
                    connectGame.Gamers.Remove(gamer);
                }
            }

            MutexGame.ReleaseMutex();

            return base.OnDisconnected(stopCalled);
        }
    }
}