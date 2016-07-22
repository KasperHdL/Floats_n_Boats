using UnityEngine;
using System.Collections;

public class PlayableZone : MonoBehaviour {
    [Range(1, 100)]
    public float radius;
    public Color gizmoColor;

    [HideInInspector]
    public SphereCollider sphere;

	// Use this for initialization
	void Start () {
        if (radius <= 0)
            Debug.LogError("Playable Zone Radius is equal or less than 0 (Propely not defined in Game State Inspector");

        sphere = GetComponent<SphereCollider>();
        sphere.radius = this.radius;
	}

    // Update is called once per frame
    void Update () {
	
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
