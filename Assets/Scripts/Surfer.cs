using UnityEngine;
using System.Collections;

public class Surfer : MonoBehaviour {

	private Rigidbody body;
	private Transform connectedTransform;

	[SerializeField] private float force = 25;

    public bool isDead;
	
	void Start () {
	
		body = GetComponent<Rigidbody>();
		connectedTransform = GetComponent<Joint>().connectedBody.transform;
	}
	
	void Update () {
		float h = Input.GetAxis("Horizontal");
		transform.LookAt(connectedTransform);
		body.AddForce(transform.right * h * force);
		Debug.DrawLine(transform.position, transform.position + transform.right * h * force * 10, Color.green);
		
		
		
	}

    // Make them fall
    public void EnablePhysics()
    {
        body.constraints = new RigidbodyConstraints(); // Just reset the damn thing
    }
}
