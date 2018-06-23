using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ConnectGamer GamerById(long Id) => Games.SelectMany(x => x.Gamers).FirstOrDefault(x => x.GameId == Id);

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
                key = Games.Max(x => x.Id) + 1;
            }
            return key;
        }

        private ConnectGamer SearchGamer(long Id, Gamer gamer)
        {
            if (gamer == null) return null;

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
                }
                else
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
                
            return connectGamer;
        }

        public void Connect(long? TempGameId)
        {
            long GameId = TempGameId ?? 0;
            Identity identity = new Identity();

            ConnectGamer connectGamer = null;
            if (identity.isAuth)
            {
                connectGamer = SearchGamer(GameId, identity.User.Get<Gamer>());

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
            }
        }

        public void SetAnswer(long RecipientId)
        {
            ConnectGamer gamer = Gamer();
            GameMechanic game = gamer.Game.Game;

            IO_Answer answer = game.AddAnswer(gamer.GamerId, RecipientId);

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
                        .SetGamers(gamer.Game.Gamers.Select(x => new JsonGamer(x)).ToList());

                    Clients.Clients(GamersId(gamer))
                        .SetAnswers(game.Answers.Select(x => new JsonAnswer(x)).ToList());

                }
            }
            else
            {
                Clients.Caller.Reload();
            }
        }

        private void SetResult(ConnectGamer gamer)
        {
            GameMechanic game = gamer.Game.Game;

            List<JsonResult> results = game.Results().Select(x => new JsonResult() {
                Feature = x.Feature.Value(LanguageDictionary.GetLanguages()),
                Id = x.Gamer.Id
            }).ToList();
            
            Clients.Clients(GamersId(gamer)).SetResult(results);
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            ConnectGamer gamer = Gamer();
            
            if (gamer != null)
            {
                Clients.Clients(GamersId(gamer)).DeleteGamer(gamer.GamerId);

                ConnectGame connectGame = gamer.Game;
                GameMechanic game = connectGame.Game;

                connectGame.Gamers.Remove(gamer);
                game.RemoveGamer(gamer.GameId);

                if (game.Gamers.Count() == 0)
                {
                    Keys.Add(game.Id);
                    Games.Remove(connectGame);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}