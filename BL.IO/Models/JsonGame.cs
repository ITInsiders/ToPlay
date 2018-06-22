using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.IO.Models
{
    public class JsonGame
    {
        public long Id { get; set; }

        public JsonGame()
        {

        }

        public JsonGame(ConnectGame game)
        {
            Id = game.Id;
        }
    }
}
