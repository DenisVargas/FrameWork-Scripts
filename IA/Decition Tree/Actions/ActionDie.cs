public class ActionDie : ActionNode {
    public override void Execute(Npc Reference)
    {
        Npc Character = Reference;
        print("El Npc Ha sido derrotado");
        Destroy(Character.gameObject);
    }
}
