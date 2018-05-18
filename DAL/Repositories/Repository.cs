using System;
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
        private DbSet<T> Entities => this.DB.Set<T>();

        public Repository(Context DB)
        {
            this.DB = DB;
        }

        public void Create(T item)
        {
            this.Entities.Add(item);
        }
        public void Create(IEnumerable<T> items)
        {
            this.Entities.AddRange(items);
        }

        public void Update(T item)
        {
            this.DB.Entry(item).State = EntityState.Modified;
        }
        public void Update(IEnumerable<T> items)
        {
            foreach (T item in items) Update(item);
        }

        public void Delete(T item)
        {
            this.Entities.Remove(item);
        }
        public void Delete(IEnumerable<T> items)
        {
            this.Entities.RemoveRange(items);
        }
        public void Delete(Func<T, bool> predicate)
        {
            Delete(this.Entities.Where(predicate));
        }

        public T Get(params object[] keyValues)
        {
            return this.Entities.Find(keyValues);
        }
        public IEnumerable<T> Get()
        {
            return this.Entities;
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return this.Entities.Where(predicate);
        }

        public IEnumerable<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return this.Entities.Select(selector);
        }

        public bool Any(Func<T, bool> predicate)
        {
            return this.Entities.Any(predicate);
        }
        public bool All(Func<T, bool> predicate)
        {
            return this.Entities.All(predicate);
        }
    }
}