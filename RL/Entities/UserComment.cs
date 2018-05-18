using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.RL.Entities
{
    [Table("UserComments")]
    public class UserComment : Comment
    {
        [ForeignKey("Recipient")]
        public long RecipientID { get; set; }
        public virtual User Recipient { get; set; }

        protected override object Child => this;
    }
}
