using UnityEngine;

public class StatePursuit : State
{
    Npc A;
    Transform CloserTarget;
    //-------------------------------------Battle-------------------------------------\\
    bool Attaking = false;
    float TimeToAttack;
    float AttackSpeed;
    float TargetsAlive = 0;
    //Constructor
    public StatePursuit(GameObject Instance) : base(Instance)
    {
        A = Instance.GetComponent<Npc>();
        AttackSpeed = 1 / A.BSO_AttackSpeed; //WORTH
    }
    //---------------------------------Metodos-------------------------------------------
    public override void awake()
    {
        MonoBehaviour.print("Entre en Pursuit");
        TimeToAttack = AttackSpeed;
    }

    public override void execute()
    {
        CloserTarget = A.CurrentTarget;
        Persuit();
    }
    public override void sleep()
    {
        MonoBehaviour.print("Sali de pursuit");
    }


    
    private void Persuit()
    {
        if (Vector3.Distance(GameInstance.transform.position, CloserTarget.position) >= A.BSO_AttackRange)
        {
            //Direccion al Objetivo.
            Vector3 Target = (CloserTarget.transform.position - GameInstance.transform.position).normalized;
            //Corrijo Manualmente la altura.
            Target.y = GameInstance.transform.forward.y;

            A.MoveTo(Target);
        }
    }
}