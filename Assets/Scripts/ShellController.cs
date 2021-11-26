using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShellController : MonoBehaviour
{
    public UnityAction TurnEnd;
    public ParticleSystem exp;
    public Camera shellCam;

    public float damage = 30.0f;
    public float radius = 2.0f;

    private float delta;

    // Start is called before the first frame update
    void Start() {
        delta = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = this.transform.position;
        if(pos.y < -5) Destroy(this.gameObject);
    }

    void OnDestroy() {
        TurnEnd?.Invoke();
    }

    // Collision deteceted
    private void OnCollisionEnter(Collision collision) {
        // if(delta != 0.0f) return;
        // delta += Time.deltaTime;
        Explode(collision);
    }

    void Explode(Collision collision) {
        Destroy(this.gameObject);
        ExplodeEffect();
        DestroyAround(collision);
    }

    void DestroyAround(Collision collision) {
        Collider[] hitColliders = new Collider[50];
        Vector3 centre = this.transform.position;
        int numColliders = Physics.OverlapSphereNonAlloc(centre, radius, hitColliders);
        for (int i = 0; i < numColliders; i++) {
            GameObject o = hitColliders[i].gameObject;
            Debug.Log(o.name);
            switch(o.tag) {
                case "Obstacle":
                    Destroy(o.gameObject);
                    break;
                case "Player":
                    float effect = 1.0f;
                    if(collision.gameObject.tag != "Player") 
                        effect = ComputeEffect(this.transform, o.transform);
                    float actualDamage = damage * effect;
                    o.BroadcastMessage("ApplyDamage", actualDamage);
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

    float ComputeEffect(Transform shell, Transform target) {
        Vector3 shellOriginPos = shell.position;
        Vector3 targetOriginPos = target.position;

        float distance = Vector3.Distance(shellOriginPos, targetOriginPos);
        if(distance > radius + 1.0f) return 0.0f;
        return 1.0f - 0.5f*distance/(radius + 1.0f);
    }

    public void CamSetting(Rect r) {
        shellCam.rect = r;
    }
}
