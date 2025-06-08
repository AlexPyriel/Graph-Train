using System.Collections.Generic;

namespace Utils
{
    public class PriorityQueue<T>
    {
        private readonly List<(T item, float priority)> _items = new();

        public int Count => _items.Count;

        public void Enqueue(T item, float priority)
        {
            _items.Add((item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            float bestPriority = _items[0].priority;

            for (int i = 1; i < _items.Count; i++)
            {
                if (_items[i].priority < bestPriority)
                {
                    bestPriority = _items[i].priority;
                    bestIndex = i;
                }
            }

            T bestItem = _items[bestIndex].item;
            _items.RemoveAt(bestIndex);

            return bestItem;
        }
    }
}