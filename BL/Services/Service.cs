using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.BL.Interfaces;
using TP.DAL.Interfaces;
using TP.DAL.Repositories;

namespace TP.BL.Services
{
    public class Service<T> : IService<T> where T : class, new()
    {
        private static Service<T> instance;
        public static Service<T> I => instance ?? (instance = new Service<T>());

        protected Service() { }

        private static IUnitOfWork DB => EFUnitOfWork.I;
        private static IRepository<T> Entities => DB.Set<T>();

        public virtual void Create(T item) => Entities.Create(item);
        public virtual void Create(IEnumerable<T> items) => Entities.Create(items);

        public virtual void Update(T item) => Entities.Update(item);
        public virtual void Update(IEnumerable<T> items) => Entities.Update(items);

        public virtual void Delete(T item) => Entities.Delete(item);
        public virtual void Delete(IEnumerable<T> items) => Entities.Delete(items);
        public virtual void Delete(Func<T, bool> predicate) => Entities.Delete(predicate);

        public virtual T Get(params object[] keyValues) => Entities.Get(keyValues);
        public virtual List<T> Get() => Entities.Get().ToList();
        public virtual List<T> Get(Func<T, bool> predicate) => Entities.Get(predicate).ToList();

        public virtual List<TResult> Select<TResult>(Func<T, TResult> selector) =>
            Entities.Select(selector).ToList();

        public virtual bool Any(Func<T, bool> predicate) => Entities.Any(predicate);
        public virtual bool All(Func<T, bool> predicate) => Entities.All(predicate);
    }
}
