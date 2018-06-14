using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("IOGamingSession")]
        public long IOGamingSessionId { get; set; }
        public virtual IOGamingSession IOGamingSession { get; set; }

        public string Value { get; set; }

        public virtual Answer Answer { get; set; }
    }
}
