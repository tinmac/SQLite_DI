using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLite_DI.Model;

namespace SQLite_DI.Db
{
    public interface IPersonDb
    {
        List<Person> GetAll();

        List<Person> GetAllWhere(Expression<Func<Person, bool>> predicate);

        Person GetSingleWhere(Expression<Func<Person, bool>> predicate);

        bool AnyWhere(Expression<Func<Person, bool>> predicate);

        Person Insert(Person Person);

        Person Update(Person Person);

        Person Delete(Person Person);

    }
}
