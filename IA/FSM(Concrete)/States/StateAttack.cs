using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : State
{
    Npc N;
    Transform AttackTarget;

    float TimeToBasicAttack = 0; //Tiempo hasta el proximo ataque.

    int basePhysicalDamage; //Daño fisico basico.
    int baseMagicDamage; //Daño magico basico.
    int modifier_Strenght; //Modificador fuerza.
    float attackRate; //  1 / velocidad de ataque.


    public StateAttack(GameObject A, params object[] BSO_States) : base(A)
    {
        N = A.GetComponent<Npc>();
        basePhysicalDamage = (int)BSO_States[0];
        baseMagicDamage = (int)BSO_States[1];
        modifier_Strenght = (int)BSO_States[2];
        attackRate = 1 / (float)BSO_States[3];
    }

    public override void awake()
    {
        //MonoBehaviour.print("Entre En el estado Attack");
        //MonoBehaviour.print(N.CurrentTarget);
    }
    public override void execute()
    {
        Attack();
    }

    public override void sleep()
    {
        //MonoBehaviour.print("Estoy saliendo de Atack");
        //MonoBehaviour.print(N.CurrentTarget);
    }
    
    private void Attack()
    {
        if (N.CurrentTarget != null)
        {
            AttackTarget = N.CurrentTarget.transform;
            //Calculo de Daño.
            TimeToBasicAttack -= Time.deltaTime;

            if (TimeToBasicAttack <= 0 && AttackTarget != null)
            {
                MonoBehaviour.print("Te ataco viejo");
                float TargetLife = AttackTarget.GetComponent<Unit>().HealhPoints;
                TargetLife -= basePhysicalDamage + modifier_Strenght * 3; //El multiplicador se debe reemplazar en el futuro por algo calculable.
                TimeToBasicAttack = attackRate;

                if (TargetLife <= 0)
                {
                    AttackTarget.GetComponent<Unit>().HealhPoints = (int)TargetLife;
                    N.CheckForEnemies();
                    if (N.CurrentTarget == null)
                        N.FSM.SetState(0);
                }
                else
                    AttackTarget.GetComponent<Unit>().HealhPoints = (int)TargetLife;
            }
        }
        else
            N.FSM.SetState(0);
    }

}