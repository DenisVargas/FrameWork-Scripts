using UnityEngine;

[System.Serializable] //Permite que el inspector pueda ver todos los elementos del script.
public class Dialoge
    {
        public string Name;

        [TextArea(3,10)]
        public string[] sentences;
    
    }


