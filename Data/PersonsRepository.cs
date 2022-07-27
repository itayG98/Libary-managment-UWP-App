using LibaryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel_MoockDB
{
    public class PersonsRepository : IRepository<Person>
    {
        private static LibaryDB mookData;

        static PersonsRepository()
        {
            mookData = LibaryDB.DataInstance;
        }
        public IQueryable<Person> Get()
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
            if (p.BorrowingCount > 0)
                throw new Exception("Cant Deleate Person first return books ");
            Person ToDel = mookData.Persons.FirstOrDefault(person => person.Equals(p));
            if (ToDel != null)
            {
                mookData.Persons.Remove(ToDel);
                return ToDel;
            }
            return null;
        }
        public bool Contain(Person p)
        {
            if (p != null)
                return mookData.Persons.FirstOrDefault(person => person.Id == p.Id) != null;
            return false;
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
                Person p = Employee.EmloyeSigning(ManagerPassword, id, fname, lname, city, street, houseNumber, password);
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

    }
}
