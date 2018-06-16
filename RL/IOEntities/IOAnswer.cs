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
    [Table("IOAnswers")]
    public class IOAnswer
    {
        [ForeignKey("Sender")]
        public long? SenderId { get; set; }
        public virtual IOGameGamer Sender { get; set; }
        
        [ForeignKey("Recipient")]
        public long? RecipientId { get; set; }
        public virtual IOGameGamer Recipient { get; set; }
        
        [ForeignKey("Task")]
        public long TaskId { get; set; }
        public virtual IOTask Task { get; set; }
    }
}
