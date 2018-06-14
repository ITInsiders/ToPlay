using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("Adjectives")]
    public class Adjective
    {
        [Key]
        public long Id { get; set; }

        public string Value { get; set; }
    }
}
