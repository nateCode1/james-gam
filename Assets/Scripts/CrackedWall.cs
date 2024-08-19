using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedWall : MonoBehaviour {
    public GameObject myParticleSystem;
    public void Explode() {
        Vector3 explosionPos = transform.position;
        Instantiate(myParticleSystem, explosionPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
