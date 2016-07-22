using UnityEngine;
using System.Collections;

public class HarpoonGun : MonoBehaviour {

	public GameObject harpoonPrefab;
	public GameObject aim;
	private Vector3 velocity;
	private ParticleSystem ps;
	private float timeStamp;
	[SerializeField]
	private float cooldown;
	[SerializeField]
	private float rotationSpeed;

	// Use this for initialization
	void Start () {
		ps = aim.GetComponent (typeof (ParticleSystem)) as ParticleSystem;
		timeStamp = 0;
		cooldown = 5;
		velocity = new Vector3 (1,0,0);
		rotationSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Vertical");
		float w = Input.GetAxis ("Horizontal");
		velocity = new Vector3 (w, 0f, h);

		if (Input.GetButtonDown("joystick button 0") && Time.time > timeStamp){
			Shoot (velocity, harpoonPrefab);
			ps.Play();
			timeStamp = Time.time + cooldown;
		}

		aim.transform.rotation = Quaternion.LookRotation(velocity);
		Debug.Log (velocity);
	}

	public void Shoot(Vector3 vel, GameObject prefab){
		Quaternion qua = Quaternion.LookRotation(vel);
		GameObject g = Instantiate (prefab, transform.position, qua) as GameObject;
		Harpoon h = g.GetComponent(typeof(Harpoon)) as Harpoon;
		h.velocity = velocity;
	}
}