using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonHeadController : MonoBehaviour
{
    PlayerProperties properties;
    KeyConfig keyConfig;

    public int speed;
    public float friction, lerpSpeed;
    float xDegrees, yDegrees;
    public Camera cannonCamera;
    public UnityAction ChangeCamera;
    private float delta;

    // Start is called before the first frame update
    void Awake() {
        Init();
    }

    void Init() {
        xDegrees = yDegrees = 0.0f;

        properties = GetComponentInParent<PlayerProperties>();
        keyConfig = GetComponentInParent<KeyConfig>();

        delta = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if(properties.itsTurn == false) return;
        if(properties.isSieging == false) return;

        delta += Time.deltaTime;

        if(Input.GetKey(keyConfig.left)) {
            yDegrees -= speed*friction;
            AdjustRotation();
        }
        if(Input.GetKey(keyConfig.right)) {
            yDegrees += speed*friction;
            AdjustRotation();
        }

        if(delta > 0.1f && Input.GetKeyDown(keyConfig.siege)) {
            delta = 0.0f;
            properties.SiegeOff();
            ChangeCamera?.Invoke();
        }
    }

    void AdjustRotation() {
        Quaternion toRotation = Quaternion.Euler(xDegrees, yDegrees, 0.0f);
        this.transform.localRotation = toRotation;
    }
}
