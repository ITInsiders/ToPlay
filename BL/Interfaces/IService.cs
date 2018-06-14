using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.BL.Interfaces
{
    public interface IService<T> where T : class, new()
    {
        void Create(T item);
        void Create(IEnumerable<T> items);

        void Update(T item);
        void Update(IEnumerable<T> items);

        void Delete(T item);
        void Delete(IEnumerable<T> items);
        void Delete(Func<T, Boolean> filter);

        T Get(params object[] keyValues);
        List<T> Get();
        List<T> Get(Func<T, Boolean> filter);

        List<TResult> Select<TResult>(Func<T, TResult> func);

        bool Any(Func<T, bool> predicate);
        bool All(Func<T, bool> predicate);

        void SaveFromDataBase();
    }
}
