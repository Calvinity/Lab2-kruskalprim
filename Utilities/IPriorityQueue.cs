using System;

namespace Utilities
{
    public interface IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        bool IsEmpty { get; }
        TItem Dequeue();
        void Enqueue(TItem item, TPriority priority);
        bool TryDequeue(out TItem item, out TPriority priority);
    }
}
