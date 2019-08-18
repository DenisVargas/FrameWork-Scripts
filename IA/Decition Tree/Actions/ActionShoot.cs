public class ActionShoot : ActionNode {
    public override void Execute(Npc Reference)
    {
        Npc Character = Reference;
        print("Pium Pium");
        Character.Bullets--;
    }
}
