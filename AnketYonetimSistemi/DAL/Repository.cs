using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _objset;

        public Repository()
        {
            _objset = context.Set<T>();
        }

        public List<T> List()
        {
            return _objset.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objset.Where(where).ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objset.AsQueryable();
        }

        public int Insert(T obj)
        {
            _objset.Add(obj);

            return Save();
        }
        public int Update(T obj)
        {
            return Save();
        }
        public int Delete(T obj)
        {
            _objset.Remove(obj);

            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objset.FirstOrDefault(where);
        }

    }
}
