using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.RL.Entities
{
    [Table("Gamers")]
    public class Gamer : User
    {
        public virtual List<GameGamer> Games { get; set; }

        protected override object Child => this;
    }
}
