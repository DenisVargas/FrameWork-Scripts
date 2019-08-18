using UnityEngine;

public class HashSetTest : MonoBehaviour {
    HashSet<int> myHashSet = new HashSet<int>(5);
	// Use this for initialization
	void Start () {
        Debug.Log("Comienzo test de HashSet------------------>");

        myHashSet.Add(1);
        myHashSet.Add(2);
        myHashSet.Add(3);
        myHashSet.Add(4);
        myHashSet.Add(5);
        myHashSet.Add(6);
        myHashSet.Add(7);
        myHashSet.Add(8);
        myHashSet.Add(9);
        myHashSet.Add(10);

        for (int i = 1; i <= 10; i++)
            if (!myHashSet.Contains(i)) Debug.LogWarning("Hay un problema con el contains! El elemento: " + i + " es incorrecto.");
        if (myHashSet.Contains(12)) Debug.LogWarning("Hay un problema con el contains!");
        if (!myHashSet.Contains(8)) Debug.LogWarning("Hay un problema con el contains!");

        myHashSet.Remove(7);

        if (myHashSet.Contains(7)) Debug.LogWarning("Hay un problema con el remove!");

        Debug.Log("Si llega hasta aca sin loguear nada esta todo correcto.");
        Debug.Log("Fin de test de HashSet<--------------------");
    }
}
