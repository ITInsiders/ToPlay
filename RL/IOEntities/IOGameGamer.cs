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
        [ForeignKey("Characteristic")]
        public long CharacteristicId { get; set; }
        public virtual IOCharacteristic Characteristic { get; set; }

        public List<IOAnswer> Answers { get; set; }

        protected override object Child => this;

        public IOGameSession IOGameSession => GameSession.Get<IOGameSession>();
    }
}
