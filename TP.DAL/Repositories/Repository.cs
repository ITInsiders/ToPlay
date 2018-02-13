using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TP.DAL.Entities;
using TP.DAL.EF;
using TP.DAL.Interfaces;

namespace TP.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Context db;

        public Repository()
        {
            this.db = Context.I;
        }

        public IEnumerable<T> GetAll()
        {
            return this.db.Set<T>();
        }

        public T Get(int Id)
        {
            return this.db.Set<T>().Find(Id);
        }

        public IEnumerable<T> Find(Func<T, Boolean> predicate)
        {
            return this.db.Set<T>().Where(predicate);
        }

        public void Create(T t)
        {
            this.db.Set<T>().Add(t);
        }

        public void Update(T t)
        {
            this.db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            this.db.Set<T>().Remove(this.Get(Id));
        }

    }
}