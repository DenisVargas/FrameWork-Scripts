using System.Collections.Generic;

public interface ISet<TValue> : IEnumerable<TValue>
{
    // Agregar un valor
    void Add(TValue value);
    // Remover un valor
    void Remove(TValue value);
    // Existe el valor en el set?
    bool Contains(TValue value);
}