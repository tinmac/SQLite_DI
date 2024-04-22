using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DI.Db
{
    public interface IGenericRepository<T> where T : class, new()
    {
        List<T> GetAll();

        List<T> GetAllWhere(Expression<Func<T, bool>> predicate);

        T GetSingleWhere(Expression<Func<T, bool>> predicate);

        bool AnyWhere(Expression<Func<T, bool>> predicate);

        T Insert(T obj);

        T Update(T obj);

        T Delete(T obj);
    }
}
