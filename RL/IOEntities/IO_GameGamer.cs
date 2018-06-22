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
    [Table("IO_GameGamers")]
    public class IO_GameGamer : GameGamer
    {
        [ForeignKey("Feature")]
        public long FeatureId { get; set; }
        public virtual IO_Feature Feature { get; set; }

        protected override object Child => this;

        public IO_GameSession IO_GameSession => GameSession.Get<IO_GameSession>();

        public IO_GameGamer()
        {

        }

        public IO_GameGamer(Gamer gamer, IO_GameSession game)
        {
            this.GamerId = gamer.Id;
            this.Gamer = gamer;
            this.GameSessionId = game.Id;
            this.GameSession = game;
        }
    }
}
