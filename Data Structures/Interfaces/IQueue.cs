public interface IQueue<T>
{
    //Tiene algun elemento?
    bool IsEmpty { get; }
    //Cantidad de elementos
    int Count { get; }

    //Inserta un elemento al final de la cola
    void Enqueue(T element);
    //Saca el primer elemento de la cola
    T Dequeue();

    //Muestra el primer elemento de la cola
    T Peek();

    //Limpia la cola
    void Clear();
}
