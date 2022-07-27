using LibaryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel_MoockDB
{
    public class LibaryItemsRepository : IRepository<LibaryItem>
    {

        private static LibaryDB mookData;

        static LibaryItemsRepository()
        {
            mookData = LibaryDB.DataInstance;
        }

        public LibaryItem Get(LibaryItem item)
        {
            return GetById(item.ItemId);
        }
        public IQueryable<LibaryItem> Get()
        {
            return mookData.LibaryItems.AsQueryable();
        }
        public IQueryable<LibaryItem> GetsSortedBy(IComparer<LibaryItem> comp)
        //Get IComparer instance and sorting the origin list
        {
            mookData.LibaryItems.Sort(comp);
            return Get();
        }
        public LibaryItem GetById(Guid id)
        {
            return mookData.LibaryItems.Find(lib => lib.ItemId.CompareTo(id) == 0);
        }
        public LibaryItem Delete(LibaryItem item)
        {
            if (item.IsBorrowed == true)
                throw new Exception("Cant Deleate borrowed book ");
            LibaryItem ToDel = mookData.LibaryItems.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (ToDel != null)
            {
                mookData.LibaryItems.Remove(ToDel);
                return item;
            }
            return null;
        }
        public bool Contain(LibaryItem Item)
        {
            if (Item != null)
                return mookData.LibaryItems.FirstOrDefault((lib)=>Item.ItemId==lib.ItemId)!=null;
            return false;
        }
        public LibaryItem Add(LibaryItem item)
        {
            LibaryItem ToAdd = mookData.LibaryItems.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (ToAdd == null)
            {
                mookData.LibaryItems.Add(item);
                return item;
            }
            return null;
        }
        public LibaryItem Borrow(Person p, LibaryItem lib)
        {
            if (p != null && lib != null && lib.IsBorrowed == false)
            {
                if (mookData.LibaryItems.Contains(lib) && p.Borrow(lib))
                    return lib;
            }
            return null;
        }
        public LibaryItem ReturnBook(Person p, LibaryItem lib)
        {
            if (p != null && lib != null)
            {
                if (p.Return(lib))
                    return lib;
            }
            return null;
        }

    }
}
