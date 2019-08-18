using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSuspect : State
{
    //El npc Chequea a su alrededor, seria genial que si calculara una posicion random y se moviera hacia el.
    Npc B;
    Vector3 OriginalOrientation;
    Vector3 SoundPos = Vector3.zero;

    float TimeToReturnIdlle = 8f;
    const float TimeToIdlle = 8f;

    public StateSuspect(GameObject A) : base(A)
    {
        B = A.GetComponent<Npc>();
        OriginalOrientation = GameInstance.transform.forward;
    }

    public override void awake()
    {
        //Cambio mi iconito de dormido por mi iconito de Whut?!
        MonoBehaviour.print("Entre en Suspect");
        Collider[] Encontrados = Physics.OverlapSphere(B.transform.position, B.ViewRange / B.minimunAwarenessDistance, B.Targets);

        foreach (var item in Encontrados)
        {
            float _distanceToTarget;
            _distanceToTarget = Vector3.Distance(B.transform.position, item.transform.position);

            if (_distanceToTarget <= B.ViewRange)
                if (SoundPos == Vector3.zero)
                    SoundPos = item.transform.position;
                else
                    SoundPos += item.transform.position;
        }
        SoundPos /= Encontrados.Length;
    }
    public override void execute()
    {
        Vector3 dir = (SoundPos - GameInstance.transform.position).normalized;
        if (GameInstance.transform.forward != dir)
        {
            GameInstance.transform.forward = Vector3.Slerp(GameInstance.transform.forward, dir, B.rotationSpeed * Time.deltaTime / 5);

        }

        //Roto de vuelta a mi posicion Original y vuelvo a Idlle.
        TimeToReturnIdlle -= Time.deltaTime;
        if (TimeToReturnIdlle <= 0)
        {
            GameInstance.transform.forward = Vector3.Slerp(GameInstance.transform.forward, OriginalOrientation, B.rotationSpeed * Time.deltaTime);
            B.FSM.SetState(0);
        }
    }
    public override void sleep()
    {
        MonoBehaviour.print("He Salido de awareness");
        TimeToReturnIdlle = TimeToIdlle;
    }
}
