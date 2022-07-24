using System;
using System.Collections.Generic;
using System.Linq;

namespace LibaryModel
{
    public abstract class Person
    {
        public const int MAX_BORROWING = 5;
        protected const string DEFAULT_PASSWORD = "1234ABCD";
        public const int MAX_PASSWOD_LENGTH = 12;

        private readonly string _id;
        private string _firstName;
        private string _lastName;
        private string _city;
        private string _street;
        private int _houseNumber;
        private string _password;

        private BorrowDetailList _borrowingList;
        private int _borrowingCount;
        private double _discountPercentage = 0;

        public string Id => _id;
        public string FirstName { get => _firstName;  set => _firstName = value; }
        public string LastName { get => _lastName;  set => _lastName = value; }
        public string City { get => _city;  set => _city = value; }
        public string Street { get => _street;  set => _street = value; }
        public int HouseNumber { get => _houseNumber;  set => _houseNumber = (value <= -1) ? -1 : value; }
        public int BorrowingCount { get => _borrowingCount; set => _borrowingCount = value; }
        public double DiscountPerCent { get => _discountPercentage; set => _discountPercentage = value > 50 ? 50 : value; }
        public BorrowDetailList BorrowingList { get => _borrowingList; set => _borrowingList = value; }
        protected Person(string id, string firstName, string lastName, string city, string street, int houseNumber = -1, string password = DEFAULT_PASSWORD)
        {
            if (Validation(id))
                _id = id;
            else
                throw new ArgumentException($"Not a valid ID for {firstName} {lastName}");
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            _password = password;
            BorrowingCount = 0;
            BorrowingList = new BorrowDetailList(MAX_BORROWING);
        }
        public static bool Validation(string id)
        {
            int sum = 0, temp;
            if (id.Length != 9)
                return false;
            id.ToCharArray();
            for (int i = 0; i < 9; i++)
            {
                if (int.TryParse(id[i].ToString(), out temp))
                {
                    if (i % 2 == 1 && i != 8)
                    {
                        temp *= 2;
                        temp = temp > 9 ?1 + temp % 10 : temp;
                    }
                    sum += temp;
                }
                else
                {
                    return false;
                }
            }
            if (sum % 10 == 0)
                return true;
            else
                return false;
        }
        public bool ChangePassword(string previous, string newPassword)
        {
            if (newPassword.Length < MAX_PASSWOD_LENGTH)
            for (int i = 0; i < newPassword.Length; i++)
            {
                if (!int.TryParse(newPassword[i].ToString(), out _) && !char.TryParse(newPassword[i].ToString(), out _))
                    return false;
            }
            if (previous == _password)
            {
                _password = newPassword;
                return true;
            }
            return false;
        }
        public bool CheckPassword(string password)
        {
            return password == _password;
        }       
        public virtual bool Borrow(LibaryItem lib)
        {
            if (BorrowingList.IndexOf(lib.ItemId) == -1 && BorrowingCount < MAX_BORROWING && lib.IsBorrowed == false)
                if (BorrowingList.TryBorrow(lib))
                {
                    BorrowingCount++;
                    return true;
                }
            return false;
        }
        public virtual bool Return(LibaryItem lib)
        {
            bool returned = false;
            int idx = BorrowingList.IndexOf(lib.ItemId);
                if (idx != -1 && BorrowingList.TryReturn(lib))
            {
                BorrowingCount--;
                BorrowingList.Borrows[idx] = new BorrowDetail().Empty;
                returned = true;
            }
            return returned;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} {City} {Street} {HouseNumber}";
        }
        public override bool Equals(object obj)
        {
            Person other = obj as Person;
            if (other != null)
                return _id == other._id;
            return false;
        }
    }
}