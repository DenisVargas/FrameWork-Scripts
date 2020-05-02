public class PoolObject<T> {

    public bool _IsActive;
    public delegate void PoolCallback(T _object);

    private T _object;
    private PoolCallback _initializationCallback;
    private PoolCallback _finalizationCallback;

    //Constructor de la clase
    public PoolObject(T obj, PoolCallback Initializacion, PoolCallback Finalization)
    {
        _object = obj;
        _initializationCallback = Initializacion;
        _finalizationCallback = Finalization;
        _IsActive = false;
    }

    public T GetObject //Funcion que devuelve el objeto.
    {
        get
        {
            return _object;
        }
    }

    public bool isActive //Retorna si esta activo, o permite setear el valor como verdadero o falso.
    {
        get //Retorna el valor.
        {
            return _IsActive;
        }
        set //Permite asignar el valor.
        {
            _IsActive = value;
            if (_IsActive) //Si esta activo, inicializa el componente.
            {
                if (_initializationCallback != null)
                    _initializationCallback(_object);
            }
            else //Sino, finaliza el componente.
            {
                if (_finalizationCallback != null)
                    _finalizationCallback(_object);
            }
        }
    }

}
