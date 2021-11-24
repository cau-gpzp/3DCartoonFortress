using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public ParticleSystem exp;

    // Start is called before the first frame update
    void Start() {
    
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Collision deteceted
    private void OnCollisionEnter(Collision collision) {
        Explode();
    }

    void Explode() {
        ExplodeEffect();
        DestroyAround();
    }

    void DestroyAround() {
        Collider[] hitColliders = new Collider[30];
        Vector3 centre = this.transform.position;
        int numColliders = Physics.OverlapSphereNonAlloc(centre, 1.5f, hitColliders);
        for (int i = 0; i < numColliders; i++)
            Destroy(hitColliders[i].gameObject);
        Destroy(this.gameObject);
    }

    void ExplodeEffect() {
        ParticleSystem effect = Instantiate(exp, this.transform.position, this.transform.rotation);
        effect.Play();
    }
}
