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
    public class ConnectGamer
    {
        public string Id { get; set; }

        public long GamerId
        {
            get { return Gamer.Id; }
            set { Gamer = Service<Gamer>.I.Get(value); }
        }
        public Gamer Gamer { get; set; }

        public long GameId => Game.Id;
        public ConnectGame Game { get; set; }
    }
}
