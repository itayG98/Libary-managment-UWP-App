using DB_Libary;
using LibaryModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LIbaryAppTesting
{
    [TestClass]
    public class LogicTests
    {
        //DB

        [TestMethod]
        public void SingelToneDB()
        {
            LibaryDB mookData1 = LibaryDB.DataInstance;
            LibaryDB mookData2 = LibaryDB.DataInstance;
            Assert.AreEqual(mookData1, mookData2);
        }

        //LibaryRepository

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
            Assert.ThrowsException<Exception>(() => Logic.Repo.Delete(libaryItem));
            Assert.ThrowsException<Exception>(() => Logic.Repo.Delete(person));

            Assert.IsNotNull(Logic.Repo.ReturnBook(person, libaryItem));

            Assert.IsNotNull(Logic.Repo.Delete(person));
            Assert.IsNotNull(Logic.Repo.Delete(libaryItem));
        }

        [TestMethod]
        public void GetPersonOrLibaryApp()
        {
            LibaryRepository Repo = new LibaryRepository();
            LibaryItem lib = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Person p = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none");
            Logic.Repo.Add(lib);
            Logic.Repo.Add(p);

            Assert.AreEqual(Repo.Get(p), p);
            Assert.AreEqual(Repo.Get(lib), lib);
        }

        [TestMethod]
        public void GetIquaryAble()
        {
            LibaryRepository Repo = new LibaryRepository();
            List<LibaryItem> libs1 = Repo.Get().ToList();
            List<LibaryItem> libs2 = LibaryDB.DataInstance.LibaryItems;
            for (int i = 0; i < libs1.Count; i++)
                Assert.AreEqual(libs1[i], libs2[i]);
        }
        [TestMethod]
        public void GetByID()
        {
            LibaryRepository Repo = new LibaryRepository();
            LibaryItem libaryItem = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Person person = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none");

            Repo.Add(person);
            Repo.Add(libaryItem);

            Guid libId = libaryItem.ItemId;
            Assert.IsTrue(Repo.GetById(libId) == libaryItem);

            string pesronsId = person.Id;
            Assert.IsTrue(Repo.GetById(pesronsId) == person);
        }


        [TestMethod]
        public void SignIn()
        {
            LibaryRepository Repo = new LibaryRepository();
            Person person = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none",12,"123456");
            Assert.IsNull(Repo.SignIn(person, "12345678"));
            Assert.IsNotNull(Repo.SignIn(person, "123456"));
        }
        [TestMethod]
        public void CostumerSignUp()
        {
            LibaryRepository Repo = new LibaryRepository();
            Assert.IsNotNull(Repo.CostumerSignUp("999984131", "Fname1", "Lname", "Rehovot", "none", 12, "123456"));
            Assert.ThrowsException<Exception>(()=>(Repo.CostumerSignUp("999984131", "Fname1", "Lname", "Rehovot", "none", 12, "123456")));
        }
        [TestMethod]
        public void EmployeeSignUp()
        {
            LibaryRepository Repo = new LibaryRepository();
            Assert.IsNotNull(Repo.EmployeeSignUp("999984131", "Fname1", "Lname", "Rehovot", "none", "12345", 12, "123456"));
            Assert.ThrowsException<Exception>(() => (Repo.EmployeeSignUp("999984131", "Fname1", "Lname", "Rehovot", "none", "12345", 12, "123456")));
        }
    }
}