using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.Extensions
{
    public static class Mapper<T, U> where T : class, new() where U : class, new()
    {
        public static Type TType = typeof(T);
        public static Type UType = typeof(U);

        public static T MergeProperties(T t, U u)
        {
            PropertyInfo[] TProperties = TType.GetProperties();
            PropertyInfo[] UProperties = UType.GetProperties();

            foreach (PropertyInfo TProperty in TProperties)
            {
                PropertyInfo UProperty =
                    UProperties.FirstOrDefault(x => x.Name == TProperty.Name && x.GetType() == TProperty.GetType());
                
                if (UProperty != null) TProperty.SetValue(t, UProperty.GetValue(u));
            }

            return t;
        }

        public static T Transformation(U u)
        {
            if (u == null) return null;

            PropertyInfo[] TProperties = TType.GetProperties();
            PropertyInfo[] UProperties = UType.GetProperties();

            T t = new T();

            foreach (PropertyInfo TProperty in TProperties)
            {
                PropertyInfo UProperty =
                    UProperties.FirstOrDefault(x => x.Name == TProperty.Name && x.PropertyType == TProperty.PropertyType);

                if (UProperty != null) TProperty.SetValue(t, UProperty.GetValue(u));
            }

            return t;
        }

        public static List<T> Transformation(List<U> listU)
        {
            List<T> listT = new List<T>();
            foreach (U u in listU) listT.Add(Transformation(u));
            return listT;
        }
    }
}
