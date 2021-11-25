using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public GameController gc;
    public float healthPoint;
    public int id;
    
    void Init() {
        healthPoint = 100.0f;
    }

    void Awake() {
        Init();
    }

    void Update() {

    }

    void Die() {
        if(healthPoint > 0.0f) return;
        
        Debug.Log("Died");
        gc.Died(id);
    }

    void OnCollisionEnter(Collision collision) {

    }

    void ApplyDamage(float damage) {
        Debug.Log(System.String.Format("damage: {0}", damage));
        healthPoint -= damage;

        if(healthPoint <= 0.0f)
            Die();
    }
}
