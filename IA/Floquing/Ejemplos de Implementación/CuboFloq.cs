using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuboOpe : MonoBehaviour {
    public float Velocidad;
    public float rotationVelocity;
    
    public Transform targetPlaceHolder;
    public List<cuboOpe> Allies = new List<cuboOpe>();

    public bool isLeader = false;
    public Transform leader;

    public float radFloq;
    public float radEvation;
    public float radAlert;

    public LayerMask alliesLayerMask;
    public LayerMask ObstacleLayerMask;
    public int alliesLayerIndex;
    public int obstacleLayerIndex;

    //public float avoidanceWeight;
    public float cohecionWeight;
    public float aligmentWeight;
    public float separationWeight;

    private Vector3 avoidance = Vector3.zero;
    private Vector3 cohesion = Vector3.zero;
    //private Vector3 alignment = Vector3.zero;
    private Vector3 separation = Vector3.zero;
    // Use this for initialization
    void Start ()
    {
        if (!isLeader)
        {
            foreach (var item in Allies)
                if (item.isLeader)
                    leader = item.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        CalculateFlock();
        moveTo(targetPlaceHolder);
	}

    private void moveTo(Transform target)
    {
        //Me muevo hacia un objetivo que cambia en el tiempo.
        Vector3 toTarget = (target.transform.position - transform.position).normalized;
        Vector3 dirToTargetPos;
        if (isLeader)
            dirToTargetPos = avoidance + toTarget;
        else
        {
            var toLeader = (leader.transform.position - transform.position).normalized;
            dirToTargetPos = avoidance + cohesion + separation + toLeader;
        }

        dirToTargetPos = new Vector3(dirToTargetPos.x, 0, dirToTargetPos.z);

        transform.forward = Vector3.Slerp(transform.forward, dirToTargetPos, rotationVelocity * Time.deltaTime);

        transform.position += transform.forward * Velocidad * Time.deltaTime;
    }

    private void CalculateFlock()
    {
        #region Obstacle Avoidance
        Collider[] obstaculos = Physics.OverlapSphere(transform.position, radEvation,ObstacleLayerMask);

        if (obstaculos.Length > 0)
        {
            Vector3 Suma = Vector3.zero;

            Vector3 closerObstaclePos = Vector3.zero;//Obstaculo mas cercano.
            float dist = 0;//Distancia al obstaculo mas cercano.
            foreach (var obstaculo in obstaculos)
            {
                Suma += (obstaculo.transform.position - transform.position).normalized;
                if (closerObstaclePos == Vector3.zero)
                {
                    dist = Vector3.Distance(transform.position, closerObstaclePos);
                    closerObstaclePos = obstaculo.transform.position;
                }
                else
                {
                    if (dist > Vector3.Distance(transform.position,obstaculo.transform.position))
                    {
                        dist = Vector3.Distance(transform.position, obstaculo.transform.position);
                        closerObstaclePos = obstaculo.transform.position;
                    }
                }
            }
            Suma *= -1;
            Suma /= obstaculos.Length;
            Suma.Normalize();
            
            //avoidance weight
            float weight = (radEvation - Vector3.Distance(transform.position, closerObstaclePos));//Radio - distancia al objeto mas cercano.

            avoidance = Suma * weight;
            avoidance = new Vector3(avoidance.x, 0, avoidance.z);
        }
        else
            avoidance = Vector3.zero;
        #endregion

        #region Alignment
        // Innecesaro para este npc.Esto solo alinea la direccion de todos los objetos cuando estan quietos.
        //if (Allies.Count > 0)
        //{
        //    Vector3 alineacionPromedio = Vector3.zero;
        //    foreach (var ally in Allies)
        //    {
        //        if (alineacionPromedio == Vector3.zero)
        //            alineacionPromedio = ally.transform.forward;
        //        else
        //            alineacionPromedio += ally.transform.forward;
        //    }
        //    alineacionPromedio /= Allies.Count;
        //    alignment = alineacionPromedio * aligmentWeight;
        //}
        #endregion

        //#region Separation
        //separation = Vector3.zero;
        //foreach (var ally in Allies)
        //{
        //    Vector3 fdir = (ally.transform.position - transform.position);
        //    float fmag = radFloq - fdir.magnitude;
        //    fdir.Normalize();
        //    fdir *= fmag;
        //    separation += fdir;
        //}
        //separation /= Allies.Count;
        //separation *= separationWeight;
        //#endregion

        //#region Cohesion
        //Vector3 coh = new Vector3();
        //foreach (var item in Allies)
        //    coh += item.transform.position - transform.position;
        //cohesion = coh/Allies.Count;
        //cohesion *= cohecionWeight;
        //#endregion
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * 5, new Vector3(0.5f,0.5f,0.5f));
        //Gizmos.DrawWireSphere(transform.position, radEvation);
        Gizmos.DrawCube(transform.position + separation,new Vector3(0.5f,0.5f,0.5f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + avoidance, new Vector3(0.5f, 0.5f, 0.5f));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radFloq);
        

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawCube(transform.position + alignment * 10, new Vector3(0.5f, 0.5f, 0.5f));
    }
}
