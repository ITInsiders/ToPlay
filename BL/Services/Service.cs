using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static Mutex Mutex = new Mutex();
        
        protected Service() {
            DB = EFUnitOfWork.I;
            Entities = DB.Set<T>();
        }

        private static IUnitOfWork DB;
        private static IRepository<T> Entities;

        public virtual void Create(T item)
        {
            
            Entities.Create(item);
            
        }
        public virtual void Create(IEnumerable<T> items)
        {
            
            Entities.Create(items);
            
        }

        public virtual void Update(T item)
        {
            
            Entities.Update(item);
            
        }
        public virtual void Update(IEnumerable<T> items)
        {
            
            Entities.Update(items);
            
        }

        public virtual void Delete(T item)
        {
            
            Entities.Delete(item);
            
        }
        public virtual void Delete(IEnumerable<T> items)
        {
            
            Entities.Delete(items);
            
        }
        public virtual void Delete(Func<T, bool> predicate)
        {
            
            Entities.Delete(predicate);
            
        }

        public virtual T Get(params object[] keyValues)
        {
            
            T Result = Entities.Get(keyValues);
            
            return Result;
        }
        public virtual List<T> Get()
        {
            
            List<T> Result = Entities.Get().ToList();
            
            return Result;
        }
        public virtual List<T> Get(Func<T, bool> predicate)
        {
            
            List<T> Result = Entities.Get(predicate).ToList();
            
            return Result;
        }

        public virtual List<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            
            List<TResult> Result = Entities.Select(selector).ToList();
            
            return Result;
        }
        public virtual bool Any(Func<T, bool> predicate)
        {
            
            bool Result = Entities.Any(predicate);
            
            return Result;
        }
        public virtual bool All(Func<T, bool> predicate)
        {
            
            bool Result = Entities.All(predicate);
            
            return Result;
        }

        public virtual void SaveFromDataBase()
        {
            DB.Save();
        }
    }
}
