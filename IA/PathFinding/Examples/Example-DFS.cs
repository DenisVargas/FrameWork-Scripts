using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : MonoBehaviour
{
    public Node first;
    public Node last;

    private void Start()
    {
        var dfs = DeepFirstSearch(first, last);
        foreach (var item in dfs)
        {
            print(item.name);
        }
    }

    //adaptado a que tenga ademas de un nodo origen, tenga un nodo final
    public List<Node> DeepFirstSearch (Node start, Node end)
    {	/*
        Crear una  lista que guarda el recorrido.
		CREAMOS UNA PILA S
		AGREGAMOS ORIGEN A LA PILA S
		MARCAMOS ORIGEN COMO VISITADO
		MIENTRAS S NO ESTE VACÍO:
		  SACAMOS UN ELEMENTO DE LA PILA S LLAMADO V
		  PARA CADA VERTICE W ADYACENTE A V EN EL GRAFO: 
			  SI W NO FUE VISITADO:
				 MARCAMOS COMO VISITADO W
				 INSERTAMOS W DENTRO DE LA PILA S

			
		agregar el chequeo de nodo final cuando sea necesario
		devolver la lista recorrida hasta el punto en que se encontró el nodo final
		*/
        List<Node> Recorrido = new List<Node>();

        Stack<Node> S = new Stack<Node>();
        S.Push(start);
        start.visited = true;
        //print("Cantidad de objetos." + S.Count);
        while (S.Count > 0)
        {
            Node v = S.Pop();
            Recorrido.Add(v);
            v.visited = true;
            //print("Links : " + v.links.Count);
            if (v == end)
                return Recorrido;
            foreach (var w in v.links)
                if (!w.visited)
                {
                    S.Push(w);
                    w.visited = true;
                    //print("Agregados: " + S.Count);
                }

        }
        //print("Cantidad de objetos en S: " + S.Count);
        //print("Cantidad de objetos en recorrido: " + Recorrido.Count);
        return Recorrido;
	}


}
