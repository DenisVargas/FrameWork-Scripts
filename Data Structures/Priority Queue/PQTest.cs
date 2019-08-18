using UnityEngine;

public class PQTest : MonoBehaviour {
    PriorityQueue<string> myPQ;
    string[] fraseCompleta = new string[] { "Aiuda", "me", "desmayo", "Callese", "viejo", "lesbiano" };

	// Use this for initialization
	void Start () {
        myPQ = new PriorityQueue<string>();

        Debug.Log("Comienzo test de Priority Queue ------------------>");

        myPQ.Enqueue(6, "desmayo");
        myPQ.Enqueue(12, "viejo");
        myPQ.Enqueue(3, "me");
        myPQ.Enqueue(15, "lesbiano");
        myPQ.Enqueue(1, "Aiuda");
        myPQ.Enqueue(9, "Callese");

        //print("Lista original");
        //int cou = 0;
        //foreach (var item in myPQ)
        //{
        //    print(string.Format("El item numero {0} es \"{1}\"", cou, item));
        //    cou++;
        //}

        //Test Peek.
        string primeraPalabra = myPQ.Peek();
        if (primeraPalabra != "Aiuda")
        {
            Debug.LogWarning("Hay un problema con el peek.");
            print("Peek devuelve : " + primeraPalabra);
        }

        //Test Dequeue
        bool printedWarning = false;
        for (int i = 0; i < fraseCompleta.Length; i++)
        {
            var elemento = myPQ.Dequeue();
            if (elemento != fraseCompleta[i])
            {
                if (printedWarning == false)
                    Debug.LogWarning("Hay un problema con el Dequeue.");
                printedWarning = true;
                Debug.LogWarning(string.Format("El elemento {0} no coincide, la palabra recibida es {1} y debería ser {2}", i, elemento, fraseCompleta[i]));
            }
        }


        Debug.Log("Si llega hasta aca sin loguear nada esta todo correcto.");
        Debug.Log("Fin de test de Priority Queue <--------------------");

        //Debug.Log("Resumen:");
        //int count = 0;
        //foreach (var item in myPQ)
        //{
        //    print(string.Format("La key es {0} y su valor es \"{1}\"",count,item));
        //    count++;
        //}
    }
}
