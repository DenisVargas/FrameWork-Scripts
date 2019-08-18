using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloqTest : MonoBehaviour {
    public List<FloqTest> Allies = new List<FloqTest>();
    public bool isLeader = false;
    public Transform leader;

    public float rotationVelocity;
    public float velocidad;
    public float radFloq;

    public LayerMask alliesLayerMask;
    public int alliesLayerIndex;

    public float cohecionWeight;
    public float aligmentWeight;
    public float separationWeight;
    
    private Vector3 cohesion = Vector3.zero;
    private Vector3 separation = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveTo(leader.transform.position);
    }

    private void moveTo(Vector3 targetLocation)
    {
        //Me muevo hacia un objetivo que cambia en el tiempo.
        Vector3 toLeader = (leader.transform.position - transform.position).normalized;//Direccion al lider.

        Vector3 dir = Vector3.zero;

        dir = new Vector3(dir.x, 0, dir.z);

        transform.forward = Vector3.Slerp(transform.forward, dir, rotationVelocity * Time.deltaTime);

        transform.position += transform.forward * velocidad * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radFloq);
    }
}
