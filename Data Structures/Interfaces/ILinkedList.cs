using System.Collections.Generic;

public interface ILinkedList<T> : IEnumerable<T>
{
	//Cantidad total de elementos
	int Count { get; }
    //Lectura y escritura por indice, si no sabes que hacer pregunta.
    T this[int index] { get; set; }

    //Primer elemento de la lista
    T First { get; }
	//Ultimo elemento de la lista
	T Last { get; }

	//Limpia la lista
	void Clear();

	//Agrega el valor al principio de la lista
	void AddFirst(T value);
	//Agrega el valor al final de la lista
	void AddLast(T value);

	//Insertar en el indice
	void InsertAt(int index, T value);
	//Remover el indice
	void RemoveAt(int index);

	//Remueve la primera ocurrencia
	void RemoveFirst(T value);
	//Remueve la ultima ocurrencia
	void RemoveLast(T value);

	//Remueve el primer elemento
	void RemoveFirst();
	//Remueve el ultimo elemento
	void RemoveLast();

	//Pregunta si la lista contiene el valor
	bool Contains(T value);
}
