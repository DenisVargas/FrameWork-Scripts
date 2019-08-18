using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{
    Npc B;
    public StatePatrol(GameObject A) : base(A)
    {
        B = A.GetComponent<Npc>();
    }
    public override void awake()
    {
        base.awake();
    }
    public override void execute()
    {
        base.execute();
    }
    public override void sleep()
    {
        base.sleep();
    }
}