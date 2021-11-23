using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ViewMode {
    ShoulderView, FirstView
}

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera, cannonCamera;
    private ViewMode viewMode;

    // Start is called before the first frame update
    void Start() {
        viewMode = ViewMode.ShoulderView;
        ChangeCam();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            viewMode += 1;
            viewMode = (ViewMode)((int)viewMode % System.Enum.GetNames(typeof(ViewMode)).Length);
            ChangeCam();
        }
    }

    void ChangeCam() {
        if(viewMode == ViewMode.ShoulderView) {
            mainCamera.enabled = true;
            cannonCamera.enabled = false;
        } else if(viewMode == ViewMode.FirstView) {
            mainCamera.enabled = false;
            cannonCamera.enabled = true;
        }
    }
}
