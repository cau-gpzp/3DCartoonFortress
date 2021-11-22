using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceExample : MonoBehaviour {
    // Start is called before the first frame update
    // private Rigidbody rb;
    Quaternion vector;

    void Start() {
        // rb = GetComponent<Rigidbody>();
        // rb.useGravity = false;
    }

    // Update is called once per frame
    void Update() {
        // if(rb.useGravity == false && Input.GetKeyDown(KeyCode.Space))
        //     rb.useGravity = true;
        // if(Input.GetKeyDown(KeyCode.UpArrow))
        //     rb.AddForce(0.0f, 10.0f, 0.0f, ForceMode.Impulse);

        if(Input.GetKeyDown(KeyCode.Space)) {
            GameObject shell = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // shell = Instantiate(BombShell);
            Rigidbody shellRb = shell.AddComponent<Rigidbody>();
            shell.transform.position = this.transform.position;
            shellRb.AddForce(0.0f, 10.0f, 10.0f, ForceMode.Impulse);
        }
    }
}
