using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Point : MonoBehaviour {

    public static int Instances = 0;
    public List<Point> adjacents;
    public float radius = 0.1f;
    public bool selected;

    public Point GetAdjacentPoint( Vector3 direction ) {
        //TODO Le das una direccion y te devuelve un punto. Si no existe, nulo
        //Usar linq, take y where 

        var p = adjacents.Where(x => x.transform.position == direction).First();
        //Checkear que sea nula o tenga algo adentro
        return p;
        /*
        foreach ( var a in adjacents ) {
            if ( a.transform.position == direction )
                return a;
        }
        return null;
        */
    }

    public void OnDrawGizmos() {
        if ( selected ) {
            Gizmos.color = Color.red;
        } else {
            Gizmos.color = Color.cyan;
        }
        Gizmos.DrawSphere(this.transform.position, radius);
        Gizmos.color = Color.cyan;
        if ( adjacents.Count != 0 ) {

            foreach ( var p in adjacents ) {
                Gizmos.DrawLine(this.transform.position, p.transform.position);
            }
        }
    }

}