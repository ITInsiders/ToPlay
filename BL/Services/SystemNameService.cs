using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.Entities;
using TP.ML.Helper;

namespace TP.BL.Services
{
    public class SystemNameService : Service<SystemName>
    {
        private static SystemNameService instance;
        public static new SystemNameService I => instance ?? (instance = new SystemNameService());

        protected SystemNameService() { }

        public string Get(string Text, Languages LNG) => base.Get(Text)?.getValue(LNG);
    }
}
