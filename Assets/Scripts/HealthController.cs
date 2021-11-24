using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float healthPoint;
    
    void Init() {
        healthPoint = 0.0f;
    }

    void Awake() {
        Init();
    }
}
