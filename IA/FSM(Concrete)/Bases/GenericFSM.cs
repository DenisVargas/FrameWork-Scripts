using System;
using System.Collections.Generic;
using System.Linq;

public class GenericFSM {
    //.....................................................................................
    int _currentState = -1;
    List<int> _states = new List<int>();

    Dictionary<int, List<Action>> _awakes = new Dictionary<int, List<Action>>();
    Dictionary<int, List<Action>> _executes = new Dictionary<int, List<Action>>();
    Dictionary<int, List<Action>> _sleeps = new Dictionary<int, List<Action>>();

    List<StateTransition> _transitions = new List<StateTransition>();
    public enum actionType
    {
        awake, execute, sleep
    }
    //.....................................................................................
    /// <summary>
    /// Actualiza el estado de la FSM
    /// </summary>
    public void Update()
    {
        int ChangeState = -1;
        if (_transitions.Where(x => x.state == _currentState || x.state == -2).Count() > 0)
        {
            //Lista que contiene todas las transiciones en la que aparezca nuestro Estado actual.
            var posTrans = _transitions.Where(x => x.state == _currentState || x.state == -2).ToList();
            foreach (var tran in posTrans)
            {
                ChangeState = tran.nextState;
                //por cada Transicion...
                foreach (var condition in tran.condition)
                    if (!condition()) //Si las condiciones no se cumplen...
                        ChangeState = -1; //El estado se resetea a -1 (original).
                //Si el estado es diferente a -1
                if (ChangeState != -1)
                {
                    SetState(ChangeState);//Seteo el nuevo estado.
                    return;//Termino nuestra actualizacion de cambio de estado.
                }
            }
        }
        //Si changeState se ha mantenido, y nuestro current state no es -1(inexistente) y existe en la lista de executes.
        if (_currentState != -1 && _executes.ContainsKey(_currentState) && ChangeState == -1)
            foreach (var act in _executes[_currentState])
                act();
    }

    /// <summary>
    /// Busca un estado dentro de la FSM.
    /// </summary>
    /// <param name="st">El estado parámetro.</param>
    /// <returns>El índice del estado pedido.</returns>
    public int SearchState(object st)
    {
        int s = (int)st;
        for (int i = 0; i < _states.Count(); i++)
            if (_states[i] == s)
                return i;
        return -1;
    }

    /// <summary>
    /// Permite seleccionar el estado actual.
    /// </summary>
    /// <param name="state"></param>
    public void SetState(object state)
    {
        int st = (int)state; //Guardo el indice del estado recibido.
        if (IsActualState(st)) //Si es el mismo al estado actual, no hago nada.
            return;

        for (int i = 0; i < _states.Count; i++)//De lo contrario...
            if (_states[i] == st)//Busco el estado en mi lista de estados.
            {
                //Si el estado es distinto a -1 y mi estado actual tiene un sleep...
                if (_currentState != -1 && _sleeps.ContainsKey(_currentState))
                    foreach (var action in _sleeps[_currentState])
                        action(); //Ejecuto la accion de Dormir.
                //He dormido el estado actual.
                _currentState = _states[i]; //Selecciono el nuevo estado.
                if (_awakes.ContainsKey(_currentState)) //Si en awake existe el método awake del estado actual(nuevo).
                    foreach (var act in _awakes[_currentState])//Recorro mi lista de awakes hasta encontrar el que busco y ejecuto la acción.
                        act();
                //He desperado el nuevo estado.
                //Complete el Cambio de Estado.
            }
    }

    /// <summary>
    /// Permite dar por terminado el estado actual.
    /// </summary>
    public void EndCurrentState()
    {
        //Si nuestro Estado actual no es nulo, y esta dentro de nuestra lista de _sleeps
        if (_currentState != -1 && _sleeps.ContainsKey(_currentState))
            foreach (var act in _sleeps[_currentState]) //Recorro _sleeps hasta encontrar el estado actual
                act(); //Ejecuto su sleep.
    }
    //-------------------------------------------------------------------------------------
    #region Añadir nuevos Elementos
    /// <summary>
    /// Añade todos los estados a la FSM
    /// </summary>
    public void AddAllStates<T>()
    {
        foreach (var value in Enum.GetValues(typeof(T)))
            _states.Add((int)value);
    }
    /// <summary>
    /// Añade un nuevo Estado a la FSM.
    /// </summary>
    /// <param name="state">Objeto(Clase) que contiene al nuevo estado.</param>
    /// <param name="baseExecute">Funcion Execute() del nuevo Estado.</param>
    /// <param name="baseAwake">Funcion Awake() del nuevo Estado.</param>
    /// <param name="baseSleep">Funcion Sleep() del nuevo Estado.</param>
    public void AddState(object state,Action baseExecute = null, Action baseAwake = null, Action baseSleep = null)
    {
        int st = (int)state; //Guardo el valor dado por parámetro.
        int ss = SearchState(st);//Ejecuto Search state. Devuelve -1 si el estado todavía no existe.
        if (ss == -1)
        {
            _states.Add(st);
            _awakes[st] = new List<Action>();
            _executes[st] = new List<Action>();
            _sleeps[st] = new List<Action>();
            if (baseExecute != null)
                AddAction(st, actionType.execute, baseExecute);
            if (baseAwake != null)
                AddAction(st, actionType.awake, baseAwake);
            if (baseSleep != null)
                AddAction(st, actionType.sleep, baseSleep);
        }
    }

    /// <summary>
    /// Añade una Acción posible a los Estados existentes.
    /// </summary>
    /// <param name="state">Objeto que contiene el Estado</param>
    /// <param name="type">Selecciona el tipo de acción</param>
    /// <param name="action">Funcion que ejecuta.</param>
    public void AddAction(object state, actionType type, Action action)
    {
        int st = (int)state;
        switch (type)
        {
            case actionType.awake:
                _awakes[st].Add(action);
                break;
            case actionType.execute:
                _executes[st].Add(action);
                break;
            case actionType.sleep:
                _sleeps[st].Add(action);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Añade una Transicion entre 2 estados existentes con una sola condición.
    /// </summary>
    /// <param name="st">El estado inicial.</param>
    /// <param name="cond">La condicion.</param>
    /// <param name="next">El estado final.</param>
    public void AddTransition(object st, Func<bool> cond, object next)
    {
        _transitions.Add(new StateTransition() { state = (int)st, condition = new List<Func<bool>>().Compose(cond), nextState = (int)next});
    }
    /// <summary>
    /// Añade una Transicion entre 2 estados existentes con multiples condiciones.
    /// </summary>
    /// <param name="st">El estado inicial.</param>
    /// <param name="cond">La lista de condiciones.</param>
    /// <param name="action">El estado Final</param>
    public void AddTransition(object st, List<Func<bool>> cond, object next)
    {
        _transitions.Add(new StateTransition() {state = (int)st, nextState = (int)next, condition = cond});
    }
    #endregion
    //-------------------------------------------------------------------------------------
    #region Retornos
    /// <summary>
    /// Retorna verdadero si el estado dado como parámetro es el estado actual de la FSM.
    /// </summary>
    public bool IsActualState(object state)
    {
        int st = (int)state;
        return (_currentState != -1) ? (_currentState == st) : false;
    }
    /// <summary>
    /// Retorna el índice del Estado actual.
    /// </summary>
    /// <returns></returns>
    public int GetCurrentState()
    {
        return _currentState;
    }
    #endregion
    //-------------------------------------------------------------------------------------
}
/// <summary>
/// Esta estructura almacena una transicion entre dos estados.
/// </summary>
public struct StateTransition
{
    public int state;
    public int nextState;
    public List<Func<bool>> condition;
}

public static class ClassExtentions
{
    public static List<T>Compose<T>(this List<T> baselist, T value)
    {
        baselist.Add(value);
        return baselist;
    }
}
