using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour {

	[SerializeField]
	private float dayLength;

	private float percent;
	private Quaternion startRot;
	private Quaternion endRot;

	// Use this for initialization
	void Start () {
		startRot = Quaternion.Euler (90,0,0);
		endRot = Quaternion.Euler (-180, 0, 0);
		dayLength = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		percent = GameState.seconds / dayLength;
		transform.rotation = Quaternion.Lerp(startRot, endRot, percent);
	}
}
