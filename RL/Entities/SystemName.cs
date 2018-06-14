using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP.ML.Helper;

namespace TP.ML.Entities
{
    [Table("SystemNames")]
    public class SystemName
    {
        [Key]
        public string En { get; set; }

        public string De { get; set; }
        public string Ru { get; set; }

        public string getValue(Languages languages)
        {
            switch (languages)
            {
                case Languages.En: return En;
                case Languages.De: return De;
                case Languages.Ru: return Ru;
            }
            return null;
        }
    }
}
