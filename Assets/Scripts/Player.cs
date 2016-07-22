using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int joystickNumber;
	public GamePad gamepad;

	private Controllable controllable;
	private bool isControllingBoat;


	[Header("Super Simple AI")]
	[SerializeField] private bool ai_controlled = false;
	[SerializeField, Range(0,1)] private float ai_turnChance = 0.5f;


	void Start(){
		gamepad = new GamePad(joystickNumber);
		controllable = GetComponent<Controllable>();
		isControllingBoat = controllable is Boat; 
	}

	void FixedUpdate () {

		Vector2 stick;
		
		if(ai_controlled){
			stick = new Vector2(
				Random.value < ai_turnChance ? Random.value: 0, 
				0.75f + Random.value * 0.25f
			);
		}else{
			stick = gamepad.GetStick(true);
			stick.y = -stick.y;
		}
		if(isControllingBoat){
			controllable.InputUpdate(stick);
		}else{
			Vector2 aimStick = gamepad.GetStick(false);
			aimStick.y = -aimStick.y;
			controllable.InputUpdate(stick, aimStick, gamepad.GetButtonDown(GamePad.Button.RB));
		}
		
	}
}
