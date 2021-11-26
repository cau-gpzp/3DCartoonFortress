using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitialize : MonoBehaviour
{
    public Vector3 initPos;

    void Awake() {
        this.transform.position = initPos;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
