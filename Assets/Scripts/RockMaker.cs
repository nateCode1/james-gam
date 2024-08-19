using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockMaker : MonoBehaviour
{
    public float maxScaleMultiplier = 10f;
    public float expScale = 0.7f;
    public float scaleSpeed = 15f;
    public LayerMask collideMask;
    public LayerMask playerMask;
    public GameObject destroyParticle;

    private float timeSinceLanded = 0f;
    private bool landed = false;
    private bool stopped = false;
    private float scaleMultiplier = 1f;
    private Vector3 baseScale;

    void Start() {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!landed && stopped) {
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        if (landed && !stopped) {
            timeSinceLanded += Time.deltaTime;
            scaleMultiplier += timeSinceLanded * expScale * scaleSpeed * Time.deltaTime;
        }
        scaleMultiplier = Mathf.Clamp(scaleMultiplier, 1, maxScaleMultiplier);
        if (scaleMultiplier == maxScaleMultiplier) Stop();
        transform.localScale = baseScale * scaleMultiplier;
    }

    void OnCollisionEnter(Collision other)
    {
        if (collideMask == (collideMask | (1 << other.gameObject.layer))) {
            landed = true;
        }
        else if (playerMask == (playerMask | (1 << other.gameObject.layer))) {
            Stop();
        }
    }

    void Stop() {
        stopped = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
