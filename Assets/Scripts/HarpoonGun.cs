using UnityEngine;
using System.Collections;

public class HarpoonGun : MonoBehaviour {

	public GameObject harpoonPrefab;
	public GameObject projectile;
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

		if (Time.time > timeStamp && projectile.activeSelf == false)
			projectile.SetActive (true);


		if (Input.GetButtonDown("joystick button 0") && Time.time > timeStamp){
			projectile.SetActive (false);
			Shoot (velocity, harpoonPrefab);
			ps.Play();
			timeStamp = Time.time + cooldown;
		}
		Debug.Log (velocity);
		transform.rotation = Quaternion.LookRotation(velocity);
		aim.transform.rotation = Quaternion.LookRotation(velocity);
	}

	public void Shoot(Vector3 vel, GameObject prefab){
		Quaternion qua = Quaternion.LookRotation(vel);
		GameObject g = Instantiate (prefab, transform.position, qua) as GameObject;
		Harpoon h = g.GetComponent(typeof(Harpoon)) as Harpoon;
		h.velocity = vel;
	}
}