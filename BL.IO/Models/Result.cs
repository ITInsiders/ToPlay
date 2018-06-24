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
            List<Result> results = new List<Result>();
            Result Max = null;

            foreach(Result item in helpers.GroupBy(x => x.Gamer.Id).Select(x => x.First()))
            {
                List<Result> search = helpers.Where(x => x.Gamer.Id != item.Gamer.Id && x.Feature.Id != item.Feature.Id).ToList();
                List<Result> temp = SearchMax(search);

                if (results.Sum(x => x.Count) + (Max?.Count ?? 0) <= temp.Sum(x => x.Count) + item.Count)
                {
                    results = temp;
                    Max = item;
                }
            }

            if (Max != null)
                results.Add(Max);

            return results;
        }
    }
}
