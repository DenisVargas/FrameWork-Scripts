using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : ActionNode{
    public override void Execute(Npc Reference)
    {
        Npc Character = Reference;
        print("No Enemies are in sight");
        Character.Move();
    }
}
