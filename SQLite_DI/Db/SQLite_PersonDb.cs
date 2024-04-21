using Microsoft.EntityFrameworkCore;
using SQLite_DI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DI.Db
{
    public class PersonSQLiteDb : IPersonDb
    {
        private SQLite_DbContext db;

        public PersonSQLiteDb()
        {
        }

        public Task<List<Person>> GetAll()
        {
            using (db = new SQLite_DbContext())
            {
                return db.People.AsNoTracking().ToListAsync();
            }
        }

        public Task<List<Person>> GetAllWhere(Expression<Func<Person, bool>> predicate)
        {
            using (db = new SQLite_DbContext())
            {
                return db.People.Where(predicate).ToListAsync();
            }
        }

        public Task<Person> GetSingleWhere(Expression<Func<Person, bool>> predicate)
        {
            using (db = new SQLite_DbContext())
            {
                return Task.FromResult(db.People.AsNoTracking().SingleOrDefault(predicate));
            }
        }

        public Task<bool> AnyWhere(Expression<Func<Person, bool>> predicate)
        {
            using (db = new SQLite_DbContext())
            {
                return Task.FromResult(db.People.AsNoTracking().Any(predicate));
            }
        }


        public async Task<Person> Insert(Person item)
        {
            try
            {
                using (db = new SQLite_DbContext())
                {
                    await db.People.AddAsync(item);
                    await db.SaveChangesAsync();
                    return item;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex}");
                return null;
            }
        }


        public async Task<Person> Update(Person item)
        {
            try
            {
                using (db = new SQLite_DbContext())
                {
                    db.People.Update(item);
                    await db.SaveChangesAsync();

                    return item;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex}");
                return null;
            }
        }


        public async Task<Person> Delete(Person Person)
        {
            try
            {
                using (db = new SQLite_DbContext())
                {
                    var toDel = db.People.Find(Person.Id);
                    db.People.Remove(toDel);
                    await db.SaveChangesAsync();
                    return toDel;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex}");
                return null;
            }
        }


    }
}
