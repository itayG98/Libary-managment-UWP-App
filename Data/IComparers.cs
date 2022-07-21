using LibaryModel;
using System.Collections.Generic;
using System.Linq;

namespace DB_Libary
{
    //LibaryItems Icomparers
    public class IComparerByItemName: IComparer<LibaryItem>
    {
        public int Compare(LibaryItem x, LibaryItem y)
        {
                return x.Name.CompareTo(y.Name);
        }
    }

    public class IComparerByItemNameReverse : IComparer<LibaryItem>
    {
        public int Compare(LibaryItem x, LibaryItem y)
        {
            return y.Name.CompareTo(x.Name);
        }
    }

    public class IComparerByPrice : IComparer<LibaryItem>
    {
        public int Compare(LibaryItem x, LibaryItem y)
        {
            return y.Price.CompareTo(x.Price);
        }
    }

    public class IComparerByPriceReverse : IComparer<LibaryItem>
    {
        public int Compare(LibaryItem x, LibaryItem y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }

    // Books Icomparers 
    public class IComparerPublisher : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            string XKey = ISBN.PublishersDict.FirstOrDefault(_key => _key.Value == x.Publisher).Key;
            string YKey = ISBN.PublishersDict.FirstOrDefault(_key => _key.Value == y.Publisher).Key;
            return XKey.CompareTo(YKey);
        }
    }

    //Persons Icomparers

    public class IComparerFName : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.FirstName.CompareTo(y.FirstName);
        }
    }

    public class IComparerByFNameReverse : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return y.FirstName.CompareTo(x.FirstName);
        }
    }

    public class IComparerLName : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.LastName.CompareTo(y.LastName);
        }
    }

    public class IComparerByLNameReverse : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return y.LastName.CompareTo(x.LastName);
        }
    }

}