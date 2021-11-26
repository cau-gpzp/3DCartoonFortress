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
        //�� ��ü�� �����̴� ��Ұ� ������ Rigidbody�� WASD �Ӹ� �ƴ϶�
        //Use gravity�� ����ؼ� �߷������� �Ǳ� ������
        //�߷¿� ���� ��ȭ�� ������ �ص־� �ϴ� ���� ������ ���� ��ġ�� ��� �����ؾ���.

        if (Input.GetKey(keyConfig.go))
        {
            playerPosition += Vector3.forward * moveSpeed * Time.deltaTime;
            //��ü�� ��ġ�� ���ŵǸ� �״��� Vector3.forward��� ������ǥ���� '��' ����, �ӵ�, �ð��� 60����1 �� ������ �� 
            //playerPosition�� �����ش�.

            myRigid.MovePosition(playerPosition);
            //���� movePosition���� ���� ���ŵ� ��ġ�� �̵��� ����.

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
