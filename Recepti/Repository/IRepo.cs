using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Recepti.Repository
{
    public interface IRepo<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
        T Get(int id);
        void Remove(T item);
        void Update(T item);
        void SaveChanges();
    }
}
