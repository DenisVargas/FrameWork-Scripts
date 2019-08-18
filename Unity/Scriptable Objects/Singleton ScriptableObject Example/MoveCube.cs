using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float Speed;

    [SerializeField]
    private MasterManager mM; //ReferenciaWorkArround
    // Start is called before the first frame update
    void Start()
    {
        if (MasterManager.Instance != null)
        {
            Speed = MasterManager.DataHolder.SomeData;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Speed * Time.deltaTime;
    }
}
