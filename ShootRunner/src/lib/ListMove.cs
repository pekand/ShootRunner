using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class ListMove
    {
        public static void MoveItem<T>(List<T> list, int oldIndex, int newIndex)
        {
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("List is empty or null.");

            // Handle out-of-range oldIndex
            if (oldIndex < 0) oldIndex = 0;
            else if (oldIndex >= list.Count) oldIndex = list.Count - 1;

            T item = list[oldIndex];
            list.RemoveAt(oldIndex);

            // Handle out-of-range newIndex
            if (newIndex < 0) newIndex = 0;
            else if (newIndex >= list.Count) newIndex = list.Count;

            list.Insert(newIndex, item);
        }
    }
}
