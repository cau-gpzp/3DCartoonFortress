using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum ViewMode {
    ShoulderView, FirstView, NOTHING
}

public class PlayerController : MonoBehaviour
{
    public UnityAction ChangeTurn;

    public Camera mainCamera, cannonCamera;
    private ViewMode viewMode;
    private bool itsTurn;

    CannonController cannon;

    void Awake() {
        TurnOff();
        cannon = this.GetComponent<CannonController>();
        cannon.Shot += Shot;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(itsTurn == false)
            return;

        if(Input.GetKeyDown(KeyCode.Tab)) {
            viewMode += 1;
            viewMode = (ViewMode)((int)viewMode % System.Enum.GetNames(typeof(ViewMode)).Length);
            ChangeCam();
        }
    }

    public void Shot() {
        Debug.Log("Shot");
        ChangeTurn?.Invoke();
    }

    public void TurnOff() {
        itsTurn = false;
        viewMode = ViewMode.NOTHING;
        ChangeCam();
    }

    public void TurnOn() {
        itsTurn = true;
        viewMode = ViewMode.ShoulderView;
        ChangeCam();
    }

    void ChangeCam() {
        if(viewMode == ViewMode.ShoulderView) {
            mainCamera.enabled = true;
            cannonCamera.enabled = false;
        } else if(viewMode == ViewMode.FirstView) {
            mainCamera.enabled = false;
            cannonCamera.enabled = true;
        } else {
            mainCamera.enabled = false;
            cannonCamera.enabled = false;
        }
    }
}
