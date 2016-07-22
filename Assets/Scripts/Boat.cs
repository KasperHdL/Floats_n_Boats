using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boat : Controllable {
	private Rigidbody body;
	private RigidbodyConstraints constraints;

	private bool ropeConnected = true;
	[SerializeField] private Surfer surfer;
	private Joint ropeJoint;
	
	[Header("Movement")]
	[SerializeField] private float motorOffset;
	[SerializeField] private float motorMaxAngle;
	[SerializeField] private float thrust;
	[SerializeField] private float reverseFactor = 0.1f;

	[Header("Collision")]
	[SerializeField] private Vector2 ropeCollisionBox;


	
	void Start () {
		body      = GetComponent<Rigidbody>();
		ropeJoint = surfer.GetComponent<Joint>();
		constraints = body.constraints;

	}

	public override void InputUpdate(Vector2 moveStick){

		if(moveStick.y < 0)
			moveStick.y *= reverseFactor;
	
		Vector3 force = transform.forward * thrust * moveStick.y * Time.deltaTime;
		force = Quaternion.Euler(0, motorMaxAngle * -moveStick.x, 0) * force;

		Debug.DrawLine(transform.position + transform.forward * motorOffset, transform.position + transform.forward * motorOffset + force);
		body.AddForceAtPosition(force, transform.position + transform.forward * motorOffset);

	
	}

	void Update () {
		//Rope Collision Detection
		GameObject[] collidingObjects = GetGameObjectsWithTagsInBox(new string[] {"Harpoon", "Boat"}, surfer.transform.position, transform.position, ropeCollisionBox);
		
		//  DEBUG BOX
		
			Vector3 delta  = surfer.transform.position - transform.position;
			Vector3 center = delta / 2 + transform.position;

			Debug.DrawLine(transform.position, center + delta.normalized * (delta.magnitude / 2),Color.red);
			Debug.DrawLine(center, center + Vector3.up * ropeCollisionBox.y);

			
		for(int i = 0; i < collidingObjects.Length; i++){
			GameObject obj = collidingObjects[i];
			
			if(obj == gameObject || obj == surfer.gameObject)
				continue;
			
			if(collidingObjects[i].tag == "Harpoon"){
				RopeHarpoonCollision(collidingObjects[i]);	
			}else if(collidingObjects[i].tag == "Boat"){
				RopeBoatCollision(collidingObjects[i]);
			}
		}

	}
	private void RopeBoatCollision(GameObject boat){
		//Disconnect Rope
		DisconnectRope();
	}

	private void RopeHarpoonCollision(GameObject harpoon){
		//check if friendly
		//disconnect rope		
		DisconnectRope();
	}

	public void DisconnectRope(){
		ropeJoint.connectedBody = null;
		ropeConnected = false;

	}

	public void ConnectRope(){
		ropeJoint.connectedBody = body;
		ropeConnected = true;
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
	//************************
	// Public Static Methods
	
	public static GameObject[] GetGameObjectsWithTagsInBox(string[] tags, Vector3 from, Vector3 to, Vector2 box){
		List<GameObject> objects = new List<GameObject>();
		
		Vector3 delta  = to - from;
		Vector3 center = delta / 2 + from;

		RaycastHit[] hits = Physics.BoxCastAll(center, new Vector3(box.x, box.y, delta.magnitude / 2), delta.normalized, Quaternion.LookRotation(delta.normalized), 1f);

		for (int i = 0; i < hits.Length; i++)
		{
			
			GameObject obj = hits[i].collider.gameObject;

			for(int j = 0; j < tags.Length; j++){
				if(tags[j] == obj.tag){
					objects.Add(obj);
				}
			}
		}

		return objects.ToArray();
	}

}
