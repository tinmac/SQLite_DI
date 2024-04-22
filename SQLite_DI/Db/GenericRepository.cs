using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace SQLite_DI.Db
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {

        private readonly object DbLock = new object();
        private IServiceProvider _serviceProvider;
        private string ct;

        private SQLite_DbContext db;
        private DbSet<TEntity> table = null;



        public GenericRepository(IServiceProvider serviceProvider)
        {
            serviceProvider = _serviceProvider;

            ct = typeof(TEntity).ToString();

            var scope = _serviceProvider.CreateScope();
            db = scope.ServiceProvider.GetRequiredService<SQLite_DbContext>();

            table = db.Set<TEntity>();

            Debug.WriteLine($"ctor...........T is {ct}");
        }



        public List<TEntity> GetAll()
        {
            var items = table.ToList();
            return items;
        }


        public List<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var items = table.Where(predicate).ToList();
            return items;
        }



        public TEntity GetSingleWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return table.AsNoTracking().SingleOrDefault(predicate);
        }



        public bool AnyWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return table.AsNoTracking().Any(predicate);
        }



        public TEntity Insert(TEntity item)
        {
            try
            {
                lock (DbLock)
                {
                    table.Add(item);
                    db.SaveChanges();

                    return item;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "T_db -->  Exception during T Insert");
                return null;
            }

        }



        public TEntity Update(TEntity item)
        {
            try
            {
                lock (DbLock)
                {
                    table.Update(item);
                    db.SaveChanges();
                }
                return item;

                //lock (DbLock)
                //{
                //    var existingItem = db.Partys.Find(item.Id);
                //    if (existingItem != null)
                //    {
                //        table.Entry(existingItem).CurrentValues.SetValues(item);
                //        db.SaveChanges();
                //        return item;
                //    }
                //    else
                //    {
                //        _logr.LogError($"Party with Id {item.Id} not found");
                //        return null;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "T_db -->  Exception during T Update ");
                return null;
            }
        }


        public TEntity Delete(TEntity obj)
        {
            try
            {
                lock (db)
                {
                    var toDel = table.Find(obj);
                    table.Remove(toDel);
                    db.SaveChanges();
                    return toDel;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "T_db -->  Exception during T Delete");
                return null;
            }
        }
    }

}
