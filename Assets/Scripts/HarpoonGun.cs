using UnityEngine;
using System.Collections;

public class HarpoonGun : MonoBehaviour {

	public GameObject harpoonPrefab;
	public GameObject projectile;
	public GameObject aim;
	private ParticleSystem ps;
	private float nextShootTime;
	[SerializeField] private float cooldown = 5f;
	[SerializeField] private float harpoonForce = 1000f;
	private Vector3 direction = new Vector3(0,0,1);

	private Camera camera;

	void Start () {
		ps = aim.GetComponent<ParticleSystem>();
		camera = Camera.main;
	}
	
	void Update(){
		if (Time.time > nextShootTime && projectile.activeSelf == false)
		projectile.SetActive (true);
	
	}
		
	public void AimGun(Vector2 aimStick){
		direction = new Vector3(aimStick.x, 0, aimStick.y);
		transform.rotation = Quaternion.LookRotation(transform.position + direction.normalized);
	}	


	public bool ShootGun(){
		if (Time.time > nextShootTime){
			projectile.SetActive (false);
			Shoot (direction);
			ps.Play();
			nextShootTime = Time.time + cooldown;
			return true;
		}
		return false;
	}

	public void Shoot(Vector3 direction){
		GameObject g = Instantiate (harpoonPrefab, transform.position, transform.rotation) as GameObject;
		Harpoon h = g.GetComponent<Harpoon>();
		h.GetComponent<Rigidbody>().AddForce(direction * harpoonForce);
	}
}