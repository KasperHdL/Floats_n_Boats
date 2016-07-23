using UnityEngine;
using System.Collections;

public class Harpoon : MonoBehaviour {

	public static float killAltitude = -5f;

	private Rigidbody body;
	public Surfer surfer;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < killAltitude)
			Destroy (gameObject);
			
		Debug.DrawLine (transform.position, transform.position + body.velocity);
//		transform.rotation = Quaternion.LookRotation(body.velocity);
	}
}
