using System;
using System.Text;

namespace LibaryModel
{
    /// <summary>
    /// A class container for the Borrow details structs of a person
    /// </summary>
    public class BorrowDetailList
    {
        private BorrowDetail[] _borrows;

        public BorrowDetail[] Borrows { get => _borrows; set => _borrows = value; }

        public BorrowDetailList(int length)
        {
            Borrows = new BorrowDetail[length];
        }
        /// <summary>
        ///  Search the unique Guid in the borrowing list
        /// </summary>
        /// <param name="itemId"> unique Guid to search</param>
        /// <returns></returns>
        public BorrowDetail this[Guid itemId]
        {
            get
            {
                foreach (BorrowDetail bo in Borrows)
                    if (itemId == bo.itemId)
                        return bo;
                return new BorrowDetail().Empty;
            }
        }
        /// <summary>
        /// return whether the libary item can be borrowd
        /// if so borrow it
        /// else return false
        /// </summary>
        /// <param name="lib"></param>
        /// <returns></returns>
        public bool TryBorrow(LibaryItem lib)
        {
            if (lib != null)
            {
                for (int i = 0; i < Borrows.Length; i++)
                {
                    if (!Contains(lib.ItemId)&&Borrows[i].Equals(new BorrowDetail().Empty))
                    {
                        Borrows[i] = new BorrowDetail(lib.ItemId, lib.Name, DateTime.Today);
                        lib.IsBorrowed = true;
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// return whether the libary item can be returned
        /// if so return it</summary>
        /// else return false<param name="lib"></param>
        /// <returns></returns>
        public bool TryReturn(LibaryItem lib)
        {
            if (lib != null)
            {
                for (int i = 0; i < Borrows.Length; i++)
                {
                    if (Borrows[i].itemId == lib.ItemId)
                    {
                        Borrows[i].DateReturnd = DateTime.Today;
                        lib.IsBorrowed = false;
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Return the index of Guid
        /// </summary>
        /// <param name="otherId"></param>
        /// <returns></returns>
       
        public int IndexOf(Guid otherId)
        {
            for (int i = 0; i < Borrows.Length; i++)
            {
                if (Borrows[i].itemId == otherId)
                    return i;
            }
            return -1;
        }
        public bool Contains(Guid itemId)
        {
            return IndexOf(itemId) != -1;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (BorrowDetail bd in Borrows)
                sb.AppendLine(bd.ToString());
            return sb.ToString();
        }
    }

    /// <summary>
    /// Struct of details contain items ID name and dates
    /// </summary>
    public struct BorrowDetail
    
    {
        public Guid itemId { get; set; }

        public string ItemsName { get; set; }

        public DateTime DateTake { get; set; }

        public DateTime DateReturnd { get; set; }
        public BorrowDetail Empty
        {
            get
            {
                return new BorrowDetail()
                {
                    itemId = Guid.Empty,
                    ItemsName = string.Empty,
                    DateTake = DateTime.MinValue,
                    DateReturnd = DateTime.MinValue
                };
            }
            set
            {
                itemId = Guid.Empty;
                ItemsName = string.Empty;
                DateTake = DateTime.MinValue;
                DateReturnd = DateTime.MinValue;
            }
        }

        public BorrowDetail(Guid id, string itemsName, DateTime dateTake)
        {
            itemId = id;
            ItemsName = itemsName;
            DateTake = dateTake;
            DateReturnd = DateTime.MaxValue;
        }

        public override string ToString()
        {
            if (DateReturnd == DateTime.MaxValue)
                return $"{itemId} {ItemsName} borrowed in {DateTake}";
            else
            {
                return $"{itemId} {ItemsName} borrowed in {DateTake} returned in {DateReturnd} ";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is BorrowDetail otherStruct)
            {
                return itemId == otherStruct.itemId;
            }
            return false;
        }
    }
}
