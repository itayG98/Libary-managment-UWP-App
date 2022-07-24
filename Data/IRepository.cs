using LibaryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Libary
{
    public interface IRepository<T>
    {
        T Add(T item);
        IQueryable<T> GetsSortedBy(IComparer <T>comp);
        IQueryable<T> Get();
        T Get(T item);
        T Delete(T item);
        bool Contain(T Item);
        Person SignIn(Person p, string password);
    }
}
