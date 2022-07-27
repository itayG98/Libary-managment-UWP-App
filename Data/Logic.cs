using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryModel;

namespace ViewModel_MoockDB
{
    /// <summary>
    /// The logic is the only obj the UI works with directly an
    /// it passed along the digrrent pages
    /// </summary>
    public class Logic
    {
        public static LibaryItemsRepository LibsRepo;
        public static PersonsRepository PersonsRepo;
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
            LibsRepo = new LibaryItemsRepository();
            PersonsRepo = new PersonsRepository();
        }
        public Logic()
        {
            Signed = null;
            CurrentItem = null;
            PersonToEdit = null;
            UpdateLogicLists();
        }

        //Logic methods
        public void UpdateLogicLists()
            //Updates the list from the database
        {
            libaryItems = LibsRepo.GetsSortedBy(new IComparerByItemName()).ToList();
            persons = PersonsRepo.GetsSortedBy(new IComparerFirstName()).ToList();
        }
        public void Sort(IComparer<LibaryItem> comp)
        {
            libaryItems=LibsRepo.GetsSortedBy(comp).ToList();
        }
        public void Sort(IComparer<Person> comp)
        {
            persons=PersonsRepo.GetsSortedBy(comp).ToList();
        }

        // Editing items choosing

        public void ClearItem()
        {
            CurrentItem = null;
        }
        public void ChooseItem(object chosen)
        {
            CurrentItem = chosen as LibaryItem;
            if (CurrentItem == null)
                throw new Exception("Cant choose non Libary Item object");
            else if (!LibsRepo.Contain(CurrentItem))
                throw new Exception("This item is non chooseable");
        }
        public void ClearPerson()
        {
            PersonToEdit = null;
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

        //Iser interactions
        public bool TrySignIn(string id, string password)
        {
            if (!Person.Validation(id))
                return false;
            Person TrySigned = persons.Find((p) => p.Id == id);
            if (TrySigned != null)
            {
                Signed = PersonsRepo.SignIn(TrySigned, password);
                if (Signed != null)
                    return true;
            }
            return false;
        }
        public void SignOut()
        {
            Signed = null;
        }
        public bool CharectersOnly(string s)
            //Validate a string which spouse to cantain only Charecters
        {
            foreach (char ch in s)
            {
                if (!char.IsLetter(ch) && ch != ' ')
                    return false;
            }
            return true;
        }
        public bool Buy()
        {
            if (CurrentItem != null && LibsRepo.Contain(CurrentItem) && CurrentItem.IsBorrowed == false)
            {
                if (LibsRepo.Delete(CurrentItem) != null)
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
                if (LibsRepo.Borrow(Signed, CurrentItem) == CurrentItem)
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
                if (LibsRepo.ReturnBook(Signed, CurrentItem) == CurrentItem)
                {
                    UpdateLogicLists();
                    return true;
                }
            }
            return false;
        }
        public List<LibaryItem> GetMyItems()
        {
            return libaryItems.FindAll((lib) => Signed.BorrowingList.Contains(lib.ItemId));
        }

        //Adding to database
        public LibaryItem AddJournal(string name, DateTime date, Frequancy Freq, string editors, double price = Journal.DEFAULT_JOURNAL_PRICE)
        {
            return LibsRepo.Add(new Journal(name, date, Freq, editors, price));
        }
        public LibaryItem AddBook(int publisher, int serialNum, string name, DateTime printedDate, string authors, string description = "info", double price = Book.DEFAULT_BOOK_PRICE, double discountRate = 0, int country = 965)
        {
            if (ISBN.PublishersDict.ContainsValue(publisher) && ISBN.PublishersDict.ContainsValue(country))
                publisher = country = 0;
            return LibsRepo.Add(new Book(publisher, serialNum, name, printedDate, authors, description, price, discountRate, country));
        }
        public Person CostumerSignUp(string id, string fname, string lname, string city, string street, int houseNumber = -1, string password = "1234ABCD")
        {
            Signed = PersonsRepo.CostumerSignUp(id, fname, lname, city, street, houseNumber, password);
            if (Signed == null)
                throw new Exception("Could'nt Sign you up check fields");
            UpdateLogicLists();
            return Signed;
        }
        public Person EmployeSignUp(string id, string fname, string lname, string city, string street, string ManagerPassword, int houseNumber = -1, string password = "1234ABCD")
        {
            Signed = PersonsRepo.EmployeeSignUp(id, fname, lname, city, street, ManagerPassword, houseNumber, password);
            if (Signed == null)
                throw new Exception("Can't Sign you as employee wrong password");
            UpdateLogicLists();
            return Signed;
        }
        
        //Delete form DataBase
        public string DeletePersonToEdit() 
        {
            if (PersonToEdit.Equals(Signed))
                return "Could not delete sigend user";
            string msg = "Deleted "+PersonsRepo.Delete(PersonToEdit).ToString();
            ClearPerson();
            UpdateLogicLists();
            return msg;
        }
        public string DeleteCurrentItem()
        {
            if (CurrentItem != null && CurrentItem.IsBorrowed == false)
            {
                return $"Deleted {LibsRepo.Delete(CurrentItem)}";
            }
            else
                throw new Exception("Couldnt succseed deleting item");
        }
    }
}
