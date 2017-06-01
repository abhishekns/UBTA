#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : ListEx.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System.Collections.Generic;

namespace ubta.Common
{
    public class ListChangeEventArgs<T>
    {
        private List<T> myItems = new List<T>();
        public ListChangeEventArgs(T item)
        {
            myItems.Add(item);
        }
        public ListChangeEventArgs(IList<T> changeItems)
        {
            myItems.AddRange(changeItems);
        }
        public IList<T> Items
        {
            get
            {
                return myItems;
            }
        }
    }
    public delegate void Change<T>(IListEx<T> source, ListChangeEventArgs<T> lcea);
    public interface IListEx<T> : IList<T>
    {
        event Change<T> Inserting;
        event Change<T> Inserted;
        event Change<T> Removing;
        event Change<T> Removed;
        void AddRange(IList<T> controllers);
        IListEx<T> Clone();
    }
    public class ListEx<T> : List<T>, IListEx<T>
    {
        public ListEx() : base()
        {
        }

        public ListEx(ICollection<T> coll)
        {
            this.AddRange(coll);
        }

        public ListEx(IListEx<T> list)
        {
            this.AddRange(list);
        }

        #region IList<T> Members


        new public void Insert(int index, T item)
        {
            OnInserting(new ListChangeEventArgs<T>(item));
            base.Insert(index, item);
            OnInserted(new ListChangeEventArgs<T>(item));
        }

        private void OnInserted(ListChangeEventArgs<T> listChangeEventArgs_in)
        {
            if (null != Inserted)
            {
                Inserted(this, listChangeEventArgs_in);
            }
        }

        private void OnInserting(ListChangeEventArgs<T> listChangeEventArgs_in)
        {
            if (null != Inserting)
            {
                Inserting(this, listChangeEventArgs_in);
            }
        }

        private void OnRemoved(ListChangeEventArgs<T> listChangeEventArgs_in)
        {
            if (null != Removed)
            {
                Removed(this, listChangeEventArgs_in);
            }
        }

        private void OnRemoving(ListChangeEventArgs<T> listChangeEventArgs_in)
        {
            if (null != Removing)
            {
                Removing(this, listChangeEventArgs_in);
            }
        }

        new public void RemoveAt(int index)
        {
            OnRemoving(new ListChangeEventArgs<T>(this[index]));
            base.RemoveAt(index);
            OnRemoved(new ListChangeEventArgs<T>(this[index]));
        }

        #endregion

        #region ICollection<T> Members

        new public void Add(T item)
        {
            OnInserting(new ListChangeEventArgs<T>(item));
            base.Add(item);
            OnInserted(new ListChangeEventArgs<T>(item));
        }

        new public void Clear()
        {
            List<T> l = new List<T>();
            l.AddRange(this);
            OnRemoving(new ListChangeEventArgs<T>(l));
            base.Clear();
            OnRemoved(new ListChangeEventArgs<T>(l));
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        new public bool Remove(T item)
        {
            OnRemoving(new ListChangeEventArgs<T>(item));
            bool r = base.Remove(item);
            OnRemoved(new ListChangeEventArgs<T>(item));
            return r;
        }

        #endregion

        #region IListEx<T> Members

        public event Change<T> Inserting;

        public event Change<T> Inserted;

        public event Change<T> Removing;

        public event Change<T> Removed;

        #endregion

        #region IListEx<T> Members


        public void AddRange(IList<T> origList)
        {
            base.AddRange(origList);
        }

        public IListEx<T> Clone()
        {
            return new ListEx<T>(this);
        }

        #endregion
    }
}