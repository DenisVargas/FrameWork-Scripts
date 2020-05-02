using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour {

    public List<Point> points;
    public int distance;

    public Point GetAdjacentPoint( Vector3 direction ) {
        //TODO Le das una direccion y te devuelve un punto. Si no existe, nulo
        //Usar linq y take y where 
        foreach ( var a in points ) {

            if ( a.transform.position == direction ) {
                print("thisiswong");
                return a;
            }
        }

        return null;
    }
    public Point GetRandomPoint() {
        Point p = points [ Random.Range(0, points.Count) ];
        return p;
    }
}
