using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryModel;

namespace DB_Libary
{
    public class Logic
    {
        public static LibaryRepository Repo;
        public List<Person> persons;
        public List<LibaryItem> libaryItems;
        private LibaryItem _currentItem;
        private Person _signed;
        private Person _personToEdit;
        public Person Signed { get => _signed; private set => _signed = value; }
        public string GetName { get => $"{Signed.FirstName} {Signed.LastName}"; }
        public LibaryItem CurrentItem { get => _currentItem; set => _currentItem = value; }
        public Person PersonToEdit { get => _personToEdit; set => _personToEdit = value; }

        static Logic()
        {
            Repo = new LibaryRepository();
        }
        public Logic()
        {
            Signed = null;
            CurrentItem = null;
            PersonToEdit = null;
            UpdateLogicLists();
        }

        public string DeleteCurrentItem()
        {
            if (CurrentItem != null && CurrentItem.IsBorrowed == false)
            {
                return $"Deleted {Repo.Delete(CurrentItem)}";
            }
            else
                throw new Exception("Couldnt succseed deleting item");
        }
        public void UpdateLogicLists()
        {
            persons = Repo.GetsSortedBy(new IComparerFirstName()).ToList();
            libaryItems = Repo.GetsSortedBy(new IComparerByItemName()).ToList();
        }
        public void ChooseItem(object chosen)
        {
            CurrentItem = chosen as LibaryItem;
            if (CurrentItem == null)
                throw new Exception("Cant choose non Libary Item object");
            else if (!Repo.Contain(CurrentItem))
                throw new Exception("This item is non chooseable");
        }
        public void Sort(IComparer<LibaryItem> comp)
        {
            libaryItems.Sort(comp);
        }
        public void Sort(IComparer<Person> comp)
        {
            persons.Sort(comp);
        }
        public void ClearPerson()
        {
            PersonToEdit = null;
        }
        public bool TrySignIn(string id, string password)
        {
            if (!Person.Validation(id))
                return false;
            Person TrySigned = persons.Find((p) => p.Id == id);
            if (TrySigned != null)
            {
                Signed = Repo.SignIn(TrySigned, password);
                if (Signed != null)
                    return true;
            }
            return false;
        }
        public Person CostumerSignUp(string id, string fname, string lname, string city, string street, int houseNumber = -1, string password = "1234ABCD")
        {
            Signed = Repo.CostumerSignUp(id, fname, lname, city, street, houseNumber, password);
            if (Signed == null)
                throw new Exception("Could'nt Sign you up check fields");
            UpdateLogicLists();
            return Signed;
        }
        public Person EmployeSignUp(string id, string fname, string lname, string city, string street, string ManagerPassword, int houseNumber = -1, string password = "1234ABCD")
        {
            Signed = Repo.EmployeeSignUp(id, fname, lname, city, street, ManagerPassword, houseNumber, password);
            if (Signed == null)
                throw new Exception("Can't Sign you as employee wrong password");
            UpdateLogicLists();
            return Signed;
        }
        public void SignOut()
        {
            Signed = null;
        }
        public List<LibaryItem> MyItems()
        {
            return libaryItems.FindAll((lib) => Signed.BorrowingList.Contains(lib.ItemId));
        }
        public string AuthorOrEditors()
        {
            if (CurrentItem is Book)
            {
                Book temp = (Book)CurrentItem;
                return string.Join(",", temp.Authors);
            }
            else
            {
                Journal temp = (Journal)CurrentItem;
                return string.Join(",", temp.Editors);
            }
        }
        public bool Buy()
        {
            if (CurrentItem != null && Repo.Contain(CurrentItem) && CurrentItem.IsBorrowed == false)
            {
                if (Repo.Delete(CurrentItem) != null)
                {
                    UpdateLogicLists();
                    ClearItem();
                    return true;
                }
            }
            return false;
        }
        public bool Borrow()
        {
            if (CurrentItem != null && Signed != null && CurrentItem.IsBorrowed == false)
            {
                if (Repo.Borrow(Signed, CurrentItem) == CurrentItem)
                {
                    UpdateLogicLists();
                    return true;
                }
            }
            return false;
        }
        public bool Return()
        {
            if (CurrentItem != null && Signed != null)
            {
                if (Repo.ReturnBook(Signed, CurrentItem) == CurrentItem)
                {
                    UpdateLogicLists();
                    return true;
                }
            }
            return false;
        }
        public void ClearItem()
        {
            CurrentItem = null;
        }
        public bool NameValidity(string s)
        {
            foreach (char ch in s)
            {
                if (!char.IsLetter(ch) && ch != ' ')
                    return false;
            }
            return true;
        }
        public LibaryItem AddJournal(string name, DateTime date, Frequancy Freq, string editors, double price = Journal.DEFAULT_JOURNAL_PRICE)
        {
            return Repo.Add(new Journal(name, date, Freq, editors, price));
        }
        public LibaryItem AddBook(int publisher, int serialNum, string name, DateTime printedDate, string authors, string description = "info", double price = Book.DEFAULT_BOOK_PRICE, double discountRate = 0, int country = 965)
        {
            if (ISBN.PublishersDict.ContainsValue(publisher) && ISBN.PublishersDict.ContainsValue(country))
                publisher = country = 0;
            return Repo.Add(new Book(publisher, serialNum, name, printedDate, authors, description, price, discountRate, country));
        }
    }
}
