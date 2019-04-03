using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Recepti.Repository
{
    public interface IRepo<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null, string include = "");
        T Get(Expression<Func<T, bool>> expression, string include = "");
        void Remove(T item);
        void Update(T item);
        void SaveChanges();
    }
}
