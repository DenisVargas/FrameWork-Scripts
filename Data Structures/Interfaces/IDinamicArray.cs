using System.Collections.Generic;

public interface IDinamicArray<T> : IEnumerable<T>
{
    //Cantidad de elementos en el array
    int Count { get; }

    //Indice del elemento
    int IndexOf(T element);

    //Agrega un elemento al final del array
    void Add(T element);
    //Inserta un elemento en el indice dado (y corre todo el resto para atras)
    void Insert(T element, int index);

    //Remueve la primera ocurrencia del elemento
    void Remove(T element);
    //Remueve el elemtento en el indice dado (y corre todo el resto para adelante)
    void RemoveAt(int index);

    //Lectura y escritura por indice, si no sabes que hacer pregunta.
    T this[int index] { get; set; }

    //Existe el elemento dentro del array?
    bool Contains(T element);
    //Limpia el array
    void Clear();
}
