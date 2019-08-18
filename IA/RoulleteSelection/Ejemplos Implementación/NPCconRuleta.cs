using System.Collections.Generic;
using UnityEngine;

public class Npc2 : MonoBehaviour {
	
	//LEEME XD
	//Bien al final esta la funcion RoulleteSelection, esa es la funcion que importa y que tiene que estar en algun lado.
	//Lo importante de la seleccion es pasarle una x cantidad de numeros que los calculas como quieras.
	//El resultado que te devuelve es siempre un numero que representa una eleccion.
	//Es como si el resultado fuera el slot y los numeros que ingresas son los numeros a los que podes apostar.

    //Atributos del npc
    public int LifePoints = 10;
    public int Damage = 10; //Va a ser un pj Melee
    public int Armour = 20;
    public float speed;

    [Header("Comportamientos seteables")]
    public float coePowerImportance = 1;
    public float coeLifeImportance = 1;
    public float coeDefenseImportance = 3;
    public float coeSpeedImportance = 1;

    //Variables de coeficiente/Accion
    private float coefAttack;
    private float coefIdlle;
    private float coefFlee;

    // *1
    //Calcular los coeficientes de cada acción:
    private void calculateCoef()
    {
        coefAttack = (Damage * coePowerImportance + LifePoints * coeLifeImportance + speed * coeSpeedImportance);
        coefIdlle = LifePoints * coeLifeImportance + Armour * coeDefenseImportance + Damage * coePowerImportance;
        coefFlee = LifePoints * coeLifeImportance + Armour * coeDefenseImportance + speed * coeSpeedImportance;
        //print("Coeficiente de ataque: " + coefAttack + ".");
        //print("Coeficiente de Idlle: " + coefIdlle + ".");
        //print("Coeficiente de Flee: " + coefFlee + ".");
    }
    // *2
    //Guardo los valores en una lista (para comodidad)
    List<float> CoefValues = new List<float>();
    private void fillList()
    {
        CoefValues.Add(coefAttack);
        CoefValues.Add(coefFlee);
        CoefValues.Add(coefIdlle);
    }
    // *3
    //Aplico la ruleta a mi lista de coeficientes.

    private void Start()
    {
        calculateCoef(); //Calculo los coeficientes.
        fillList(); //Relleno la lista de los coeficientes.
        int decition = RoulleteWheelSelection(CoefValues);

        //Segun el resultado Ejecuto una acción:
        switch (decition)
        {
            case 1:
                print("Se ha seleccionado la primera acción");
                break;
            case 2:
                print("Se ha seleccionado la segunda acción");
                break;
            case 3:
                print("Se ha selecconado la tercera acción");
                break;
            default:
                break;
        }

    }
    public static int RoulleteWheelSelection(List<float> list)
    {
        // *1
        //Calcular la sumatoria de todos los valores
        float sum = 0;
        foreach (var Numero in list)
            sum += Numero;

        // *2
        //Calculo el porcentaje que representa cada valor.
        List<float> newValues = new List<float>();
        foreach (var Numero in list)
        {
            newValues.Add(Numero / sum);
            print("El valor generado es: " + Numero / sum);
        }

        //*3
        //Calculo un valor random
        float Rnd = UnityEngine.Random.Range(0f, 1f);
        print("El numero generado es: " + Rnd);

        /*  Metodo del profe :v
        System.Random rnd = new System.Random();
        int rndPercent = rnd.Next(100);
        float r = rndPercent / 100f;
        */


        // *4
        //Sumo los elementos de a uno a un contador y lo igualo al valor random
        //Si el valor es mayor al valor random, retorno el indice del valor.
        float Sum2 = 0;
        for (int i = 0; i < newValues.Count; i++)
        {
            Sum2 += newValues[i];
            if (Sum2 > Rnd)
            {
                print("La decision fue: " + i);
                return i;
            }
        }
        
        //Aca en realidad nunca deberíamos llegar.
        return -1;
    }
}
