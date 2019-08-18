using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PathFinding.Generic
{
	public class PathFindMethod<T>
	{
		///<sumary>
		/// Guarda los nodos que todavía se deben evaluar.
		///</sumary>
		private static HashSet<T> _openSet = new HashSet<T>();
		///<sumary>
		/// Guarda los nodos que ya se han evaluado.
		///</sumary>
		private static HashSet<T> _closedSet = new HashSet<T>();
		///<sumary>
		/// Guarda los nodos "padres" de cada nodo recorrido.
		///</sumary>
		private Dictionary<T,T> parents = new Dictionary<T,T>();
		///<sumary>
		/// Guarda las distancias entre cada nodo, valor G.
		///</sumary>
        private Dictionary<T, float> actualDistances = new Dictionary<T, float>();
		///<sumary>
		/// Guarda las distancias Totales entre cada nodo, valor F.
		///</sumary>
        private Dictionary<T, float> heuristicDistances = new Dictionary<T, float>();

		///<sumary>
		/// Resetea todos los contenedores cuya data esta compartida entre los distintos algoritmos.
		///</sumary>
		private static void ClearSharedData()
		{
			_openSet.Clear();
			_closedSet.Clear();
			parents.Clear();
			actualDistances.Clear();
			heuristicDistances.Clear();
		}

		/// <summary>
		/// Búsqueda en Anchura.
		/// </summary>
		/// <param name="start">Nodo de Inicio de la búsqueda.</param>
		/// <returns>Un camino posible desde el nodo de inicio al nodo final.</returns>
		public static Ienumerable<T> BreadthFirstSearch<T>(T start, Func<T, bool> TargetCheck, Func<T, Ienumerable<T>> getNeighbours)
		{
			ClearSharedData();
			Queue<T> Pendings = new Queue<T>();
			Pendings.Enqueue(start);
			
			while (Pendings.Any())
			{
				T CurrentNode = Pendings.Dequeue();

				if(TargetCheck(CurrentNode))
					return CorrectPath(CurrentNode);
				else
				{
					var neighbours = getNeighbours(CurrentNode)
									.Where(x => !_closedSet.Contains(x));
					
					foreach (T elem in neighbours)
					{
						parents.Add(elem,CurrentNode);
						_closedSet.Add(elem);
						Pendings.Enqueue(elem);
					}
				}
			}
			return default(T);
		}

		/// <summary>
		/// Búsqueda en profundidad.
		/// </summary>
		/// <param name="start">Nodo de Inicio de la búsqueda.</param>
		/// <returns>Un camino posible desde el nodo de inicio al nodo final.</returns>
		public static Ienumerable<T> DepthFirstSearch<T>(T start, Func<T, bool> TargetCheck, Func<T, Ienumerable<T>> getNeighbours)
		{
			ClearSharedData();
			Stack<T> NodeStack = new Stack<T>();
			NodeStack.Push(start);
			
			while (NodeStack.Any())
			{
				T CurrentNode = NodeStack.Pop();
				_closedSet.Add()

				if(TargetCheck(CurrentNode))
					return CorrectPath(CurrentNode);

				var neighbours = getNeighbours(CurrentNode)
									.Where(x => !_closedSet.Contains(x));
					
				foreach (T elem in neighbours)
				{
					parents.Add(elem,CurrentNode);
					_closedSet.Add(elem);
					NodeStack.Push(elem);
				}
			}
			return default(T);
		}

		/// <summary>
		/// Búsqueda de camino mediante Dejkstra.
		/// </summary>
		/// <param name="start">Nodo de inicio de la búsqueda.</param>
		/// <param name="reference">Nodo a encontrar.</param>
		/// <param name="Path">Devuelve un camino posible al nodo objetivo.</param>
		public static Ienumerable<T> Dijkstra<T>(T start, Func<T,bool> TargetCheck, Func<T, Ienumerable<Tuple<T,float>>> GetNeighbours)
		{
			ClearSharedData();			
			Dictionary<T,T> previous = new Dictionary<T,T>();
			Dictionary<T,float> distances = new Dictionary<T, float>();
			
			_openSet.Add(start);
			distances.Add(start, 0);

			while(_openSet.Any())
			{
				T currentNode = _openSet.OrderBy(x => distances[x]).First();
				_openSet.Remove(currentNode);
				_closedSet.Add(currentNode);

				if (targetCheck(currentNode))
					return CorrectPath(currentNode);
				
				var n = getNeighbours(currentNode)
							Where(x => !_closedSet.Contains(x.item1));	
				foreach (var elem in n)
				{
					var currentDist = distances.containsKey(elem.Item1) ? distances[elem.Item1] : float.MaxValue;
					var altDist = distances[currentNode] + elem.Item2;

					if (currentDist > altDist)
					{
						distances[elem.Item1] = altDist;
						parents[elem.Item1] = currentNode;
						_openSet.Add(elem.item1);
					}
				}
				
			}

			return Enumerable.Empty<T>();
		}

		/// <summary>
		/// Búsqueda de camino mediante A Estrella.
		/// </summary>
		/// <param name="Start">Nodo de inicio de la búsqueda.</param>
		/// <param name="End">Nodo a encontrar.</param>
		/// <returns>Un camino posible hasta el nodo.</returns>
		public static Ienumerable<T> AStar<T>(T Start,Func<T,bool> targetCheck, Func<T,Ienumerable<Tuple<T,float>>> GetNeighbours, Func<T,float> GetHeuristic)
		{
			ClearSharedData();

			_openSet.Add(Start);
			actualDistances.Add(start,0f);
			heuristicDistances.Add(start,GetHeuristic(start));
			parents.Add(start,start);

			while (_openSet.Any())
			{
				T currentNode = _openSet.OrderBy(x => heuristicDistances[x]).First();
				_openSet.Remove(CurrentNode);
				_closedSet.Add(CurrentNode);

				if (targetCheck(currentNode))
					return CorrectPath(Start,End);

				var neighbours = getNeighbours(currentNode).Where(x => !_closedSet.Contains(x.item1));
				foreach (var elem in n)
				{
					var currentDist = heuristicDistances.containsKey(elem.item1) ? heuristicDistances[elem.item1] : float.MaxValue;
					var altDist = actualDistances[currentNode] + elem.Item2 + GetHeuristic(elem.Item1);
					
					if (currentDist > altDist)
					{
						heuristicDistances[elem.item1] = altDist;
						actualDistances[elem.item1] = actualDistances[currentNode] + elem.Item2;
						parents[elem.item1] = currentNode;
						_closedSet.Add(elem.Item1);
					}
				}
			}
			return Enumerable.Empty<T>();
		}

		/// <summary>
		/// Búsqueda de camino mediante Theta estrella.
		/// </summary>
		/// <param name="Start">Nodo de inicio de la búsqueda.</param>
		/// <param name="End">Nodo a encontrar.</param>
		/// <returns>El camino mas corto desde el nodo inicial al nodo final.</returns>
		public static Ienumerable<T> ThetaStar<T>(T Start,Func<T,bool> targetCheck, Func<T,Ienumerable<Tuple<T,float>>> GetNeighbours, Func<T,float> GetHeuristic, Func<T,T,bool> parentHasConnection)
		{
			ClearSharedData();

			_openSet.Add(Start);
			actualDistances.Add(start,0f);
			heuristicDistances.Add(start,GetHeuristic(start));
			parents.Add(start,start);

			while (_openSet.Any())
			{
				T currentNode = _openSet.OrderBy(x => heuristicDistances[x]).First();
				_openSet.Remove(CurrentNode);
				_closedSet.Add(CurrentNode);

				if (targetCheck(currentNode))
					return CorrectPath(Start,End);

				var neighbours = getNeighbours(currentNode).Where(x => !_closedSet.Contains(x.item1));
				foreach (var elem in n)
				{
					var currentDist = heuristicDistances.containsKey(elem.item1) ?
									 heuristicDistances[elem.item1] : float.MaxValue;
					var altDist = actualDistances[currentNode] + elem.Item2 + GetHeuristic(elem.Item1);
					
					T currentNodeParent = parents(currentNode);
					bool secondaryRouteHasLessF = false;

					if(parentHasConnection(currentNodeParent, elem.item1))
					{
						var secondaryRoute = (actualDistances[parents[currentNode]] + elem.Item2 + GetHeuristic(elem.item1));
						if(altDist > secondaryRoute)
						{
							secondaryRouteHasLessF = true;
							altDist = secondaryRoute;
						}
					}
					
					if (currentDist > altDist)
					{
						heuristicDistances[elem.item1] = altDist;
						actualDistances[elem.item1] = actualDistances[currentNode] + elem.Item2;
						parents[elem.item1] = secondaryRouteHasLessF ? currentNodeParent : currentNode;
						_closedSet.Add(elem.Item1);
					}
				}
			}
			return Enumerable.Empty<T>();
		}

		///<Sumary>
		/// Retorna el recorrido óptimo generado por uno de los algoritmos de búsqueda de caminos.
		///</Sumary>
		///<param name= "reference">Último nodo de la cadena.</param>
		private static Ienumerable<T> CorrectPath<T>(T reference)
		{
			T currentNodeParent = parents[reference];

			if (currentNodeParent == parents[currentNodeParent])
				return new Ienumerable<T>(){currentNodeParent};
			
			return Ienumerable.Empty()
								.Append(CorrectPath(currentNodeParent))
								.Add(reference);
		}
	}
}


