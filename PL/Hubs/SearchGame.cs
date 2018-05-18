using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using TP.BL.DTO;
using TP.BL.Services;
using TP.PL.Models;

namespace TP.PL.Hubs
{
    [HubName("SearchGame")]
    public class SearchGame : Hub
    {
        // Search Game Status - SGS
        private enum SGS : byte { Connect = 1, Stop, Search, Game }

        private enum Game : byte { LaserLogic = 1 }

        private class GUser
        {
            public string Id { get; set; }
            public User User { get; set; }
            public int GameID { get; set; }
            public SGS Status { get; set; }
        }

        private static List<GUser> GUsers = new List<GUser>();

        public void Connect()
        {
            string Id = Context.ConnectionId;
            Identity Identity = new Identity();
            bool isAuthentication = Identity.isAuthentication;

            if (isAuthentication)
            {
                GUser GUser = GUsers.FirstOrDefault(x => x.User.Id == Identity.User.Id);

                if (GUser != null)
                {
                    GUser.Id = Id;

                    if (GUser.GameID != 0)
                        GUser.Status = SGS.Search;
                    else
                        GUser.Status = SGS.Connect;

                    Clients.Caller.Status(GUser.Status);
                    Clients.Caller.Message(new AjaxResponse() { Status = Status.Success, Message = "Вы успешно переподключились!" });
                }
                else
                {
                    GUsers.Add(new GUser() { Id = Id, User = Identity.User, GameID = 0, Status = SGS.Connect });
                }
            }
            else
            {
                Clients.Caller.Stop(new AjaxResponse() { Status = Status.Warning, Key = "Auth", Message = "Пожалуйста авторизуйтесь!" });
            }
        }

        public void Search(int GameID)
        {
            string Id = Context.ConnectionId;

            GUser GUser = GUsers.FirstOrDefault(x => x.Id == Id);

            if (GUser != null && GUser.Status != SGS.Stop)
            {
                GUser.Status = SGS.Stop;
                GUser.GameID = GameID;

                GUser foundGUser = GUsers.FirstOrDefault(x => x.Status == SGS.Search && x.GameID == GameID && x.Id != Id);

                if (foundGUser != null)
                {
                    GUser.Status = foundGUser.Status = SGS.Game;

                    string URL = "";

                    if (GameID == (int)Game.LaserLogic)
                        URL = "/Home";

                    Clients.Client(foundGUser.Id).StartGame(URL);
                    Clients.Caller.StartGame(URL);
                }
                else
                {
                    GUser.Status = SGS.Search;
                }
            }
        }

        public void Stop(string Id = null)
        {
            Id = Id ?? Context.ConnectionId;
            GUser GUser = GUsers.FirstOrDefault(x => x.Id == Id);
            if (GUser != null) GUser.Status = SGS.Stop;
        }

        public void DestructionTimer(object Id)
        {
            GUser GUser = GUsers.FirstOrDefault(x => x.Id == (string)Id);
            if (GUser != null)
            {
                Thread.Sleep(5000);
                GUsers.Remove(GUser);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string Id = Context.ConnectionId;
            Stop(Id);

            Thread Timer = new Thread(new ParameterizedThreadStart(DestructionTimer));
            Timer.Start(Id);

            return base.OnDisconnected(stopCalled);
        }
    }
}