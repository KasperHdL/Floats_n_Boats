using UnityEngine;
using System.Collections;

public class Bouy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, .05f);
	}
}
