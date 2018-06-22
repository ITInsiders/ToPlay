using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.Entities;

namespace TP.BL.IO.Models
{
    public class JsonGamer
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string URL { get; set; }
        public bool Ready { get; set; }
        public long Coints { get; set; }

        public JsonGamer()
        {

        }

        public JsonGamer(ConnectGamer gamer)
        {
            Id = gamer.Gamer.Id;
            Login = gamer.Gamer.Login;
            Coints = gamer.Gamer.Coint;
            Ready = gamer.Gamer.Ready;
            URL = gamer.Gamer.MainImage.URL;
        }
    }
}
