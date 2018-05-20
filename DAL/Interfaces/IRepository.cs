using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        void Create(IEnumerable<T> items);

        void Update(T item);
        void Update(IEnumerable<T> items);

        void Delete(T item);
        void Delete(IEnumerable<T> items);
        void Delete(Func<T, Boolean> filter);

        T Get(params object[] keyValues);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, Boolean> filter);

        IEnumerable<TResult> Select<TResult>(Func<T, TResult> func);

        bool Any(Func<T, bool> predicate);
        bool All(Func<T, bool> predicate);
    }
}
