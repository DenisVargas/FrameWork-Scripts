using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Unit_TestAvoid : MonoBehaviour {
    public GameObject Obstacle;

    Vector3 avoidance = Vector3.zero;
	// Use this for initialization
	void Start () {
        print("Empecé wey");
	}
	
	// Update is called once per frame
	void Update () {

        avoidance = getAvoidance(transform,Obstacle.transform);
	}

    private Vector3 getAvoidance(Transform Origin, Transform Destiny)
    {
        return (Destiny.localPosition - Origin.localPosition).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (avoidance != Vector3.zero)
            Gizmos.DrawLine(transform.position,Obstacle.transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,avoidance);
        Gizmos.DrawWireCube(avoidance,new Vector3(1,1,1));
    }
}
