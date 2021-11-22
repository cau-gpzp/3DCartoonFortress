using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverSphereExample : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Debug.Log(this.name);

        Collider[] hitColliders = new Collider[10];
        Vector3 centre = this.transform.position;
        int numColliders = Physics.OverlapSphereNonAlloc(centre, 5.0f, hitColliders);
        for (int i = 0; i < numColliders; i++) {
            if(this.name == hitColliders[i].name) continue;
            Debug.Log(hitColliders[i].name);
            Destroy(hitColliders[i].gameObject);
        }
    }

    // Update is called once per frame
    void Update()  {
        
    }
}
