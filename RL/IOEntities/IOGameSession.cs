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
    [Table("IOGameSessions")]
    public class IOGameSession : GameSession
    {
        public virtual List<IOGameTask> GameTasks { get; set; }

        public List<IOGameGamer> IOGameGamers => GameGamers.Select(x => x.Get<IOGameGamer>()).ToList();
    }
}
