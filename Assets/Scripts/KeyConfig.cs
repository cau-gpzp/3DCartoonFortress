using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyConfig : MonoBehaviour
{
    public KeyCode up, down, left, right;
    public KeyCode fire;
    public KeyCode siege;

    KeyConfig() {
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        fire = KeyCode.Space;
        siege = KeyCode.Tab;
    }
}
