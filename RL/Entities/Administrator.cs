using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.Entities
{
    [Table("Administrators")]
    public class Administrator : User
    {
        protected override object Child => this;
    }
}
