using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	/// <summary>
	/// Chequea si el comando fue tomado.
	/// </summary>
	public List<Tuple<Func<string,bool>,string,Action>> ButtonActions = new List<Tuple<Func<string, bool>, string, Action>>();
	public List<Tuple<Func<string, bool>, string, Func<string, float>, Action<float>>> AxisActions = new List<Tuple<Func<string, bool>, string, Func<string, float>, Action<float>>>();
	public List<Tuple<Func<string, float>, string, Action<float>>> AxisTrack = new List<Tuple<Func<string, float>, string, Action<float>>>();

	private void Update()
	{
		foreach (var BindedAction in ButtonActions)
		{
			//Obtengo la función y el identificador.
			var func = BindedAction.Item1;
			string buttonName = BindedAction.Item2;

			//Ejecuto la función bindeada.
			if (func(buttonName)) BindedAction.Item3();
		}

		foreach (var BindedAxis in AxisActions)
		{
			//Obtengo la función y el identificador.
			var func = BindedAxis.Item1;
			string AxisName = BindedAxis.Item2;

			//Ejecuto la función.
			if (func(AxisName)) BindedAxis.Item4(BindedAxis.Item3(AxisName));
		}

		foreach (var MouseInput in AxisTrack)
		{
			//Obtengo la función y su identificador
			var func = MouseInput.Item1(MouseInput.Item2);

			//Ejecuto la función.
			MouseInput.Item3(func);
		}
	}
}
