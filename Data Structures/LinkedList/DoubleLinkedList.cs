using System.Collections;
using System.Collections.Generic;

public class DoubleLinkedList<T> : ILinkedList<T>,IEnumerable<T>
{
	int _count;
	Nodo<T> _first;
	Nodo<T> _last;
	public int Count{get{ return _count; }}
	public T First{ get { return _first.value; } }
	public T Last { get { return _last.value; } }

	public T this[int index]
	{
		get
		{
			var current = _first;
			for (int i = 0; i < index; i++)
			{
				if (i == index) break;
				current = current.next;
			}
			return current.value;
		}

		set
		{
			var current = _first;
			for (int i = 0; i < index; i++)
			{
				if (i == index) break;
				current = current.next;
			}
			current.value = value;
		}
	}

	class Nodo<I>
	{
		public I value;
		public Nodo<I> next;
		public Nodo<I> previous;
		public Nodo(I value, Nodo<I> previous = null, Nodo<I> next = null)
		{
			this.value = value;
			this.previous = previous;
			this.next = next;
		}
	}

	/// <summary>
	/// Añade un elemento al inicio de la lista.
	/// </summary>
	/// <param name="value">Valor que almacenará el elemento.</param>
	public void AddFirst(T value)
	{
		if (_count == 0)
		{
			_first = new Nodo<T>(value);
			_last = _first;
		}
		else
		{
			var oldfirst = _first;
			_first = new Nodo<T>(value,null,oldfirst);
			oldfirst.previous = _first;
		}
		_count++;
	}

	/// <summary>
	/// Añade un elemento al final de la lista.
	/// </summary>
	/// <param name="value">Valor que almacenará el elemento.</param>
	public void AddLast(T value)
	{
		if (_count == 0) AddFirst(value);
		else
		{
			var oldLast = _last;
			_last = new Nodo<T>(value, oldLast);
			oldLast.next = _last;
		}
		_count++;
	}

	/// <summary>
	/// Limpia la lista.
	/// </summary>
	public void Clear()
	{
		_count = 0;
		_first = null;
		_last = null;
	}

	/// <summary>
	/// Chequea si un valor existe dentro de la lista.
	/// </summary>
	/// <param name="value">Valor a encontrar.</param>
	/// <returns>Verdadero si el valor dado existe dentro de la lista.</returns>
	public bool Contains(T value)
	{
		var current = _first;
		bool coincidence = false;
		for (int i = 0; i < _count - 1; i++)
		{
			if (current.value.Equals(value))
			{
				coincidence = true;
				break;
			}
			current = current.next;
		}
		return coincidence;
	}

	/// <summary>
	/// Inserta un nuevo elemento en la posición dada.
	/// </summary>
	/// <param name="index">índice del elemento a insertar.</param>
	/// <param name="value">Valor del elemento que será insertado.</param>
	public void InsertAt(int index, T value)
	{
		if (index > _count) throw new System.IndexOutOfRangeException("El índice dado excede al maximo de elementos contenidos en la Lista.");

		if (index == 0)
		{
			AddFirst(value);
			return;
		}
		if (index == _count - 1)
		{
			AddLast(value);
			return;
		}

		var current = _first;
		for (int i = 0; i <= index; i++)
		{
			if (i == index)
			{
				var ant = current.previous;
				var newE = new Nodo<T>(value, current.previous, current);
				ant.next = newE;
				current.previous = newE;
			}
			current = current.next;
		}
		_count++;
	}

	/// <summary>
	/// Remueve el elemento en el índice dado.
	/// </summary>
	public void RemoveAt(int index)
	{
		if (index > _count || index < 0) throw new System.IndexOutOfRangeException("El índice dado no es válido, o es mayor a la cantidad de elementos disponibles");

		if (index == 0)
		{
			RemoveFirst();
			return;
		}
		if (index == _count - 1)
		{
			RemoveLast();
			return;
		}

		var current = _first;
		for (int i = 0; i <= index; i++)
		{
			if (i == index) current.previous.next = current.next;
			current = current.next;
		}
	}

	/// <summary>
	/// Remueve el primer elemento.
	/// </summary>
	public void RemoveFirst()
	{
		_first = _first.next;
		_first.previous = null;
		_count--;
	}

	/// <summary>
	/// Remueve el último elemento.
	/// </summary>
	public void RemoveLast()
	{
		if (_count == 1)
		{
			Clear();
			return;
		}
		_last = _last.previous;
		_last.next = null;
		_count--;
	}

	/// <summary>
	/// Remueve el primer elemento cuyo valor sea equivalente al valor dado por parámetro.
	/// </summary>
	/// <param name="value">Valor a buscar.</param>
	public void RemoveFirst(T value)
	{
		if (_first.value.Equals(value))
		{
			RemoveFirst();
			return;
		}

		var current = _first;
		for (int i = 0; i < _count; i++)
		{
			if (current.value.Equals(value))
			{
				current.previous.next = current.next;
				break;
			}
			current = current.next;
		}
	}

	/// <summary>
	/// Remueve el ultimo elemento cuyo valor coincida con el valor recibido por parámetro.
	/// </summary>
	/// <param name="value">Valor a buscar.</param>
	public void RemoveLast(T value)
	{
		if (_count == 1 && _first.value.Equals(value))
		{
			Clear();
			return;
		}

		var current = _first;
		Nodo<T> lastFinded = null;
		for (int i = 0; i < _count; i++)
		{
			if (current.value.Equals(value)) lastFinded = current;
			current = current.next;
		}

		if (lastFinded != null)
		{
			lastFinded.previous.next = lastFinded.next;
			lastFinded.next.previous = lastFinded.previous;
			_count--;
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		var current = _first;
		for (int i = 0; i < _count; i++)
		{
			yield return current.value;
			current = current.next;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		yield return GetEnumerator();
	}
}
