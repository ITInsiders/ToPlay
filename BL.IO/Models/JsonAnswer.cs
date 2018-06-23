using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.IOEntities;

namespace TP.BL.IO.Models
{
    public class JsonAnswer
    {
        public long SenderId { get; set; }

        public long RecipientId { get; set; }

        public long Coins { get; set; }

        public JsonAnswer(IO_Answer answer)
        {
            SenderId = answer.SenderId ?? 0;
            RecipientId = answer.RecipientId;
            Coins = answer.Coins;
        }
    }
}
