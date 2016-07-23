using UnityEngine;
using System.Collections;

public class Surfer : Controllable{

	private Boat boat;
	private Rigidbody body;
	private Rigidbody boatBody;
	public Rigidbody anchorPoint;
	private Transform connectedTransform;
	private Joint joint;
	private LineRenderer rope;
	[SerializeField] private Transform boatRopeAnchor;
	[SerializeField] private float ropeOffset;

	private float dragWhenAttached;
	[SerializeField] private float dragWhenDetached = 0.25f;
	
	[SerializeField] private float force = 25;

    public bool isDead;
	
	[SerializeField] private HarpoonGun harpoonGun;
	void Start () {
		joint = GetComponent<Joint>();
		body = GetComponent<Rigidbody>();
		rope = GetComponent<LineRenderer>();
		connectedTransform = joint.connectedBody.transform;

		boat = connectedTransform.GetComponent<Boat>();
		boatBody = boat.body;

		dragWhenAttached = body.drag;

	}

	void FixedUpdate(){
		if(joint.connectedBody == boatBody){
			//rope connected
			Vector3 delta = boatRopeAnchor.position - transform.position;
			
			rope.SetPositions(new Vector3[2] {boatRopeAnchor.position, transform.position + delta.normalized * ropeOffset});
		}
	}
	
	public override void InputUpdate (Vector2 moveStick, Vector2 aimStick, bool shoot) {
		if(aimStick.magnitude > 0)
			harpoonGun.AimGun(aimStick);
		
		if(shoot)
			harpoonGun.ShootGun();
			
		if(joint.connectedBody == boatBody){
			transform.rotation = Quaternion.LookRotation(Vector3.up, connectedTransform.position - transform.position);
			body.AddForce(transform.right * moveStick.x * force);
			Debug.DrawLine(transform.position, transform.position + transform.right * moveStick.x * force * 10, Color.green);

		}else{
			//free floating
			transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
		}
				
	}
	
	public void DisconnectRope(){
		joint.connectedBody = anchorPoint;
		body.drag = dragWhenDetached;
		rope.enabled = false;
	}

	public void ConnectRope(Rigidbody body){
		joint.connectedBody = body;
		body.drag = dragWhenAttached;
		
		rope.enabled = true;	
	}
	
	public void OnCollisionEnter(Collision collision){		
		GameObject g = collision.gameObject;
		
		if(g.tag == "Boat"){
			Boat b = g.GetComponent<Boat>();
			if(b == boat &&	joint.connectedBody != boatBody){
				ConnectRope(boatBody);
			}
		}else if(g.tag == "Harpoon"){
			isDead = true;
			GameState.CheckIfTeamWon();
		}
	}
}
