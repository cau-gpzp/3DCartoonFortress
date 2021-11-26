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
    private float delta, friction;


    public float moveSpeed = 50.0f;
    float xDegrees, yDegrees;
    Quaternion fromRotation, toRotation;

    Rigidbody myRigid;
    Vector3 playerPosition;

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
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if(properties.itsTurn == false) return;
        if(properties.isSieging == true) return;

        Movement();
        delta += Time.deltaTime;

        // siege mode
        if(delta > 0.1f && Input.GetKeyDown(keyConfig.siege)) {
            delta = 0.0f;
            properties.SiegeOn();
            ChangeCamera();
        }

 
    }
    void AdjustRotation()
    {
        //xDegrees = Mathf.Max(xDegrees, -90);
        //xDegrees = Mathf.Min(xDegrees, 20);

        // fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
        // transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        transform.rotation = toRotation;
    }
    public void Movement()
    {

        playerPosition = this.transform.position;
        //이 물체를 움직이는 요소가 오로지 Rigidbody의 WASD 뿐만 아니라
        //Use gravity를 사용해서 중력이적용 되기 때문에
        //중력에 의한 변화도 갱신을 해둬야 하는 이유 때문에 실제 위치를 계속 적용해야함.

        if (Input.GetKey(keyConfig.go))
        {
            playerPosition += Vector3.forward * moveSpeed * Time.deltaTime;
            //물체의 위치가 갱신되면 그다음 Vector3.forward라는 세계좌표기준 '앞' 값과, 속도, 시간의 60분의1 을 곱해준 후 
            //playerPosition에 뎌해준다.

            myRigid.MovePosition(playerPosition);
            //이제 movePosition덕에 새로 갱신된 위치로 이동될 것임.

        }
        if (Input.GetKey(keyConfig.turnleft))
        {
            //yDegrees -= moveSpeed * friction;
            //AdjustRotation();
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed * 4, Space.World);

        }
        if (Input.GetKey(keyConfig.back))
        {
            playerPosition -= Vector3.forward * moveSpeed * Time.deltaTime;

            myRigid.MovePosition(playerPosition);
        }
        if (Input.GetKey(keyConfig.turnright))
        {
            //yDegrees += moveSpeed * friction;
            //AdjustRotation();
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed * 4, Space.World);

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
