using System;
using UnityEngine;

public class MvCUnit : MonoBehaviour {
	public Controller myController;

	// Use this for initialization
	void Start ()
	{
		myController.ButtonActions.Add(Tuple.Create<Func<string,bool>,string,Action>( Input.GetButton, "Sprint", Run ));
		myController.AxisActions.Add(Tuple.Create<Func<string, bool>, string, Func<string,float>, Action<float>>( Input.GetButton, "Horizontal", Input.GetAxis, MoveForward ));
		myController.AxisTrack.Add(Tuple.Create<Func<string, float>, string, Action<float>>(Input.GetAxis, "Mouse X", Rotate));
	}

	void Run()
	{
		print("Estoy Corriendo wey!!");
	}

	void MoveForward(float xValue)
	{
		transform.position += transform.forward * xValue * 20 * Time.deltaTime;
		print("Me muevo");
	}

	void Rotate(float mouseXValue)
	{
		transform.Rotate(new Vector3(0, mouseXValue * 50 * Time.deltaTime, 0));
	}
}
