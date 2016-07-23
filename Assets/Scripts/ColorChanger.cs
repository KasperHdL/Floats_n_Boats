using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour
{
    public Material material;
    public Color[] predefinedColours;
    public int currentColorIndex;

    // Use this for initialization
    void Start()
    {
        currentColorIndex = Random.Range(0, predefinedColours.Length);
        material.color = predefinedColours[currentColorIndex];
    }

    public void NextColor(GamePad.Button button)
    {
        switch (button)
        {
            case GamePad.Button.LS:
                currentColorIndex--;
                break;
            case GamePad.Button.RB:
                currentColorIndex++;
                break;
        }

        if (currentColorIndex > predefinedColours.Length-1)
            currentColorIndex = 0;
        if (currentColorIndex < 0)
            currentColorIndex = predefinedColours.Length-1;

        material.color = predefinedColours[currentColorIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
