﻿using UnityEngine;
using System.Collections;

public class SelectionState : MonoBehaviour
{
    public GameObject[] slots = new GameObject[4];
    public GamePad[] pads = new GamePad[4];
    public Button[] buttons = new Button[4];
    private ColorChanger[] colorChanger = new ColorChanger[4];

    public bool[] joystickAllowedToChangeColor = new bool[4];

    public int occupiedSlots = 0;

    // Use this for initialization
    void Start()
    {
        pads[0] = new GamePad(1);
        pads[1] = new GamePad(2);
        pads[2] = new GamePad(3);
        pads[3] = new GamePad(4);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pads.Length; i++)
        {
            if (pads[i] == null)
                continue;           // Player have already selected slot

            if (joystickAllowedToChangeColor[i])
            {
                if (pads[i].GetButtonDown(GamePad.Button.LS))
                    colorChanger[i].NextColor(GamePad.Button.LS);
                if (pads[i].GetButtonDown(GamePad.Button.RB))
                    colorChanger[i].NextColor(GamePad.Button.RB);
            } else
            {
                if (pads[i].GetButtonDown(GamePad.Button.Y))
                    SelectSlot(i, 0);
                else if (pads[i].GetButtonDown(GamePad.Button.A))
                    SelectSlot(i, 1);
                else if (pads[i].GetButtonDown(GamePad.Button.X))
                    SelectSlot(i, 2);
                else if (pads[i].GetButtonDown(GamePad.Button.B))
                    SelectSlot(i, 3);
            }
        }

        if (occupiedSlots >= pads.Length)
            StartCoroutine(StartGame());
    }


    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);

        for(int i = 0; i < slots.Length;i++)
        {
            slots[i].GetComponent<Player>().enabled = true;
            buttons[i].enabled = false;
        }
    }

    bool SelectSlot(int joystickNumber, int slot)
    {
        if(slots[slot].GetComponent<Player>() == null) {
            colorChanger[joystickNumber] = slots[slot].GetComponent<ColorChanger>();
            Player p = slots[slot].AddComponent<Player>() as Player;
            p.joystickNumber = joystickNumber + 1;
            p.gamepad = pads[joystickNumber];
            p.enabled = false;
            joystickAllowedToChangeColor[joystickNumber] = true;

            StartCoroutine(buttons[slot].Occupy());

            occupiedSlots++;

            Debug.Log("Joystick " + (p.joystickNumber) + " took " + slots[slot].name);

            return true;
        }

        return false;
    }
}