using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.Entities;
using TP.ML.IOEntities;

namespace TP.BL.IO.Models
{
    public class Result
    {
        public Gamer Gamer { get; set; }
        public IO_Feature Feature { get; set; }
        public int Count { get; set; }

        public static List<Result> SearchMax(List<Result> helpers)
        {
            List<Result> results;

            if (helpers.Count == 0)
                results = new List<Result>();
            else
            {
                List<Result> R = new List<Result>();
                foreach (Result H in helpers.GroupBy(x => x.Gamer.Id))
                {
                    List<Result> r = SearchMax(helpers.Where(x => x.Gamer.Id != H.Gamer.Id).ToList());

                    if (R.Sum(x => x.Count) < r.Sum(x => x.Count))
                        R = r;
                }
                foreach (Result H in helpers.GroupBy(x => x.Feature.Id))
                {
                    List<Result> r = SearchMax(helpers.Where(x => x.Feature.Id != H.Feature.Id).ToList());

                    if (R.Sum(x => x.Count) < r.Sum(x => x.Count))
                        R = r;
                }
                results = R;
            }

            return results;
        }
    }
}
