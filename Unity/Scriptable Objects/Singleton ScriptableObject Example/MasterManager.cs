using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Demo/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    [SerializeField]
    private ExampleManager _dataHolder;
    public static ExampleManager DataHolder { get { return Instance._dataHolder; } }
}
