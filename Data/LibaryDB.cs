using System;
using System.Collections.Generic;
using LibaryModel;

namespace ViewModel_MoockDB
{
    /// <summary>
    /// A class rpresent a Data Base for the libary
    /// </summary>
    public class LibaryDB
    {
        List<LibaryItem> _libaryItems;
        List<Person> _persons;
        private static LibaryDB _DataInstance;

        private static List<string> _bookGenres;
        private static Dictionary<string, int> _countries;
        private static Dictionary<string, int> _bookPublishers;
        private static List<string> _journalEditors;

        public List<Person> Persons { get => _persons; set => _persons = value; }
        public List<LibaryItem> LibaryItems { get => _libaryItems; set => _libaryItems = value; }
        public static List<string> BookGenres { get => _bookGenres; set => _bookGenres = value; }
        public static Dictionary<string, int> Countries { get => _countries; set => _countries = value; }
        public static Dictionary<string, int> BookPublishers { get => _bookPublishers; set => _bookPublishers = value; }
        public static List<string> Journaleditors { get => _journalEditors; set => _journalEditors = value; }
        public static LibaryDB DataInstance
        {
            get
            {
                if (_DataInstance == null)
                    _DataInstance = new LibaryDB();
                return _DataInstance;
            }
        }

        static LibaryDB()
        {
            Countries = new Dictionary<string, int>();
            BookPublishers = new Dictionary<string, int>();
        }
        private LibaryDB()
        {
            //Known Editors for journals 
            Journaleditors = new List<string>()
            {
                "Avraham b",
                "John D",
                "Josh",
                "Ron",
                "Amir B"
                ,"Roy N",
                "Avraham b"
            };
            //Countries dict
            Countries.Add("Unknown", 000);
            Countries.Add("English", 001);
            Countries.Add("French", 002);
            Countries.Add("Geraman", 003);
            Countries.Add("Israel", 965);
            Countries.Add("Portugal", 972);
            Countries.Add("Mexico", 970);
            Countries.Add("Latvia", 9934);
            Countries.Add("Costa Rica", 9930);

            //Publishers dict
            BookPublishers.Add("Unknown", 000);
            BookPublishers.Add("Stimatzki", 001);
            BookPublishers.Add("Amazon", 002);
            BookPublishers.Add("Kineret", 003);
            BookPublishers.Add("Eitan", 004);
            BookPublishers.Add("Zmora", 005);
            BookPublishers.Add("Global Books", 006);
            BookPublishers.Add("Universal Books", 007);
            BookPublishers.Add("Frech Corazon", 456);
            BookPublishers.Add("Champs-Élysées", 887);
            //Book Genres list
            BookGenres = new List<string>
            {
                "Action_and_adventure",
                "Science_Fiction",
                "Comic",
                "Classic",
                "Science",
                "Fantasy",
                "Short_Stories",
                "Horror",
                "Romance",
                "Detective",
                "Cookbook",
                "Drama", // Last index for Children
                "History",
                "Poetry",
                "Self_Help",
                "Educationals",
                "Computer_Science",
                "Finance",
                "Geography",
                "Math",
            };

            //Add the init static data
            Book.Verified_Publishers = BookPublishers;
            ISBN.CountriesDict = Countries;
            ISBN.PublishersDict = BookPublishers;


            //Libary Items data
            LibaryItems = new List<LibaryItem>()
            {
                new Book(001,67,"Maps of meaning",new DateTime(1900,10,10),"Robert S",150,965),
                new Book(001,67,"Maps of meaning",new DateTime(1900,10,10),"Robert S",150,965),
                new Book(001,67,"Maps of meaning",new DateTime(1900,10,10),"Robert S",150,965),
                new Book(001,67,"Maps of meaning",new DateTime(1900,10,10),"Robert S",150,965),
                new Book(001,67,"Maps of meaning",new DateTime(1900,10,10),"Robert S",150,965),
                new Book(001,67,"Maps of meaning",new DateTime(1900,10,10),"Robert S",150,965),
                new Book(001,67,"Maps of meaning",new DateTime(1900,10,10),"Robert S",150,965),
                new Book(001,120,"Harry potter and the Philosopher's stone",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,121,"Harry potter and the Chamber of secrets",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,122,"Harry potter and the Prisoner of Azkaban",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,123,"Harry potter and the Goblet fire",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,124,"Harry potter and the Order of the phoenix",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,125,"Harry potter and the half bllood prince ",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,126,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,127,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,120,"Harry potter and the Philosopher's stone",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,121,"Harry potter and the Chamber of secrets",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,122,"Harry potter and the Prisoner of Azkaban",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,123,"Harry potter and the Goblet fire",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,124,"Harry potter and the Order of the phoenix",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,125,"Harry potter and the half bllood prince ",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,126,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,127,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,120,"Harry potter and the Philosopher's stone",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,121,"Harry potter and the Chamber of secrets",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,122,"Harry potter and the Prisoner of Azkaban",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,123,"Harry potter and the Goblet fire",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,124,"Harry potter and the Order of the phoenix",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,125,"Harry potter and the half bllood prince ",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,126,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,127,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,120,"Harry potter and the Philosopher's stone",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,121,"Harry potter and the Chamber of secrets",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,122,"Harry potter and the Prisoner of Azkaban",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,123,"Harry potter and the Goblet fire",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,124,"Harry potter and the Order of the phoenix",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,125,"Harry potter and the half bllood prince ",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,126,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,127,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,125,"Harry potter and the half bllood prince ",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,126,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,127,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,126,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,127,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(001,126,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(456,444,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(887,443,"Harry potter and the deathly Hallows part 1",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(887,443,"Harry potter and the deathly Hallows part 2",new DateTime(2000,10,10),"J.K.Rowling",50,965),
                new Book(887,999,"Le Petit Prince",new DateTime(2000,10,10),"Antoine de Saint-Exupéry",99.9,002),
                new Book(887,999,"Le Petit Prince",new DateTime(2000,10,10),"Antoine de Saint-Exupéry",99.9,002),
                new Book(887,999,"Le Petit Prince",new DateTime(2000,10,10),"Antoine de Saint-Exupéry",99.9,002),
                new Book(887,999,"Le Petit Prince",new DateTime(2000,10,10),"Antoine de Saint-Exupéry",99.9,002),


                new Journal("Yedioth Aharonot",DateTime.Today,Frequancy.Daily,"Avraham b",15),
                new Journal("Yedioth Aharonot",DateTime.Today,Frequancy.Daily,"Avraham b",15),
                new Journal("Laisha",DateTime.Today,Frequancy.Monthly,"John D",15),
                new Journal("Laisha",DateTime.Today,Frequancy.Monthly,"John D",15),
                new Journal("Laisha",DateTime.Today,Frequancy.Monthly, "John D",15),
                new Journal("Forbes",new DateTime(2022,7,10),Frequancy.Monthly,"Josh",25),
                new Journal("Forbes",new DateTime(2022,7,10),Frequancy.Monthly,"Josh",25),
                new Journal("Forbes",new DateTime(2022,7,10),Frequancy.Monthly,"Josh",25),
                new Journal("Forbes",new DateTime(2022,7,10),Frequancy.Monthly,"Josh",25),
                new Journal("Forbes",new DateTime(2022,7,10),Frequancy.Monthly,"Josh",25),
                new Journal("Sela group",DateTime.Today,Frequancy.Quarterly,"Roy N",19.9),
                new Journal("Sela group",DateTime.Today,Frequancy.Quarterly,"Roy N",19.9),
                new Journal("Sela group",DateTime.Today,Frequancy.Quarterly,"Roy N",19.9),
                new Journal("Sela group",DateTime.Today,Frequancy.Quarterly,"Roy N",19.9),
                new Journal("Yedioth Aharonot",DateTime.Today,Frequancy.Daily, "Avraham b",15),
                new Journal("Yedioth Aharonot",DateTime.Today,Frequancy.Daily, "Avraham b",15),
                new Journal("Yedioth Aharonot",DateTime.Today,Frequancy.Daily, "Avraham b",15),
            };

            //Persons Data
            Persons = new List<Person>
            {
                Employee.EmloyeSigning("12345","000000000","Manager","Example","Rehovot","none",1,"12345"),
                Employee.EmloyeSigning("12345","999268121","Itay","Getahun","Rehovot","none"),
                Employee.EmloyeSigning("12345","999240872","Kendrick","Lamar","Compotn","none"),
                new Costumer("999984131","Exmpl","Exmpl","Rehovot","none",12),
                new Costumer("999691249","Fname2","Lname","Rehovot","none",11),
                new Costumer("345678320","Fname1 ","Lname","Rehovot","none",10),
                new Costumer("453743742","Fname2 ","Lname","Rehovot","none",10),
                new Costumer("472789320","Fname3 ","Lname","Rehovot","none",10),
                new Costumer("453783474","Fname4 ","Lname","Rehovot","none",10),
                new Costumer("999095433","Fname5 ","Lname","Rehovot","none",10),
                new Costumer("999326176","Fname6 ","Lname","Rehovot","none",10),
                new Costumer("999297849","Fname7 ","Lname","Rehovot","none",10),
                new Costumer("000000117","Fname8 ","Lname","Rehovot","none",10),
                new Costumer("111111118","Fname9 ","Lname","Rehovot","none",10),
                new Costumer("234234235","Fname10 ","Lname","Rehovot","none",5),
                new Costumer("888888880","Fname11 ","Lname","Rehovot","none",5),
                new Costumer("555555556","Fname12 ","Lname","Rehovot","none",5),
                new Costumer("212121214","Fname13 ","Lname","Rehovot","none",5),
                new Costumer("666666664","Fname14 ","Lname","Rehovot","none",5),
            };
        }
    }
}



