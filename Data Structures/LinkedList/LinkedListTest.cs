using System.Collections.Generic;
using UnityEngine;

public class LinkedListTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Comienzo test de lista enlazada ------------------>");

        //-------------------> Inserte aqui su lista enlazada.
        //-------------------> vvvvvvvvvvvvvvvvvvvvvvvv   <
        ILinkedList<int> dinamicArray = new LinkedList<int>();
        //-------------------> ^^^^^^^^^^^^^^^^^^^^^^^^   <

        dinamicArray.AddFirst(10);
        dinamicArray.AddFirst(20);
        dinamicArray.AddFirst(30);

        print("E1: " + dinamicArray[0] + " E2: " + dinamicArray[1] + " E3: " + dinamicArray[2]);

        if (dinamicArray.Count != 3)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { 30, 20, 10 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        dinamicArray.AddLast(40);
        dinamicArray.AddLast(50);
        dinamicArray.AddLast(60);

        print("E1: " + dinamicArray[0] + " E2: " + dinamicArray[1] + " E3: " + dinamicArray[2] + " E4: " + dinamicArray[3] + " E5: " + dinamicArray[4] + " E6: " + dinamicArray[5]);


        if (dinamicArray.Count != 6)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { 30, 20, 10, 40, 50, 60 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        if (dinamicArray.First != 30)
            Debug.LogWarning("First no vale lo que deberia.");

        if (dinamicArray.Last != 60)
            Debug.LogWarning("Last no vale lo que deberia.");

        dinamicArray.RemoveFirst();

        if (dinamicArray.Count != 5)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { 20, 10, 40, 50, 60 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        if (dinamicArray.First !=20)
            Debug.LogWarning("First no vale lo que deberia.");

        dinamicArray.RemoveLast();

        if (dinamicArray.Count != 4)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { 20, 10, 40, 50 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        if (dinamicArray.Last != 50)
            Debug.LogWarning("First no vale lo que deberia.");

        if (dinamicArray.Contains(30) || !dinamicArray.Contains(20))
            Debug.LogWarning("Hay un problema con el contains");

        dinamicArray.Clear();

        if (dinamicArray.Count != 0)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>()))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        Debug.Log("Si llega hasta aca sin loguear nada esta todo correcto.");

        Debug.Log("Fin de test de lista enlazada <--------------------");
    }

    bool CheckEquality(ILinkedList<int> list, List<int> checker)
    {

        for (int i = 0; i < list.Count; i++)
        {
            if (!checker[i].Equals(list[0]))
            {
                print("Value in: " + i + " is: " + checker[i] + " instead of: " + list[i] + ".");
                return false;
            }
        }
        return true;
    }
}
