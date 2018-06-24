using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP.ML.Entities;

namespace TP.ML.IOEntities
{
    [Table("IO_GameSessions")]
    public class IO_GameSession : GameSession
    {
        public virtual List<IO_GameTask> GameTasks { get; set; }
        public virtual List<IO_Answer> Answers { get; set; }

        protected override object Child => this;

        public List<IO_GameGamer> IO_GameGamers => GameGamers.Select(x => x.Get<IO_GameGamer>()).ToList();

        public IO_GameSession()
        {
            GameId = 2;
            GameTasks = new List<IO_GameTask>();
            GameGamers = new List<GameGamer>();
            Answers = new List<IO_Answer>();
        }
    }
}
