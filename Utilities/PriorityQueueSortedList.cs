using System;
using System.Collections.Generic;

namespace Utilities
{
    public class PriorityQueueSortedList<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        public bool IsEmpty {
            get {
                return queue.Count == 0;
            }
        }

        public TItem Dequeue()
        {
            try {
                TPriority first = queue.Keys[0];
                TItem value = queue[first][0];
                queue[first].RemoveAt(0);
                if (queue[first].Count == 0) {
                    queue.Remove(first);
                }
                return value;
            } catch {
                throw new ApplicationException("the priority queue is empty.");
            }
        }

        public void Enqueue(TItem item, TPriority priority)
        {
            if (!queue.ContainsKey(priority)) {
                queue[priority] = new List<TItem>();
            }
            queue[priority].Add(item);
        }

        public bool TryDequeue(out TItem item, out TPriority priority)
        {
            try {
                priority = queue.Keys[0];
                item = queue[priority][0];
                queue[priority].RemoveAt(0);
                if (queue[priority].Count == 0) {
                    queue.Remove(priority);
                }
                return true;
            } catch {
                item = default;
                priority = default;
                return false;
            }
        }

        private SortedList<TPriority, List<TItem>> queue = new SortedList<TPriority, List<TItem>>();
    }
}
