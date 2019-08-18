using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T> : IPriorityQueue<T>, IEnumerable<T>
{
    List<KeyValuePair<float, T>> _priorityQueue = new List<KeyValuePair<float, T>>();

    /// <summary>
    /// Clase Auxiliar que permite guardar un par de valores relacionados entre sí.
    /// </summary>
    /// <typeparam name="K">El tipo de dato para la key.</typeparam>
    /// <typeparam name="V">El tipo de dato para el valor.</typeparam>
    class KeyValuePair<K,V>
    {
        public K key;
        public V val;

        public KeyValuePair(K key, V val)
        {
            this.key = key;
            this.val = val;
        }
    }

    public bool IsEmpty { get { return _priorityQueue.Count == 0; } }

    public void Enqueue(float priority, T data)
    {
        //Creo el KeyValuePair.
        var valuePair = new KeyValuePair<float, T>(priority, data);

        if (_priorityQueue.Count == 0)
        {
            _priorityQueue = new List<KeyValuePair<float, T>>() { valuePair };
            return;
        }
        else
        {
            /*
             * Acá lo que vamos haciendo es añadir el elemento al final, y luego escalarlo
             * hacia arriba si su valor es menor al padre.
            */ 
            //if (!_priorityQueue.Contains(valuePair))
            _priorityQueue.Add(valuePair);//Añadimos el par al array.

            int lastIndex = _priorityQueue.Count - 1;

            int currentIndex = lastIndex;
            int parentIndex = getParentIndex(currentIndex);

            var Current = _priorityQueue[currentIndex];
            var parent = _priorityQueue[parentIndex];

            while (Current.key < parent.key)
            {
                SwapElements(parentIndex,currentIndex);

                currentIndex = parentIndex;
                if (currentIndex == 0) break;
                Current = _priorityQueue[currentIndex];

                parentIndex = getParentIndex(currentIndex);
                parent = _priorityQueue[parentIndex];
            }
        }
    }
    public T Dequeue()
    {
        if (IsEmpty) return default(T);
        var ReturnValue = _priorityQueue[0].val;//Guardo una referencia al objeto que voy a devolver.

        SwapElements(0, _priorityQueue.Count - 1);//Swapeo con el ultimo elemento.
        _priorityQueue.RemoveAt(_priorityQueue.Count - 1);
        if (IsEmpty) return ReturnValue;//Si queda vacío, corto; Si no...
        var CurrentElement = _priorityQueue[0]; //Tomo el primer y nuevo elemento.

        //Calculo los hijos y guardo sus referencias.
        KeyValuePair<float, T> rightChildren = null;
        KeyValuePair<float, T> LeftChildren = null;
        CalculateChildren(0, out rightChildren, out LeftChildren);

        // Si corresponde, escalalamos el elemento hacia abajo.
        int CurrentIndex = 0;
        while (LeftChildren != null && CurrentElement.key > LeftChildren.key)
        {
            int newElementIndex;
            if (rightChildren != null && rightChildren.key < LeftChildren.key) //Si existe el rightChildren y su valor es incluso menor al leftChildren...
                newElementIndex = GetRightChildrenIndex(CurrentIndex);//Será nuestro nuevo elemento.
            else
                newElementIndex = GetLeftChildIndex(CurrentIndex);//Sino, nos quedamos con el leftChildren.

            SwapElements(CurrentIndex, newElementIndex);//intercambiamos los nodos.

            CurrentElement = _priorityQueue[newElementIndex];//Actualizamos nuestro nodo actual.
            CurrentIndex = newElementIndex;//Actualizamos el índice de nuestro nodo actual.
            CalculateChildren(CurrentIndex, out rightChildren, out LeftChildren);//Actualizamos los hijos de nuestro nodo.
        }
        return ReturnValue;
    }
    public T Peek()
    {
        return IsEmpty ? default(T) : _priorityQueue[0].val;
    }

    void SwapElements(int key1, int key2)
    {
        var firstElement = _priorityQueue[key1];
        _priorityQueue[key1] = _priorityQueue[key2];
        _priorityQueue[key2] = firstElement;
    }
    int GetLeftChildIndex(int elementIndex)
    {
        return elementIndex * 2 + 1;
    }
    int GetRightChildrenIndex(int elementIndex)
    {
        return elementIndex * 2 + 2;
    }
    int getParentIndex(int elementIndex)
    {
        return elementIndex - 1 == 0 ? 0 : (elementIndex - 1) / 2;
    }
    void CalculateChildren(int index, out KeyValuePair<float, T> rightChildren, out KeyValuePair<float,T> leftChildren)
    {
        int lastElementIndex = _priorityQueue.Count - 1;
        int leftChildIndex = GetLeftChildIndex(index);
        leftChildIndex = GetLeftChildIndex(index);//Recalculo el index del hijo izquierdo.
        int rightChildrenIndex = GetRightChildrenIndex(index);//Recalculo el index del hijo derecho.

        rightChildren = rightChildrenIndex < lastElementIndex ?
            _priorityQueue[rightChildrenIndex] : null;//Actualizo mi hijo derecho.

        leftChildren = leftChildIndex < lastElementIndex ?
            _priorityQueue[leftChildIndex] : null;//Actualizo mi hijo izquierdo.
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _priorityQueue)
            yield return item.val;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
