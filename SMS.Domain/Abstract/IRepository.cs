using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Domain.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        //void Delete(int id);
        void Delete(T item);
    }
}
