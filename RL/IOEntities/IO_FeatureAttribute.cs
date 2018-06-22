using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.IOEntities
{
    [Table("IO_FeatureAttributes")]
    public class IO_FeatureAttribute
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Feature")]
        public long FeatureId { get; set; }
        public virtual IO_Feature Feature { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Attribute")]
        public long AttributeId { get; set; }
        public virtual IO_Attribute Attribute { get; set;}
    }
}
