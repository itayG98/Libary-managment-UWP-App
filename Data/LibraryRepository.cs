using LibaryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Libary
{
    public class LibraryRepository : IRepository<LibaryItem>, IRepository<Person>
    {

        private static LibaryDB mookData;

        static LibraryRepository()
        {
            mookData = LibaryDB.DataInstance;
        }

        //Libary Items implementation
        public LibaryItem Add(LibaryItem item)
        {
            LibaryItem ToAdd = mookData.LibaryItems.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (ToAdd == null)
            {
                mookData.LibaryItems.Add(item);
                return item;
            }
            return null;
        }
        public LibaryItem Get(LibaryItem item)
        {
            return GetById(item.ItemId);
        }
        public LibaryItem Update(LibaryItem item)
        {
            LibaryItem old = mookData.LibaryItems.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (old != null)
            {
                mookData.LibaryItems.Remove(old);
                mookData.LibaryItems.Add(item);
                return item;
            }
            else
                return null;
        }
        public LibaryItem Delete(LibaryItem item)
        {
            LibaryItem ToDel = mookData.LibaryItems.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (ToDel != null)
            {
                mookData.LibaryItems.Remove(ToDel);
                return item;
            }
            return null;
        }
        public IQueryable<LibaryItem> Get()
        {
            return mookData.LibaryItems.AsQueryable();
        }
        public LibaryItem GetById(Guid id)
        {
            return mookData.LibaryItems.Find(lib => lib.ItemId.CompareTo(id) == 0);

        }
        public IQueryable<LibaryItem> GetsSortedBy(IComparer<LibaryItem> comp)
        //Get IComparer instance and sorting the origin list
        {
            mookData.LibaryItems.Sort(comp);
            return Get();
        }
        public LibaryItem Borrow(Person p, LibaryItem lib)
        {
            if (p != null && lib != null && lib.IsBorrowed == false)
            {
                if (mookData.LibaryItems.Contains(lib) && p.Borrow(lib))
                    return lib;
            }
            return null;
        }
        public LibaryItem ReturnBook(Person p, LibaryItem lib)
        {
            if (p != null && lib != null)
            {
                if (p.Return(lib))
                    return lib;
            }
            return null;
        }
        public bool Contain(LibaryItem Item)
        {
            if (Item != null)
                return mookData.LibaryItems.Contains(Item);
            return false;
        }


        //Persons implementation
        IQueryable<Person> IRepository<Person>.Get()
        {
            return mookData.Persons.AsQueryable();
        }
        public Person Get(Person p)
        {
            return mookData.Persons.FirstOrDefault(i => i.Equals(p));
        }
        public IQueryable<Person> GetsSortedBy(IComparer<Person> comp)
        //Get IComparer instance and sorting the origin list
        {
            mookData.Persons.Sort(comp);
            return mookData.Persons.AsQueryable();
        }
        public Person GetById(string id)
        {
            return mookData.Persons.Find(i => i.Id == id);
        }
        public Person Delete(Person p)
        {
            LibaryItem ToDel = mookData.LibaryItems.FirstOrDefault(i => i.Equals(p));
            if (ToDel != null)
            {
                mookData.LibaryItems.Remove(ToDel);
                return p;
            }
            return null;
        }
        public Person Add(Person p)
        {
            Person ToAdd = mookData.Persons.FirstOrDefault(i => i.Equals(p));
            if (ToAdd == null)
            {
                mookData.Persons.Add(p);
                return p;
            }
            return null;
        }
        public Person Update(Person p)
        {
            Person old = mookData.Persons.FirstOrDefault(i => i.Equals(p));
            if (old != null)
            {
                mookData.Persons.Remove(old);
                mookData.Persons.Add(p);
                return p;
            }
            else
                return null;
        }
        public Person SignIn(Person p, string password)
        {
            if (p.CheckPassword(password))
                return p;
            else
                return null;
        }
        public Person CostumerSignUp(string id, string fname, string lname, string city, string street, int houseNumber = -1, string password = "1234ABCD")
        {
            if (mookData.Persons.Find(p => p.Id == id) == null)
            {
                Person p = new Costumer(id, fname, lname, city, street, houseNumber, password);
                if (p != null)
                {
                    mookData.Persons.Add(p);
                    return p;
                }
            }
            else 
                throw new Exception("ID is allready signed"); 
            return null;
        }
        public Person EmployeeSignUp(string id, string fname, string lname, string city, string street, string ManagerPassword, int houseNumber = -1, string password = "1234ABCD")
        {
            if (mookData.Persons.Find(p => p.Id == id) == null)
            {
                Person p = Employye.EmloyeSigning(ManagerPassword, id, fname, lname, city, street, houseNumber, password);
                if (p != null)
                {
                    mookData.Persons.Add(p);
                    return p;
                }
            }
            else
                throw new Exception("ID is allready signed");
            return null;
        }
        public bool Contain(Person p)
        {
            if (p != null)
                return mookData.Persons.Contains(p);
            return false;
        }
    }
}
