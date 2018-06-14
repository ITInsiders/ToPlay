using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Sender")]
        public long SenderId { get; set; }
        public virtual User Sender { get; set; }

        [ForeignKey("From")]
        public long? FromId { get; set; }
        public Comment From { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public Comment()
        {
            this.CreationDate = DateTime.Now;
        }

        protected virtual object Child => this;
        public T Get<T>() where T : Comment, new() => this.Child is T ? (T)this.Child : null;

        public Comment Back => this.From;
        public Comment Root => this.Back.Root ?? this.Back;
    }
}
