using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.Entities
{
    [Table("GameGamers")]
    public class GameGamer
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Gamer")]
        public long GamerId { get; set; }
        public Gamer Gamer { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("GameSession")]
        public long? GameSessionId { get; set; }
        public GameSession GameSession { get; set; }

        protected virtual object Child => this;
        public T Get<T>() where T : GameGamer, new() => this.Child is T ? (T)this.Child : null;
    }
}
