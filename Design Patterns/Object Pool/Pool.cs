using System.Collections.Generic;

public class Pool<T> {
    private List<PoolObject<T>> _PoolList;
    public delegate T CallBackFactory();

    private int _count;
    private bool _IsDynamic = false;
    private PoolObject<T>.PoolCallback _init;  //Exporta una función que actua como método de inicialización.
    private PoolObject<T>.PoolCallback _finit; //Exportar una función que actua como médoto de finalización.
    private CallBackFactory _FactoryMethod;

    //Constructor
    public Pool(int StockInicial, CallBackFactory factoryMethod, PoolObject<T>.PoolCallback Initialize, PoolObject<T>.PoolCallback Finalize, bool IsDynamic)
    {
        _PoolList = new List<PoolObject<T>>(); //inicializo la lista.

        //Guardamos los punteros
        _FactoryMethod = factoryMethod;
        _IsDynamic = IsDynamic;
        _count = StockInicial;
        _init = Initialize;
        _finit = Finalize;

        //Generamos el stock inicial
        for (int i = 0; i < _count; i++)
        {
            _PoolList.Add(new PoolObject<T>(_FactoryMethod(), _init, _finit)); //Agregamos a nuestra lista, un objeto nuevo.
        }
    }

    public PoolObject<T> GetPoolObject() //Devuelve un objeto del pool, y si es dinamico, puede generar bajo demanda.
    {
        for (int i = 0; i < _count; i++)
        {
            if (!_PoolList[i]._IsActive)
            {
                _PoolList[i]._IsActive = true;
                return _PoolList[i];
            }
        }
        if (_IsDynamic)
        {
            PoolObject<T> po = new PoolObject<T>(_FactoryMethod(),_init,_finit);
            po._IsActive = true;
            _PoolList.Add(po);
            _count++;
            return po;
        }
        return null;
    }

    public T GetObjectFromPool()
    {
        for (int i = 0; i < _count; i++)
        {
            if (!_PoolList[i]._IsActive)
            {
                _PoolList[i]._IsActive = true;
                return _PoolList[i].GetObject;
            }
        }
        if (_IsDynamic)
        {
            PoolObject<T> po = new PoolObject<T>(_FactoryMethod(), _init, _finit);
            po._IsActive = true;
            _PoolList.Add(po);
            _count++;
            return po.GetObject;
        }

        return default(T);
    }

    public void DisablePoolObject(T obj)//Deshabilita el objeto.
    {
        foreach (var item in _PoolList)
        {
            if (item.GetObject.Equals(obj))
            {
                item._IsActive = false;
                return;
            }
        }
    }
}
