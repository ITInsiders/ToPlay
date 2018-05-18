using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.RL.Entities
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public long Id { get; set; }

        public string URL { get; set; }
        
        public bool Main { get; set; }

        public DateTime DownloadDate { get; set; }

        public Image()
        {
            this.DownloadDate = DateTime.Now;
            this.Main = false;
        }

        [ForeignKey("Id")]
        protected virtual object Child { get; set; }
        public T Get<T>() where T : Image, new() => Child is T ? (T)Child : null;
    }
}
