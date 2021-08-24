using UnityEngine;

public class DialogeTrigger : MonoBehaviour {    
    public KeyCode InteractKeyButton;
    public float MaxDistance;
    private GameObject Player;
    private Animator A;
    private bool st;
    [Space]
    [Header("Nombre y lineas de Dialogo")]
    public Dialoge dialoge;

    private void Awake()//Seteos iniciales.
    {
        Player = GameObject.Find("PJ");
        //print(Player);
        A = GameObject.Find("Dialog Box").GetComponent<Animator>();
    }

    private void Update()
    {
        st = A.GetBool("DialogActive");//Evito que al apretar e multiples veces, se reinicie el dialogo.
        if (Vector3.Distance(Player.transform.position, transform.position) < MaxDistance)
            if (Input.GetKeyDown(InteractKeyButton) && !st)
                TriggerDialoge();
    }

    public void TriggerDialoge()
    {
        FindObjectOfType<DialogManager>().StartDialoge(dialoge);
    }
}
