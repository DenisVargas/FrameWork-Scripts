using UnityEngine;

public class StackTest : MonoBehaviour
{
    public Stack<int> myStack = new Stack<int>();

    // Use this for initialization
    void Start()
    {
        Debug.LogWarning("Acá inicia el test de Stack");
        if (myStack.IsEmpty) Debug.LogWarning("La Stack inció vacía como corresponde");

        myStack.Push(10);
        myStack.Push(20);
        myStack.Push(30);

        if (myStack.IsEmpty) print("La queue está vacía");

        if (myStack.Peek() != 30) print("El ultimo elemento no coincide");

        if (myStack.Count != 3) print("El count no coincide");

        myStack.Pop();

        if (myStack.Count != 2) print("El count no coincide");

        if (myStack.Peek() != 20) print("El ultimo elemento no coincide");

        myStack.Push(40);

        if (myStack.Count != 3) print("El count no coincide");

        if (myStack.IsEmpty) print("La queue esta vacía");

        int[] lawea = { 40, 20, 10 };
        for (int i = 0; i < 3; i++)
        {
            var currentValue = myStack.Pop();
            if (currentValue != lawea[i]) print(string.Format("El valor {0} es incorrecto", i));
        }

        myStack.Clear();

        if (!myStack.IsEmpty) print("El Stack no se limpio correctamente");

        Debug.LogWarning("Acá Termina el test de Stack.");
        Debug.LogWarning("Si no debuguea nada esta todo Ok.");
    }
}
