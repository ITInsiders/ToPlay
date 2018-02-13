using System;
using TP.DAL.EF;
using TP.DAL.Interfaces;
using TP.DAL.Entities;

namespace TP.DAL.Repositories
{
    public class EFUnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private Context db;
        private Repository<T> ItemsRepository;

        public EFUnitOfWork()
        {
            db = Context.I;
        }

        public IRepository<T> Items
        {
            get => ItemsRepository ?? (ItemsRepository = new Repository<T>());
        }

        public void Save()
        {
            db.SaveChanges();
            ItemsRepository = null;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
