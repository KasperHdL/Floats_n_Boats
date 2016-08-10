using UnityEngine;
using System.Collections;
using System;

// Has no logic but just holds data for other classes to access easily

public static class GameState {
    public static Team[] teams;

    public static float roundTimeSeconds;
    public static float roundLengthSeconds;
    public static bool isResetting = false;


    public static void CheckIfTeamWon()
    {
        int index = -1;
        int numOfAliveSurfers = 0;

        for (int i = 0; i < teams.Length; i++)
        {
            if (!teams[i].surf.isDead)
            {
                numOfAliveSurfers++;
                index = i;
            }

            if (numOfAliveSurfers > 1)
                return;
        }

        if (index == -1 || index > teams.Length)
        {
            Debug.LogError("Bad Team Index");
        }

        EndRound(teams[index]);
    }

    private static void EndRound() // Time ran out
    {

    }

    private static void EndRound(Team winnerTeam) // Some Team Won
    
    {
        if(isResetting)
            return;
        
        Debug.Log(winnerTeam  + " Won!");
        Camera.main.GetComponent<Cam>().lerpToMenu(3f);
 
        for(int i = 0; i < teams.Length; i++){
            float offset;
            if(teams[i].boat == winnerTeam.boat){
                offset = 2f;
            }else
            {
                offset = -1f;
            }
            teams[i].boat.GetComponent<Player>().lerpToMenu(4f, offset);
            teams[i].surf.GetComponent<Player>().lerpToMenu(4f, offset);
            teams[i].surf.isDead = false;
            
        }


    }

    
}

[System.Serializable]
public struct Team
{
    public String name;
    public Color color;
    public Boat boat;
    public Surfer surf;
}
