using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ordenadores : MonoBehaviour
{
    public int[] lawea = new int[] { 1, 5, 3, 8, 9, 4, 2 };

    public void SelectionSort()
    {
        for (int i = 0; i < lawea.Length; i++)
        {
            var current = lawea[i];
            int index = i;

            for (int j = i + 1; j < lawea.Length; j++)
            {
                if (lawea[j] < current)
                {
                    current = lawea[j];
                    index = j;
                }
            }
        }
    }

    /*
     * Avanzo hacia adelante y voy restando elementos desde atrás.
    */ 
    public void BubbleSort()
    {
        var Contador = lawea.Length;
        while (Contador >= 0)
        {
            for (int i = 0; i < Contador; i++)
            {
                if (i + 1 < Contador)
                {
                    if (lawea[i + 1] < lawea[i])
                    {
                        var oldvalue = lawea[i];
                        lawea[i] = lawea[i + 1];
                        lawea[i + 1] = oldvalue;
                    }
                }
            }
            Contador--;
        }
    }

    /*
     * Recursivo.
    */ 
    public void QuickSort(int[] element, int leftIndex, int rightIndex)
    {
        if (rightIndex - leftIndex == 0)
            return;
        else if (rightIndex - leftIndex == 1)
        {
            //comparo. Si se puede switcheo.
            if (element[rightIndex] < element[leftIndex]) Swap(rightIndex, leftIndex);
            return;
        }

        //Calculo mi pivot: (int)Lenght / 2;
        int pivotIndex = leftIndex + ((rightIndex - leftIndex) / 2);
        int pivot = element[pivotIndex];

        int leftElementIndex = 0;//índice del elemento de la izquierda.
        int righElementIndex = 0;//índice del elemento de la derecha.

        for (int i = 0; i < pivotIndex; i++)//Recorro de izquierda a derecha hasta encontrar el pivot.
        {
            //Si el valor es mayor o igual al pivot.
            if (element[i] >= pivot )// Guardo el valor y su indice.
            {
                leftElementIndex = i;
                for (int j = element.Length - 1; j >= pivotIndex; j--)//Recorro de derecha a izquierda hasta encontrar el pivot.
                {
                    if (j == pivotIndex) break;
                    //Si el valor es menor al pivot.
                    if (element[j] <= pivot)// Guardo el valor y su indice.
                    {
                        righElementIndex = j;
                        //Switcheo los valores de los objetos en los indices dados.
                        if (righElementIndex != pivot || leftElementIndex != pivot)
                            Swap(leftElementIndex, righElementIndex);
                    }
                }
            }
        }

        QuickSort(element, leftElementIndex, pivotIndex);//QuickSort(array, index1, pivot);
        QuickSort(element, pivotIndex, righElementIndex);//QuickSort(element, pivot, index2);
    }

    void Swap(int indexA, int indexB)
    {
        var oldvalue = lawea[indexA];
        lawea[indexA] = lawea[indexB];
        lawea[indexA] = oldvalue;
    }

    public void Start()
    {
        //SelectionSort();
        //BubbleSort();
        QuickSort(lawea, 0, lawea.Length - 1);
    }
}
