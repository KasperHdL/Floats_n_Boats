using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GamePad gamepad;

	[SerializeField] private Controllable controllable;
	[SerializeField] private bool isControllingBoat;


	[Header("Super Simple AI")]
	[SerializeField] private bool ai_controlled = false;
	[SerializeField, Range(0,1)] private float ai_turnChance = 0.5f;


	void FixedUpdate () {

		Vector2 stick;
		
		if(ai_controlled){
			stick = new Vector2(
				Random.value < ai_turnChance ? Random.value: 0, 
				0.75f + Random.value * 0.25f
			);
		}else{
			stick = gamepad.GetStick(true);
		}

		controllable.InputUpdate(stick);
	}
}
