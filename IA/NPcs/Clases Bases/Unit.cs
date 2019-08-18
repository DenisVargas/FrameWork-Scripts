using UnityEngine;

public class Unit : MonoBehaviour {
    //Este es el comportamiento Básico de las unidades.
    public int HealhPoints;
    
	// Update is called once per frame
	public virtual void Update () {
        if (HealhPoints <= 0)
            Die();
	}

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
