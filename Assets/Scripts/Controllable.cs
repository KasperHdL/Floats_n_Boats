using UnityEngine;
using System.Collections;

public class Controllable : MonoBehaviour {

	public virtual void InputUpdate(Vector2 moveStick){}
	public virtual void InputUpdate(Vector2 moveStick, Vector2 aimStick, bool shoot){}
}
