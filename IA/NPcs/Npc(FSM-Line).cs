using System;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Unit {
    //Características
    //--------------General Stats-------------------------------------------------------------------------
    [Header("General Unit Stats")]
    bool EnemyInSight;
    public bool IsRested = true;
    public bool IsAwakeAndAlert = true;
    public float movementSpeed;
    public float rotationSpeed;
    public LayerMask Targets;
    public LayerMask Obstaculos;
    public int layerIndex_Targets;
    public int layerIndex_Obstacles;
    //_____________________________________Battle Settings (BS)__________________________________________\\
    [Header("Battle Settings")][Space]
    public Transform CurrentTarget = null;
    //--------------Offensive Stats (BSo)-----------------------------------------------------------------
    public int BSO_BasicDamage;
    public int BSO_Strenght;
    public int BSO_MagicPower;
    public float BSO_AttackSpeed = 0.6f;
    public float BSO_AttackRange;

    //--------------Defense Stats (BSD)-------------------------------------------------------------------
    public int BSD_Armour;
    public int BSD_MagicResistance;
    //____________________________________________________________________________________________________\\

    //--------------Line of Sight-------------------------------------------------------------------------\\
    [Header("Line of Sight")][Space]
    public float viewAngle;
    public float ViewRange;
    public float minimunAwarenessDistance;
    
    //Objectivos
    public Vector3 PosTarget;
    public List<Transform> LivingVisibleTargets = new List<Transform>();
    public List<Transform> Waypoints = new List<Transform>();
    //____________________________________________________________________________________________________\\

    //------------------Floquing Parameters---------------------------------------------------------------\\
    [Header("Floquing Parameters")][Space]
    public float floqRadius;
    public float obstacleRadius;
    public List<Collider> allies = new List<Collider>();

    public bool groupLeader = false;
    public Vector3 groupTarget;
    public List<Collider> groupAllies = new List<Collider>();

    [HideInInspector] public Vector3 directionToMove;
    [HideInInspector] public Vector3 separacion;
    [HideInInspector] public Vector3 cohecion;
    [HideInInspector] public Vector3 alineacion;
    [HideInInspector] public Vector3 avoidance;

    public float separationWeight;
    public float cohecionWeight;
    public float alineacionWeight;
    public float avoidanceWeight;

    //____________________________________________________________________________________________________\\

    //Estados.
    public GenericFSM FSM;
    List<State> EstadosDelNpc;

	void Awake ()
    {
	    //Inicializo la state machine y añado los estados.
        FSM = new GenericFSM();
        object[] BattleStats = { BSO_BasicDamage, BSO_MagicPower, BSO_Strenght, BSO_AttackSpeed, BSO_AttackRange };
        EstadosDelNpc = new List<State>
        {
            new StateIdle(gameObject), //0
            new StatePursuit(gameObject), //1
            new StateAttack(gameObject,BattleStats), //2
        };
        //Para añadir un Estado necesito un entero(índice) + Un action para Awake, Execute y Sleep;
        for (int i = 0; i < EstadosDelNpc.Count; i++)
            FSM.AddState(i, EstadosDelNpc[i].execute, EstadosDelNpc[i].awake, EstadosDelNpc[i].sleep);

        FSM.SetState(0);
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
        FSM.Update(); //Chequea que no haya ocurrido ningun cambio en la FSM.
        //-------------Line of Sight----------------------------------------------------------------------------
        CheckForEnemies();
        //---------------------State triggers-------------------------------------------------------------------

        if (CurrentTarget != null)
        {
            float Dist = Vector3.Distance(transform.position, CurrentTarget.transform.position);
            //Si esta dentro de mi rango, lo Ataco.
            if (Dist <= BSO_AttackRange)
                FSM.SetState(2); //Attack Current Target
            

            //Si hay al menos un objetivo visible y esta mas lejos que mi rango de ataque.
            if (Dist > BSO_AttackRange && CurrentTarget != null)
                FSM.SetState(1); //Pursuit Target.
        }
    }

    #region Funciones Adicionales
    public void MoveTo(Vector3 Target) //Si no estoy agrupado con ningun aliado.
    {//Me muevo en direccion al objetivo, evitando obstaculos y a cualquier unidad aliada cercana.
        Vector3 finalPos = Target;
        //Hay que calcular avoid en el rango de avoidance.

        //

        if (transform.forward != Target)
            transform.forward = Vector3.Slerp(transform.forward, Target, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    public void CheckForEnemies()
    {
        if (IsAwakeAndAlert)
            CheckLineOfSight();
        if (LivingVisibleTargets.Count > 0)
            CurrentTarget = GetCloserVisibleTarget();
    }

    /// <summary>
    /// Chequea si hay objetivos dentro de la linea de vision.
    /// </summary>
    private void CheckLineOfSight()
    {
        
        Collider[] Encontrados = Physics.OverlapSphere(transform.position, ViewRange,Targets); //worth.
        
        
        foreach (var item in Encontrados)
        {
            Vector3 _dirToTarget  = (item.transform.position - transform.position).normalized;
            float _angleToTarget = Vector3.Angle(transform.forward, _dirToTarget);
            float distanceToTarget = Vector3.Distance(transform.position, item.transform.position);

            RaycastHit ray;
                
            if (Physics.Raycast(transform.position, _dirToTarget, out ray, ViewRange))
            {
                print("Choque con :" + ray.collider.gameObject);
                if (ray.collider.gameObject.layer == layerIndex_Targets)
                    if (distanceToTarget <= ViewRange && _angleToTarget <= viewAngle)
                    {
                        if (!LivingVisibleTargets.Contains(item.transform))
                        {
                            LivingVisibleTargets.Add(item.transform);
                            print("Añadi un target, actualmente tengo: " + LivingVisibleTargets.Count);
                        }
                    }
            }
        }
    }
    
    /// <summary>
    /// Devuelve el objetivo visible mas cercano.
    /// </summary>
    private Transform GetCloserVisibleTarget()
    {
        //Descartamos todos los objetos que tengan puntos de vida menores a 0
        //Hacemos una copia de la lista. FUNDAMENTAL NO MODIFICAR UNA LISTA AL RECORRERLA.
        List<Transform> B = new List<Transform>();
        foreach (var item in LivingVisibleTargets)
            if (item.GetComponent<Unit>().HealhPoints > 0)
                B.Add(item);

        Transform A = null;
        float MinDist = 0;
        if (B.Count > 0)
        {
            foreach (var item in B)
            {
                if (A == null)
                {
                    if (item.GetComponent<Unit>().HealhPoints > 0)
                    {
                        A = item;
                        MinDist = Vector3.Distance(transform.position, item.transform.position);
                    }
                }
                else if (Vector3.Distance(transform.position, item.transform.position) < MinDist)
                {
                    A = item;
                    MinDist = Vector3.Distance(transform.position, item.transform.position);
                }
            }
            return A;
        }
        else
            return null;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, floqRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, obstacleRadius);
        
        //Estado.
        if (LivingVisibleTargets.Count > 0)
        {
            Gizmos.color = Color.green;
            Transform Target = GetCloserVisibleTarget();
            if (Target != null)
                Gizmos.DrawLine(transform.position, GetCloserVisibleTarget().position);
        }

        //Limites de la vision.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ViewRange);

        Gizmos.color = Color.yellow;
        Vector3 Derecha = Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (Derecha * ViewRange));

        Vector3 Izquierda = Quaternion.AngleAxis(-viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (Izquierda * ViewRange));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ViewRange / minimunAwarenessDistance);


        /*
        //Debuging Battle Settings.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BSO_AttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * 10);
        */
    }
}