using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private IServiceProvider _serviceProvider;
        private SQLite_DbContext db;

        public PersonSQLiteDb(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            db = scope.ServiceProvider.GetRequiredService<SQLite_DbContext>();
        }

        public List<Person> GetAll()
        {
            return db.People.ToList();
        }

        public List<Person> GetAllWhere(Expression<Func<Person, bool>> predicate)
        {
            return db.People.Where(predicate).ToList();
        }

        public Person GetSingleWhere(Expression<Func<Person, bool>> predicate)
        {
            return db.People.SingleOrDefault(predicate);
        }

        public bool AnyWhere(Expression<Func<Person, bool>> predicate)
        {
            return db.People.Any(predicate);
        }


        public Person Insert(Person item)
        {
            try
            {
                db.People.Add(item);
                db.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex}");
                return null;
            }
        }


        public Person Update(Person item)
        {
            try
            {
                db.People.Update(item);
                db.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex}");
                return null;
            }
        }


        public Person Delete(Person Person)
        {
            try
            {
                var toDel = db.People.Find(Person.Id);
                db.People.Remove(toDel);
                db.SaveChanges();
                return toDel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex}");
                return null;
            }
        }


    }
}
