using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("TaskAdjectives")]
    public class TaskAdjective
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Task")]
        public long TaskId { get; set; }
        public virtual Task Task { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Adjective")]
        public long AdjectiveId { get; set; }
        public virtual Adjective Adjective { get; set; }
    }
}
