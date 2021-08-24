using System;
using UnityEngine;

//Esto es observer con Delegado

public class KeyboardController : MonoBehaviour
{
    public event Action OnJumpPressed = delegate { };
    public event Action OnCrouchedPressed = delegate { };
    public event Action OnCrouchedRelease = delegate { };

    //Event Añade restricciones
    //Solo se puede llamar desde la clase en la que es definida
    //Solo se puede añadir, no sobreescribir

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnJumpPressed();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnCrouchedPressed();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            OnCrouchedRelease();
        }
    }
}
