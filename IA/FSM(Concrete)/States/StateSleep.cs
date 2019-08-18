using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSleep : State
{
    Npc B;
    public StateSleep(GameObject A) : base(A)
    {
        B = A.GetComponent<Npc>();
    }
    public override void awake()
    {
        //Muestro un iconito de zzz
        MonoBehaviour.print("Entre en estado de sleep");
    }
    public override void execute()
    {
        //Ejecuto la animacion de estoy dormido.
        MonoBehaviour.print("Estoy Dormido");
        B.IsAwakeAndAlert = false;

        Collider[] Encontrados = Physics.OverlapSphere(B.transform.position, B.ViewRange / B.minimunAwarenessDistance, B.Targets);


        //Si encuentro algun enemigo dentro del área de deteccion minima, me despierto.
        if (Encontrados.Length > 0)
        {
            B.IsAwakeAndAlert = true;
            B.FSM.SetState(4);
        }

    }
    public override void sleep()
    {
        MonoBehaviour.print("Estoy Despierto");
        B.IsAwakeAndAlert = true;
        //Me he despertado we.
    }
}
