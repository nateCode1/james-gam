using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyant : MonoBehaviour
{
    public float force = 600f;
    private BoxCollider bc; 
    
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapBox(transform.position + bc.center, 0.5f * new Vector3(bc.size.x*transform.lossyScale.x, bc.size.y*transform.lossyScale.y, bc.size.z*transform.lossyScale.z), Quaternion.identity);

        foreach (Collider c in hits) {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(force * Time.deltaTime * Vector3.up);
            }
        }
    }
}
