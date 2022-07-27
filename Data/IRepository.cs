using LibaryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel_MoockDB
{
    public interface IRepository<T>
    {
        T Add(T item);
        IQueryable<T> GetsSortedBy(IComparer <T>comp);
        IQueryable<T> Get();
        T Get(T item);
        T Delete(T item);
        bool Contain(T Item);
    }
}
