using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
    Npc N;
    float timeToGetBored = 15f;
    const float Cooldown = 15f;

    public StateIdle(GameObject Objeto) : base(Objeto)
    {
        N = Objeto.GetComponent<Npc>();
    }

    public override void awake()
    {
        //MonoBehaviour.print("Entre en el Estado Idlle");
    }

    public override void execute()
    {
        MonoBehaviour.print("Estoy en el Estado Idlle");

        //Si mi NPc esta cansado... Si pasa tiempo sin hacer nada, se aburre y se duerme.
        /*
        if (!N.IsRested)
        {
            timeToGetBored -= Time.deltaTime;
            if (timeToGetBored <= 0)
                N.FSM.SetState(3);
        }
        */
    }

    public override void sleep()
    {
        //MonoBehaviour.print("Sali del Estado Idlle");
        //timeToGetBored = Cooldown;
        //MonoBehaviour.print(timeToGetBored);
    }
}
