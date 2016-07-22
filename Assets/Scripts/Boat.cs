using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {
	private Rigidbody body;

	[SerializeField] private float motorOffset;
	[SerializeField] private float motorMaxAngle;
	[SerializeField] private float thrust;
	[SerializeField] private float reverseFactor = 0.1f;

	[Header("Super Simple AI")]
	[SerializeField] private bool ai_controlled = false;
	[SerializeField, Range(0,1)] private float ai_turnChance = 0.5f;


	void Start () {
		body = GetComponent<Rigidbody>();
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
			
	}
}
