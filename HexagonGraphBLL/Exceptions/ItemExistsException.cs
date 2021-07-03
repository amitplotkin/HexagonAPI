using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonGraphBLL.Exceptions
{
    public class ItemExistsException<T>:Exception
    {
        public T item;
       public  ItemExistsException(T Item):base($"the {typeof(T)} exists int the graph")
        {
            this.item = Item;
            
        }
            
    }
}
