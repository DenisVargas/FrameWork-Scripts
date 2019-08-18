public class Stack<T> : IStack<T>
{
	DinamicArray<T> myArr = new DinamicArray<T>();
	public bool IsEmpty
	{
		get { return Count == 0; }
	}

	public int Count
	{
		get { return myArr.Count; }
	}

	public void Clear()
	{
		myArr.Clear();
	}

	public T Pop()
	{
		T last = myArr[myArr.Count - 1];
		myArr.RemoveAt(myArr.Count - 1);
		return last;
	}

	public T Peek()
	{
		return myArr[myArr.Count - 1];
	}

	public void Push(T element)
	{
		myArr.Insert(element, 0);
	}
}
