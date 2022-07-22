using System;
using LibaryModel;
using DB_Libary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Book b1 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b2 = new Book(022, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124, 965);
            Assert.IsFalse(b1.Equals(b2));
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
            Person p1 = new Employye("999268121", "Itay", "Getahun", "Rehovot", "none");
            Assert.IsTrue(p1.ChangePassword("1234ABCD", "1234567"));
            Assert.IsTrue(p1.CheckPassword("1234567"));

            p1 = new Employye("999268121", "Itay", "Getahun", "Rehovot", "none", 12, "123456");
            Assert.IsTrue(p1.ChangePassword("123456", "123456789"));
            Assert.IsTrue(p1.CheckPassword("123456789"));
        }

        [TestMethod]
        public void Borrow() 
        {
            Journal J1 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 24);
            Journal J2 = new Journal("Laisha", DateTime.Today, Frequancy.Monthly, "John D", 15);
            Book b1 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b2 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124);
            Book b3 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", 100, 965);
            Book b4 = new Book(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S and daviv", 124);

        }
    }
}
