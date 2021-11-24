using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera, cannonCamera;
    PlayerProperties properties;
    KeyConfig keyConfig;

    void Init() {
        properties = GetComponent<PlayerProperties>();
        keyConfig = GetComponent<KeyConfig>();
        TurnOff();
    }

    void Awake() {
        Init();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(properties.itsTurn == false || properties.view == ViewMode.Nothing) return;
        if(properties.isSieging == true) return;

        // siege mode
        if(properties.isSieging == false && Input.GetKeyDown(keyConfig.siege)) {
            properties.SiegeOn();
            ChangeCamera();
        }
    }

    public void TurnOff() {
        properties.Off();
        ChangeCamera();
    }

    public void TurnOn() {
        properties.On();
        ChangeCamera();
    }

    void ChangeCamera() {
        switch(properties.view) {
            case ViewMode.ShoulderView:
                mainCamera.enabled = true;
                cannonCamera.enabled = false;
                break;
            case ViewMode.FirstView:
                mainCamera.enabled = false;
                cannonCamera.enabled = true;
                break;
            default: // nothing (off)
                mainCamera.enabled = false;
                cannonCamera.enabled = false;
                break;
        }
    }
}
