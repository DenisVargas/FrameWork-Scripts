using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderDijkstra : MonoBehaviour {

   // public Nodo start;
   // public Nodo End;
   // public WalkerSimple walker;

   // private void Start()
   // {
   //     var path = Dijkstra.Run(start, satisfies, Expand);

   //     foreach (var nodo in path)
   //     {
   //         Debug.Log(nodo.name);
   //     }

   //     walker.waypoints = path;
   // }

   // bool satisfies(Nodo n)
   // {
   //     return n == End;
   // }
   

   //public List<Tuple<Nodo, float>> Expand(Nodo n)//le pasamos un nodo y nos devuelve una lista de vecinos con sus pesos
   // {

   //     List<Tuple<Nodo, float>> weightN = new List<Tuple<Nodo, float>>();//Del nodo se recorre los vecino y por cada vecino agrega una tupla que contiene el vencino y el peso

   //     foreach (var item in n.neighbours)//lista de vecinos
   //     {
   //         weightN.Add(Tuple.Create(item, Vector3.Distance(n.transform.position, item.transform.position)));//agrego una tupla por cada nodo, que contiene sus vecinos y costo tentativo
   //     }

   //     return weightN; 
   // }
}
