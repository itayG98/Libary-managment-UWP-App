using System;
using System.Collections.Generic;
using System.Linq;

namespace LibaryModel
{
    /// <summary>
    /// A class represent a Book item in a libary
    /// </summary>
    public class Book : LibaryItem
    {
        public const double DEFAULT_BOOK_PRICE = 100;
        public static Dictionary<string, int> Verified_Publishers;


        public ISBN _isbn;
        private int _publisher;
        private string _authors;

        static Book()
        {
            Verified_Publishers = new Dictionary<string, int>();
        }

        public Book(int publisher, int serialNum, string name, DateTime printedDate, string authors, string description = "info", double price = DEFAULT_BOOK_PRICE, double discountRate = 0, int country = 965) : base(name, printedDate, description, price, discountRate)
        {
            _isbn = new ISBN(publisher, serialNum, country);
            Publisher = publisher;
            Authors = authors;
        }

        public Book(int publisher, int serialNum, string name, DateTime printedDate, string authors, double price = DEFAULT_BOOK_PRICE, int country = 965) : base(name, printedDate, price)
        {
            _isbn = new ISBN( publisher, serialNum, country);

            Publisher = publisher;
            Authors = authors;

        }

        public string Authors
        {
            get
            {
                    return _authors;
            }
            private set
            {
               _authors = value;
            }
        }

        public int Publisher { get => _publisher; set => _publisher = value; }

        public override bool Equals(object obj)
        {
            Book other = obj as Book;
            if (other != null)
                return _isbn.Equals(other._isbn);
            return false;
        }

        public override string ToString()
        {
            return $"Book {Name} {_isbn} cost {Price:C} by {string.Join(" ", Authors) }";
        }
    }
}
