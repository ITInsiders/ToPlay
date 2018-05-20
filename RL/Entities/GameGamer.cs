using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.RL.Entities
{
    [Table("GameGamer")]
    public class GameGamer
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Game")]
        public long GameId { get; set; }
        public Game Game { get; set; }

        [ForeignKey("Gamer")]
        public long GamerId { get; set; }
        public Gamer Gamer { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public GameGamer()
        {
            this.StartDate = DateTime.Now;
        }
    }
}
