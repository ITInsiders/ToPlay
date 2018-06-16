using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("IOGameTasks")]
    public class IOGameTask
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("GameSession")]
        public long GameSessionId { get; set; }
        public virtual IOGameSession GameSession { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Task")]
        public long TaskId { get; set; }
        public virtual IOTask Task { get; set; }
    }
}
