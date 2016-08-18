using UnityEngine;
using System.Collections;
using System;

public class GameStateInspector : MonoBehaviour {
    [Header("Players Settings")]
    public Team[] teams;    
    private bool localReset = false;

    [Header("Boat Settings")]

    [Header("Surf Settings")]

    [Header("Round Settings")]
    [Range(30, 240)] // Round 
    public int roundLengthSeconds;

    [Header("Map Settings")]
    public PlayableZone playableZone;

    private float roundStartTime;

    public float deathYLevel = -5f;

    [Header("Shaking on Reset")]
    public float shakeDuration = 0.25f;
    public float shakeAmount = 0.5f;
    
    // Use this for initialization
    void Start ()
    {
        roundStartTime = Time.time;

        GameState.teams = this.teams;
        GameState.roundLengthSeconds = this.roundLengthSeconds;
	}

    void Update()
    {
        if(localReset)
            return;
            
        GameState.roundTimeSeconds = Time.time - roundStartTime;
        if(GameState.roundTimeSeconds < 3f)
            return;
            
        //Check fi players is out of bounds
        foreach(Team team in GameState.teams)
        {
            if(team.surf.transform.position.y < deathYLevel){
                team.surf.isDead = true;
                
                CameraShake.Instance.start(shakeDuration, shakeAmount);
                
                GameState.CheckIfTeamWon();
                localReset = true;
                reseting(5f);
            }
        }

 
    }


    public void reseting(float time){
        localReset = true;
        StartCoroutine(routine_reseting(time));
        
    }

    IEnumerator routine_reseting(float time){
        
        yield return new WaitForSeconds(time);
        roundStartTime = Time.time;

        GameState.isResetting = false;
        localReset = false;
    }
}
