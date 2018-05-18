using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.RL.Entities
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Game()
        {
            this.CreationDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
        }

        public virtual List<GameComment> Comments { get; set; }

        public virtual List<GameImage> Images { get; set; }

        public virtual List<GameGamer> Gamers { get; set; }
    }
}
