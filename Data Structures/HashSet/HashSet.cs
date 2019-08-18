using System.Collections;
using System.Collections.Generic;

class HashSet<Tvalue> : ISet<Tvalue>
{
    List<Tvalue>[] mySet;
    
    public HashSet(int AmmountOfElements)
    {
        mySet = new List<Tvalue>[AmmountOfElements];
    }

    public void Add(Tvalue value)
    {
        var index = value.GetHashCode() % mySet.Length;

        if (mySet[index] == null) mySet[index] = new List<Tvalue>();
        if (!mySet[index].Contains(value)) mySet[index].Add(value);
    }
    public bool Contains(Tvalue value)
    {
        var index = value.GetHashCode() % mySet.Length;
        var bucket = mySet[index];
        if (bucket == null) return false;
        for (int i = 0; i < mySet[index].Count; i++)
            if (bucket[i].Equals(value)) return true;
        return false;
    }
    public void Remove(Tvalue value)
    {
        var index = value.GetHashCode() % mySet.Length;

        if (mySet[index] != null)
            if (mySet[index].Contains(value)) mySet[index].Remove(value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public IEnumerator<Tvalue> GetEnumerator()
    {
        for (int i = 0; i < mySet.Length; i++)
            if (mySet[i] != null)
                for (int j = 0; j < mySet[i].Count; j++)
                    if (mySet[i][j] != null) yield return mySet[i][j];
    }
}
