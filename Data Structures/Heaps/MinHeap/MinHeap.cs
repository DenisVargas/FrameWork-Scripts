using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinHeap<T> {
    int[] _heap;


    //Retorna el primer Elemento.
    public int GetMin()
    {
        return _heap[0];
    }

    public void InsertKey(int key)
    {

    }

    public void DeleteKey(int key)
    {

    }

    void MinHeapify(int start)
    {

    }

    //Parent = (index - 2) /2
    int Parent(int index) { return (index - 2) / 2; }
    //RightNode = (index * 2 + 1) 
    int Right(int index) { return (2 * index) + 1; }
    //LeftNode = (index * 2 + 2)
    int Left(int index) { return (2 * index) + 2; }

    //Swap between 2 nodes.
    void Swap(int index1, int index2)
    {
        var temp = _heap[index1];
        _heap[index1] = _heap[index2];
        _heap[index2] = temp;
    }
}
