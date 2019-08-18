using System.Collections.Generic;

public interface IMatrix<T> : IEnumerable<T>
{
	int Width { get; }
	int Height { get; }
	int Lenght { get; }

	T this[int x, int y] { get; set; }

	bool Contains(T element);
	void Clear();

	T[] GetRange(int x0, int y0, int x1, int y1);
}
