using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public class Node : MonoBehaviour {
		public string nodeName;//Opcional.

		//PathFinding.
		public List<Node> Connections = new List<Node>();
		public Node Parent;
		public float G = 0;
		public float H = 0;
		public float F = 0;
		public float Index = 0;
		public bool isBlocked = false;
		public bool Visited = false;

		private void OnDrawGizmos()
		{
			if (Connections != null && Connections.Count > 0)
				foreach (var item in Connections)
					if (item.Connections.Contains(this))
					{
						Gizmos.color = Color.green;
						Gizmos.DrawLine(transform.position, item.transform.position);
					}
					else
					{
						Gizmos.color = Color.red;
						Gizmos.DrawLine(transform.position, item.transform.position);
					}
		}
	}

	public static class NodeExtentions
	{
		public static void ResetState(this Node node)
		{
			node.Parent = null;
			node.G = 0;
			node.H = 0;
			node.F = 0;
			node.Index = 0;
			node.Visited = false;
			node.isBlocked = false;
		}
		//Calculamos F.
		public static float CalculateF (this Node node, Node Target, float ThetaIndex = 0)
		{
			return (node.Parent.F + node.G + node.H + ThetaIndex);
		}
		
		//Calculamos G.
		public static float CalculateG (this Node node, Node NeighbourNode)
		{
			return Vector3.Distance(NeighbourNode.transform.position, node.transform.position);
		}
		
		//Calculamos H.
		public static float CalculateH (this Node node, Node NeighbourNode)
		{
			//Calculamos posicion Manhattan.
			return ((NeighbourNode.transform.position.x - node.transform.position.x) + (NeighbourNode.transform.position.z - node.transform.position.z));
		}
		
		//Calculamos Index (Opcional para AStar, Obligatorio para ThetaStar).
		public static float CalculateIndex (this Node node, Node ObjectiveNode)
		{
			return Vector3.Distance(node.transform.position, ObjectiveNode.transform.position);
		}
		
	}
}


