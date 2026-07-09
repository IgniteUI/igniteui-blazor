using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
namespace System
{
    public class Script
    {
        public static object Literal(string script, params object[] args)
        {
            return null;
        }

        public static object Eval(string s)
        {
            return null;
        }

        public static void Alert(string message)
        {
        }
    }

    //public class ObservableCollection<T>
    //       : List<T>
    //{
    //    public ObservableCollection()
    //        : base()
    //    {
    //    }

    //    public ObservableCollection(IEnumerable<T> source)
    //        : base(source)
    //    {
    //    }

    //    public ObservableCollection(int capacity)
    //        : base(capacity)
    //    {
    //    }
    //}

    public delegate object RenderFunction<T>(T arg);
}

namespace System.Html
{
    public interface IntlNumberFormatOptions
    {

    }

}

namespace System.Collections
{
    public interface IList : ICollection, IEnumerable
    {
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }
        object this[int index] { get; set; }
        int Add(object value);
        void Clear();
        bool Contains(object value);
        int IndexOf(object value);
        void Insert(int index, object value);
        void Remove(object value);
        void RemoveAt(int index);
    }
    public interface ICollection : IEnumerable
    {
        int Count { get; }
        void CopyTo(Array array, int index);
        bool IsSynchronized { get; }
        object SyncRoot { get; }
    }
    public interface IEnumerable
    {
        IEnumerator GetEnumerator();
    }
}


    namespace System
{
    public class Array : IList
    {
        public int Length
        {
            get
            {
                return 0;
            }
        }

        public object this[int index]
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
        public void Clear()
        {

        }

        public static void Resize<T>(ref T[] array, int newSize)
        {
        }

        public int GetLength(int dimension)
        {
            return 0;
        }

        public object Clone()
        {
            return null;
        }

        public static void Clear(Array array, int index, int length) { }

        public static void Copy(Array source, int sourceIndex, Array dest, int destIndex, int count) { }

        public static void Copy(Array sourceArray, Array destinationArray, int length) { }

        public static int IndexOf<T>(T[] array, T value)
        {
            return -1;
        }

        public static void Reverse(Array array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {
                var other = array.Length - i - 1;

                var temp = array[i];
                array[i] = array[other];
                array[other] = temp;
            }
        }
        // TODO: Find proper mappings and clean up
        #region New

        public object GetValue(int index) { return null; }

        public int Rank { get { return 0; } }

        public System.Collections.IEnumerator GetEnumerator() { return null; }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        int IList.Add(object value)
        {
            return -1;
        }

        void IList.Clear()
        {
        }

        bool IList.Contains(object value)
        {
            return false;
        }

        int IList.IndexOf(object value)
        {
            return -1;
        }

        void IList.Insert(int index, object value)
        {
        }

        void IList.Remove(object value)
        {
        }

        void IList.RemoveAt(int index)
        {
        }

        int ICollection.Count
        {
            get { return this.Length; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return null; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        public void CopyTo(Array array, int index)
        {
        }


        #endregion // New
    }
}

