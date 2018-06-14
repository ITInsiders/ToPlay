using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.IO.Repositories
{
    public class Repository<T> where T : class, new()
    {
        List<T> repositoryT;

        Repository()
        {
            repositoryT = new List<T>();
        }


    }
}
