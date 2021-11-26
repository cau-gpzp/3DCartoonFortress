using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewMode {
    ShoulderView, FirstView,
}

public class PlayerProperties : MonoBehaviour
{
    public ViewMode view;
    public bool itsTurn;
    public bool isSieging;
    public Rect cameraRect;

    PlayerProperties() {
        // view = ViewMode.Nothing;
        itsTurn = false;
        isSieging = false;
    }

    public void On() {
        itsTurn = true;
        view = ViewMode.ShoulderView;
        SiegeOff();
    }

    public void Off() {
        itsTurn = false;
        view = ViewMode.ShoulderView;
    }

    public void SiegeOn() {
        isSieging = true;
        view = ViewMode.FirstView;
    }

    public void SiegeOff() {
        isSieging = false;
        view = ViewMode.ShoulderView;
    }
}
