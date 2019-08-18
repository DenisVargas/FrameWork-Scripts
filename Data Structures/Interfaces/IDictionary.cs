using System.Collections.Generic;

//Clase utilidad para guardar claves-valor
public class KeyValuePair<TKey, TValue>
{
    //Clave
    public TKey key;
    //Valor
    public TValue value;

    public KeyValuePair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

public interface IDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    // Agregar una clave con su valor
    void Add(TKey key, TValue value);
    // Remover una clave (y por concecuencia su valor)
    void Remove(TKey key);
    // Obtener valor segun una clave
    TValue GetValue(TKey key);
    // Esta la clave definida?
    bool ContainsKey(TKey key);

    // Add/Remove pero con indexer
    TValue this[TKey key] { get; set; }
}
