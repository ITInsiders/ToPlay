using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TP.DAL.EF;
using TP.DAL.Interfaces;

namespace TP.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private Context DB;
        private DbSet<T> Entities;
        private static Mutex MutexRepository = new Mutex();

        public Repository(Context DB)
        {
            this.DB = DB;
            this.Entities = this.DB.Set<T>();
        }

        public void Create(T item)
        {
            MutexRepository.WaitOne();
            this.Entities.Add(item);
            MutexRepository.ReleaseMutex();
        }
        public void Create(IEnumerable<T> items)
        {
            MutexRepository.WaitOne();
            this.Entities.AddRange(items);
            MutexRepository.ReleaseMutex();
        }

        public void Update(T item)
        {
            MutexRepository.WaitOne();
            this.DB.Entry(item).State = EntityState.Modified;
            MutexRepository.ReleaseMutex();
        }
        public void Update(IEnumerable<T> items)
        {
            MutexRepository.WaitOne();
            foreach (T item in items) Update(item);
            MutexRepository.ReleaseMutex();
        }

        public void Delete(T item)
        {
            MutexRepository.WaitOne();
            this.Entities.Remove(item);
            MutexRepository.ReleaseMutex();
        }
        public void Delete(IEnumerable<T> items)
        {
            MutexRepository.WaitOne();
            this.Entities.RemoveRange(items);
            MutexRepository.ReleaseMutex();
        }
        public void Delete(Func<T, bool> predicate)
        {
            MutexRepository.WaitOne();
            Delete(this.Entities.Where(predicate));
            MutexRepository.ReleaseMutex();
        }

        public T Get(params object[] keyValues)
        {
            MutexRepository.WaitOne();
            T item = this.Entities.Find(keyValues);
            MutexRepository.ReleaseMutex();
            return item;
        }
        public IEnumerable<T> Get()
        {
            MutexRepository.WaitOne();
            IEnumerable<T> items = this.Entities;
            MutexRepository.ReleaseMutex();
            return items;
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            MutexRepository.WaitOne();
            IEnumerable<T> items = this.Entities.Where(predicate);
            MutexRepository.ReleaseMutex();
            return items;
        }

        public IEnumerable<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            MutexRepository.WaitOne();
            IEnumerable<TResult> items = this.Entities.Select(selector);
            MutexRepository.ReleaseMutex();
            return items;
        }

        public bool Any(Func<T, bool> predicate)
        {
            MutexRepository.WaitOne();
            bool result = this.Entities.ToList().Any(predicate);
            MutexRepository.ReleaseMutex();
            return result;
        }
        public bool All(Func<T, bool> predicate)
        {
            MutexRepository.WaitOne();
            bool result = this.Entities.ToList().All(predicate);
            MutexRepository.ReleaseMutex();
            return result;
        }
    }
}