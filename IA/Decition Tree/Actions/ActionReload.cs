public class ActionReload : ActionNode {

    public override void Execute(Npc Reference)
    {
        Npc Character = Reference;

        print("Recargando!");
        Character.Bullets += 10;
        print("Balas actuales: " + Character.Bullets + ".");
    }
}
