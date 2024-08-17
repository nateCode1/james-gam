using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float multiplier = 20.0F;
    public GameObject myParticleSystem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExplodeBomb();
        }
    }
    void ExplodeBomb()
    {
        Vector3 explosionPos = transform.position;
        Instantiate(myParticleSystem, explosionPos, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float distance = Vector3.Distance(explosionPos, hit.transform.position);
                float normalizedDistance = distance / radius;
                float falloff = Mathf.Pow(normalizedDistance, 3);
                float adjustedPower = power * multiplier * (1 - falloff);
                rb.AddExplosionForce(adjustedPower, explosionPos, radius, 0.0F);
                Debug.Log("Force Applied: " + adjustedPower + " to " + hit.name + " at distance: " + distance);
            }
        }
    }

}