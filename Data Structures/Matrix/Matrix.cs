using System.Collections;
using System.Collections.Generic;

public class Matrix<T> : IMatrix<T>
{
	T[] Mtrx = new T[1];
	int _width = 0;
	int _height = 0; 

	public Matrix(int Width, int Height)
	{
		_width = Width;
		_height = Height;
		Mtrx = new T[Width * Height];
	}
	public T this[int x, int y]
	{
		get { return Mtrx[(x + y * _width)]; }
		set { Mtrx[x + y * _width] = value; }
	}

	/// <summary>
	/// Retorna la cantidad de columnas que tiene la matriz.
	/// </summary>
	public int Width{get{return _width;}}
	/// <summary>
	/// Retorna la cantidad de filas que tiene la matriz.
	/// </summary>
	public int Height { get { return _height; } }
	/// <summary>
	/// Retorna la cantidad de elementos que contiene la matriz.
	/// </summary>
	public int Lenght { get { return _width * _height; } }

	/// <summary>
	/// Si es válido, retorna una nueva matriz que contiene el rango de elementos dados.
	/// </summary>
	/// <returns></returns>
	public T[] GetRange(int InitialX, int InitialY, int FinalX, int FinalY)
	{
		if (InitialX < 0 || InitialX >= _width)
			throw new System.IndexOutOfRangeException("El índice x0 es ínvalido.");
		if (FinalX < 0 || FinalX >= _width || FinalX < InitialX)
			throw new System.IndexOutOfRangeException("El índice x1 es ínvalido.");
		if (InitialY < 0 || InitialY >= _width)
			throw new System.IndexOutOfRangeException("El índice y1 es ínvalido.");
		if (FinalY < 0 || FinalY >= _width || FinalY < InitialY)
			throw new System.IndexOutOfRangeException("El índice y1 es ínvalido.");

		int NewMatrixDimension = (1 + (FinalX - InitialX)) * ( 1 +(FinalY - InitialY));
		T[] NewMatrix = new T[NewMatrixDimension];
		int element = 0;
		for (int y = InitialY; y <= FinalY; y++)
			for (int x = InitialX; x <= FinalX; x++)
			{
				NewMatrix[element] = Mtrx[x + y * Width];
				element++;
			}
		return NewMatrix;
	}

	/// <summary>
	/// Limpia la Matriz.
	/// </summary>
	public void Clear()
	{
		Mtrx = new T[0];
		_width = 0;
		_height = 0;
	}

	/// <summary>
	/// Chequea si un elemento existe dentro de la matriz.
	/// </summary>
	/// <param name="element">Elemento a chequear.</param>
	/// <returns>Verdadero si encuentra una coincidencia.</returns>
	public bool Contains(T element)
	{
		bool coincidence = false;
		for (int i = 0; i < Mtrx.Length; i++) if (Mtrx[i].Equals(element))
			{
				coincidence = true;
				break;
			}
		return coincidence;
	}
	
	public IEnumerator<T> GetEnumerator()
	{
		for (int i = 0; i < Mtrx.Length; i++)
			yield return Mtrx[i];
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
