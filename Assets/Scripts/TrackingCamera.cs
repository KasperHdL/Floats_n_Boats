using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour {

	public Transform obj;

	public float smoothFactor = 0.8f;

	public float yOffset = 25;
	public float minOffsetXZ = 20;

	void Start () {
		if(obj == null)
			obj = GameObject.FindWithTag("Player").transform;	
	}
	
	void FixedUpdate () {
		
		Vector3 desiredPosition = getDesiredPosition(transform.position, obj.position, minOffsetXZ);

		Vector3 delta = desiredPosition - transform.position;

		//add noise so the camera cannot be pushed infinitely
		if(delta.magnitude > .5f)
			desiredPosition += new Vector3(Random.value, 0, Random.value);

		delta *= smoothFactor;
		transform.position += delta * Time.deltaTime;

		//look at obj
		transform.LookAt(obj);
	}

	Vector3 getDesiredPosition(Vector3 camera, Vector3 target, float minDistance){
		Vector3 delta = camera - target;
		delta.y = 0;

		Vector3 desiredPosition = delta.normalized * minDistance;
		desiredPosition.y = yOffset;

		return desiredPosition + target;
	}
}
