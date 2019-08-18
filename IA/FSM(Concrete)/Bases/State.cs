using UnityEngine;

public abstract class State
{   //Todos los states guardan una Referencia al GameObject que modifican.
    public GameObject GameInstance;
    public State(GameObject Objeto)
    {
        GameInstance = Objeto;
    }
    public virtual void awake()
    {
    }
    public virtual void execute()
    {
    }
    public virtual void sleep()
    {
    }
}

