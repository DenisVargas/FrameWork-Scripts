using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderBFS : MonoBehaviour
{
    public Nodo start;
    public Nodo End;
    public WalkerSimple walker;

    private void Start()
    {      
        var path = BFS.Run(start, satisfies, Expand);

        foreach (var nodo in path )
        {
            Debug.Log(nodo.name);
        }

        walker.waypoints = path;
    }

    bool satisfies(Nodo n)
    {
        return n == End;
    }

    public List<Nodo> Expand(Nodo n)
    {
        return n.neighbours;
    }

}
