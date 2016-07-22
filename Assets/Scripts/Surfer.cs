using UnityEngine;
using System.Collections;

public class Surfer : MonoBehaviour {

	private Boat boat;
	private Rigidbody body;
	public Rigidbody anchorPoint;
	private Transform connectedTransform;
	private Joint joint;
	private RigidbodyConstraints constraints;
	
	[SerializeField] private bool controlling = false;
	[SerializeField] private float force = 25;

    public bool isDead;
	
	void Start () {
		joint = GetComponent<Joint>();
		body = GetComponent<Rigidbody>();
		constraints = body.constraints;
		connectedTransform = joint.connectedBody.transform;
		boat = connectedTransform.GetComponent<Boat>();
	}
	
	void Update () {
		if(joint.connectedBody != null){
			float h = Input.GetAxis("Horizontal");
			
			transform.LookAt(connectedTransform);
			body.AddForce(transform.right * h * force);
			Debug.DrawLine(transform.position, transform.position + transform.right * h * force * 10, Color.green);

		}
				
	}

    public void EnablePhysics()
    {
		body.useGravity = true;
        body.constraints = new RigidbodyConstraints(); // Just reset the damn thing
	} 
	
    public void DisablePhysics()
    {
		body.useGravity = false;
        body.constraints = constraints; // Just reset the damn thing
    }

	
	public void DisconnectRope(){
		joint.connectedBody = anchorPoint;

	}

	public void ConnectRope(Rigidbody body){
		joint.connectedBody = body;
		
	}
	
	public void OnCollisionEnter(Collision collision){		
		GameObject g = collision.gameObject;
		if(g.tag == "Boat"){
			Boat b = g.GetComponent<Boat>();
			if(b == boat){
				if(joint.connectedBody == anchorPoint)
					ConnectRope(b.GetComponent<Rigidbody>());
			}else{
				DisconnectRope();
				boat.DisconnectRope();
			}
		}else if(g.tag == "Harpoon"){
			isDead = true;
			GameState.CheckIfTeamWon();
		}
	}
}
