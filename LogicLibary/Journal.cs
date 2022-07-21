using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryModel
{
    /// <summary>
    /// A class represent a Journal item in a libary
    /// </summary>
    /// 
    public enum Frequancy { Daily, Weekly, ByWeekly, Monthly, Quarterly, Anualy }
    public class Journal : LibaryItem
    {
        public const double DEFAULT_JOURNAL_PRICE = 50;
        private string _editors;

        public Frequancy JournalFrequancy { get; set; }
        public string Editors { get => _editors; set => _editors = value; }




        public Journal(string name, DateTime printedDate, Frequancy frequancy, string editors ,double price = DEFAULT_JOURNAL_PRICE) : base(name, printedDate, price)
        {
            JournalFrequancy = frequancy;
            Editors = editors;
        }

        public Journal(string name, DateTime printedDate, string description, Frequancy frequancy, string editors, double price = DEFAULT_JOURNAL_PRICE, double discountRate = 0) : base(name, printedDate, description, price, discountRate)
        {
            JournalFrequancy = frequancy;
            Editors = editors;
        }


        public override bool Equals(object obj)
        {
            Journal temp = obj as Journal;
            if (temp != null)
                return Name == temp.Name && JournalFrequancy == temp.JournalFrequancy && Editors == temp.Editors;
            return false;
        }

        public override string ToString()
        {
            return $"Journal {Name} cost {Price:C} by {Editors} Printed in : {PrintedDate.ToLongDateString()}";
        }
    }
}
