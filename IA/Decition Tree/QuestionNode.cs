using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Node
{
    // *1
    //Guardo los posibles resultados.
    public Node TrueNode;
    public Node FalseNode;

    // *2
    //Creo los parámetros con el que se evalua el nodo y guardo mi valor actual.
    public enum Questions
    {
        HasHp,
        HasBullets,
        EnemyInSight
    }
    public Questions CurrentCuestion;

    // *3
    //Por cada pregunta debo crear una funcion (Acción).
    Func<Npc, bool> HasHp = NpcSeleccionado => NpcSeleccionado.LifePoints > 0; //Explicación de esta linea abajo we.
    #region Explicación
    /*
     *                      //Cuando solo hay un     //Esto es una expresion: una expresion es una secuencia de operandos que se pueden evaluar como un valor, objeto o Namespace.
                            //Parametro no es nece-  //Por eso no es necesario usar Return. Cuando el lambda se escribe sin "{}" se deduce que lo que le sigue es una Expresion y se evalúa como tal.
                            //sario usar "()"        //Si se usa {} se deduce que lo que le sigue es una Instruccion. Las instrucciones pueden ser varias líneas.
                            //En el ejemplo seria así Parametro de entrada => { return ParametrodeEntrada.Lifepoints > 0; }; 

     * Una Func es una funcion que Devuelve un tipo de valor, su Opuesto seria Action que sería equivalente a un método Void.
        Ambos pueden recibir hasta 4 valores de parámetro.
        
    */
    //Esto seria la abreviatura de:
    /*
         private bool HasHP (Npc ParametrodeEntrada1)
         {
            return ParametrodeEntrada1.Lifepoints > 0; //Booleano.
         }

        O TAMBIEN:

        private bool HasHP (Npc ParametrodeEntrada1)
         {
            if(ParametrodeEntrada1 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
         }

    */
    #endregion
    Func<Npc, bool> HasBullets = Npcseleccionado => Npcseleccionado.Bullets > 0;
    Func<Npc, bool> EnemyInSight = Npcseleccionado => Npcseleccionado.EnemyInSight;

    // #*4
    //Creamos un Diccionario que: Por cada pregunta guarda una funcion (Func).
    public Dictionary<Questions, Func<Npc, bool>> DecitionTree = new Dictionary<Questions, Func<Npc, bool>>();

    // *5
    //Rellenamos el Diccionario.
    private void Awake()
    {
        DecitionTree.Add(Questions.EnemyInSight, EnemyInSight);
        DecitionTree.Add(Questions.HasBullets, HasBullets);
        DecitionTree.Add(Questions.HasHp, HasHp);
    }
    //De esta forma estariamos asociando una funcion a cada pregunta.



    public override void Execute(Npc Reference)
    {
        foreach (var item in DecitionTree)
        {
            if (item.Key == CurrentCuestion)
            {
                (item.Value(Reference) ? TrueNode : FalseNode).Execute(Reference);
                #region Explicacion2 
                /*
                 * Esto podria ir tranquilamente sin "{}" ya que sigue siendo una única línea.
                 * Lo que hacemos es ejecutar Execute pasando la referencia. Para seleccionar un nodo, en vez de usar una estructura if
                 usamos un if simplificado como EXPRESION  que devuelve un tipo de dato, en este caso seria equivalente a un NODE (TrueNode o FalseNode).

            El equivalente con una estructura if seria la siguiente:

            If(Desicion.key == question)
                if (Desicion.value(reference)) Accedemos al valor que me retorna un booleano(Verdadero)
                {
                    trueNode.Execute(reference); //Segun el valor se ejecuta una acción.
                }
                else
                {
                    falseNode.Execute();
                }

                */
                #endregion
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (TrueNode)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, TrueNode.transform.position);
        }

        if (FalseNode)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, FalseNode.transform.position);
        }        
    }
}
