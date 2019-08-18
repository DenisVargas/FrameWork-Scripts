using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC3 : MonoBehaviour {
    public float viewAngle;
    public float viewDistance;

    public LayerMask Targets;
    public int targetLayer;
    public int obstaclesLayer;

    public List<Transform> VisibleTargets;
    public List<Transform> NonVisibleTargets;
    
    // Use this for initialization
    void Start () {
        VisibleTargets = new List<Transform>();
       
    }
	
	// Update is called once per frame
	void Update () {
        CheckVision();

	}

    private void OnDrawGizmos()
    {
        if (VisibleTargets.Count > 0)
        {
            foreach (var item in VisibleTargets)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, item.transform.position);
            }
        }
        if (NonVisibleTargets.Count > 0)
        {
            foreach (var item in NonVisibleTargets)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, item.transform.position);
            }
        }
        
        //Limites
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.yellow;
        Vector3 Derecha = Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (Derecha * viewDistance));

        Vector3 Izquierda = Quaternion.AngleAxis(-viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (Izquierda * viewDistance));
    }


    private void CheckVision()
    {
        Collider[] Encontrados = Physics.OverlapSphere(transform.position, Targets);
        for (int i = 0; i < Encontrados.Length; i++)
        {
            //objetivo
            Vector3 Target = Encontrados[i].transform.position;
            //Dirección
            Vector3 _dirToTarget = (Target - transform.position).normalized;
            //Distancia.
            float _distanceToTarget = Vector3.Distance(transform.position,Target) + 1;
            //angulo.
            float _angleToTarget = Vector3.Angle(transform.forward, _dirToTarget);

            if (_angleToTarget <= viewAngle && _distanceToTarget <= viewDistance)
            {
                RaycastHit r;
                if (Physics.Raycast(transform.position, _dirToTarget, out r, _distanceToTarget))
                {
                    print("El objeto colisionado es: " + r.collider);
                    int a = r.collider.gameObject.layer;
                    //print(LayerMask.NameToLayer("NPC"));
                    print("La layer a es: " + a);
                    if (a == obstaclesLayer)
                    {
                        print("El objeto no esta visible");
                        if (!NonVisibleTargets.Contains(Encontrados[i].transform))
                            NonVisibleTargets.Add(Encontrados[i].transform);
                    }
                    if (a == targetLayer)
                    {
                        print("El objeto es visible");
                        if (!VisibleTargets.Contains(Encontrados[i].transform))
                        {
                            VisibleTargets.Add(Encontrados[i].transform);
                        }

                    }

                    if (_angleToTarget > viewAngle)
                    {
                        print("El objeto esta fuera del angulo de vision");
                        if (!NonVisibleTargets.Contains(Encontrados[i].transform))
                            NonVisibleTargets.Add(Encontrados[i].transform);
                    }
                }
            }
        }
    }
    
}
