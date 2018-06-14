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
    [Table("IOGamingSessions")]
    public class IOGamingSession : GamingSession
    {
        public virtual List<Task> Tasks { get; set; }
    }
}
