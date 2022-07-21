using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryModel
{
    /// <summary>
    /// A class representing a libary's item in the libary
    /// </summary>
    public abstract class LibaryItem
    {
        private string _name;
        private double _price;
        private double _discountRate;
        private string _description;
        private DateTime _printedDate;
        protected bool _isBorrowed;
        private readonly Guid _itemId;

        /// <summary>
        /// Uniqe GUID id
        /// </summary>
        public Guid ItemId => _itemId;

        /// <summary>
        /// Printing date of the Libary item
        /// </summary>
        public DateTime PrintedDate { get => _printedDate; set => _printedDate = value; }
        /// <summary>
        /// Price of  Libary item
        /// </summary>
        public double Price { get => _price; set => _price = value; }
        /// <summary>
        /// Current discount of book may be differ if a special costumer buy
        /// </summary>
        public double DiscountPercentage { get => _discountRate; set => _discountRate = value; }

        /// <summary>
        /// Descreption of the Libary item
        /// </summary>
        public string Description { get => _description; set => _description = value; }
        /// <summary>
        /// Currrent status of the Libary item
        /// </summary>
        public bool IsBorrowed { get => _isBorrowed; set => _isBorrowed = value; }
        public string Name { get => _name; set => _name = value; }

        protected LibaryItem(string name, DateTime printedDate, string description, double price, double discountRate = 0)
        {
            _itemId = Guid.NewGuid();
            IsBorrowed = false;
            Name = name;
            _printedDate = printedDate;
            Price = price;
            DiscountPercentage = discountRate;
            Description = description;
        }
        protected LibaryItem(string name, DateTime printedDate, double price = 100)
        {
            _itemId = Guid.NewGuid();
            IsBorrowed = false;
            Name = name;
            _printedDate = printedDate;
            Price = price;
            DiscountPercentage = 0;
            Description = string.Empty;
        }

        public override string ToString()
        {
            return $"{Name} {Price} Printed in : {PrintedDate.ToLongDateString()}";
        }
    }


}


