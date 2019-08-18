using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    Debug.Log(lawea[j]);
                }
            }

            var elViejo = lawea[i];
            if (current != elViejo)
            {
                lawea[i] = current;
                lawea[index] = elViejo;
            }
        }
    }


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


    public void QuickSort(int[] element, int Left, int Rigth)
    {
        if (Rigth - Left == 0)
            return;
        else if (Rigth - Left == 1)
            return;
        else if (Rigth - Left == 2)
        {
            //comparo.
            //Si se puede switcheo.
            //retorno.
        }
        //Calculo mi pivot: (int)Lenght / 2;

        //Recorro de izquierda a derecha hasta encontrar el pivot.
            //Si el valor es mayor al pivot.
                // Guardo el valor y su indice.
        //Recorro de derecha a izquierda hasta encontrar el pivot.
            //Si el valor es menor al pivot.
                // Guardo el valor y su indice.

        //Switcheo los valores de los objetos en los indices dados.

        //QuickSort(element, index1, pivot);
        //QuickSort(element, pivot, index2);
    }

    public void Start()
    {
        //SelectionSort();
        //BubbleSort();
        QuickSort(lawea, 0, lawea.Length - 1);
    }


}
