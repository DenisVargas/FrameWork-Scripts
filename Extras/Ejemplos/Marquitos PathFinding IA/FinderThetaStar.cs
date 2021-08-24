using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinderThetaStar : MonoBehaviour
{
    public Nodo start;
    public Nodo End;
    public float range;
    public LayerMask visibles = ~0;//
    public WalkerSimple walker;

    Ray lastRay;

    private void Start()
    {
        var path = ThetaStar.Run(start, satisfies, Expand, heuristic, insigth, cost);

        foreach (var nodo in path)
        {
            Debug.Log(nodo.name);
        }

        walker.waypoints = path;
    }

    bool satisfies(Nodo n)
    {
        return n == End;
    }

    public List<Tuple<Nodo, float>> Expand(Nodo n)//le pasamos un nodo y nos devuelve una lista de vecinos con sus pesos
    {
        List<Tuple<Nodo, float>> weightN = new List<Tuple<Nodo, float>>();//Del nodo se recorre los vecino y por cada vecino agrega una tupla que contiene el vencino y el peso

        foreach (var item in n.neighbours)//lista de vecinos
        {
            weightN.Add(Tuple.Create(item, Vector3.Distance(n.transform.position, item.transform.position)));//agrego una tupla por cada nodo, que contiene sus vecinos y costo tentativo
        }

        return weightN;
    }

    public float heuristic(Nodo n)
    {

        float heuristicroute = Vector3.Distance(n.transform.position, End.transform.position);

        return heuristicroute;
    }

    public bool insigth(Nodo origin, Nodo End)
    {
        RaycastHit hitInfo;
        lastRay = new Ray(
                             origin.transform.position, 
                             (End.transform.position - origin.transform.position)
                             .normalized
                         );
        range = Vector3.Distance(End.transform.position, origin.transform.position);

        if (Physics.Raycast(lastRay, out hitInfo, range + 1, visibles))
            return hitInfo.transform == End.transform;

        return false;
    }

    public float cost(Nodo n, Nodo g)
    {
        float fathershipcost = Vector3.Distance(g.transform.position, n.transform.position);

        return fathershipcost;
    }

    private void OnDrawGizmos()
    {
        if (End)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, End.transform.position);
        }

        if (lastRay.origin != null && lastRay.direction != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(lastRay.origin, lastRay.origin + lastRay.direction * range);
        }
    }
}
