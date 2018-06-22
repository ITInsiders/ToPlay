using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("IO_TaskAttributes")]
    public class IO_TaskAttribute
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Task")]
        public long TaskId { get; set; }
        public virtual IO_Task Task { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Attribute")]
        public long AttributeId { get; set; }
        public virtual IO_Attribute Attribute { get; set; }
    }
}
