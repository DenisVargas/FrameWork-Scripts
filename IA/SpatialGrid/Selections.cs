using System;
using System.Collections.Generic;
using UnityEngine;

namespace IA.SpatialGrid.Utility
{
    /// <summary>
    ///  Utilidad que permite seleccionar Entidades dentro de un área cuadrada, tomando la celda activa de la grilla.
    /// </summary>
    [Serializable]
    public class BoxSelection
    {
        [SerializeField] private float width = 15f;
        [SerializeField] private float height = 30f;

        public BoxSelection( float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        public IEnumerable<IGridEntity> Select(Vector3 From)
        {
            var h = height * 0.5f;
            var w = width * 0.5f;

            return SpatialGrid.instance.GetEntitiesInRange(
                From + new Vector3(-w, 0, -h),
                From + new Vector3(w, 0, h),
                x => true);
        }
    }

    /// <summary>
    /// Utilidad que permite seleccionar Entidades en un radio, tomando la celda activa de la grilla.
    /// </summary>
    [Serializable]
    public class CircleSelection
    {
        [SerializeField] float radius = 20f;

        public CircleSelection(float radius, Vector3 origin)
        {
            this.radius = radius;
        }

        public IEnumerable<IGridEntity> Select(Vector3 From)
        {
            //Si es una esfera
            //creo una "caja" con las dimensiones deseadas, y luego filtro segun distancia para formar el círculo
            return SpatialGrid.instance.GetEntitiesInRange(
                From + new Vector3(-radius, 0, -radius),
                From + new Vector3(radius, 0, radius),
                entityPos => {
                    var position2d = entityPos - From;
                    position2d.y = 0;
                    return position2d.sqrMagnitude < radius * radius; //Una forma mas "performante" de comparar distancias
                });
        }
    }
}
