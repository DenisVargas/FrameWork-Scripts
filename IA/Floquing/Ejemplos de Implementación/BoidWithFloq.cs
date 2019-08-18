using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Boid : MonoBehaviour
{
    public bool isLeader;

    public LayerMask LayerBoid;
    public LayerMask LayerObst;

	public List<Collider> friends;
	public List<Collider> obstacles;

    public float speed;
    public float rotSpeed;

    public Vector3 dir;

    private Vector3 _vectCohesion;
    private Vector3 _vectAlineacion;
    private Vector3 _vectSeparacion;
    private Vector3 _vectLeader;
    private Vector3 _vectAvoidance;
    private Vector3 _vectWander;

    public float radFlock;
    public float radObst;

    public float avoidWeight;
    public float leaderWeight;
    public float alineationWeight;
    public float separationWeight;
    public float cohesionWeight;

    public Transform BoidLeader;

    public float WanderThink;
    float currentTimeToWander;

    public float TimeToGetFriends = 1f;
    float currentTimeToGetFriends;

    public float TimeToGetObst = 1f;
    float currentTimeToGetObst;

    public Collider closerOb;

    void Update()
    {
        GetFriendsAndObstacles();
        Flock();
    }
    void Flock()
    {
        GetFriendsAndObstacles();
        closerOb = GetCloserOb();
        _vectCohesion = getCohesion () * cohesionWeight;
		_vectAlineacion = getAlin () * alineationWeight;
		_vectSeparacion = getSep () * separationWeight;
		_vectLeader = getLeader () * leaderWeight;
        _vectWander = getWander();
        _vectAvoidance = getObstacleAvoidance() * avoidWeight;

        dir = _vectAvoidance;
		dir += isLeader ?  _vectWander : _vectCohesion + _vectAlineacion + _vectSeparacion + _vectLeader;

		transform.forward = Vector3.Slerp (transform.forward, dir, rotSpeed * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * speed;
    }


    void GetFriendsAndObstacles()
    {
		friends.Clear ();
		friends.AddRange (Physics.OverlapSphere (transform.position, radFlock, LayerBoid));
		obstacles.Clear ();
		obstacles.AddRange (Physics.OverlapSphere (transform.position, radObst, LayerObst));

	}
    Collider GetCloserOb()
    {
        if (obstacles.Count > 0)
        {
            Collider closer = null;
            float dist = 99999;
            foreach (var item in obstacles)
            {
                var newDist = Vector3.Distance(item.transform.position, transform.position);
                if (newDist < dist)
                {
                    dist = newDist;
                    closer = item;
                }
            }
            return closer;
        }
        else
            return null;
    }
    Vector3 getWander()
    {
        Vector3 wander = _vectWander;

        if (isLeader && currentTimeToWander >= WanderThink)
        {
            wander = Vector3.Slerp(transform.forward, new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)), rotSpeed);
            currentTimeToWander = 0;
        }
        else
        {
            currentTimeToWander += Time.deltaTime;
        }

        return wander;
    }

    Vector3 getAlin()
    {
		Vector3 al = new Vector3();
        foreach (var item in friends)
            al += item.transform.forward;
        return al /= friends.Count;
    }

    Vector3 getSep()
    {
		Vector3 sep = new Vector3 ();
        foreach (var item in friends)
        {
            Vector3 f = new Vector3();
            f = transform.position - item.transform.position;
            float mag = radFlock - f.magnitude;
            f.Normalize();
            f *= mag;
            sep += f;
        }
        return sep /= friends.Count;
    }

    Vector3 getCohesion()
    {
		Vector3 coh = new Vector3 ();
        foreach (var item in friends)
            coh += item.transform.position - transform.position;
        return coh /= friends.Count;
    }
    Vector3 getObstacleAvoidance()
    {
        if (closerOb)
            return transform.position - closerOb.transform.position;
        else return Vector3.zero;
    }
    Vector3 getLeader()
    {
        if (!isLeader)
            return BoidLeader.transform.position - transform.position;
        else
            return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if(_vectAvoidance != Vector3.zero)
        Gizmos.DrawLine(transform.position, _vectAvoidance);
    }

}
