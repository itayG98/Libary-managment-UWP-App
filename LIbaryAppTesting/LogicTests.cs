using ViewModel_MoockDB;
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

            Logic.LibsRepo.Add(libaryItem);
            Logic.PersonsRepo.Add(person);
            Assert.IsNotNull(mookData.LibaryItems.Find(lib => lib.Equals(libaryItem)));
            Assert.IsNotNull(mookData.Persons.Find(p => p.Equals(person)));

            Logic.LibsRepo.Borrow(person, libaryItem);
            Assert.ThrowsException<Exception>(() => Logic.LibsRepo.Delete(libaryItem));
            Assert.ThrowsException<Exception>(() => Logic.PersonsRepo.Delete(person));

            Assert.IsNotNull(Logic.LibsRepo.ReturnBook(person, libaryItem));

            Assert.IsNotNull(Logic.PersonsRepo.Delete(person));
            Assert.IsNotNull(Logic.LibsRepo.Delete(libaryItem));
        }

        [TestMethod]
        public void GetPersonOrLibaryApp()
        {
            LibaryItemsRepository LibaryRepoRepo = new LibaryItemsRepository();
            PersonsRepository personRepo = new PersonsRepository();
            LibaryItem lib = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Person p = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none");
            Logic.LibsRepo.Add(lib);
            Logic.PersonsRepo.Add(p);

            Assert.AreEqual(personRepo.Get(p), p);
            Assert.AreEqual(LibaryRepoRepo.Get(lib), lib);
        }

        [TestMethod]
        public void GetIquaryAble()
        {
            LibaryItemsRepository libsRepo = new LibaryItemsRepository();
            PersonsRepository personRepo = new PersonsRepository();

            List<LibaryItem> libs1 = libsRepo.Get().ToList();
            List<LibaryItem> libs2 = LibaryDB.DataInstance.LibaryItems;
            for (int i = 0; i < libs1.Count; i++)
                Assert.AreEqual(libs1[i], libs2[i]);
        }
        [TestMethod]
        public void GetByID()
        {
            LibaryItemsRepository LibsRepo = new LibaryItemsRepository();
            PersonsRepository personRepo = new PersonsRepository();

            LibaryItem libaryItem = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Person person = new Costumer("342432424", "Fname1", "Lname", "Rehovot", "none");

            personRepo.Add(person);
            LibsRepo.Add(libaryItem);

            Guid libId = libaryItem.ItemId;
            Assert.AreEqual(LibsRepo.GetById(libId),libaryItem);

            string pesronsId = person.Id;
            Assert.AreEqual(personRepo.GetById(pesronsId),person);
        }

        [TestMethod]
        public void SignIn()
        {
            LibaryItemsRepository LibsRepo = new LibaryItemsRepository();
            PersonsRepository personRepo = new PersonsRepository();
            Person person = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none", 12, "123456");
            Assert.IsNull(personRepo.SignIn(person, "12345678"));
            Assert.IsNotNull(personRepo.SignIn(person, "123456"));
        }
        [TestMethod]
        public void CostumerSignUp()
        {
            LibaryItemsRepository LibsRepo = new LibaryItemsRepository();
            PersonsRepository personRepo = new PersonsRepository();
            Assert.IsNotNull(personRepo.CostumerSignUp("444444442", "Fname1", "Lname", "Rehovot", "none", 12, "123456"));
            Assert.ThrowsException<Exception>(() => (personRepo.CostumerSignUp("444444442", "Fname1", "Lname", "Rehovot", "none", 12, "123456")));
        }
        [TestMethod]
        public void EmployeeSignUp()
        {
            LibaryItemsRepository LibsRepo = new LibaryItemsRepository();
            PersonsRepository personRepo = new PersonsRepository();
            Assert.IsNotNull(personRepo.EmployeeSignUp("342432424", "Fname1", "Lname", "Rehovot", "none", "12345", 12, "123456"));
            Assert.IsNotNull(personRepo.GetById("342432424"));
            Assert.ThrowsException<Exception>(() => (personRepo.EmployeeSignUp("342432424", "Fname1", "Lname", "Rehovot", "none", "12345", 12, "123456")));
        }

        [TestMethod]
        public void Contain()
        {
            LibaryItemsRepository LibsRepo = new LibaryItemsRepository();
            PersonsRepository personRepo = new PersonsRepository();
            LibaryItem lib1 = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Person p1 = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none");
            LibaryItem lib2 = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Person p2 = new Costumer("999017130", "Fname1", "Lname", "Rehovot", "none");
            LibsRepo.Add(lib1);
            personRepo.Add(p1);
            Assert.IsTrue(LibsRepo.Contain(lib1));
            Assert.IsTrue(personRepo.Contain(p1));
            Assert.IsFalse(LibsRepo.Contain(lib2));
            Assert.IsFalse(personRepo.Contain(p2));

            LibsRepo.Delete(lib1);
            LibsRepo.Delete(lib2);

            Assert.IsTrue(LibsRepo.Contain(lib1));
            Assert.IsTrue(personRepo.Contain(p1));
        }


        //Logic

        [TestMethod]
        public void CurrentItem()
        {
            Logic logic = new Logic();
            Journal joExicst =(Journal)logic.AddJournal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Journal joNotExicst = new Journal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            
            logic.ChooseItem(joExicst);
            Assert.AreEqual(joExicst, logic.CurrentItem);
            Assert.ThrowsException<Exception>(()=>logic.ChooseItem(joNotExicst));
        }
        [TestMethod]
        public void ChooseAndDeleteCurrentItem()
        {
            Logic logic = new Logic();
            Journal jo = (Journal)logic.AddJournal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            logic.ChooseItem(jo);
            Assert.AreEqual(jo, logic.CurrentItem);

            logic.ClearItem();
            Assert.IsNull(logic.CurrentItem);

            logic.ChooseItem(jo);
            string msg = logic.DeleteCurrentItem();
            msg.Contains(jo.ToString());
            Assert.IsTrue(logic.libaryItems.Contains(jo));
        }

        [TestMethod]
        public void DeletePerson() 
        {
            Logic logic = new Logic();
            Person Cos = logic.CostumerSignUp("222111114", "Fname1 ", "Lname", "Rehovot", "none", 10);
            Person Emp =logic.EmployeSignUp("222222226", "Fname1 ", "Lname", "Rehovot", "none", "12345", 10);
            logic.PersonToEdit = Cos;
            Assert.IsTrue(logic.DeletePersonToEdit() is string);
            Assert.IsNull(logic.persons.Find((p) => p.Id == "222111114"));
        }
        [TestMethod]
        public void PersonToEditAndClear()
        {
            Logic logic = new Logic();
            Person p = new Costumer("999984131", "Fname1", "Lname", "Rehovot", "none");
            logic.PersonToEdit = p;
            Assert.AreEqual(p, logic.PersonToEdit);
            logic.ClearPerson();
            Assert.IsNull(logic.PersonToEdit);
        }

        [TestMethod]
        public void TrySignInAndOut()
        {
            //Signed Employye ("999268121", "Itay")
            Logic logic = new Logic();
            Assert.IsTrue(logic.TrySignIn("999268121", "1234ABCD"));
            Assert.IsTrue(logic.Signed.Id == "999268121");
            logic.SignOut();
            Assert.IsNull(logic.Signed);
        }

        [TestMethod]
        public void SignUpPersons()
        {
            Logic logic = new Logic();
            Assert.IsNotNull(logic.CostumerSignUp("444443436", "Fname1", "Lname", "Rehovot", "none", 13));
            Assert.IsNotNull(logic.EmployeSignUp("232323238", "Fname1", "Lname", "Rehovot", "none", "12345", 13));
            logic.UpdateLogicLists();
            Assert.IsNotNull(logic.persons.Find((p) => p.Id == "444443436" && p is Costumer));
            Assert.IsNotNull(logic.persons.Find((p) => p.Id == "232323238" && p is Employee));
        }

        [TestMethod]
        public void Buy()
        {
            Logic logic = new Logic();
            Journal jo1 = (Journal)logic.AddJournal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            Journal jo2 = (Journal)logic.AddJournal("Yedioth Aharonot", DateTime.Today, Frequancy.Daily, "Avraham b", 15);
            logic.ChooseItem(jo1);

            Assert.IsTrue(logic.TrySignIn("999268121", "1234ABCD"));
            Assert.IsTrue(logic.Buy());
            jo2.IsBorrowed = true;
            logic.ChooseItem(jo2);
            Assert.IsFalse(logic.Buy());

        }
        [TestMethod]
        public void BorrowAndReturn()
        {
            Logic logic = new Logic();
            Book book = (Book)logic.AddBook(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", "Info", 150, 965);

            Assert.IsTrue(logic.TrySignIn("999268121", "1234ABCD"));

            logic.ChooseItem(book);
            Assert.IsTrue(logic.Borrow());
            Assert.AreEqual(book, logic.CurrentItem);

            Assert.IsFalse(logic.Borrow());
            Assert.IsTrue(logic.Return());
            Assert.IsFalse(logic.Return());
        }

        [TestMethod]
        public void MyItems()
        {
            Logic logic = new Logic();
            Book book1 = (Book)logic.AddBook(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", "Info", 150, 965);
            Book book2 = (Book)logic.AddBook(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", "Info", 150, 965);
            Book book3 = (Book)logic.AddBook(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", "Info", 150, 965);
            Book book4 = (Book)logic.AddBook(001, 67, "Maps of meaning", new DateTime(1900, 10, 10), "Robert S", "Info", 150, 965);
            List<LibaryItem> BorrowedBooks = new List<LibaryItem> { book1, book2, book3, book4 };
            Assert.IsTrue(logic.TrySignIn("999268121", "1234ABCD"));

            logic.ChooseItem(book1);
            Assert.IsTrue(logic.Borrow());
            logic.ChooseItem(book2);
            Assert.IsTrue(logic.Borrow());
            logic.ChooseItem(book3);
            Assert.IsTrue(logic.Borrow());
            logic.ChooseItem(book4);
            Assert.IsTrue(logic.Borrow());

            List<LibaryItem> books = logic.GetMyItems();
            for (int i = 0; i < BorrowedBooks.Count; i++)
                Assert.IsTrue(books[i].Equals(BorrowedBooks[i]));
            Assert.IsTrue(books.Count == BorrowedBooks.Count);
        }
    }
}