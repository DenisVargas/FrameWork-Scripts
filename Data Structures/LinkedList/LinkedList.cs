using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : ILinkedList<T>, IEnumerable<T>
{
	int _count = 0;
	Nodo<T> _first;
	Nodo<T> _Last;
	public int Count
	{
		get
		{
			return _count;
		}
	}
	
	public T First
	{
		get{ return _first.value; }
	}
	public T Last
	{
		get
		{ return _Last.value; }
	}

	public T this[int index]
	{
		get
		{
			var current = _first;
			for (int i = 0; i < _count; i++)
			{
				UnityEngine.MonoBehaviour.print("Asked: " + index + " value returned:" + current.value + " in index: " + i);
				if (i == index) return current.value;
				else current = current.next;
			}
			return current.value;
		}

		set
		{
			var current = _first;
			for (int i = 0; i < index; i++)
			{
				if (i == index) current.value = value;
				current = current.next;
			}
		}
	}

	class Nodo<I>
	{
		public I value;
		public Nodo<I> next;
	}

	/// <summary>
	/// Añade un elemento al principio de la Lista.
	/// </summary>
	/// <param name="value"></param>
	public void AddFirst(T value)
	{
		if (_count == 0)
		{
			_first = new Nodo<T>();
			_first.value = value;
			_first.next = null;
			_Last = _first;
		}
		else
		{
			Nodo<T> original = _first;
			_first = new Nodo<T>();
			_first.value = value;
			_first.next = original;
		}
		_count++;
	}

	/// <summary>
	/// Añade un elemento al final de la Lista.
	/// </summary>
	/// <param name="value"></param>
	public void AddLast(T value)
	{
		if (_count == 0)
			AddFirst(value);
		else
		{
			Nodo<T> nuevoNodo = new Nodo<T>();
			Nodo<T> original = _Last;
			nuevoNodo.value = value;
			nuevoNodo.next = null;
			_Last = nuevoNodo;
			original.next = nuevoNodo;
			_count++;
		}
	}

	/// <summary>
	/// Limpia todos los elementos de la Lista.
	/// </summary>
	public void Clear()
	{
		_count = 0;
		_first = null;
		_Last = null;
	}

	/// <summary>
	/// Determina si un valor existe dentro de la Lista.
	/// </summary>
	/// <param name="value">Valor a buscar.</param>
	/// <returns>True si el valor existe en la Lista.</returns>
	public bool Contains(T value)
	{
		Nodo<T> current = _first;
		while (current.next != null)
		{
			if (current.value.Equals(value))
				return true;

			if (current.next == null)
				break;
			else current = current.next;
		}
		return false;
	}

	/// <summary>
	/// Inserta un nuevo elemento en el indice dado.
	/// </summary>
	/// <param name="index">El índice deseado para el nuevo elemento.</param>
	/// <param name="value">El valor del elemento en el índice dado.</param>
	public void InsertAt(int index, T value)
	{
		throw new System.NotImplementedException();
	}

	/// <summary>
	/// Remueve el elemento en el indice dado.
	/// </summary>
	public void RemoveAt(int index)
	{
		throw new System.NotImplementedException();
	}

	/// <summary>
	/// Remueve la primera coincidencia del valor dado.
	/// </summary>
	/// <param name="value"></param>
	public void RemoveFirst(T value)
	{
		if (_count == 1 && _first.value.Equals(value))
		{
			Clear();
			_count--;
		}

		if (_count > 1)
		{
			Nodo<T> current = _first;
			for (int i = 1; i < _count-1; i++)
			{
				if (current.next != null && !current.next.value.Equals(value))
				{
					current = current.next;
					continue;
				}
				else if (current.next != null && current.next.value.Equals(value))
				{
					if (current.next.next == null)
					{
						current.next = null;
						_count--;
						break;
					}
					else
					{
						Nodo<T> NewConnection = current.next.next;
						current.next.next = null; //¿Hace falta que el nodo eliminado no tenga referencia a su siguiente?.
						current.next = NewConnection;
						_count--;
						break;
					}
				}
			}
		}
	}

	/// <summary>
	/// Remueve el primer elemento de la Lista.
	/// </summary>
	public void RemoveFirst()
	{
		if (_count == 1)
			Clear();
		else
			_first = _first.next;
		_count--;
	}

	/// <summary>
	/// Remueve la ultima ocurrencia.
	/// </summary>
	/// <param name="value">Valor a ser removido</param>
	public void RemoveLast(T value)
	{
		if (_count == 1 && _first.value.Equals(value))
		{
			Clear();
			_count--;
		}
		else if (_count > 1)
		{
			Nodo<T> Current = _first;

			Nodo<T> Coincidence = _first.value.Equals(value) ? _first : null;
			Nodo<T> nextToLastCoincidence = null, previousToLastCoincidence = null;

			for (int i = 1; i < _count - 1; i++)
			{
				if (Current.next.value.Equals(value))
				{
					previousToLastCoincidence = Current;
					Coincidence = Current.next;
					nextToLastCoincidence = Coincidence.next;
				}
				Current = Current.next;
			}

			if (Coincidence != null)
			{
				previousToLastCoincidence.next = nextToLastCoincidence;
				_Last = nextToLastCoincidence == null ? previousToLastCoincidence : nextToLastCoincidence;
				_count--;
			}
		}
	}

	/// <summary>
	/// Remueve el ultimo elemento de la Lista.
	/// </summary>
	public void RemoveLast()
	{
		if (_count == 1)
			Clear();

		if (_count > 1)
		{
			var Current = _first;
			for (int i = 0; i < _count - 1; i++)
			{
				if (Current.next.Equals(_Last))
				{
					_Last = Current;
					Current.next = null;
				}
				else
					Current = Current.next;
			}
		}
		_count--;
	}

	/// <summary>
	/// Permite que esta coleccion sea recorrible por una estructura Foreach.
	/// </summary>
	public IEnumerator<T> GetEnumerator()
	{
		var current = _first;
		for (int i = 0; i < _count; i++)
		{
			yield return current.value;
			current = _first.next;
		}
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		yield return GetEnumerator();
	}
}
