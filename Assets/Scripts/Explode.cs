using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float multiplier = 20.0F;
    public GameObject myParticleSystem;

    public void ExplodeBomb()
    {
        Vector3 explosionPos = transform.position;
        Instantiate(myParticleSystem, explosionPos, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float adjustedPower = power * multiplier;
                rb.AddExplosionForce(adjustedPower, explosionPos, radius, 0.0F);
            }
            CrackedWall crackedWall = hit.gameObject.GetComponent<CrackedWall>();
            if (crackedWall != null) {
                crackedWall.Explode();
            }
        }
    }
}
