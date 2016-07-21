using UnityEngine;
using System.Collections;

public class Harpoon : MonoBehaviour {

	public Vector3 velocity;
	public float killAltitude;
	private float speed;
	private Rigidbody rig;


	// Use this for initialization
	void Start () {
		speed = 2000f;
		killAltitude = -5f;
		rig = gameObject.GetComponent (typeof (Rigidbody)) as Rigidbody;
		rig.AddForce (velocity * speed);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < killAltitude)
			Destroy (gameObject);
	}
}
