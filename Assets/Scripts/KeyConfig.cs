using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyConfig : MonoBehaviour
{
    public KeyCode up, down, left, right, go, back, turnright, turnleft;
    public KeyCode fire;
    public KeyCode siege;

    KeyConfig() {
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        fire = KeyCode.Space;
        siege = KeyCode.Tab;

        //¿òÁ÷ÀÓ

        go = KeyCode.W;
        back = KeyCode.S;
        turnright = KeyCode.D;
        turnleft = KeyCode.A;


    }
}
