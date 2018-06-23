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
            Mutex.WaitOne();
            Entities.Create(item);
            Mutex.ReleaseMutex();
        }
        public virtual void Create(IEnumerable<T> items)
        {
            Mutex.WaitOne();
            Entities.Create(items);
            Mutex.ReleaseMutex();
        }

        public virtual void Update(T item)
        {
            Mutex.WaitOne();
            Entities.Update(item);
            Mutex.ReleaseMutex();
        }
        public virtual void Update(IEnumerable<T> items)
        {
            Mutex.WaitOne();
            Entities.Update(items);
            Mutex.ReleaseMutex();
        }

        public virtual void Delete(T item)
        {
            Mutex.WaitOne();
            Entities.Delete(item);
            Mutex.ReleaseMutex();
        }
        public virtual void Delete(IEnumerable<T> items)
        {
            Mutex.WaitOne();
            Entities.Delete(items);
            Mutex.ReleaseMutex();
        }
        public virtual void Delete(Func<T, bool> predicate)
        {
            Mutex.WaitOne();
            Entities.Delete(predicate);
            Mutex.ReleaseMutex();
        }

        public virtual T Get(params object[] keyValues)
        {
            Mutex.WaitOne();
            T Result = Entities.Get(keyValues);
            Mutex.ReleaseMutex();
            return Result;
        }
        public virtual List<T> Get()
        {
            Mutex.WaitOne();
            List<T> Result = Entities.Get().ToList();
            Mutex.ReleaseMutex();
            return Result;
        }
        public virtual List<T> Get(Func<T, bool> predicate)
        {
            Mutex.WaitOne();
            List<T> Result = Entities.Get(predicate).ToList();
            Mutex.ReleaseMutex();
            return Result;
        }

        public virtual List<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            Mutex.WaitOne();
            List<TResult> Result = Entities.Select(selector).ToList();
            Mutex.ReleaseMutex();
            return Result;
        }
        public virtual bool Any(Func<T, bool> predicate)
        {
            Mutex.WaitOne();
            bool Result = Entities.Any(predicate);
            Mutex.ReleaseMutex();
            return Result;
        }
        public virtual bool All(Func<T, bool> predicate)
        {
            Mutex.WaitOne();
            bool Result = Entities.All(predicate);
            Mutex.ReleaseMutex();
            return Result;
        }

        public virtual void SaveFromDataBase()
        {
            DB.Save();
        }
    }
}
