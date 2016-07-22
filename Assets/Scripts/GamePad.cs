using UnityEngine;
using System.Collections;

public class GamePad {

	public enum Button{
		A,
		B,
		X,
		Y,
		LB,
		RB,
		Up,
		Right,
		Down,
		Left,
		LS,
		RS,
		Start,
		Back,
	}

	public enum Axis{
		L_XAxis,
		L_YAxis,
		R_XAxis,
		R_YAxis,
		TriggersL,
		TriggersR,
	}

	
	public int joystickNumber;

	public GamePad(int joystickNumber){
		this.joystickNumber = joystickNumber;
	}

	public bool GetButtonDown(Button button){
		return Input.GetButtonDown(button + "_" + joystickNumber);
	}

	public bool GetButtonUp(Button button){
		return Input.GetButtonUp(button + "_" + joystickNumber);
	}

	public bool GetButton(Button button){
		return Input.GetButton(button + "_" + joystickNumber);
	}


	public float GetAxis(Axis axis){
		return Input.GetAxis(axis + "_" + joystickNumber);
	}

	public Vector2 GetStick(bool isLeft){
		Vector2 v;
		
		if(isLeft) v = new Vector2(GetAxis(Axis.L_XAxis), GetAxis(Axis.L_YAxis));
		else v = new Vector2(GetAxis(Axis.R_XAxis), GetAxis(Axis.R_YAxis));

		return v;
	}
}
