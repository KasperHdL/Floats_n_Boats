using UnityEngine;
using System.Collections;

public class GameStateInspector : MonoBehaviour {
    public Team[] teams;

    private float startTime;

    // Use this for initialization
    void Start ()
    {
        startTime = Time.realtimeSinceStartup;

        GameState.teams = this.teams;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        GameState.seconds = Time.realtimeSinceStartup - startTime;
    }
}
