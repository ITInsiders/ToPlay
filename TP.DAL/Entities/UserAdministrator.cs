using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.DAL.Entities
{
    public class UserAdministrator
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public User User { get; set; }
    }
}
