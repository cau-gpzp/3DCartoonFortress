using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonBarrelController : MonoBehaviour
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
    private float delta;

    // Start is called before the first frame update
    void Awake() {
        Init();
    }

    void Init() {
        xDegrees = yDegrees = 0.0f;

        properties = GetComponentInParent<PlayerProperties>();
        keyConfig = GetComponentInParent<KeyConfig>();

        shotting = false;

        delta = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if(properties.itsTurn == false) return;
        if(properties.isSieging == false) return;

        if(Input.GetKey(keyConfig.up)) {
            xDegrees -= speed*friction;
            AdjustRotation();
        }
        if(Input.GetKey(keyConfig.down)) {
            xDegrees += speed*friction;
            AdjustRotation();
        }
        
        if(!shotting && Input.GetKeyDown(keyConfig.fire)) {
            Fire();
            shotting = true;
        }
    }

    void AdjustRotation() {
        xDegrees = Mathf.Max(xDegrees, -60);
        xDegrees = Mathf.Min(xDegrees, 0);

        Quaternion toRotation = Quaternion.Euler(xDegrees, yDegrees, 0.0f);
        this.transform.localRotation = toRotation;
    }

    public void Fire() {
        shotPos.rotation = this.transform.rotation;
        GameObject shootingShell = Instantiate(shell, shotPos.position, shotPos.rotation);
        ShellController sc = shootingShell.GetComponent<ShellController>();
        sc.TurnEnd += TurnEnd;
        sc.CamSetting(properties.cameraRect);
        shootingShell.GetComponent<Rigidbody>().AddForce(shotPos.transform.forward * firePower, ForceMode.Impulse);
    }

    void TurnEnd() {
        shotting = false;
        ChangeTurn?.Invoke();
    }
}
