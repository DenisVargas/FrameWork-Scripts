using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

	public Transform target;

	public Transform obstacleContainer;

	public float obstacleRadius;
	public float speed;

	public float toTargetWeight = 1;
	public float avoidObstacleWeight = 1;

	private Vector3 dirToTarget;
	private Vector3 dirToObstacle;

	private float toTargetWeightFixed;
	private float avoidObstacleWeightFixed;
	private float avoidMultiplier;

	private Transform[] obstacles;

	private void Start() {

		obstacles = obstacleContainer.GetComponentsInChildren<Transform>();

	}

	// Update is called once per frame
	void Update() {
		var totalWeight = toTargetWeight + avoidObstacleWeight;

		toTargetWeightFixed = toTargetWeight / totalWeight;
		avoidObstacleWeightFixed = avoidObstacleWeight / totalWeight;

		dirToTarget = (target.position - transform.position).normalized;

		var obstacle = GetClosest();

		var distanceToObstacle = Vector3.Distance(transform.position, obstacle.position);

		dirToObstacle = Vector3.zero;
		if (distanceToObstacle <= obstacleRadius) {
			dirToObstacle = -(obstacle.position - transform.position).normalized;
		}

		avoidMultiplier = 1 - distanceToObstacle / obstacleRadius;

		transform.position += (dirToTarget * toTargetWeightFixed + dirToObstacle * avoidObstacleWeightFixed * avoidMultiplier) * speed * Time.deltaTime;

		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

	Transform GetClosest() {

		Transform closest = null;
		var minDist = float.PositiveInfinity;

		foreach (Transform obstacle in obstacles) {

			var dist = Vector3.Distance(transform.position, obstacle.position);

			if (dist < minDist) {
				minDist = dist;
				closest = obstacle;
			}

		}
		return closest;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, transform.position + dirToTarget * toTargetWeightFixed * 3);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + dirToObstacle * avoidObstacleWeightFixed * avoidMultiplier*3);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, obstacleRadius);
	}
}
