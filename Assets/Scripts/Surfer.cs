using UnityEngine;
using System.Collections;

public class Surfer : Controllable{

	private Boat boat;
	private Rigidbody body;
	private Rigidbody boatBody;
	public Rigidbody anchorPoint;
	private Transform connectedTransform;
	private Joint joint;
	private RigidbodyConstraints constraints;
	private LineRenderer rope;
	[SerializeField] private Transform boatRopeAnchor;
	[SerializeField] private float ropeOffset;

	private float dragWhenAttached;
	[SerializeField] private float dragWhenDetached = 0.25f;
	
	[SerializeField] private bool controlling = false;
	[SerializeField] private float force = 25;

    public bool isDead;
	
	[SerializeField] private HarpoonGun harpoonGun;
	void Start () {
		joint = GetComponent<Joint>();
		body = GetComponent<Rigidbody>();
		rope = GetComponent<LineRenderer>();
		constraints = body.constraints;
		connectedTransform = joint.connectedBody.transform;
		boat = connectedTransform.GetComponent<Boat>();
		boatBody = boat.GetComponent<Rigidbody>();

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
			body.AddForce(transform.right * moveStick.x * force);
			Debug.DrawLine(transform.position, transform.position + transform.right * moveStick.x * force * 10, Color.green);

		}else{
			//free floating
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
		body.drag = dragWhenDetached;

	}

	public void ConnectRope(Rigidbody body){
		joint.connectedBody = body;
		body.drag = dragWhenAttached;
		
	}
	
	public void OnCollisionEnter(Collision collision){		
		GameObject g = collision.gameObject;
		if(g.tag == "Boat"){
			Boat b = g.transform.parent.GetComponent<Boat>();
			if(b == boat &&	joint.connectedBody != boatBody){
				ConnectRope(boatBody);
			}
		}else if(g.tag == "Harpoon"){
			isDead = true;
			GameState.CheckIfTeamWon();
		}
	}
}
