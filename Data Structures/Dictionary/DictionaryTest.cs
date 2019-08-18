using UnityEngine;

public class DictionaryTest : MonoBehaviour {
    Dictionary<int, string> myDictionary;
	// Use this for initialization
	void Start () {

        myDictionary = new Dictionary<int, string>();

        Debug.Log("Comienzo test de Dictionary------------------>");
        //Add.
        myDictionary.Add(10, "Numero 10");
        myDictionary.Add(10, "Numero 10");
        myDictionary.Add(12, "Numero 12");
        myDictionary.Add(13, "Numero 13");

        //En este caso ¿debería ser 4 o 3?.
        if (myDictionary.Count != 4)
        {
            Debug.LogWarning("Hay un problema con el add, count Incorrecto");
            print("El count es: " + myDictionary.Count);
        }
        if (myDictionary[10] != "Numero 10"
            || myDictionary[12] != "Numero 12"
            || myDictionary[13] != "Numero 13")
            Debug.LogWarning("Hay un problema con el add, elemento incorrecto.");

        //Geters
        if (myDictionary[10] != "Numero 10")
            Debug.LogWarning("Hay un problema con el getter");

        //Setter
        myDictionary[10] = "Nuevo Numero";
        if (myDictionary[10] != "Nuevo Numero")
            Debug.LogWarning("Hay un problema con el setter");

        if (!myDictionary.ContainsKey(12))
            Debug.LogWarning("Hay un problema con el Contains, numero existente no reconocido.");
        if (myDictionary.ContainsKey(20))
            Debug.LogWarning("Hay un problema con el Contains, numero inexistente reconocido.");

        //Remove.
        myDictionary.Remove(10);
        if (myDictionary.Count != 3)
        {
            Debug.LogWarning("Hay un problema con el add, count Incorrecto");
            print("El count es: " + myDictionary.Count + ", y debería ser: " + 3);
        }

        Debug.Log("Si llega hasta aca sin loguear nada esta todo correcto.");
        Debug.Log("Fin de test de Dictionary<--------------------");

        //Ienumerator
        foreach (var item in myDictionary)
        {
            print(string.Format("El elemento {0} es: {1}", item.key, item.value));
        }
    }
}
