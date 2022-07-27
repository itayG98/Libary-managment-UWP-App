using System;
using LibaryModel;
using ViewModel_MoockDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LIbaryAppTesting
{
    [TestClass]
    public class ModelTests
    {
        //Book
        [TestMethod]
        public void EqualsBookMethodTrue()
        {
            Book b1 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b2 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124, 965);
            Assert.IsTrue(b1.Equals(b2));
        }
        [TestMethod]
        public void EqualsBookMethodWithoutCountry()
        {
            Book b1 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b2 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124);
            Assert.IsTrue(b1.Equals(b2));
        }
        [TestMethod]
        public void EqualsBookMethodFalse()
        {
            LibaryDB mookData = LibaryDB.DataInstance;
            Book b1 = new Book(001, 68,"Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b2 = new Book(001, 68, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124, 965);
            Book b3 = new Book(002, 68, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124);
            Assert.IsTrue(b1.Equals(b2));
            Assert.IsFalse(b2.Equals(b3));
        }

        //Journal
        [TestMethod]
        public void EqualsJournalMethodTrue()
        {
            Journal J1 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 24);
            Journal J2 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 15);
            Assert.IsTrue(J1.Equals(J2));
        }
        [TestMethod]
        public void EqualsJournalMethodFalse()
        {
            Journal J1 = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Journal J2 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 15);
            Assert.IsFalse(J1.Equals(J2));
        }

        //Libary Item
        [TestMethod]
        public void EqualsLibMethodTrue()
        {
            Journal J1 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 24);
            LibaryItem J2 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 15);
            Assert.IsTrue(J1.Equals(J2));
        }
        [TestMethod]
        public void EqualsLibJournalMethodTrue()
        {
            LibaryItem J1 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 24);
            LibaryItem J2 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 15);
            Assert.IsTrue(J1.Equals(J2));
        }
        [TestMethod]
        public void EqualsLibMethodFalse()
        {
            Journal J1 = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Book b1 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124, 965);
            Assert.IsFalse(J1.Equals(b1));
        }

        //ISNB 
        [TestMethod]
        public void EqualsISNB() 
        {
            LibaryDB mookData = LibaryDB.DataInstance;
            ISBN i1 = new ISBN(1,12);
            ISBN i2 = new ISBN(2,12);
            ISBN i1Copy = i1;
            ISBN i1New = new ISBN(1, 12);

            Assert.IsFalse(i1.Equals(i2));
            Assert.IsTrue(i1.Equals(i1Copy));
            Assert.IsTrue(i1New.Equals(i1Copy));
            Assert.IsTrue(i1.Equals(i1New));
        }

        //Person

        [TestMethod]
        public void IDValidate()
        {
            Assert.IsTrue(Person.Validation("999392053"));
            Assert.IsFalse(Person.Validation("11123"));
            Assert.IsFalse(Person.Validation("999666080"));
        }

        [TestMethod]
        public void ChangePassword()
        {
            Person p1 = Employee.EmloyeSigning("12345", "999268121", "Itay", "Getahun", "Rehovot", "none");
            Assert.IsTrue(p1.ChangePassword("1234ABCD", "1234567"));
            Assert.IsTrue(p1.CheckPassword("1234567"));

            p1 = Employee.EmloyeSigning("12345", "999268121", "Itay", "Getahun", "Rehovot", "none", 12, "123456");
            Assert.IsTrue(p1.ChangePassword("123456", "123456789"));
            Assert.IsTrue(p1.CheckPassword("123456789"));
        }

        [TestMethod]
        public void Borrow()
        {
            Costumer p1 = new Costumer("999268121", "Itay", "Getahun", "Rehovot", "none", 12, "123456");
            Employee p2 = Employee.EmloyeSigning("12345","347166134", "Itay", "Getahun", "Rehovot", "none", 12);
            Journal J1 = new Journal("Laisha", DateTime.Today, Frequancy.Daily, "John D", 24);
            Journal J2 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 15);
            Book b1 = new Book(1, 69, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b2 = new Book(1, 65, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 104);
            Book b3 = new Book(1, 667, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100);
            Book b4 = new Book(1, 670, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 114);

            Assert.IsTrue(p1.Borrow(J1));
            Assert.IsTrue(p1.Borrow(J2));
            Assert.IsTrue(p1.Borrow(b1));
            Assert.IsTrue(p1.Borrow(b2));
            Assert.IsTrue(p1.Borrow(b3));
            Assert.IsFalse(p1.Borrow(b4));

            Assert.IsFalse(p2.Borrow(b3));
            Assert.IsTrue(p1.BorrowingCount == Person.MAX_BORROWING);
        }

        [TestMethod]
        public void Return()
        {
            Costumer p1 = new Costumer("999268121", "Itay", "Getahun", "Rehovot", "none", 12, "123456");
            Employee p2 = Employee.EmloyeSigning("12345", "347166134", "Itay", "Getahun", "Rehovot", "none", 12);
            Journal j1 = new Journal("Laisha", DateTime.Today, Frequancy.Daily, "John D", 24);
            Journal j2 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 15);
            Book b1 = new Book(1, 69, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b2 = new Book(1, 65, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 104);
            Book b3 = new Book(1, 667, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100);
            Book b4 = new Book(1, 670, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 114);

            Assert.IsTrue(p1.Borrow(j1));
            Assert.IsTrue(p1.Borrow(j2));
            Assert.IsTrue(p1.Borrow(b1));
            Assert.IsTrue(p1.Return(j1));
            Assert.IsTrue(p1.Return(j2));
            Assert.IsTrue(p1.Return(b1));
            Assert.IsTrue(p1.BorrowingCount == 0);
            Assert.IsFalse(p1.Return(b4));
            Assert.IsFalse(p2.Return(b3)); 
        }

        [TestMethod]
        public void EqualsPerson() 
        {
            Costumer p1 = new Costumer("999268121", "Itay", "Getahun", "Rehovot", "none", 12, "123456");
            Employee p2 = Employee.EmloyeSigning("12345", "347166134", "Itay", "Getahun", "Rehovot", "none", 12);
            Person p = p1;

            Assert.AreEqual(false, p1.Equals(p2));
            Assert.AreEqual(true, p1.Equals(p));
        }

        [TestMethod]
        public void SignEmployye()  
        {
            Assert.IsNull(Employee.EmloyeSigning("123456", "347166134", "Itay", "Getahun", "Rehovot", "none", 12));
            Assert.IsNull(Employee.EmloyeSigning("123456", "347166134", "Itay", "Getahun", "Rehovot", "none", 12));
        }

        //BorrowList
        [TestMethod]
        public void BorrowDetailListBorrowReturn() 
        {
            LibaryDB mookData = LibaryDB.DataInstance;

            LibaryItem lib = mookData.LibaryItems[1];
            Person p = mookData.Persons[1];
            BorrowDetailList BDL = p.BorrowingList;
            p.Borrow(lib);
            Assert.IsFalse(BDL.TryBorrow(lib));
            Assert.IsTrue(BDL.TryReturn(lib));
            BDL.TryBorrow(lib);
            BDL.TryReturn(lib);
        }

        [TestMethod]
        public void BorrowUntillFullBorrowAndReturn()
        {
            LibaryDB mookData = LibaryDB.DataInstance;

            List<LibaryItem> libs = mookData.LibaryItems;
            Person p = mookData.Persons[0];

            for (int i = 0; p.Borrow(libs[i]); i++) { }
            Assert.IsTrue(libs[5].IsBorrowed == false && p.BorrowingCount==5);
            Assert.IsTrue(p.BorrowingList.Contains(libs[2].ItemId));
            for (int i = 0; p.Return(libs[i]); i++) { }
            Assert.IsTrue(libs[0].IsBorrowed == false && p.BorrowingCount == 0);
        }

    }
}
