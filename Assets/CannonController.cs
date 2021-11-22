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
    float xDegrees, zDegrees;
    Quaternion fromRotation, toRotation;
    // private Camera cam;

    // Start is called before the first frame update
    void Start() {
        // cam = Camera.main;
        xDegrees = 45.0f;
        zDegrees = 0.0f;
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
        else if(Input.GetKey(KeyCode.DownArrow))
            xDegrees += speed*friction;
        else if(Input.GetKey(KeyCode.LeftArrow))
            zDegrees += speed*friction;
        else if(Input.GetKey(KeyCode.RightArrow))
            zDegrees -= speed*friction;
        AdjustRotation();
        
        if(Input.GetKeyDown(KeyCode.Space))
            FireShell();
    }

    void AdjustRotation() {
        xDegrees = Mathf.Max(xDegrees, 10);
        xDegrees = Mathf.Min(xDegrees, 90);
        zDegrees = Mathf.Max(zDegrees, -60);
        zDegrees = Mathf.Min(zDegrees, 60);

        fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDegrees, 0, zDegrees);
        transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
    }

    public void FireShell() {
        shotPos.rotation = this.transform.rotation;
        GameObject shootingShell = Instantiate(shell, shotPos.position, shotPos.rotation) as GameObject;
        shellRb = shootingShell.GetComponent<Rigidbody>();
        shellRb.AddForce(this.transform.up * firePower, ForceMode.Acceleration);
    }
}
