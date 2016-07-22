using UnityEngine;
using System.Collections;

public class Surfer : MonoBehaviour {

	private Rigidbody body;
	private Transform connectedTransform;

	[SerializeField] private bool _isDead = false;
	[SerializeField] private float force = 25;
	



	
	public bool isDead{
		get { return _isDead;}
		private set {_isDead = value;}
	}
	
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
}
