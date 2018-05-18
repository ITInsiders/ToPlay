using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.RL.Entities
{
    [Table("MarkComments")]
    public class MarkComment : Comment
    {
        public bool Like { get; set; }
        public bool Repost { get; set; }

        public MarkComment()
        {
            this.Like = true;
            this.Repost = false;
        }

        protected override object Child => this;
    }
}
