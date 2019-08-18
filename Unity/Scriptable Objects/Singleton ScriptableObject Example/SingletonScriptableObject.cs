using UnityEngine;
using System.Linq;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                T[] _results = Resources.FindObjectsOfTypeAll<T>();
                if (_results.Length == 0)
                {
                    Debug.LogError("No hay instancias de " + typeof(T).ToString() + " disponibles.");
                    return null;
                }
                if (_results.Length > 1)
                {
                    Debug.LogError("Hay mas de una instancia de " + typeof(T).ToString() + " disponible.");
                    return null;
                }

                return _results[0];
            }

            return _instance;
        }
    }
}
