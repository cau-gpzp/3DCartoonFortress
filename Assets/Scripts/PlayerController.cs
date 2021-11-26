using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera, cannonCamera;
    PlayerProperties properties;
    KeyConfig keyConfig;
    CannonController cc;
    private float delta;

    void Init() {
        properties = GetComponent<PlayerProperties>();
        keyConfig = GetComponent<KeyConfig>();
        cc = GetComponent<CannonController>();
        cc.ChangeCamera += ChangeCamera;

        TurnOff();
        delta = 0.0f;
    }

    void Awake() {
        Init();
    }

    // Start is called before the first frame update
    void Start() {
        CamSetting(properties.cameraRect);
    }

    // Update is called once per frame
    void Update() {
        if(properties.itsTurn == false) return;
        if(properties.isSieging == true) return;

        delta += Time.deltaTime;

        // siege mode
        if(delta > 0.1f && Input.GetKeyDown(keyConfig.siege)) {
            delta = 0.0f;
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

    public void CamSetting(Rect r) {
        mainCamera.rect = r;
        cannonCamera.rect = r;
    }
}
