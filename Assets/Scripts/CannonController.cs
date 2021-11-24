using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonController : MonoBehaviour
{
    PlayerProperties properties;
    KeyConfig keyConfig;

    public GameObject shell;
    public Transform shotPos;
    public float firePower;
    public UnityAction ChangeTurn;
    private bool shotting;

    public int speed;
    public float friction, lerpSpeed;
    float xDegrees, yDegrees;
    Quaternion fromRotation, toRotation;
    public Camera cannonCamera;

    // Start is called before the first frame update
    void Awake() {
        Init();        
    }

    void Init() {
        // xDegrees = yDegrees = 0.0f;
        Vector3 pos = this.transform.position;
        pos.y += 1;
        pos.z -= 1;
        cannonCamera.transform.position = pos;

        properties = GetComponent<PlayerProperties>();
        keyConfig = GetComponent<KeyConfig>();

        shotting = false;
    }

    // Update is called once per frame
    void Update() {
        if(properties.itsTurn == false || properties.view == ViewMode.Nothing) return;
        if(properties.isSieging == false) return;

        if(Input.GetKey(KeyCode.UpArrow))
            xDegrees -= speed*friction;
        if(Input.GetKey(KeyCode.DownArrow))
            xDegrees += speed*friction;
        if(Input.GetKey(KeyCode.LeftArrow))
            yDegrees -= speed*friction;
        if(Input.GetKey(KeyCode.RightArrow))
            yDegrees += speed*friction;
        AdjustRotation();
        
        if(!shotting && Input.GetKeyDown(KeyCode.Space)) {
            Fire();
            shotting = true;
        }
    }

    void AdjustRotation() {
        xDegrees = Mathf.Max(xDegrees, -90);
        xDegrees = Mathf.Min(xDegrees, 20);

        // fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
        // transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        transform.rotation = toRotation;   
    }

    public void Fire() {
        // TODO: Vector transformation
        // Vector3 normVector = this.transform.InverseTransformDirection(this.transform.forward);
        shotPos.rotation = this.transform.rotation;
        GameObject shootingShell = Instantiate(shell, shotPos.position, shotPos.rotation);
        shootingShell.GetComponent<ShellController>().TurnEnd += TurnEnd;
        shootingShell.GetComponent<Rigidbody>().AddForce(shotPos.transform.forward * firePower, ForceMode.Impulse);
    }

    void TurnEnd() {
        ChangeTurn!.Invoke();
    }
}
