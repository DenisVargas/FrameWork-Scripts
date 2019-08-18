using System.Collections.Generic;

public interface IPriorityQueue<T> : IEnumerable<T>
{
    void Enqueue(float priority, T data);
    T Dequeue();
    T Peek();
    bool IsEmpty { get; }
}
