using System.Collections.Generic;
using UnityEngine;

public class DinamicArrayTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Comienzo test de array dinamico ------------------>");

        //-------------------> Inserte aqui su array dinamico.
        //-------------------> vvvvvvvvvvvvvvvvvvvvvvvv   <
        IDinamicArray<int> dinamicArray = new DinamicArray<int>();
        //-------------------> ^^^^^^^^^^^^^^^^^^^^^^^^   <

        dinamicArray.Add(10);
        dinamicArray.Add(20);
        dinamicArray.Add(30);

        if (dinamicArray.Count != 3)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { 10, 20, 30 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        dinamicArray[0] = -10;

        if (!CheckEquality(dinamicArray, new List<int>() { -10, 20, 30 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        dinamicArray.Add(40);

        if (dinamicArray.Count != 4)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { -10, 20, 30, 40 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        dinamicArray.Insert(40, 1);

        if (dinamicArray.Count != 5)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { -10, 40, 20, 30, 40 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        if (!dinamicArray.Contains(20) || !dinamicArray.Contains(40) || dinamicArray.Contains(0))
            Debug.LogWarning("Hay un problema con el contains");

        if (dinamicArray.IndexOf(40) != 1 || dinamicArray.IndexOf(30) != 3)
            Debug.LogWarning("Hay un problema con el IndexOf");

        if (dinamicArray.IndexOf(50) != -1)
            Debug.LogWarning("Hay un problema con el IndexOf");

        dinamicArray.Remove(40);

        if (dinamicArray.Count != 4)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { -10, 20, 30, 40 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        dinamicArray.RemoveAt(2);

        if (dinamicArray.Count != 3)
            Debug.LogWarning("Count no vale lo que deberia.");

        if (!CheckEquality(dinamicArray, new List<int>() { -10, 20, 40 }))
            Debug.LogWarning("Los elementos contenidos no son los correctos.");

        dinamicArray.Clear();

        if (dinamicArray.Count != 0)
            Debug.LogWarning("Count no vale lo que deberia.");

        Debug.Log("Si llega hasta aca sin loguear nada esta todo correcto.");

        Debug.Log("Fin de test de array dinamico <--------------------");
    }

    bool CheckEquality(IDinamicArray<int> array, List<int> checker)
    {
        for (int i = 0; i < checker.Count; i++)
        {
            if (!checker[i].Equals(array[i])) return false;
        }
        return true;
    }

}
