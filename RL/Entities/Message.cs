using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.Entities
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Sender")]
        public long? SenderId { get; set; }
        public virtual User Sender { get; set; }

        [ForeignKey("Recipient")]
        public long? RecipientId { get; set; }
        public virtual User Recipient { get; set; }

        [ForeignKey("From")]
        public long FromId { get; set; }
        public Message From { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime ReceivingDate { get; set; }

        public virtual List<Message> Messages { get; set; }

        public Message()
        {
            this.DepartureDate = DateTime.Now;
        }

        public Message Back => this.From;
        public Message Root => this.Back.Root ?? this.Back;
    }
}
