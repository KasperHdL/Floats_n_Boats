using UnityEngine;
using System.Collections;

public class BouyancyApplier : MonoBehaviour {

	[SerializeField, Range(500f,1500f)] private float boyancyForce;
	[SerializeField] private string[] tagsAffected;
	
	
	void OnTriggerStay(Collider collider){
		for(int i = 0;i < tagsAffected.Length; i++){
			if(collider.tag == tagsAffected[i]){
				collider.attachedRigidbody.AddForce(Vector3.up * boyancyForce * Time.deltaTime * collider.attachedRigidbody.mass);
			}
		}
		
	}
}
