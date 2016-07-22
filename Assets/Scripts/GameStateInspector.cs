using UnityEngine;
using System.Collections;

public class GameStateInspector : MonoBehaviour {
    [Header("Players Settings")]
    public Team[] teams;    

    [Header("Boat Settings")]

    [Header("Surf Settings")]

    [Header("Round Settings")]
    [Range(30, 240)] // Round 
    public int roundLengthSeconds;

    private float roundStartTime;

    // Use this for initialization
    void Start ()
    {
        roundStartTime = Time.time;

        GameState.teams = this.teams;
        GameState.roundLengthSeconds = this.roundLengthSeconds;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        GameState.roundTimeSeconds = Time.time - roundStartTime;
    }
}
