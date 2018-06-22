using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP.ML.IOEntities;

namespace TP.ML.Entities
{
    [Table("Gamers")]
    public class Gamer : User
    {
        protected override object Child => this;

        [NotMapped]
        public bool Ready { get; set; }
        [NotMapped]
        public long Coint { get; set; }

        public virtual List<IO_Answer> Answers { get; set; }
    }
}
