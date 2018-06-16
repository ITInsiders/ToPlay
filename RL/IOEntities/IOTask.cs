using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("IOTasks")]
    public class IOTask
    {
        [Key]
        public long Id { get; set; }
        public string Value { get; set; }

        public virtual List<IOGameTask> IOGameTasks { get; set; }
        public virtual List<IOTaskAdjective> TaskAdjectives { get; set; }
        public virtual List<IOAnswer> Answers { get; set; }
    }
}
