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
    [Table("IO_Answers")]
    public class IO_Answer
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Sender")]
        public long? SenderId { get; set; }
        public virtual Gamer Sender { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Recipient")]
        public long RecipientId { get; set; }
        public virtual Gamer Recipient { get; set; }

        [Key]
        [Column(Order = 3)]
        [ForeignKey("Task")]
        public long TaskId { get; set; }
        public virtual IO_Task Task { get; set; }
    }
}
