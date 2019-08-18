using System;
using UnityEngine;

namespace IA.SpatialGrid
{
    public interface IGridEntity
    {
        /// <summary>
        /// Devuelve el [GameObject] que contiene al Master.
        /// </summary>
        GameObject GetGameObject { get; }
        /// <summary>
        /// Devuelve el componente [Transform] asociado al contenedor.
        /// </summary>
        Transform GetTransform { get; }
        /// <summary>
        /// Devuelve la Posición Global del Contenedor.
        /// </summary>
        Vector3 positionInWorld { get; }
    }
    public interface IGridEntityMaster
    {
        /// <summary>
        /// Devuelve una referencia al evento OnMoveMaster de la entidad.
        /// Esta función se ejecuta internamente dentro del Jugador.
        /// </summary>
        Action OnMoveMaster { get; set; }
        /// <summary>
        /// Devuelve el [GameObject] que contiene al Master.
        /// </summary>
        GameObject GetGameObject { get; }
        /// <summary>
        /// Devuelve el componente [Transform] asociado al contenedor.
        /// </summary>
        Transform GetTransform { get; }
        /// <summary>
        /// Devuelve la Posición Global del Contenedor.
        /// </summary>
        Vector3 GetPosition { get; }
    }
}

