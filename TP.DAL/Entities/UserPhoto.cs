using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.DAL.Entities
{
    public class UserPhoto
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string SRC { get; set; }
        public bool Main { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
