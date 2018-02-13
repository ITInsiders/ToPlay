using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.DAL.Repositories;

namespace TP.BL.Services
{
    class Services<T> where T : class
    {
        protected static EFUnitOfWork<T> Database = new EFUnitOfWork<T>();

        public static IEnumerable<T> GetAll() => Database.Items.GetAll();
        public static T Get(int Id) => Database.Items.Get(Id);
        public static IEnumerable<T> Find(Func<T, Boolean> predicate) => Database.Items.Find(predicate);
        public static void Create(T item) => Database.Items.Create(item);
        public static void Update(T item) => Database.Items.Update(item);
        public static void Delete(int Id) => Database.Items.Delete(Id);

    }
}
