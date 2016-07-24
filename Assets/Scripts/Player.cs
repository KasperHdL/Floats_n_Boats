using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int joystickNumber;
    public GamePad gamepad;
	public bool controlled = true;
	private Controllable controllable;
	private bool isControllingBoat;

	[Header("Super Simple AI")]
	public bool ai_controlled = false;
	[SerializeField, Range(0,1)] private float ai_turnChance = 0.5f;

    void Start() {
		if(!controlled) return;
        gamepad = new GamePad(joystickNumber);
        controllable = GetComponent<Controllable>();
        isControllingBoat = controllable is Boat;
    }

	void FixedUpdate () {
		if(!controlled)return;

		Vector2 stick;
		
		if(ai_controlled){
			stick = new Vector2(
				Random.value < ai_turnChance ? Random.value: 0, 
				0.75f + Random.value * 0.25f
			);
		}else{
			stick.y = gamepad.GetAxis(GamePad.Axis.TriggersR);
			stick.x = gamepad.GetAxis(GamePad.Axis.L_XAxis);
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
