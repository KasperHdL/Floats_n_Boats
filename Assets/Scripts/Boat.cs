using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boat : Controllable {
	public Rigidbody body;

	[SerializeField] private Surfer surfer;
	[SerializeField] private Transform model;
	[SerializeField] private Transform motorModel;
	
	[Header("Movement")]
	[SerializeField] private float motorOffset;
	[SerializeField] private float motorMaxAngle;
	[SerializeField] private float force;
	[SerializeField] private float reverseFactor = 0.1f;
	[SerializeField] private float fullForceThreshold = 0.5f;
	[SerializeField] private float tiltMaxAngle;

	[Header("Collision")]
	[SerializeField] private Vector2 ropeCollisionBox;

	[SerializeField] private GameObject prefab_boatTrail;
	[SerializeField] private float particleDistance = 0.25f;
	private Vector3 lastParticlePoint;

	private AudioSource audioSource;
	
	void Start () {
		body = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	public override void InputUpdate(Vector2 moveStick){

		//scale with threshold
		//float thrust = Mathf.Min(moveStick.magnitude * (1 / fullForceThreshold), 1f);
	
		//reverse
		//if(moveStick.y < 0)
		//	thrust =  -thrust * reverseFactor;
		if(Mathf.Abs(moveStick.x) > .5f && moveStick.y < 0.1f)
			moveStick.y = 0.15f;
			
		audioSource.pitch = body.velocity.magnitude / 10f * 0.6f + 1.0f + Random.value * 0.2f;
		//calculate force and rotate force 
		Vector3 motorForce = transform.forward * force * moveStick.y * Time.deltaTime;
		motorForce = Quaternion.Euler(0, motorMaxAngle * -moveStick.x, 0) * motorForce;

		Debug.DrawLine(transform.position + transform.forward * motorOffset, transform.position + transform.forward * motorOffset + motorForce);
		body.AddForceAtPosition(motorForce, transform.position + transform.forward * motorOffset);

		model.localRotation = Quaternion.Euler(0, 0, tiltMaxAngle * -body.angularVelocity.y);
		motorModel.localRotation = Quaternion.Euler(0, motorMaxAngle * 5 * -moveStick.x + 180, 0);
	}

	void Update () {
		//Rope Collision Detection
		GameObject[] collidingObjects = GetGameObjectsWithTagsInBox(new string[] {"Harpoon"}, surfer.transform.position, transform.position, ropeCollisionBox);
		
		//  DEBUG BOX
		
			Vector3 delta  = surfer.transform.position - transform.position;
			Vector3 center = delta / 2 + transform.position;

			Debug.DrawLine(transform.position, center + delta.normalized * (delta.magnitude / 2),Color.red);
			Debug.DrawLine(center, center + Vector3.up * ropeCollisionBox.y);

		// Particle

		if((new Vector3(transform.position.x, 1, transform.position.z) - lastParticlePoint).magnitude > particleDistance && transform.position.y > -2f){
			Vector3 point = transform.position;
			point.y = 1f;
			
			RaycastHit hit;
			if(Physics.Raycast(point, -Vector3.up, out hit, 100f, 1 << 4)){
				
				Instantiate(prefab_boatTrail, hit.point + hit.normal * 0.1f, Quaternion.LookRotation(transform.forward, hit.normal));	
			}

			lastParticlePoint = point;
			
		}
		
		for(int i = 0; i < collidingObjects.Length; i++){
			
			GameObject obj = collidingObjects[i];
			
			if(obj == gameObject || obj == surfer.gameObject)
				continue;
			
			if(obj.tag == "Harpoon"){
				if(obj.GetComponent<Harpoon>().surfer != surfer)
					RopeHarpoonCollision(obj);	
			}
		}

	}
	private void RopeHarpoonCollision(GameObject harpoon){
		//check if friendly
		//disconnect rope		
		surfer.DisconnectRope();
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
