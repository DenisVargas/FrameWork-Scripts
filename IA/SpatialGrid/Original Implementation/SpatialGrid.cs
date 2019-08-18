using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpatialGrid : MonoBehaviour
{
    #region Variables

    public float originX, originZ;//punto de inicio de la grilla en X y Z.
    public float cellWidth, cellHeight;//ancho y alto de las celdas
    public int gridFullwidth, gridFullheight;//cantidad de columnas (el "ancho" y "alto" de la grilla)

    private Dictionary<GridEntity, Tuple<int, int>> _lastPositions;//ultimas posiciones conocidas de los elementos, guardadas para comparación.
    private HashSet<GridEntity>[,] _buckets;//los "contenedores"

    /*
    const es implicitamente statica, tengo que ponerle el valor apenas la declaro y solo sirve para tipos de dato primitivos.
    readonly es implicitamente statica, puedo ponerle el valor en el constructor.
    */

    readonly public Tuple<int, int> Outside = Tuple.Create(-1, -1);//el valor de posicion que tienen los elementos cuando no estan en la zona de la grilla.
    readonly public GridEntity[] EmptyGridEntityCollection = new GridEntity[0];//Una colección vacía a devolver en las queries si no hay nada que devolver

    #endregion

    #region FUNCIONES
    private void Awake()
    {
       _lastPositions = new Dictionary<GridEntity, Tuple<int, int>>();
       _buckets = new HashSet<GridEntity>[gridFullwidth, gridFullheight];

       //creamos todos los hashsets
       for (int i = 0; i < gridFullwidth; i++)
           for (int j = 0; j < gridFullheight; j++)
               _buckets[i, j] = new HashSet<GridEntity>();

       //P/alumnos: por que no puedo usar OfType<>() despues del RecursiveWalker() aca?
       //Porque Transform no hereda de GridEntity, por lo que no se puede seleccionar ni castear usando ofType. GridEntity es un componente.
       var ents = RecursiveWalker(transform)
           .Select(x => x.GetComponent<GridEntity>())
           .Where(x => x != null);

       foreach (var e in ents)
       {
           e.OnMove += UpdateEntity;
           UpdateEntity(e);
       }
    }

    public void UpdateEntity(GridEntity entity)
    {
       var lastPos = _lastPositions.ContainsKey(entity) ? _lastPositions[entity] : Outside;
       var currentPos = GetPositionInGrid(entity.gameObject.transform.position);

       //Misma posición, no necesito hacer nada
       if (lastPos.Equals(currentPos))
           return;

       //Si la nueva posición cambio y esta dentro de la grilla...
       bool toChange = IsInsideGrid(lastPos);

       //Lo "sacamos" de la posición anterior
       if (toChange)
       {
           _buckets[lastPos.Item1, lastPos.Item2].Remove(entity);
           //Lo "metemos" a la celda nueva, o lo sacamos si salio de la grilla
           _buckets[currentPos.Item1, currentPos.Item2].Add(entity);
           _lastPositions[entity] = currentPos;
       }
       else
           _lastPositions.Remove(entity);
    }

    public IEnumerable<GridEntity> Query(Vector3 aabbFrom, Vector3 aabbTo, Func<Vector3, bool> filterByPosition)
    {
       var from = new Vector3(Mathf.Min(aabbFrom.x, aabbTo.x), 0, Mathf.Min(aabbFrom.z, aabbTo.z));
       var to = new Vector3(Mathf.Max(aabbFrom.x, aabbTo.x), 0, Mathf.Max(aabbFrom.z, aabbTo.z));

       var fromCoord = GetPositionInGrid(from);
       var toCoord = GetPositionInGrid(to);

       //¡Ojo que clampea a 0,0 el Outside! TODO: Checkear cuando descartar el query si estan del mismo lado
       fromCoord = Tuple.Create(Mathf.Clamp(fromCoord.Item1, 0, gridFullwidth), Mathf.Clamp(fromCoord.Item2, 0, gridFullheight));
       toCoord = Tuple.Create(Mathf.Clamp(toCoord.Item1, 0, gridFullwidth), Mathf.Clamp(toCoord.Item2, 0, gridFullheight));

       if (!IsInsideGrid(fromCoord) && !IsInsideGrid(toCoord))
           return EmptyGridEntityCollection;

       //TODO p/Alumno: ¿Cómo haría esto con un Aggregate en vez de generar posiciones?
       //TODO p/Alumno: Cambiar por Where/Take

       // Creamos tuplas de cada celda
       var cols = Generate(fromCoord.Item1, x => x + 1)
           .TakeWhile(x => x < gridFullwidth && x <= toCoord.Item1);

       var rows = Generate(fromCoord.Item2, y => y + 1)
           .TakeWhile(y => y < gridFullheight && y <= toCoord.Item2);

       var cells = cols.SelectMany(
           col => rows.Select(
               row => Tuple.Create(col, row)
           )
       );

       // Iteramos las que queden dentro del criterio
       return cells
           .SelectMany(cell => _buckets[cell.Item1, cell.Item2])
           .Where(e =>
               from.x <= e.transform.position.x && e.transform.position.x <= to.x &&
               from.z <= e.transform.position.z && e.transform.position.z <= to.z
           ).Where(x => filterByPosition(x.transform.position));
    }

    public Tuple<int, int> GetPositionInGrid(Vector3 pos)
    {
       //quita la diferencia, divide segun las celdas y floorea
       return Tuple.Create(Mathf.FloorToInt((pos.x - originX) / cellWidth),
                           Mathf.FloorToInt((pos.z - originZ) / cellHeight));
    }

    public bool IsInsideGrid(Tuple<int, int> position)
    {
       //si es menor a 0 o mayor a width o height, no esta dentro de la grilla
       return 0 <= position.Item1 && position.Item1 < gridFullwidth &&
           0 <= position.Item2 && position.Item2 < gridFullheight;
    }

    void OnDestroy()
    {
       var ents = RecursiveWalker(transform).Select(x => x.GetComponent<GridEntity>()).Where(x => x != null);
       foreach (var e in ents)
           e.OnMove -= UpdateEntity;
    }

    #region GENERATORS
    private static IEnumerable<Transform> RecursiveWalker(Transform parent)
    {
       foreach (Transform child in parent)
       {
           foreach (Transform grandchild in RecursiveWalker(child))
               yield return grandchild;
           yield return child;
       }
    }

    IEnumerable<T> Generate<T>(T seed, Func<T, T> mutate)
    {
       T accum = seed;
       while (true)
       {
           yield return accum;
           accum = mutate(accum);
       }
    }
    #endregion

    #endregion

    #region GRAPHIC REPRESENTATION
    public bool AreGizmosShutDown;
    public bool activatedGrid;
    public bool showLogs = true;
    private void OnDrawGizmos()
    {
       var rows = Generate(originZ, curr => curr + cellHeight)
               .Select(row => Tuple.Create(new Vector3(originX, 0, row),
                                           new Vector3(originX + cellWidth * gridFullwidth, 0, row)));

       //equivalente de rows
       /*for (int i = 0; i <= height; i++)
       {
           Gizmos.DrawLine(new Vector3(x, 0, z + cellHeight * i), new Vector3(x + cellWidth * width,0, z + cellHeight * i));
       }*/

       var cols = Generate(originX, curr => curr + cellWidth)
                  .Select(col => Tuple.Create(new Vector3(col, 0, originZ), new Vector3(col, 0, originZ + cellHeight * gridFullheight)));

       var allLines = rows.Take(gridFullwidth + 1).Concat(cols.Take(gridFullheight + 1));

       foreach (var elem in allLines)
       {
           Gizmos.DrawLine(elem.Item1, elem.Item2);
       }

       if (_buckets == null || AreGizmosShutDown) return;

       var originalCol = GUI.color;
       GUI.color = Color.red;
       if (!activatedGrid)
       {
           IEnumerable<GridEntity> allElems = Enumerable.Empty<GridEntity>();
           foreach (var elem in _buckets)
               allElems = allElems.Concat(elem);

           int connections = 0;
           foreach (var ent in allElems)
           {
               foreach (var neighbour in allElems.Where(x => x != ent))
               {
                   Gizmos.DrawLine(ent.transform.position, neighbour.transform.position);
                   connections++;
               }
               if (showLogs)
                   Debug.Log("tengo " + connections + " conexiones por individuo");
               connections = 0;
           }
       }
       else
       {
           int connections = 0;
           foreach (var elem in _buckets)
           {
               foreach (var ent in elem)
               {
                   foreach (var n in elem.Where(x => x != ent))
                   {
                       Gizmos.DrawLine(ent.transform.position, n.transform.position);
                       connections++;
                   }
                   if (showLogs)
                       Debug.Log("tengo " + connections + " conexiones por individuo");
                   connections = 0;
               }
           }
       }

       GUI.color = originalCol;
       showLogs = false;
    }
    #endregion
}
