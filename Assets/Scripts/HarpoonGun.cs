using UnityEngine;
using System.Collections;

public class HarpoonGun : MonoBehaviour {

	public GameObject harpoonPrefab;
	private Vector3 velocity;
	private float timeStamp;
	[SerializeField]
	private float cooldown;
	[SerializeField]
	private float rotationSpeed;

	// Use this for initialization
	void Start () {
		timeStamp = 0;
		cooldown = 5;
		velocity = new Vector3 (1,0,0);
		rotationSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			velocity = Quaternion.AngleAxis(rotationSpeed, Vector3.up) * velocity;
		}

		if (Input.GetKey (KeyCode.D)) {
			velocity = Quaternion.AngleAxis(-rotationSpeed, Vector3.up) * velocity;
		}

		if (Input.GetKeyDown (KeyCode.Space) && Time.time > timeStamp){
			Shoot (velocity, harpoonPrefab);
			timeStamp = Time.time + cooldown;
		}

		Debug.DrawLine(transform.position, velocity*10, Color.green);
	}

	public void Shoot(Vector3 vel, GameObject prefab){
		Quaternion qua = Quaternion.LookRotation(vel);
		GameObject g = Instantiate (prefab, transform.position, qua) as GameObject;
		Harpoon h = g.GetComponent(typeof(Harpoon)) as Harpoon;
		h.velocity = velocity;
	}
}