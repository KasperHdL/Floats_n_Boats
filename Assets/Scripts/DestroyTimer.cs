using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	public float length = 0f;

	private float killTime; 
	void Start(){
		if(length == 0)
			Destroy(gameObject);
		else
			killTime = Time.time + length;
	}
	void FixedUpdate () {
		if(Time.time > killTime)
			Destroy(gameObject);
	}
}
