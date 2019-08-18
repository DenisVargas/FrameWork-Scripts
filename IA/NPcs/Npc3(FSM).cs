using System.Collections.Generic;
using UnityEngine;

public class Npc3 : MonoBehaviour {

    GenericFSM FSM;
    List<IState> EstadosDelNpc = new List<IState> {new StateIdle(),new StateJump(),new StateRun()};
        
	void Start ()
    {
	    //Inicializo la state machine y añado los estados.
        FSM = new GenericFSM();
        //Para añadir un Estado necesito un entero(índice) + Un action para Awake, Execute y Sleep;
        for (int i = 0; i < EstadosDelNpc.Count; i++)
            FSM.AddState(i, EstadosDelNpc[i].execute, EstadosDelNpc[i].awake, EstadosDelNpc[i].sleep);
        
    }
	
	// Update is called once per frame
	void Update () {
        FSM.Update();
        if (Input.GetKey(KeyCode.Q))
            FSM.SetState(0);
        if (Input.GetKey(KeyCode.W))
            FSM.SetState(1);
        if (Input.GetKey(KeyCode.E))
            FSM.SetState(2);
    }
}

//SOLUCION 2, HACER LAS CLASES ESTATICAS (?)
//Me permitiría reusar las funciones para todos los npcs con el mismo comportamiento.

//Clase Base
public interface IState
{
    void awake();
    void execute();
    void sleep();
}

//Estados del NPC
public class StateIdle : IState
{
    public void awake()
    {
        MonoBehaviour.print("Entre en el Estado Idlle");
    }

    public void execute()
    {
        MonoBehaviour.print("Estoy en el Estado Idlle");
    }

    public void sleep()
    {
        MonoBehaviour.print("Sali del Estado Idlle");
    }
}

public class StateJump : IState
{
    public void awake()
    {
        MonoBehaviour.print("Entre en el Estado Jump");
    }

    public void execute()
    {
        MonoBehaviour.print("Estoy en el Estado Jump");
    }

    public void sleep()
    {
        MonoBehaviour.print("Sali del Estado Jump");
    }
}

public class StateRun : IState
{
    public void awake()
    {
        MonoBehaviour.print("Entre en el Estado Run");
    }

    public void execute()
    {
        MonoBehaviour.print("Estoy en el Estado Run");
    }

    public void sleep()
    {
        MonoBehaviour.print("Sali del Estado Run");
    }
}

