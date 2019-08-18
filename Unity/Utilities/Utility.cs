using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {
    // Transformaciones

    public static int ClampValue(int input, int minValue, int maxValue)
    {
        //Si el valor es -10 y el minimo es 0 retornará min(0)
        //Si el valor es 100 y el maximo es 10, retornará max(10)
        return input < minValue ? minValue : (input > maxValue ? maxValue : input);
    }

	public static T Log<T>(T param, string message = "") {
		Debug.Log(message +  param.ToString());
		return param;
	}

	public static IEnumerable<Src> Generate<Src>(Src seed, Func<Src, Src> generator) {
		while (true) {
			yield return seed;
			seed = generator(seed);
		}
	}
}
