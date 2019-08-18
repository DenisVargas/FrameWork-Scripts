using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour {
    public GameObject target;
    public float viewAngle;
    public float viewDistance;

    private Vector3 _dirToTarget;
    private float _angleToTarget;
    private float _distanceToTarget;
    private bool _targetInSight;
    
	
	// Update is called once per frame
	void Update () {
        //La dirección desde un punto a otro es: Posición Final - Posición Inicial NORMALIZADA
        _dirToTarget = (target.transform.position - transform.position).normalized;

        //Vector3.Angle nos da el ángulo entre dos direcciones
        _angleToTarget = Vector3.Angle(transform.forward,_dirToTarget);

        //Vector3.Distance nos da la distancia entre dos posiciones
        //Es lo mismo que hacer: Posición Final - Posición Inicial y sacar la magnitud del vector
        //_distanceToTarget = (target.transform.position - transform.position).magnitude;
        _distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        //Si entra en el angulo y en el rango de vision

        if (_angleToTarget <= viewAngle && _distanceToTarget <= viewDistance)
        {
            RaycastHit ray;
            bool obstaculosOpe = false;

            //Chequeo de collisiones
            if (Physics.Raycast(transform.position, _dirToTarget, out ray, _distanceToTarget))
                if (ray.collider.gameObject.layer == Layers.WALL)
                    obstaculosOpe = true;

            if (!obstaculosOpe)
                _targetInSight = true;
            else
                _targetInSight = false;

        }
        else
            _targetInSight = false;

    }

    private void OnDrawGizmos()
    {
        if (_targetInSight)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.transform.position);

        //Limites
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.yellow;
        Vector3 Derecha = Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (Derecha * viewDistance));
        
        Vector3 Izquierda = Quaternion.AngleAxis(-viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (Izquierda * viewDistance));


    }
}
