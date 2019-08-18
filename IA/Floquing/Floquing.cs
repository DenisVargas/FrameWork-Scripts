using System.Collections.Generic;
using UnityEngine;

namespace IA.Floquing
{
    /// <summary>
    /// Extensiones de la clase Transform que permite calcular vectores de Floquing.
    /// </summary>
    public static class Floquing
    {
        #region Cohetion

        /// <summary>
        /// Calcula el vector Cohesion.
        /// </summary>
        /// <param name="Origin">Posicion del objeto de origen.</param>
        /// <param name="Destiny">Posicion del objeto de destino.</param>
        /// <returns>Vector3.Coh</returns>
        public static Vector3 getCohesion(Vector3 Origin, Vector3 Destiny, float Magnitude = 1f)
        {
            return ((Destiny - Origin).normalized) * Magnitude;
        }
        /// <summary>
        /// Calcula el vector Cohesion.
        /// </summary>
        /// <param name="Origin">Transform del objeto de origen.</param>
        /// <param name="Towards">Lista de Transform´s de los objetivos.</param>
        /// <returns>Vector3.Coh</returns>
        public static Vector3 getCohesion(this Transform Origin, IEnumerable<Transform> Towards, float Magnitude = 1f)
        {
            Vector3 coh = Vector3.zero;
            int Targets = 0;

            foreach (var Destiny in Towards)
            {
                coh += Destiny.position - Origin.position;
                Targets++;
            }

            return ((coh / Targets).normalized) * Magnitude;
        }

        #endregion

        #region Separation

        /// <summary>
        /// Calcula el vector Separation.
        /// </summary>
        /// <param name="Origin">Objeto de origen.</param>
        /// <param name="Target">Objeto "Amigo".</param>
        /// <param name="DesiredSeparation">Distancia de separacion deseada.</param>
        /// <returns>Vector3.Sep</returns>
        public static Vector3 getSeparation(Vector3 Origin, Vector3 Target, float DesiredSeparation = 1f)
        {
            Vector3 Sep = Origin - Target;
            float mag = DesiredSeparation - Sep.magnitude;
            return Sep.normalized * mag;
        }
        /// <summary>
        /// Calcula el vector Separation.
        /// </summary>
        /// <param name="Origin">Objeto de origen.</param>
        /// <param name="Towards">Lista de Transform´s de los objetos "Amigos".</param>
        /// <param name="DesiredSep">Distancia de separacion deseada.</param>
        /// <returns>Vector3.Sep</returns>
        public static Vector3 getSeparation(this Transform Origin, IEnumerable<Transform> Towards, float DesiredSep = 1f)
        {
            Vector3 Sep = Vector3.zero;
            int targets = 0;

            foreach (Transform Target in Towards)
            {
                Vector3 ItemSep = Origin.position - Target.position;
                float mag = DesiredSep - ItemSep.magnitude;

                Sep += ItemSep.normalized * mag;
                targets++;
            }

            return (Sep / targets);
        }

        #endregion

        #region Align

        /// <summary>
        /// Calcula el vector Alignment.
        /// </summary>
        /// <param name="Origin">Objeto de origen.</param>
        /// <param name="Towards">Lista de "amigos".</param>
        /// <returns>Vector3.Alig</returns>
        public static Vector3 getAlignment(this Transform Origin, IEnumerable<Transform> Towards)
        {
            Vector3 Alig = Vector3.zero;
            int targets = 0;

            foreach (Transform Target in Towards)
            {
                Alig += Target.forward;
                targets++;
            }

            return (Alig / targets).normalized;
        }
        #endregion

        #region Avoidance

        /// <summary>
        /// Calcula el vector Avoidance.
        /// </summary>
        /// <param name="Origin">Vector posicion de origen del objeto A.</param>
        /// <param name="Target">Vector posicion de origen del objeto B.</param>
        /// <param name="Magnitude">Magnitud del vector resultante.</param>
        /// <returns>Vector3.Avoidance</returns>
        public static Vector3 getAvoidance(this Transform Origin, Vector3 Target, float Magnitude = 1f)
        {
            return ((Origin.position - Target).normalized) * Magnitude;
        }
        /// <summary>
        /// Calcula el vector Avoidance.
        /// </summary>
        /// <param name="Origin">Vector posicion de origen del objeto A.</param>
        /// <param name="Towards">Lista de vectores posicion de los objetos a evitar.</param>
        /// <param name="Magnitude">Magnitud del vector resultante.</param>
        /// <returns>Vector3.Avoidance</returns>
        public static Vector3 getAvoidance(this Transform Origin, IEnumerable<Vector3> Towards, float Magnitude = 1f)
        {
            Vector3 avoid = Vector3.zero;
            int targets = 0;

            foreach (Vector3 Target in Towards)
            {
                avoid += Origin.position - Target;
                targets++;
            }

            return (avoid / targets).normalized * Magnitude;
        }
        #endregion
    }
}

