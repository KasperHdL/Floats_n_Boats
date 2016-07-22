using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boat : MonoBehaviour {
	private Rigidbody body;

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

	[Header("Super Simple AI")]
	[SerializeField] private bool ai_controlled = false;
	[SerializeField, Range(0,1)] private float ai_turnChance = 0.5f;


	void Start () {
		body      = GetComponent<Rigidbody>();
		ropeJoint = surfer.GetComponent<Joint>();

	}


	void Update () {

		float h, v;
		
		if(ai_controlled){
			h = Random.value < ai_turnChance ? Random.value: 0; 
			v = 0.75f + Random.value * 0.25f;
		}else{
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
		}


		
		if(v < 0)
			v = v * reverseFactor;
	
		Vector3 force = transform.forward * thrust * v * Time.deltaTime;
		force = Quaternion.Euler(0, motorMaxAngle * -h, 0) * force;

		Debug.DrawLine(transform.position + transform.forward * motorOffset, transform.position + transform.forward * motorOffset + force);
		body.AddForceAtPosition(force, transform.position + transform.forward * motorOffset);

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
