using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP.ML.Helper;

namespace TP.ML.IOEntities
{
    [Table("IO_Tasks")]
    public class IO_Task
    {
        [Key]
        public long Id { get; set; }
        public string En { get; set; }
        public string De { get; set; }
        public string Ru { get; set; }
        public string Value(Languages Lng)
        {
            switch(Lng)
            {
                case Languages.De:
                    return De;
                case Languages.Ru:
                    return Ru;
                default:
                    return En;
            }
        }

        public virtual List<IO_GameTask> GameTasks { get; set; }
        public virtual List<IO_TaskAttribute> TaskAttributes { get; set; }
        public virtual List<IO_Answer> Answers { get; set; }

        public IO_Task()
        {
            GameTasks = new List<IO_GameTask>();
            TaskAttributes = new List<IO_TaskAttribute>();
            Answers = new List<IO_Answer>();
        }
    }
}
