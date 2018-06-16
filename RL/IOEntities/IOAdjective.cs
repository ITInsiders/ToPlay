using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("IOAdjectives")]
    public class IOAdjective
    {
        [Key]
        public long Id { get; set; }

        public string Value { get; set; }

        public virtual List<IOTaskAdjective> TaskAdjectives { get; set; }
        public virtual List<IOCharacteristicAdjective> CharacteristicAdjective { get; set; }
    }
}
