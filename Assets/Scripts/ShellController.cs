using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShellController : MonoBehaviour
{
    public UnityAction TurnEnd;

    public ParticleSystem exp;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = this.transform.position;
        if(pos.y < -10) Destroy(this.gameObject);
    }

    void OnDestroy() {
        TurnEnd!.Invoke();
    }

    // Collision deteceted
    private void OnCollisionEnter(Collision collision) {
        Explode();
    }

    void Explode() {
        ExplodeEffect();
        DestroyAround();
        Destroy(this.gameObject);
    }

    void DestroyAround() {
        Collider[] hitColliders = new Collider[30];
        Vector3 centre = this.transform.position;
        int numColliders = Physics.OverlapSphereNonAlloc(centre, 1.5f, hitColliders);
        for (int i = 0; i < numColliders; i++) {
            switch(hitColliders[i].gameObject.tag) {
                case "Land":
                    Destroy(hitColliders[i].gameObject);
                    break;
                default:
                    continue;
            }
        }
    }

    void ExplodeEffect() {
        ParticleSystem effect = Instantiate(exp, this.transform.position, this.transform.rotation);
        effect.Play();
    }
}
