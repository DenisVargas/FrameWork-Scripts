using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour {

    //clase para interconectar
    public List<Nodo> neighbours = new List<Nodo>();
   
    private void Awake()
    {
        foreach (var node in neighbours)
        {
            if (!node.neighbours.Contains(this)) node.neighbours.Add(this);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var item in neighbours)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, item.transform.position);
        }
    }
}
