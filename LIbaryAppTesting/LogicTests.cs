using System;
using LibaryModel;
using DB_Libary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LIbaryAppTesting
{
    [TestClass]
    public class LogicTests
    {
        //Libary Repository

        [TestMethod]
        public void SingelTone()
        {
            LibaryDB mookData1 = LibaryDB.DataInstance;
            LibaryDB mookData2 = LibaryDB.DataInstance;
            Assert.AreEqual(mookData1, mookData2);
        }
        [TestMethod]
        public void AddAndRemove()
        {
            LibaryDB mookData = LibaryDB.DataInstance;
            LibaryItem libaryItem = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Person person = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none");

            Logic.Repo.Add(libaryItem);
            Logic.Repo.Add(person);
            Assert.IsNotNull(mookData.LibaryItems.Find(lib => lib.Equals(libaryItem)));
            Assert.IsNotNull(mookData.Persons.Find(p => p.Equals(person)));

            Logic.Repo.Borrow(person, libaryItem);
            Assert.ThrowsException<Exception>(()=>Logic.Repo.Delete(libaryItem));
            Assert.ThrowsException<Exception>(()=>Logic.Repo.Delete(person));

            Assert.IsNotNull(Logic.Repo.ReturnBook(person, libaryItem));

            Assert.IsNotNull(Logic.Repo.Delete(person));
            Assert.IsNotNull(Logic.Repo.Delete(libaryItem));
        }

    }
}