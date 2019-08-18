using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IA.SpatialGrid
{
    public class SpatialGrid : MonoBehaviour
    {
        [HideInInspector]
        public static SpatialGrid instance = null;

        [SerializeField] Dictionary<Vector2, List<IGridEntity>> _buckets;
        [SerializeField] Dictionary<IGridEntity, Vector2> _lastPositions;

        [SerializeField] Transform _masterPosition = null;

        [SerializeField] Vector2 _masterLastPos = Vector2.zero;
        [SerializeField] Vector2 _origin;
        [SerializeField] Vector2 _cellSize;
        [SerializeField] Vector2 _gridSize = new Vector2(1,1);

        public readonly Vector2 outside = new Vector2(-1, -1);
        public readonly IGridEntity[] EmptyGridEntityCollection = new IGridEntity[0];
        public Vector2 ActiveCell
        {
            get { return _active; }

            set
            {
                //Mientras la posición 2D este dentro de los límites de la grilla...
                if (value.x >= 0 && value.x < _gridSize.x && value.y >= 0 && value.y < _gridSize.y)
                    _active = value;
            }
        }

        Vector2 _active;


        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            _active = new Vector2(0,0);
            _lastPositions = new Dictionary<IGridEntity, Vector2>();

            _buckets = new Dictionary<Vector2, List<IGridEntity>>();

            for (int i = 0; i < _gridSize.x; i++)
                for (int j = 0; j < _gridSize.y; j++)
                    _buckets[new Vector2(i, j)] = new List<IGridEntity>();

            IGridEntityMaster master = _masterPosition.GetComponent<IGridEntityMaster>();
            _masterLastPos = GetPositionInGrid(master.GetPosition);
            master.OnMoveMaster += UpdateGridMasterEntityPosition;

            IEnumerable<IGridEntity> entities = LazyGetChildren(transform)
                                    .Select(x => x.GetComponentInChildren<IGridEntity>());

            foreach (var entity in entities)
            {
                Vector2 entityPos = GetPositionInGrid(entity.positionInWorld);

                if (IsInsideGrid(entityPos) &&_buckets.ContainsKey(entityPos))
                    _buckets[entityPos].Add(entity);

                entity.GetTransform.gameObject.SetActive(GetPositionInGrid(entity.positionInWorld) == ActiveCell);
            }
        }

        /// <summary>
        /// Filtra y retorna todas las entidades que se encuentren en la grilla teniendo en cuenta su posición dentro de la misma.
        /// </summary>
        /// <param name="From">Origen menor en el mundo</param>
        /// <param name="To">Origen mayor en el mundo</param>
        /// <param name="filterByPosition">Criterio secundario. Permite agregar un paso extra al filtrado.</param>
        /// <returns> Lista de entidades en el rango indicado, dentro de la grilla.</returns>
        public IEnumerable<IGridEntity> GetEntitiesInRange(Vector3 From, Vector3 To, Func<Vector3, bool> filterByPosition)
        {
            Vector3 _from = new Vector3(Mathf.Min(From.x, To.x), 0, Mathf.Min(From.z, To.z));//Posición del Ancla A en el mundo.
            Vector3 _to = new Vector3(Mathf.Max(From.x, To.x), 0, Mathf.Max(From.z, To.z));  //Posición del Ancla B en el mundo.

            Vector2 fromCoord = GetPositionInGrid(_from);                                    //Posición del Ancla A en la grilla.
            Vector2 toCoord = GetPositionInGrid(_to);                                        //Posición del Ancla B en la grilla.

            if (!IsInsideGrid(fromCoord) && !IsInsideGrid(toCoord))
                return EmptyGridEntityCollection;

            return _buckets[ActiveCell]
                   .Where(entity => _from.x <= entity.positionInWorld.x && entity.positionInWorld.x <= _to.x
                                      && _from.z <= entity.positionInWorld.z && entity.positionInWorld.z <= _to.z)
                   .Where(x => filterByPosition(x.positionInWorld));
        }
        /// <summary>
        /// Retorna todas las entidades que existen dentro de la Celda indicada por parámetro.
        /// </summary>
        /// <param name="CellCoords">Las coordenadas que identifican a la celda.</param>
        public IEnumerable<IGridEntity> GetEntitiesInBucket(Vector2 CellCoords)
        {
            if (_buckets.ContainsKey(CellCoords))
                return _buckets[CellCoords];

            return new List<IGridEntity>();
        }

        /// <summary>
        /// Dada una posicion en el mundo, devuelve la coordenada correspondiente a la grilla.
        /// </summary>
        /// <param name="pos">Posción en el mundo</param>
        /// <returns>Posición en la grilla</returns>
        Vector2 GetPositionInGrid(Vector3 pos)
        {
            return new Vector2(Mathf.Abs(Mathf.FloorToInt((pos.x - _origin.x) / _cellSize.x)),
                                Mathf.Abs(Mathf.FloorToInt((pos.z - _origin.y) / _cellSize.y)));
        }
        /// <summary>
        /// Recalcula la celda activa de la grilla de acuerdo a la posición actual del jugador.
        /// </summary>
        void UpdateGridMasterEntityPosition()
        {
            //Calculo la posicíon del player, la active cell sera la posición en la grilla donde está el jugador.
            ActiveCell = GetPositionInGrid(_masterPosition.position);

            if (ActiveCell == _masterLastPos) return;// El jugador no se ha cambiado de bucket.

            //Actualizo el estado de las entidades que siguen vivas en la celda activa y la anterior celda visitada por el jugador.
            var EntitiesToUpdate = GetEntitiesInBucket(ActiveCell)
                                   .Concat(GetEntitiesInBucket(_masterLastPos))
                                   .Where(x => x.GetTransform.GetComponentInChildren<IKilleable>().IsAlive)
                                   .Select(x => Tuple.Create(x, x.GetTransform.GetComponentInChildren<IKilleable>(), x.GetGameObject));

            foreach (var toUpdate in EntitiesToUpdate)
                if (toUpdate.Item2.IsAlive)
                    toUpdate.Item3.SetActive(GetPositionInGrid(toUpdate.Item1.positionInWorld) == ActiveCell);

            _masterLastPos = ActiveCell;//Actualizo la referencia a la ultima celda que visitó el jugador.
        }
        /// <summary>
        /// Determina si una posición se encuentra dentro de los límites de la grilla.
        /// </summary>
        /// <param name="position">Posición a comprobar</param>
        /// <returns>Verdadero si la posicion se encuentra dentro de la grilla</returns>
        bool IsInsideGrid(Vector2 position)
        {
            return _origin.x <= position.x && position.x < _gridSize.x
                && _origin.y <= position.y && position.y < _gridSize.y;
        }

        private void OnDestroy()
        {
            _masterPosition.GetComponent<IGridEntityMaster>().OnMoveMaster -= UpdateGridMasterEntityPosition;
        }

        static IEnumerable<Transform> LazyGetChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                IGridEntity entidad = child.GetComponentInChildren<IGridEntity>();
                if (entidad != null)
                    yield return child;
            }
        }
        IEnumerable<T> GenerateLazyCollection<T>(T seed, Func<T, T> mutate)
        {
            T accum = seed;
            while (true)
            {
                yield return accum;
                accum = mutate(accum);
            }
        }

        #region GRAPHIC REPRESENTATION
        public bool AreGizmosShutDown;
        private void OnDrawGizmos()
        {
            if (_buckets == null || AreGizmosShutDown) return;

            var rows = GenerateLazyCollection(_origin.y, curr => curr + _cellSize.y)
                    .Select(row => Tuple.Create(new Vector3(_origin.x, 0, row),
                                                new Vector3(_origin.x + _cellSize.x * _gridSize.x, 0, row)));

            var cols = GenerateLazyCollection(_origin.x, curr => curr + _cellSize.x)
                       .Select(col => Tuple.Create(new Vector3(col, 0, _origin.y), new Vector3(col, 0, _origin.y + _cellSize.y * _gridSize.y)));

            var allLines = rows.Take((int)_gridSize.x + 1).Concat(cols.Take((int)_gridSize.y + 1));

            foreach (var elem in allLines)
                Gizmos.DrawLine(elem.Item1, elem.Item2);
        }
        #endregion
    }
}
