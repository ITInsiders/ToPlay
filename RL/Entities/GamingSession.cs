using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.Entities
{
    [Table("GamingSessions")]
    public class GamingSession
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Game")]
        public long GameId { get; set; }
        public Game Game { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual List<GameGamer> GameGamers { get; set; }

        public GamingSession()
        {
            this.Start = DateTime.Now;
        }

        protected virtual object Child => this;
        public T Get<T>() where T : GamingSession, new() => this.Child is T ? (T)this.Child : null;
    }
}
