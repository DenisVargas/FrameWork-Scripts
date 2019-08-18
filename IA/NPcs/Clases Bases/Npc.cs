using UnityEngine;

public class Npc : MonoBehaviour {
    //Esto tendrá un Estado.
    public float LifePoints = 10;
    public int Bullets = 0;
    public bool EnemyInSight;

    public float Speed;

    //Este NPC usará un Árbol de desiciones.
    public Node DecitionTreeStart;
	
	
	void Update () {
        DecitionTreeStart.Execute(this);
	}

    public void Move()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
}
