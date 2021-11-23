using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject shell;
    Rigidbody shellRb;
    public Transform shotPos;
    // public GameObject explosion;
    public float firePower;

    public int speed;
    public float friction, lerpSpeed;
    float xDegrees, yDegrees;
    Quaternion fromRotation, toRotation;
    
    public Camera cannonCamera;

    // Start is called before the first frame update
    void Start() {
        xDegrees = 0.0f;
        yDegrees = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        // RaycastHit hit;
        // Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // if(Physics.Raycast(ray, out hit)) {
        //     if(hit.transform.gameObject.tag == "Cannon") {
        //     }
        // }

        if(Input.GetKey(KeyCode.UpArrow))
            xDegrees -= speed*friction;
        if(Input.GetKey(KeyCode.DownArrow))
            xDegrees += speed*friction;
        if(Input.GetKey(KeyCode.LeftArrow))
            yDegrees -= speed*friction;
        if(Input.GetKey(KeyCode.RightArrow))
            yDegrees += speed*friction;
        AdjustRotation();
        
        if(Input.GetKeyDown(KeyCode.Space))
            FireShell();
    }

    void AdjustRotation() {
        xDegrees = Mathf.Max(xDegrees, -90);
        xDegrees = Mathf.Min(xDegrees, 0);
        yDegrees = Mathf.Max(yDegrees, -60);
        yDegrees = Mathf.Min(yDegrees, 60);

        fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
        // transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        transform.rotation = toRotation;   
    }

    public void FireShell() {
        shotPos.rotation = this.transform.rotation;
        GameObject shootingShell = Instantiate(shell, shotPos.position, shotPos.rotation) as GameObject;
        shellRb = shootingShell.GetComponent<Rigidbody>();
        shellRb.AddForce(this.transform.forward * firePower, ForceMode.Acceleration);
    }
}
