﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP.ML.Entities;

namespace TP.ML.IOEntities
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Sender")]
        public long? SenderId { get; set; }
        public virtual User Sender { get; set; }
        
        [ForeignKey("Recipient")]
        public long RecipientID { get; set; }
        public virtual User Recipient { get; set; }
        
        [ForeignKey("Task")]
        public long TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}