using System;
using System.Collections.Generic;

namespace Utilities
{
    public class PriorityQueueHeap<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        public bool IsEmpty {
            get {
                return queue.Count == 0;
            }
        }

        public TItem Dequeue()
        {
            //VerifyHeap(); // Remove later.
            if (queue.Count > 0) {
                KeyValuePair<TPriority, TItem> first = queue[0];
                queue[0] = queue[queue.Count - 1];
                queue.RemoveAt(queue.Count - 1);
                int i = 0;
                while (i < queue.Count) {
                    int left = 2 * i + 1;
                    int right = 2 * i + 2;
                    int c;
                    if (left < queue.Count && right < queue.Count) {
                        c = (queue[left].Key.CompareTo(queue[right].Key) <= 0) ? left : right;
                    } else if (left < queue.Count) {
                        c = left;
                    } else {
                        break;
                    }

                    if (queue[c].Key.CompareTo(queue[i].Key) < 0) {
                        KeyValuePair<TPriority, TItem> tmp = queue[i];
                        queue[i] = queue[c];
                        queue[c] = tmp;
                    } else {
                        break;
                    }
                    i = c;
                }
                //VerifyHeap(); // Remove later.
                return first.Value;
            } else {
                throw new ApplicationException("the priority queue is empty.");
            }
        }

        public void Enqueue(TItem item, TPriority priority)
        {
            //VerifyHeap(); // Remove later.
            KeyValuePair<TPriority, TItem> newPair = new KeyValuePair<TPriority, TItem>(priority, item);
            int i = queue.Count;
            queue.Add(newPair);
            while (i > 0 && queue[i].Key.CompareTo(queue[(i - 1) / 2].Key) < 0) {
                KeyValuePair<TPriority, TItem> tmp = queue[i];
                queue[i] = queue[(i - 1) / 2];
                queue[(i - 1) / 2] = tmp;
                i = (i - 1) / 2;
            }
            //VerifyHeap(); // Remove later.
        }

        public bool TryDequeue(out TItem item, out TPriority priority)
        {
            //VerifyHeap(); // Remove later.
            if (queue.Count > 0) {
                KeyValuePair<TPriority, TItem> first = queue[0];
                queue[0] = queue[queue.Count - 1];
                queue.RemoveAt(queue.Count - 1);
                int i = 0;
                while (i < queue.Count) {
                    int left = 2 * i + 1;
                    int right = 2 * i + 2;
                    int c;
                    if (left < queue.Count && right < queue.Count) {
                        c = (queue[left].Key.CompareTo(queue[right].Key) <= 0) ? left : right;
                    } else if (left < queue.Count) {
                        c = left;
                    } else {
                        break;
                    }

                    if (queue[c].Key.CompareTo(queue[i].Key) < 0) {
                        KeyValuePair<TPriority, TItem> tmp = queue[i];
                        queue[i] = queue[c];
                        queue[c] = tmp;
                    } else {
                        break;
                    }
                    i = c;
                }
                item = first.Value;
                priority = first.Key;
                //VerifyHeap(); // Remove later.
                return true;
            } else {
                item = default;
                priority = default;
                return false;
            }
        }

        private void VerifyHeap()
        {
            bool ok = true;
            for (int i = 0; i < queue.Count; ++i) {
                if (i > 0 && queue[(i - 1)/2].Key.CompareTo(queue[i].Key) > 0) {
                    Console.WriteLine("Bad heap! queue[" + (i - 1) / 2 +
                                      "] {Key is " + queue[(i - 1) / 2].Key + "} " +
                                      " > queue[" + i + "] {Key is " +
                                      queue[i].Key + "}!");
                    ok = false;
                }
                if (i < queue.Count / 2 - 1 && (queue[i].Key.CompareTo(queue[2 * i + 1].Key) > 0 ||
                                                queue[i].Key.CompareTo(queue[2 * i + 2].Key) > 0)) {
                    Console.WriteLine("Bad heap! queue[" + i +
                                      "] {Key is " + queue[i].Key + "} " +
                                      " > (queue[" + 2*i+1 + "] {Key is " +
                                      queue[2*i+1].Key + "} or queue[" + 2*i+2 + "] {Key is " +
                                      queue[2*i+2].Key + "})!");
                    ok = false;
                }
            }
        }

        private List<KeyValuePair<TPriority, TItem>> queue = new List<KeyValuePair<TPriority, TItem>>(100);
    }
}
