using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.Services;
using TP.ML.Entities;
using TP.BL.IO.Actions;

namespace TP.BL.IO.Models
{
    public class ConnectGame
    {
        public GameMechanic Game { get; set; }
        public long Id => Game.Id;

        public List<ConnectGamer> Gamers = new List<ConnectGamer>();
        public ConnectGamer Gamer(string Id) => Gamers.FirstOrDefault(x => x.Id == Id);

        public ConnectGame()
        {

        }

        public ConnectGame(long Id)
        {
            Game = new GameMechanic(Id);
        }
    }
}
