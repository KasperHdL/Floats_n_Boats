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

        CheckIfPlayersExitedSphere();
    }

    private void CheckIfPlayersExitedSphere()
    {
        foreach(Team team in GameState.teams)
        {
            if(!playableZone.sphere.bounds.Contains(team.surf.transform.position) || 
               !playableZone.sphere.bounds.Contains(team.boat.transform.position))
            {
                team.surf.isDead = true;

                team.surf.EnablePhysics();
                team.boat.EnablePhysics();
            }
        }
    }
}
