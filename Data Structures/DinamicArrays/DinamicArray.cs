using System.Collections;
using System.Collections.Generic;

public class DinamicArray<T> : IDinamicArray<T>,IEnumerable<T>
{
	T[] array;
	int count;

	public DinamicArray()
	{
		array = new T[1];
		count = 0;
	}
	public DinamicArray(int count)
	{
		array = new T[count * 2];
		this.count = count;
	}

	public T this[int index]
	{
		get{return array[index];}
		set{array[index] = value;}
	}

	public int Count
	{
		get{return count;}
	}

	public void Add(T element)
	{
		count++;
		if (count > array.Length)
			array[count - 1] = element;
		else
		{
			T[] newArray = new T[count * 2];
			for (int i = 0; i < array.Length; i++)
			{
				if (i > count) break;
				if (array[i] == null) continue;

				if (i == count - 1)
					newArray[count - 1] = element;
				else
					newArray[i] = array[i];
			}
			array = newArray;
		}
	}

	/// <summary>
	/// Inserta un elemento nuevo en el índice dado desplazando los consecuentes.
	/// </summary>
	/// <param name="element">Elemento a insertar.</param>
	/// <param name="index">Índice del nuevo elemento.</param>
	public void Insert(T element, int index)
	{
		count++;
		T[] newArray = array.Length < count ? new T[array.Length] : new T[array.Length * 2];
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == null) continue;
			if (i == index) newArray[i] = element;
			else if (i > index) newArray[i] = array[i - 1];
			else newArray[i] = array[i];
		}
		array = newArray;
	}

	/// <summary>
	/// Limipia el array dinamico.
	/// </summary>
	public void Clear()
	{
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = default(T);
		}
		count = 0;
	}

	/// <summary>
	/// Determina si el valor dado existe en el array.
	/// </summary>
	/// <returns>True si el valor existe.</returns>
	public bool Contains(T element)
	{
		for (int i = 0; i < count; i++)
			if( array[i].Equals(element)) return true;
		return false;
	}

	/// <summary>
	/// Devuelve el índice del valor dado si este existe en el array.
	/// </summary>
	/// <returns>Índice del valor dado.</returns>
	public int IndexOf(T element)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Equals(element)) return i;
		}
		return -1;
	}

	/// <summary>
	/// Remueve el primer valor que coincida con el valor indicado.
	/// </summary>
	/// <param name="element">Valor a eliminar.</param>
	public void Remove(T element)
	{
		bool removed = false;
		for (int i = 0; i < count; i++)
			if (array[i].Equals(element))
			{
				removed = true;
				T[] newArray = new T[array.Length];
				for (int J = 0; J < count -1; J++) newArray[J] = J < i ? array[J] : array[J + 1];
				array = newArray;
				break;
			}
		if (removed) count--;
	}

	/// <summary>
	/// Remueve el elemento en el índice dado.
	/// </summary>
	/// <param name="index">Indice del elemento a eliminar.</param>
	public void RemoveAt(int index)
	{
		if (index >= 0 && index < count)
		{
			count--;
			T[] newArray = new T[array.Length];
			for (int i = 0; i < count; i++)
				newArray[i] = i < index ? array[i] : array[i + 1];
			array = newArray;
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		for (int i = 0; i < count; i++)
			yield return array[i];
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		yield return GetEnumerator();
	}
}
//Pregunta: Como devuelvo NULL si estoy usando genéricos y no se que tipo es el "Default";
//Respuesta: Default devuelve 0 cuando el valor por defecto es un int-float-double(numèricos)
//Si se trata de Clases, devolverá NULL como Default.
