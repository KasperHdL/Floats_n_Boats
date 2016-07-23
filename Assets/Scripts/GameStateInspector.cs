using UnityEngine;
using System.Collections;
using System;

public class GameStateInspector : MonoBehaviour {
    [Header("Players Settings")]
    public Team[] teams;    

    [Header("Boat Settings")]

    [Header("Surf Settings")]

    [Header("Round Settings")]
    [Range(30, 240)] // Round 
    public int roundLengthSeconds;

    [Header("Map Settings")]
    public PlayableZone playableZone;

    private float roundStartTime;

    public float deathYLevel = -5f;
    // Use this for initialization
    void Start ()
    {
        roundStartTime = Time.time;

        GameState.teams = this.teams;
        GameState.roundLengthSeconds = this.roundLengthSeconds;
	}

    void Update()
    {
        GameState.roundTimeSeconds = Time.time - roundStartTime;

        //Check fi players is out of bounds
        foreach(Team team in GameState.teams)
        {
            if(team.surf.transform.position.y < deathYLevel){
                team.surf.isDead = true;
            }
        }
 
    }

}
