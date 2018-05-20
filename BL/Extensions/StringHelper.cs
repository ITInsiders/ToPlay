using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.Extensions
{
    public class StringHelper
    {
        public static string StringToSize(string String, int Size, int Level = 0)
        {
            if (String == null || String == "") return null;
            else if (String.Length == Size) return null;
            else if (String.Length > Size) return String.Substring(0, Size);
            else return StringToSize(String + String[Level], Size, Level + 1);
        }
    }
}
