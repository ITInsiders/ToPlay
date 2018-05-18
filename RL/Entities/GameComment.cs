using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.RL.Entities
{
    [Table("GameComments")]
    public class GameComment : Comment
    {
        [ForeignKey("Game")]
        public long GameId { get; set; }
        public virtual Game Game { get; set; }

        protected override object Child => this;
    }
}
