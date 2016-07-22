using UnityEngine;
using System.Collections;

public class HarpoonGun : Controllable {

	public GameObject harpoonPrefab;
	public GameObject projectile;
	public GameObject aim;
	private ParticleSystem ps;
	private float nextShootTime;
	[SerializeField] private float cooldown = 5f;
	[SerializeField] private float harpoonForce = 1000f;

	private Camera camera;

	void Start () {
		ps = aim.GetComponent<ParticleSystem>();
		camera = Camera.main;
	}
	
	void Update(){
		if (Time.time > nextShootTime && projectile.activeSelf == false)
		projectile.SetActive (true);
	
	}
		
	public override void InputUpdate(Vector2 aimStick, bool shoot){
		
		Vector3 delta = transform.position - camera.transform.position;
		delta.y = 0;
		delta = delta.normalized;
		
		transform.rotation = Quaternion.LookRotation(delta.normalized);
	
		if (shoot && Time.time > nextShootTime){
			projectile.SetActive (false);
			Vector3 direction = new Vector3(aimStick.x, 0, aimStick.y);
			Shoot (direction, harpoonPrefab);
			ps.Play();
			nextShootTime = Time.time + cooldown;
		}
		
	}

	public void Shoot(Vector3 direction, GameObject prefab){
		GameObject g = Instantiate (prefab, transform.position, transform.rotation) as GameObject;
		Harpoon h = g.GetComponent<Harpoon>();
		h.GetComponent<Rigidbody>().AddForce(direction * harpoonForce);
	}
}