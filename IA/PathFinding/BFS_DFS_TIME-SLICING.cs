using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utility
{
    public static IEnumerable<T> Generate<T>(T seed, Func<T, T> modify)
    {
        T acum = seed;
        while (true)
        {
            yield return acum;
            acum = modify(acum);
        }
    }

	#region BFS, DFS, Busqueda generica
    public static T DFS<T>(T start, Func<T, bool> targetCheck, Func<T, IEnumerable<T>> GetNeighbours)
    {
        Stack<T> pending = new Stack<T>();
        pending.Push(start);
        HashSet<T> visited = new HashSet<T>();

        T current = default(T);

        while (pending.Any())
        {
            current = pending.Pop();
            visited.Add(current);

            if (targetCheck(current))
            {
                return current;
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x));
                foreach (var elem in n)
                {
                    pending.Push(elem);
                }
            }
        }

        return default(T);
    }

    public static T BFS<T>(T start, Func<T, bool>  targetCheck, Func<T, IEnumerable<T>> GetNeighbours)
    {
        HashSet<T> visited = new HashSet<T>();
        visited.Add(start);
        Queue<T> pending = new Queue<T>();
        pending.Enqueue(start);

        while (pending.Any())
        {
            T current = pending.Dequeue();

            if (targetCheck(current))
            {
                return current;
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x));
                foreach (var elem in n)
                {
                    visited.Add(elem);
                    pending.Enqueue(elem);
                }
            }
        }

        return default(T);
    }

    public static T GenericIndividualSearch<T>(T start, Func<T, bool> targetCheck, Func<T, IEnumerable<T>> GetNeighbours,
                                                Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> AddItems)
    {
        HashSet<T> visited = new HashSet<T>();
        IEnumerable<T> pending = new T[1] { start };

        while (pending.Any())
        {
            T current = pending.First();
            pending = pending.Skip(1);
            visited.Add(current);

            if (targetCheck(current))
            {
                return current;
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x)).ToList(); //para romper side effect
                pending = AddItems(pending, n);
            }
        }

        return default(T);
    }

    public static IEnumerable<T> GenericSearch<T>(T start, Func<T, bool> targetCheck, Func<T, IEnumerable<T>> GetNeighbours,
                                                Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> AddItems)
    {
        HashSet<T> visited = new HashSet<T>();
        IEnumerable<T> pending = new T[1] { start };
        Dictionary<T, T> parent = new Dictionary<T, T>();

        while (pending.Any())
        {
            T current = pending.First();
            pending = pending.Skip(1);
            visited.Add(current);

            if (targetCheck(current))
            {
                return Generate(current, x => parent[x])
                        .TakeWhile(x => parent.ContainsKey(x))
                        .Reverse();
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x)).ToList();
                pending = AddItems(pending, n);
                foreach(var elem in n)
                {
                    parent.Add(elem, current);
                }
            }
        }

        return Enumerable.Empty<T>();
    }
	#endregion

	
    public static IEnumerable<T> Dijkstra<T>(T start, Func<T, bool> targetCheck, Func<T, IEnumerable<Tuple<T, float>>> GetNeighbours)
    {
        HashSet<T> visited = new HashSet<T>();
        Dictionary<T, T> previous = new Dictionary<T, T>();
        Dictionary<T, float> distances = new Dictionary<T, float>();
        List<T> pending = new List<T>();

        distances.Add(start, 0);
        pending.Add(start);

        while (pending.Any())
        {
            T current = pending.OrderBy(x => distances[x]).First();
            pending.Remove(current);
            visited.Add(current);

            if (targetCheck(current))
            {
                return Generate(current, x => previous[x])
                        .TakeWhile(x => previous.ContainsKey(x))
                        .Reverse();
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x.Item1));
                foreach (var elem in n)
                {
                    var altDist = distances[current] + elem.Item2;
                    if (!distances.ContainsKey(elem.Item1) || distances[elem.Item1] > altDist)
                    {
                        distances[elem.Item1] = altDist;
                        previous[elem.Item1] = current;
                        pending.Add(elem.Item1);
                    }
                }
            }
        }

        return Enumerable.Empty<T>();
    }


	#region Time SLICING
    public static IEnumerable<Tuple<T, IEnumerable<T>>> TimeSlice_A_Dijkstra<T>(T start,
                                                            Func<T, bool> targetCheck,
                                                            Func<T, IEnumerable<Tuple<T, float>>> GetNeighbours)
    {
        HashSet<T> visited = new HashSet<T>();
        Dictionary<T, T> previous = new Dictionary<T, T>();
        Dictionary<T, float> distances = new Dictionary<T, float>();
        List<T> pending = new List<T>();

        distances.Add(start, 0);
        pending.Add(start);

        while (pending.Any())
        {
            T current = pending.OrderBy(x => distances[x]).First();
            pending.Remove(current);
            visited.Add(current);

            if (targetCheck(current))
            {
                var path = Generate(current, x => previous[x])
                        .TakeWhile(x => previous.ContainsKey(x))
                        .Reverse();

                yield return Tuple.Create(current, path);
                break;
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x.Item1));
                foreach (var elem in n)
                {
                    var altDist = distances[current] + elem.Item2;
                    if (!distances.ContainsKey(elem.Item1) || distances[elem.Item1] > altDist)
                    {
                        distances[elem.Item1] = altDist;
                        previous[elem.Item1] = current;
                        pending.Add(elem.Item1);
                    }
                }

                yield return Tuple.Create(current, Enumerable.Empty<T>());
            }
        }
    }

    public static IEnumerator StartTimeA()
    {
        var wait = new WaitForSeconds(0.05f);
        var path = TimeSlice_A_Dijkstra(0, x => x == 7, x => new Tuple<int, float>[] { Tuple.Create(x + 1, 1f), Tuple.Create(x + 2, 1f) });
        foreach (var elem in path)
        {
            Debug.Log(elem);
            yield return wait;
        }
    }

    public static IEnumerator TimeSlice_B_GenericSearch<T>(T start, 
                                                            Func<T, bool> targetCheck,
                                                            Func<T, IEnumerable<T>> GetNeighbours,
                                                            Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> AddItems,
                                                            Action<IEnumerable<T>> callback)
    {
        HashSet<T> visited = new HashSet<T>();
        IEnumerable<T> pending = new T[1] { start };
        Dictionary<T, T> parent = new Dictionary<T, T>();

        var wait = new WaitForSeconds(0.05f);

        while (pending.Any())
        {
            T current = pending.First();
            pending = pending.Skip(1);
            visited.Add(current);

            if (targetCheck(current))
            {
                var path = Generate(current, x => parent[x])
                        .TakeWhile(x => parent.ContainsKey(x))
                        .Reverse();

                callback(path);
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x)).ToList();
                pending = AddItems(pending, n);
                foreach (var elem in n)
                {
                    parent.Add(elem, current);
                }

                yield return wait;
            }
        }

        callback(Enumerable.Empty<T>());
    }

	#endregion
	
    public static IEnumerable<T> AStar<T>(T start,
                                            Func<T, bool> targetCheck,
                                            Func<T, IEnumerable<Tuple<T, float>>> GetNeighbours,
                                            Func<T, float> GetHeuristic)
    {
        HashSet<T> visited = new HashSet<T>();
        Dictionary<T, T> previous = new Dictionary<T, T>();
        Dictionary<T, float> actualDistances = new Dictionary<T, float>();
        Dictionary<T, float> heuristicDistances = new Dictionary<T, float>();
        List<T> pending = new List<T>();
        pending.Add(start);
        actualDistances.Add(start, 0f);
        heuristicDistances.Add(start, GetHeuristic(start));

        while (pending.Any())
        {
            var current = pending.OrderBy(x => heuristicDistances[x]).First();
            pending.Remove(current);
            visited.Add(current);

            if (targetCheck(current))
            {
                return Generate(current, x => previous[x])
                                .TakeWhile(x => previous.ContainsKey(x))
                                .Reverse();
            }
            else
            {
                var n = GetNeighbours(current).Where(x => !visited.Contains(x.Item1));
                foreach (var elem in n)
                {
                    var altDist = actualDistances[current] + elem.Item2 + GetHeuristic(elem.Item1);
                    var currentDist = heuristicDistances.ContainsKey(elem.Item1) ? heuristicDistances[elem.Item1] : float.MaxValue;

                    if (currentDist > altDist)
                    {
                        heuristicDistances[elem.Item1] = altDist;
                        actualDistances[elem.Item1] = actualDistances[current] + elem.Item2;
                        previous[elem.Item1] = current;
                        pending.Add(elem.Item1);
                    }
                }
            }
        }
        return Enumerable.Empty<T>();
    }
}