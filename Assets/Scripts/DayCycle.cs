using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour
{
    private Quaternion startRot;
    private Quaternion endRot;

    // Use this for initialization
    void Start()
    {
        startRot = Quaternion.Euler(90, 0, 0);
        endRot = Quaternion.Euler(-180, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float t = GameState.roundTimeSeconds / GameState.roundLengthSeconds;
        transform.rotation = Quaternion.Slerp(startRot, endRot, t);
    }
}
