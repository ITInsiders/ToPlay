using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("IOCharacteristicAdjectives")]
    public class IOCharacteristicAdjective
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Characteristic")]
        public long CharacteristicId { get; set; }
        public virtual IOCharacteristic Characteristic { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Adjective")]
        public long AdjectiveId { get; set; }
        public virtual IOAdjective Adjective { get; set;}
    }
}
