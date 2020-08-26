﻿using System;
using UnityEngine;

namespace IA.LineOfSight
{
    [Serializable]
    public class CompositeLineOfSight
    {
        public LayerMask visibles = ~0;
        public Transform origin;
        public float range;
        public float angle;

        /// <summary>
        /// El vector resultante de la resta de ambas posiciones: B - A.
        /// </summary>
        [Tooltip("El vector resultante de la resta de ambas posiciones: B - A.")]
        public Vector3 positionDiference = Vector3.zero;
        /// <summary>
        /// Dirección normalizada hacia el objetivo.
        /// </summary>
        [Tooltip("Dirección normalizada hacia el objetivo.")]
        public Vector3 dirToTarget = Vector3.zero;
        /// <summary>
        /// Último ángulo calculado entre la posición de origen y el objetivo.
        /// </summary>
        [Tooltip("Último ángulo calculado entre la posición de origen y el objetivo.")]
        public float angleToTarget = 0;
        /// <summary>
        /// Última distancia calculada entre la posición de origen y el objetivo.
        /// </summary>
        [Tooltip("Última distancia calculada entre la posición de origen y el objetivo.")]
        public float distanceToTarget = 0;


        /// <summary>
        /// Crea un nueva Línea de visión.
        /// </summary>
        /// <param name="origin">El origen de coordenadas para el cálculo de visión</param>
        /// <param name="range">La distancia máxima de la visión</param>
        /// <param name="angle">El ángulo máximo de visión</param>
        public CompositeLineOfSight(Transform origin, float range, float angle)
        {
            this.origin = origin;
            this.range = range;
            this.angle = angle;
            LayerMask visibility = ~0;
        }
        /// <summary>
        /// Indica si el objetivo específicado está dentro de la línea de visión
        /// </summary>
        /// <param name="target">Objetivo a comprobar</param>
        /// <returns>Verdadero si el Objetivo específicado está dentro de la línea de visión</returns>
        public bool IsInSight(Transform target)
        {
            positionDiference = target.position - origin.transform.position;
            distanceToTarget = positionDiference.magnitude;
            angleToTarget = Vector3.Angle(origin.transform.forward, positionDiference);

            dirToTarget = positionDiference.normalized;

            if (distanceToTarget > range || angleToTarget > angle) return false;

            RaycastHit hitInfo;
            if (Physics.Raycast(origin.position, dirToTarget, out hitInfo, range, visibles))
                return hitInfo.transform == target;

            return true;
        }
    }

    //Snippet for Debugg
//#if (UNITY_EDITOR)
//    void OnDrawGizmosSelected()
//    {
//        var currentPosition = transform.position;

//        Gizmos.color = targetIsInSight ? Color.green : Color.red;       //Target In Sight es un bool en una clase Externa.
//        float distanceToTarget = mySight.positionDiference.magnitude;   //mySight es una instancia de la clase LineOfSight.
//        if (distanceToTarget > range) distanceToTarget = range;
//        mySight.dirToTarget.Normalize();
//        Gizmos.DrawLine(transform.position, transform.position + mySight.dirToTarget * distanceToTarget);

//        Gizmos.color = Color.white;
//        Gizmos.matrix *= Matrix4x4.Scale(new Vector3(1, 0, 1));
//        Gizmos.DrawWireSphere(transform.position, range);

//        Gizmos.color = Color.yellow;
//        Gizmos.DrawLine(currentPosition, currentPosition + Quaternion.Euler(0, viewAngle + 1, 0) * transform.forward * range);
//        Gizmos.DrawLine(currentPosition, currentPosition + Quaternion.Euler(0, -viewAngle - 1, 0) * transform.forward * range);

//    }
//#endif
}
