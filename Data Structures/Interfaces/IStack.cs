using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStack<T>
{
    //Tiene algun elemento?
    bool IsEmpty { get; }
    //Cantidad de elementos
    int Count { get; }

    //Inserta un elemento al tope de la pila
    void Push(T element);
    //Saca el primer elemento de la pila
    T Pop();

    //Muestra el primer elemento de la pila
    T Peek();

    //Limpia la cola
    void Clear();
}
