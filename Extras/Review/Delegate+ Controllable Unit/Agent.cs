using UnityEngine;
using FSM;

[RequireComponent(typeof(Rigidbody), typeof(KeyboardController))]
public class Agent : MonoBehaviour
{
    public float jumpForce;

    enum Inputs
    {
       JUMP_PRESS,
       CROUCH_PRESS,
       CROUCH_RELEASE,
       FLOOR_TOUCHED
    }
    StateMachine<Inputs> StateMachine;
    KeyboardController keyController;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        keyController = GetComponent<KeyboardController>();
        
        //Primero creo los estados
        #region Definicion

        var idlle = new State<Inputs>("IDLE");
        var jump = new State<Inputs>("Jumping");
        var crouched = new State<Inputs>("Crouched");

        #endregion
        //Ahora las Transiciones
        #region Transiciones

        idlle.AddTransition(Inputs.JUMP_PRESS, jump);
        idlle.AddTransition(Inputs.CROUCH_PRESS, crouched);

        jump.AddTransition(Inputs.FLOOR_TOUCHED, idlle);

        crouched.AddTransition(Inputs.CROUCH_RELEASE, idlle);
        crouched.AddTransition(Inputs.JUMP_PRESS, jump);
        #endregion

        //Ahora definimos los comportamientos.
        #region Comportamientos

        idlle.OnEnter += () => print("IDLE");

        jump.OnEnter += () => rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        crouched.OnEnter += () => transform.localScale = new Vector3(1, 0.5f, 1);
        crouched.OnExit += () => transform.localScale = new Vector3(1, 1, 1);

        #endregion

        //Luego la fsm pasandole el primer Estado.
        StateMachine = new StateMachine<Inputs>(idlle);

        //Relleno las funciones del keyboard controller
        keyController.OnJumpPressed += () => StateMachine.Feed(Inputs.JUMP_PRESS);
        keyController.OnCrouchedPressed += () => StateMachine.Feed(Inputs.CROUCH_PRESS);
        keyController.OnCrouchedRelease += () => StateMachine.Feed(Inputs.CROUCH_RELEASE);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        StateMachine.Feed(Inputs.FLOOR_TOUCHED);
    }
}
