using UnityEngine;
using System.Collections;

public class HarpoonGun : MonoBehaviour {

	[SerializeField] private GameObject harpoonPrefab;
	[SerializeField] private GameObject projectile;
	[SerializeField] private GameObject aim;
	[SerializeField] private SpriteRenderer skillshotIndicator;
	[SerializeField] private Surfer surfer;
	
	private Collider surferCollider;

	
	private Color indicatorColor;
	private ParticleSystem ps;
	private float nextShootTime;
	[SerializeField] private float cooldown = 1f;
	[SerializeField] private float harpoonForce = 1000f;
	private Vector3 direction = new Vector3(0,0,1);

	private Camera camera;

	void Start () {
		ps = aim.GetComponent<ParticleSystem>();
		camera = Camera.main;
		indicatorColor = skillshotIndicator.color;
		surferCollider = surfer.GetComponent<Collider>();
	}
	
	void Update(){
		if (Time.time > nextShootTime && projectile.activeSelf == false)
			projectile.SetActive (true);
	
		float t = (Time.time - (nextShootTime - cooldown)) / cooldown;
		
		Color c = indicatorColor;
		
		if(t < 1f)
			c.a = 0.5f * t;
		else
			c.a = 1f;
		
		skillshotIndicator.color = c;

		transform.LookAt(transform.position + direction);
		
	}
		
	public void AimGun(Vector2 aimStick){
		direction = Vector3.right * -aimStick.x + Vector3.forward * -aimStick.y;
	}	


	public bool ShootGun(){
		if (Time.time > nextShootTime){
			projectile.SetActive (false);
			Shoot ();
			ps.Play();
			nextShootTime = Time.time + cooldown;
			return true;
		}
		return false;
	}

	public void Shoot(){
		GameObject g = Instantiate (harpoonPrefab, transform.position, transform.rotation) as GameObject;
		Harpoon h = g.GetComponent<Harpoon>();
		h.surfer = surfer;
		h.GetComponent<Rigidbody>().AddForce(transform.forward * harpoonForce);
		Physics.IgnoreCollision(surferCollider, h.GetComponent<Collider>());
	}
}