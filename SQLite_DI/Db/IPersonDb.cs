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
        Task<List<Person>> GetAll();

        Task<List<Person>> GetAllWhere(Expression<Func<Person, bool>> predicate);

        Task<Person> GetSingleWhere(Expression<Func<Person, bool>> predicate);

        Task<bool> AnyWhere(Expression<Func<Person, bool>> predicate);

        Task<Person> Insert(Person Person);

        Task<Person> Update(Person Person);

        Task<Person> Delete(Person Person);

    }
}
