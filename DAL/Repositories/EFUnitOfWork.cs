using System;
using System.Reflection;
using TP.DAL.EF;
using TP.DAL.Interfaces;

namespace TP.DAL.Repositories
{
    public partial class EFUnitOfWork
    {
        private static EFUnitOfWork instance;
        public static EFUnitOfWork I => instance ?? (instance = new EFUnitOfWork());

        private Context DB;

        protected EFUnitOfWork()
        {
            this.DB = new Context();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    this.DB.Dispose();

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
