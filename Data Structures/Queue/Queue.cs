public class Queue<T> : IQueue<T>
{
	LinkedList<T> myQ = new LinkedList<T>();
	public bool IsEmpty
	{
		get{ return Count == 0; }
	}

	public int Count
	{
		get { return myQ.Count; }
	}

	public void Clear()
	{
		myQ.Clear();
	}

	public T Dequeue()
	{
		T returnElement = myQ.First;
		myQ.RemoveFirst();
		return returnElement;
	}

	public void Enqueue(T element)
	{
		myQ.AddLast(element);
	}

	public T Peek()
	{
		return myQ.First;
	}
}
