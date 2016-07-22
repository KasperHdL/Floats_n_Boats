using UnityEngine;
using System.Collections;

public class GamePad {

	public static string appendedName = "Joystick";

	public enum Button{
		A,
		B,
		X,
		Y,
		LeftButton,
		RightButton,
		Up,
		Right,
		Down,
		Left,
		LeftStickButton,
		RightStickButton,
		Start,
		Back,
	}

	public enum Axis{
		LeftStickX,
		LeftStickY,
		RightStickX,
		RightStickY,
		LeftTrigger,
		RightTrigger
	}

	
	public int joystickNumber;

	public GamePad(int joystickNumber){
		this.joystickNumber = joystickNumber;
	}

	public bool GetButtonDown(Button button){
		return Input.GetButtonDown(appendedName + joystickNumber + button);
	}

	public bool GetButtonUp(Button button){
		return Input.GetButtonUp(appendedName + joystickNumber + button);		
	}

	public bool GetButton(Button button){
		return Input.GetButton(appendedName + joystickNumber + button);
	}


	public float GetAxis(Axis axis){
		return Input.GetAxis(appendedName + joystickNumber + axis);	
	}

	public Vector2 GetStick(bool isLeft){
		Vector2 v;
		
		if(isLeft) v = new Vector2(GetAxis(Axis.LeftStickX), GetAxis(Axis.LeftStickY));
		else v = new Vector2(GetAxis(Axis.RightStickX), GetAxis(Axis.RightStickY));

		return v;
	}
}
