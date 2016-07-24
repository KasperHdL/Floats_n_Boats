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

	private Vector3 menuPos;
	private Quaternion menuRotation;
    void Start() {
		menuPos = transform.position;
		menuRotation = transform.rotation;
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
    public void lerpToMenu(float animationLength, float offsetY){
        StartCoroutine(routine_lerpToMenu(animationLength, offsetY));
    }

    IEnumerator routine_lerpToMenu(float length, float offsetY){
        this.enabled = false;
		
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float startTime = Time.time;
        float t = 0;
        
        while(startTime + length > Time.time){
            
            t = (Time.time - startTime) / length;
            transform.position = Vector3.Lerp(startPosition, menuPos + Vector3.up * offsetY, t);
            transform.rotation = Quaternion.Lerp(startRotation, menuRotation, t);
            
            yield return null;
        }

		this.enabled = true;
    }

}
