using UnityEngine;
using System.Collections;
using System;

public static class GameState {
    public static Team[] teams;

    public static float seconds;

    public static void CheckIfTeamWon()
    {
        int index = -1;
        int numOfAliveSurfers = 0;

        for(int i = 0; i < teams.Length;i++)
        {
            if (!teams[i].surf.isDead)
            {
                numOfAliveSurfers++;
                index = i;
            }

            if (numOfAliveSurfers > 1)
                return;
        }

        if(index == -1 || index > teams.Length) {
            Debug.LogError("Bad Team Index");
        }

        TeamWon(teams[index]);
    }

    private static void TeamWon(Team team)
    {

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
