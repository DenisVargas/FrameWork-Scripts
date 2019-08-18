using System.Collections;
using System.Collections.Generic;

public class Dictionary<TKey, Tvalue> : IDictionary<TKey, Tvalue>
{
    List<KeyValuePair<TKey, Tvalue>> myDictionary = new List<KeyValuePair<TKey, Tvalue>>();

    public int Count { get { return myDictionary.Count; } }
    public Tvalue this[TKey key]
    {
        get
        {
            for (int i = 0; i < myDictionary.Count; i++)
                if (myDictionary[i].key.Equals(key)) return myDictionary[i].value;
            return default(Tvalue);
        }
        set
        {
            for (int i = 0; i < myDictionary.Count; i++)
                if (myDictionary[i].key.Equals(key))
                {
                    myDictionary[i].value = value;
                    break;
                }
        }
    }

    public void Add(TKey key, Tvalue value)
    {
        KeyValuePair<TKey, Tvalue> newItem = new KeyValuePair<TKey, Tvalue>(key, value);
        if (!myDictionary.Contains(newItem)) myDictionary.Add(newItem);
    }
    public bool ContainsKey(TKey key)
    {
        for (int i = 0; i < myDictionary.Count; i++)
            if (myDictionary[i].key.Equals(key)) return true;
        return false;
    }
    public Tvalue GetValue(TKey key)
    {
        for (int i = 0; i < myDictionary.Count; i++)
            if (myDictionary[i].key.Equals(key)) return myDictionary[i].value;
        return default(Tvalue);
    }
    public void Remove(TKey key)
    {
        for (int i = 0; i < myDictionary.Count; i++)
            if (myDictionary[i].key.Equals(key))
            {
                myDictionary.Remove(myDictionary[i]);
                return;
            }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public IEnumerator<KeyValuePair<TKey, Tvalue>> GetEnumerator()
    {
        foreach (var item in myDictionary)
            yield return item;
    }
}
