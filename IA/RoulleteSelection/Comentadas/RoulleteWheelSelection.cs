Using System.Collections.Generic;
Using UnityEngine;

public static class RoulleteSelection
{
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
