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
    [Table("IOGameGamers")]
    public class IOGameGamer : GameGamer
    {
        [ForeignKey("Сharacteristic")]
        public long СharacteristicId { get; set; }
        public virtual Сharacteristic Сharacteristic { get; set; }

        protected override object Child => this;
    }
}
