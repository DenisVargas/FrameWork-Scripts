using UnityEngine;

public class QueueTest : MonoBehaviour {
    public Queue<int> myQueue = new Queue<int>();

	// Use this for initialization
	void Start ()
    {
        Debug.LogWarning("Acá inicia el test de Queue");
        if (myQueue.IsEmpty) Debug.LogWarning("La queue inció vacía como corresponde");

        myQueue.Enqueue(10);
        myQueue.Enqueue(20);
        myQueue.Enqueue(30);

        if (myQueue.IsEmpty) print("La queue está vacía");

        if (myQueue.Peek() != 10) print("El primer elemento no coincide");

        if (myQueue.Count != 3) print("El count no coincide");

        myQueue.Dequeue();

        if (myQueue.Count != 2) print("El count no coincide");

        if (myQueue.Peek() != 20) print("El primer elemento no coincide");

        myQueue.Enqueue(40);

        if (myQueue.Count != 3) print("El count no coincide");

        if (myQueue.IsEmpty) print("La queue esta vacía");

        int valueToCompare = 20;
        for (int i = 0; i < 3; i++)
        {
            var currentValue = myQueue.Dequeue();
            if (currentValue != valueToCompare) print(string.Format("El valor {0} es incorrecto",i));
            valueToCompare += 10;
        }

        myQueue.Clear();

        if (!myQueue.IsEmpty) print("La Queue no fue limpiada correctamente.");

        Debug.LogWarning("Acá Termina el test de Queue.");
        Debug.LogWarning("Si no debuguea nada esta todo Ok.");
    }
}
