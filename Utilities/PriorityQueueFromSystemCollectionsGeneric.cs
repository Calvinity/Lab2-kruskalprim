using System;
using System.Collections.Generic;

namespace Utilities
{
    public class PriorityQueueFromSystemCollectionsGeneric<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        public bool IsEmpty {
            get {
                return queue.Count == 0;
            }
        }
        public TItem Dequeue()
        {
            return queue.Dequeue();
        }
        public void Enqueue(TItem item, TPriority priority)
        {
            queue.Enqueue(item, priority);
        }
        public bool TryDequeue(out TItem item, out TPriority priority)
        {
            return queue.TryDequeue(out item, out priority);
        }

        private readonly PriorityQueue<TItem, TPriority> queue = new PriorityQueue<TItem, TPriority>();
    }
}
